using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;
using Emplyo_EMS.Server.Data;
using Emplyo_EMS.Server.Models;
using System.Linq;
using Emplyo_EMS.Server.Models;
using System;

namespace Emplyo_EMS.Server.Controllers.Shared
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Auth/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Auth/Login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Username and password are required.";
                return View();
            }

            string hashedPassword = ComputeSha256Hash(password);
            var user = _context.Users
                               .Where(u => u.Username == username && u.PasswordHash == hashedPassword)
                               .Select(u => new { u.UserId, u.Username, u.RoleId })
                               .FirstOrDefault();

            if (user == null)
            {
                ViewBag.Error = "Invalid credentials.";
                return View();
            }

            // Set session
            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetInt32("RoleId", user.RoleId);

            return RedirectToAction("Index", "Dashboard");
        }

        // GET: /Auth/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // GET: /Auth/CurrentUser
        public IActionResult CurrentUser()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var username = HttpContext.Session.GetString("Username");
            var roleId = HttpContext.Session.GetInt32("RoleId");

            if (userId == null)
                return Unauthorized();

            return Json(new { userId, username, roleId });
        }

        // Optional: POST /Auth/Register (Restricted to SuperAdmin only)
        [HttpPost]
        public IActionResult Register(string username, string password, int roleId)
        {
            var currentUserRole = HttpContext.Session.GetInt32("RoleId");
            if (currentUserRole != 1) // Assuming 1 = Super Admin
                return Unauthorized();

            if (_context.Users.Any(u => u.Username == username))
            {
                return BadRequest("Username already exists.");
            }

            var user = new User
            {
                Username = username,
                PasswordHash = ComputeSha256Hash(password),
                RoleId = roleId
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User registered successfully.");
        }

        // Helper: SHA256 hash
        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                var builder = new StringBuilder();
                foreach (var b in bytes)
                    builder.Append(b.ToString("x2"));
                return builder.ToString();
            }
        }
    }
}
