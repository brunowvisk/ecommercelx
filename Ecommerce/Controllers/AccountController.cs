using Microsoft.AspNetCore.Mvc;
using Ecommerce.Models.Database;

namespace Ecommerce.Controllers
{
    public class AccountController : Controller
    {
        private readonly EcommerceContext _context;

        public AccountController(EcommerceContext context)
        {
            _context = context;
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

            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            
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
                    return RedirectToAction("MyAccount");
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
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                // Check if email already exists
                var existingUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
                if (existingUser != null)
                {
                    ViewBag.ErrorMessage = "Email already exists.";
                    return View(user);
                }

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