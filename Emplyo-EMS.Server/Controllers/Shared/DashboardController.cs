using System;
using Emplyo_EMS.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Emplyo_EMS.Server.Controllers.Shared
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Dashboard
        [HttpGet]
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleId = HttpContext.Session.GetInt32("RoleId");

            if (userId == null || roleId == null)
                return Unauthorized();

            switch (roleId)
            {
                case 1: // Super Admin
                    return Json(GetSuperAdminDashboard());

                case 2: // HR
                    return Json(GetHRDashboard());

                case 3: // Manager
                    return Json(GetManagerDashboard(userId.Value));

                case 4: // Employee
                    return Json(GetEmployeeDashboard(userId.Value));

                default:
                    return BadRequest("Invalid role.");
            }
        }

        // ========================= DASHBOARD BUILDERS =========================

        private object GetSuperAdminDashboard()
        {
            return new
            {
                TotalEmployees = _context.Employees.Count(),

                DepartmentPerformance = _context.Departments.Select(d => new
                {
                    d.DepartmentName,
                    EmployeeCount = _context.Employees.Count(e => e.DepartmentId == d.DepartmentId),
                    AvgRating = _context.Performances
                        .Where(p => p.Employee.DepartmentId == d.DepartmentId)
                        .Average(p => (double?)p.Rating) ?? 0
                }).ToList(),

                AttendanceIssues = _context.AttendanceRecords.Count(a =>
                    a.Status == "Late" || a.Status == "Absent")
            };
        }


        private object GetHRDashboard()
        {
            return new
            {
                AttendanceToday = _context.AttendanceRecords.Count(a => a.Date == DateTime.Today),
                PendingLeaveRequests = _context.LeaveRequests.Count(l => l.Status == "Pending"),

                // ⭐ Detailed performance of each employee
                EmployeePerformance = _context.Employees.Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    Department = e.Department.DepartmentName,
                    AvgRatingReceived = _context.Performances
                        .Where(p => p.EmployeeId == e.EmployeeId)
                        .Average(p => (double?)p.Rating) ?? 0
                }).ToList(),

                // ⭐ Performance feedback given by employees to their managers
                ManagerFeedback = _context.Employees
                    .Where(m => _context.Performances.Any(p => p.ManagerId == m.EmployeeId))
                    .Select(m => new
                    {
                        m.FirstName,
                        m.LastName,
                        Department = m.Department.DepartmentName,
                        AvgRatingAsManager = _context.Performances
                            .Where(p => p.ManagerId == m.EmployeeId)
                            .Average(p => (double?)p.Rating) ?? 0
                    }).ToList()
            };
        }


        private object GetManagerDashboard(int managerUserId)
        {
            var departmentId = _context.Employees
                .Where(e => e.UserId == managerUserId)
                .Select(e => e.DepartmentId)
                .FirstOrDefault();

            return new
            {
                DepartmentStats = new
                {
                    AttendanceToday = _context.AttendanceRecords
                        .Count(a => a.Date == DateTime.Today && a.Employee.DepartmentId == departmentId),
                    TotalTasks = _context.Tasks
                        .Count(t => t.AssignedBy == managerUserId)
                },
                PendingLeaveRequests = _context.LeaveRequests
                    .Count(l => l.Employee.DepartmentId == departmentId && l.Status == "Pending"),
                EmployeeFeedbacks = _context.Performances
                    .Count(p => p.ManagerId == managerUserId)
            };
        }

        private object GetEmployeeDashboard(int employeeUserId)
        {
            var totalLeaveDaysTaken = _context.LeaveRequests
            .Where(l => l.Employee.UserId == employeeUserId && l.Status == "Approved")
            .Sum(l => EF.Functions.DateDiffDay(l.StartDate, l.EndDate) + 1); // Include end date

            var leaveBalance = 20 - totalLeaveDaysTaken;


            return new
            {
                PersonalAttendance = _context.AttendanceRecords
                    .Where(a => a.Employee.UserId == employeeUserId)
                    .OrderByDescending(a => a.Date)
                    .Take(7)
                    .ToList(),

                LeaveBalance = leaveBalance,

                Tasks = _context.Tasks
                    .Where(t => t.AssignedTo == employeeUserId)
                    .Select(t => new { t.TaskDescription, t.Status })
                    .ToList()
            };
        }

    }
}
