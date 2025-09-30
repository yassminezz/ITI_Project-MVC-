using ItiProject_ms1.Models;
using ItiProject_ms1.Views.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ItiProject_ms1.Controllers
{
    public class StudentController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        UniDbContext context = new UniDbContext();

        public IActionResult ShowStuds()
        {
            var sd = new StudentDeptViewModel
            {
                students = context.students.ToList(),
                departments = context.departments.ToList()
            };
            return View("Index", sd);
        }

        // GET
        public IActionResult UpdateStud(int id)
        {
            var s = context.students.FirstOrDefault(st => st.Id == id);
            if (s == null)
                return NotFound();

            var lst = context.departments.ToList();

            StudDeptViewModel sd = new StudDeptViewModel
            {
                student = s,
                departments = lst
            };

            return View("Form", sd);
        }

        // POST
        [HttpPost]
        public IActionResult UpdateStud(StudDeptViewModel vm)
        {
            var s = context.students.FirstOrDefault(st => st.Id == vm.student.Id);
            if (s == null)
                return NotFound();

            s.Name = vm.student.Name;
            s.Address = vm.student.Address;
            s.Grade = vm.student.Grade;
            s.DeptId = vm.student.DeptId;

            context.SaveChanges();

            return RedirectToAction("ShowStuds");
        }
        public IActionResult AddStud()
        {
            StudDeptViewModel sd = new StudDeptViewModel
            {
                student = new Student(),
                departments = context.departments.ToList()
            };
            return View("Form", sd);
        }
        [HttpPost]
        public IActionResult AddStud(StudDeptViewModel sd)
        {
            if (ModelState.IsValid)
            {
                context.students.Add(sd.student);
                context.SaveChanges();
                return RedirectToAction("ShowStuds");
            }

            sd.departments = context.departments.ToList();
            return View("Form", sd);
        }
        public IActionResult deleteStud(int id)
        {
            var st = context.students.FirstOrDefault(s => s.Id == id);
            if (st != null)
            {
                context.students.Remove(st);
                context.SaveChanges();
            }

            return RedirectToAction("ShowStuds"); // fixed casing
        }
        public IActionResult ShowInfo(int id)
        {
            var s = context.students.FirstOrDefault(s => s.Id == id);
            return View("StudInfo", s);
        }
    }
}
