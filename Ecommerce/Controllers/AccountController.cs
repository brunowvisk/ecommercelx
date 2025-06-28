using Microsoft.AspNetCore.Mvc;
using Ecommerce.Models.Database;
using System.Security.Cryptography;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Controllers
{
    public class AccountController : Controller
    {
        private readonly EcommerceContext _context;

        public AccountController(EcommerceContext context)
        {
            _context = context;
        }

        // Helper method to hash passwords
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.ErrorMessage = "Email and password are required.";
                return View();
            }

            // Hash the input password to compare with stored hash
            string hashedPassword = HashPassword(password);
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == hashedPassword);
            
            if (user != null)
            {
                // Store user info in session (simple implementation)
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("UserName", user.FullName);
                HttpContext.Session.SetString("IsAdmin", user.IsAdmin.ToString());

                if (user.IsAdmin)
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid email or password.";
                return View();
            }
        }

        // GET: Account/MyAccount
        public IActionResult MyAccount()
        {
            var userId = HttpContext.Session.GetString("UserId");
            
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login");
            }

            var user = _context.Users.Find(int.Parse(userId));
            
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            return View(user);
        }

        // GET: Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user, string confirmPassword)
        {
            if (ModelState.IsValid)
            {
                // Check if passwords match
                if (user.Password != confirmPassword)
                {
                    ViewBag.ErrorMessage = "Passwords do not match.";
                    return View(user);
                }

                // Check if email already exists
                var existingUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
                if (existingUser != null)
                {
                    ViewBag.ErrorMessage = "Email already exists.";
                    return View(user);
                }

                // Hash the password before saving
                user.Password = HashPassword(user.Password);
                user.RegisterDate = DateTime.Now;
                user.IsAdmin = false; // New users are not admin by default
                
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                
                ViewBag.SuccessMessage = "Registration successful! Please login.";
                return RedirectToAction("Login");
            }

            return View(user);
        }

        // GET: Account/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}