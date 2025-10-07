using ItiProject_ms1.Models;
using ItiProject_ms1.Repository;
using ItiProject_ms1.Views.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.Json;

namespace ItiProject_ms1.Controllers
{
    [Authorize(Roles = "Admin,Instructor,Student")]
    public class CoursesController : Controller
    {
        private readonly IBaseRepository<Course> _courseRepo;
        private readonly IBaseRepository<Department> _deptRepo;
        private readonly IBaseRepository<Instructor> _instructorRepo;
        private readonly IBaseRepository<Student> _studentRepo;
        private readonly UserManager<IdentityUser> _userManager;

        public CoursesController(
            IBaseRepository<Course> courseRepo,
            IBaseRepository<Department> deptRepo,
            IBaseRepository<Instructor> instructorRepo,
            IBaseRepository<Student> studentRepo,
            UserManager<IdentityUser> userManager)
        {
            _courseRepo = courseRepo;
            _deptRepo = deptRepo;
            _instructorRepo = instructorRepo;
            _studentRepo = studentRepo;
            _userManager = userManager;
        }

        // ===============================
        // 🔹 Admin + Instructor
        // ===============================
       [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> Index()
        {
            var allCourses = _courseRepo.GetAll(); // fetch all courses
            var user = await _userManager.GetUserAsync(User);

            if (User.IsInRole("Admin"))
            {
                // Admin → show all courses
                System.Diagnostics.Debug.WriteLine($"Admin logged in, showing all courses. Count: {allCourses.Count}");
            }
            else if (User.IsInRole("Instructor"))
            {
                // Instructor → filter only their courses
                var instructor = _instructorRepo.GetAll()
                    .FirstOrDefault(i => !string.IsNullOrEmpty(i.UserId)
                                         && string.Equals(i.UserId.Trim(), user.Id.Trim(), StringComparison.OrdinalIgnoreCase));

                if (instructor != null)
                {
                    allCourses = allCourses.Where(c => c.InstructorId == instructor.Id).ToList();
                    System.Diagnostics.Debug.WriteLine($"Instructor found: {instructor.Name}, Courses count: {allCourses.Count}");
                }
                else
                {
                    allCourses = new List<Course>();
                    System.Diagnostics.Debug.WriteLine($"Instructor not found for UserId: {user.Id}");
                }
            }

            var vm = new CrssDeptssViewModel
            {
                courses = allCourses,
                departments = _deptRepo.GetAll()
            };

            System.Diagnostics.Debug.WriteLine($"Total courses sent to view: {vm.courses.Count}");

            return View("Index", vm);
        }




        // ===============================
        // 🔹 Student - Browse Enrolled Courses
        // ===============================
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Browse()
        {
            var user = await _userManager.GetUserAsync(User);
            var student = _studentRepo.GetAll().FirstOrDefault(s => s.UserId == user.Id);

            if (student == null)
                return RedirectToAction("AccessDenied", "Account");

            var allCourses = _courseRepo.GetAll();
            var joinedCourses = GetJoinedCoursesFromSession();

            // Pass joined courses to ViewBag
            ViewBag.JoinedCourses = joinedCourses;

            var vm = new CrssDeptssViewModel
            {
                courses = allCourses,
                departments = _deptRepo.GetAll()
            };

            return View(vm);
        }


        private List<int> GetJoinedCoursesFromSession()
        {
            var data = HttpContext.Session.GetString("JoinedCourses");
            return data != null
                ? JsonSerializer.Deserialize<List<int>>(data)
                : new List<int>();
        }

        private void SaveJoinedCoursesToSession(List<int> courses)
        {
            var data = JsonSerializer.Serialize(courses);
            HttpContext.Session.SetString("JoinedCourses", data);
        }
        [Authorize(Roles = "Student")]
        public IActionResult JoinCourse(int courseId)
        {
            var joinedCourses = GetJoinedCoursesFromSession();

            if (!joinedCourses.Contains(courseId))
            {
                joinedCourses.Add(courseId);
                SaveJoinedCoursesToSession(joinedCourses);
                TempData["Message"] = "You joined the course successfully!";
            }

            return RedirectToAction("Browse", "Courses"); // specify controller
        }

        [Authorize(Roles = "Student")]
        public IActionResult LeaveCourse(int courseId)
        {
            var joinedCourses = GetJoinedCoursesFromSession();

            if (joinedCourses.Contains(courseId))
            {
                joinedCourses.Remove(courseId);
                SaveJoinedCoursesToSession(joinedCourses);
                TempData["Message"] = "You left the course successfully.";
            }

            return RedirectToAction("Browse", "Courses"); // specify controller
        }



        // ===============================
        // 🔹 Shared: Course Details
        // ===============================
        [Authorize(Roles = "Instructor,Student,Admin")]

        public async Task<IActionResult> Details(int id)
        {
            if (User.IsInRole("Admin"))
            {
                var course1 = _courseRepo.GetByID(id);
                if (course1 == null) return NotFound();
                return View(course1);
            }
            var course = _courseRepo.GetByID(id);
            if (course == null) return NotFound();

            // Instructor can view only their own courses
            if (User.IsInRole("Instructor"))
            {
                var user = await _userManager.GetUserAsync(User);
                var instructor = _instructorRepo.GetAll().FirstOrDefault(i => i.UserId == user.Id);
                if (instructor == null || course.InstructorId != instructor.Id)
                    return RedirectToAction("AccessDenied", "Account");
            }

            // Student → allowed if joined
            if (User.IsInRole("Student"))
            {
                var joinedCourses = GetJoinedCoursesFromSession();
                if (!joinedCourses.Contains(course.Id))
                    return RedirectToAction("AccessDenied", "Account");
            }

            return View(course);
        }

        // ===============================
        // 🔹 Admin only - Create/Edit/Delete
        // ===============================
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var vm = new DeptCourseViewModel
            {
                course = new Course(),
                departments = _deptRepo.GetAll(),
                instructors = _instructorRepo.GetAll()
            };
            return View(vm);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(DeptCourseViewModel crs)
        {
            if (ModelState.IsValid)
            {
                _courseRepo.Add(crs.course);
                return RedirectToAction("Index");
            }

            crs.departments = _deptRepo.GetAll();
            crs.instructors = _instructorRepo.GetAll();
            return View(crs);
        }


        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> Edit(int id)
        {
            var crs = _courseRepo.GetByID(id);
            if (crs == null) return NotFound();

            // CRITICAL FIX: Admin check first to bypass ownership check
            if (!User.IsInRole("Admin"))
            {
                // Instructor can edit only their own
                if (User.IsInRole("Instructor"))
                {
                    var user = await _userManager.GetUserAsync(User);
                    var instructor = _instructorRepo.GetAll().FirstOrDefault(i => i.UserId == user.Id);
                    if (instructor == null || crs.InstructorId != instructor.Id)
                        return RedirectToAction("AccessDenied", "Account");
                }
            }

            var vm = new DeptCourseViewModel
            {
                course = crs,
                departments = _deptRepo.GetAll(),
                instructors = _instructorRepo.GetAll()
            };
            return View("Edit", vm);
        }

        [Authorize(Roles = "Admin,Instructor")]
        [HttpPost]
        public async Task<IActionResult> Edit(DeptCourseViewModel updatedCrs)
        {
            if (ModelState.IsValid)
            {
                // CRITICAL FIX: Admin check first. If NOT Admin, run the ownership check.
                if (!User.IsInRole("Admin"))
                {
                    // This block only executes for non-Admin Instructors.
                    var user = await _userManager.GetUserAsync(User);
                    var instructor = _instructorRepo.GetAll().FirstOrDefault(i => i.UserId == user.Id);

                    if (instructor == null || updatedCrs.course.InstructorId != instructor.Id)
                        return RedirectToAction("AccessDenied", "Account");
                }

                // Admins and authorized Instructors proceed here.
                _courseRepo.Update(updatedCrs.course);
                return RedirectToAction("Index");
            }

            updatedCrs.departments = _deptRepo.GetAll();
            updatedCrs.instructors = _instructorRepo.GetAll();
            return View("Edit", updatedCrs);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var course = _courseRepo.GetByID(id);
            if (course != null)
                _courseRepo.Delete(course);

            return RedirectToAction("Index");
        }
    }
}
