using ItiProject_ms1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ItiProject_ms1.Controllers
{
    public class AuthController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        private readonly UniDbContext _context;

        public AuthController(UniDbContext context)
        {
            _context = context;
        }

        // GET: /Auth/Login
        // GET: /Auth/Login
        //[HttpGet]
//        public IActionResult Login()
//        {
//            return View();
//        }

//        // POST: /Auth/Login
//        [HttpPost]
//       public IActionResult Login(string email, string password)
//{
//            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

//            if (user == null)
//            {
//                ViewBag.Error = "Invalid Email or Password";
//                return View();
//            }

//            // Store general user info
//            HttpContext.Session.SetString("UserEmail", user.Email);
//            HttpContext.Session.SetString("UserRole", user.Role);
//            HttpContext.Session.SetInt32("UserId", user.Id);

//            // Store student id if user is a student
//            if (user.Role == "Student")
//            {
//                var student = _context.Students.FirstOrDefault(s => s.UserId == user.Id);
//                if (student != null)
//                {
//                    HttpContext.Session.SetInt32("StudentId", student.Id);
//                }
//            }

//            // Redirect based on role
//            if (user.Role == "Admin")
//                return RedirectToAction("Index", "Courses");
//            else
//                return RedirectToAction("Browse", "Courses"); // or Courses page

//        }


//        public IActionResult Logout()
//        {
//            HttpContext.Session.Clear();
//            return RedirectToAction("Login");
//        }
    }
}
