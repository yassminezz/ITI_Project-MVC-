using ItiProject_ms1.Models;
using ItiProject_ms1.Repository;
using ItiProject_ms1.Views.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ItiProject_ms1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CourseStudentsController : Controller
    {
        private readonly IBaseRepository<CourseStudents> _csRepo;
        private readonly IBaseRepository<Course> _courseRepo;
        private readonly IBaseRepository<Student> _studentRepo;

        public CourseStudentsController(
            IBaseRepository<CourseStudents> csRepo,
            IBaseRepository<Course> courseRepo,
            IBaseRepository<Student> studentRepo)
        {
            _csRepo = csRepo;
            _courseRepo = courseRepo;
            _studentRepo = studentRepo;
        }

        // GET: CourseStudents
        public IActionResult Index()
        {
            var lst = _csRepo.GetAll()
                             .Select(cs => new CourseStudents
                             {
                                 Id = cs.Id,
                                 CrsId = cs.CrsId,
                                 StdId = cs.StdId,
                                 Degree = cs.Degree,
                                 Course = _courseRepo.GetByID(cs.CrsId),
                                 Student = _studentRepo.GetByID(cs.StdId)
                             }).ToList();

            return View(lst);
        }

        // GET: CourseStudents/Details/5
        public IActionResult Details(int id)
        {
            var cs = _csRepo.GetByID(id);
            if (cs == null) return NotFound();

            cs.Course = _courseRepo.GetByID(cs.CrsId);
            cs.Student = _studentRepo.GetByID(cs.StdId);

            return View(cs);
        }

        // GET: CourseStudents/Create
        public IActionResult Create()
        {
            var model = new CourseStudentViewModel
            {
                courseStudents = new CourseStudents(),
                courses = _courseRepo.GetAll(),
                students = _studentRepo.GetAll()
            };
            return View(model);
        }

        // POST: CourseStudents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CourseStudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                _csRepo.Add(model.courseStudents);
                return RedirectToAction(nameof(Index));
            }

            model.courses = _courseRepo.GetAll();
            model.students = _studentRepo.GetAll();
            return View(model);
        }

        // GET: CourseStudents/Edit/5
        public IActionResult Edit(int id)
        {
            var cs = _csRepo.GetByID(id);
            if (cs == null) return NotFound();

            var model = new CourseStudentViewModel
            {
                courseStudents = cs,
                courses = _courseRepo.GetAll(),
                students = _studentRepo.GetAll()
            };
            return View("Edit", model);
        }

        // POST: CourseStudents/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CourseStudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                _csRepo.Update(model.courseStudents);
                return RedirectToAction(nameof(Index));
            }

            model.courses = _courseRepo.GetAll();
            model.students = _studentRepo.GetAll();
            return View("Edit", model);
        }

        // GET: CourseStudents/Delete/5
        public IActionResult Delete(int id)
        {
            var cs = _csRepo.GetByID(id);
            if (cs != null)
            {
                _csRepo.Delete(cs);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
