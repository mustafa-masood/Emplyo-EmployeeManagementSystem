using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Emplyo_EMS.Server.Data;
using Emplyo_EMS.Server.Models;
using System;

namespace HRSystem.Controllers.Shared
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Account/Profile/5
        [HttpGet]
        public IActionResult GetProfile(int userId)
        {
            var currentUserId = HttpContext.Session.GetInt32("UserId");
            var currentRole = HttpContext.Session.GetInt32("RoleId");

            if (currentRole == 4 && currentUserId != userId)
                return Unauthorized();

            var employee = _context.Employees.FirstOrDefault(e => e.UserId == userId);
            if (employee == null)
                return NotFound();

            return Json(employee);
        }

        // POST: /Account/UpdateProfile (Super Admin / HR)
        [HttpPost]
        public IActionResult UpdateProfile(Employee updated)
        {
            var currentRole = HttpContext.Session.GetInt32("RoleId");
            if (currentRole != 1 && currentRole != 2)
                return Unauthorized();

            var employee = _context.Employees.FirstOrDefault(e => e.UserId == updated.UserId);
            if (employee == null)
                return NotFound();

            // HR cannot change salary
            if (currentRole == 2)
                updated.Salary = employee.Salary;

            _context.Entry(employee).CurrentValues.SetValues(updated);
            _context.SaveChanges();

            return Ok("Profile updated successfully.");
        }

        // POST: /Account/RequestProfileUpdate (Employee)
        [HttpPost]
        public IActionResult RequestProfileUpdate(ProfileUpdateRequest request)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var role = HttpContext.Session.GetInt32("RoleId");

            if (role != 4 || request.UserId != userId)
                return Unauthorized();
            
            request.Status = "Pending";
            _context.ProfileUpdateRequests.Add(request);
            _context.SaveChanges();

            return Ok("Profile update request submitted.");
        }

        // POST: /Account/ApproveProfileUpdate (HR)
        [HttpPost]
        public IActionResult ApproveProfileUpdate(int requestId, bool approve)
        {
            var role = HttpContext.Session.GetInt32("RoleId");
            if (role != 2) return Unauthorized();

            var request = _context.ProfileUpdateRequests.FirstOrDefault(r => r.Id == requestId && r.Status == "Pending");
            if (request == null)
                return NotFound();

            var employee = _context.Employees.FirstOrDefault(e => e.UserId == request.UserId);
            if (employee == null)
                return NotFound();

            if (approve)
            {
                employee.FirstName = request.UpdatedFirstName;
                employee.LastName = request.UpdatedLastName;
                request.Status = "Approved";
            }
            else
            {
                request.Status = "Rejected";
            }

            _context.SaveChanges();
            return Ok($"Request {request.Status.ToLower()}.");
        }
    }
}
