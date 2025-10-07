using ItiProject_ms1.Models;
using ItiProject_ms1.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ItiProject_ms1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DepartmentsController : Controller
    {
        private readonly IBaseRepository<Department> _deptRepo;

        public DepartmentsController(IBaseRepository<Department> deptRepo)
        {
            _deptRepo = deptRepo;
        }

        // GET: Departments
        public IActionResult Index()
        {
            var lst = _deptRepo.GetAll();
            return View(lst);
        }

        // GET: Departments/Details/5
        public IActionResult Details(int id)
        {
            var dept = _deptRepo.GetByID(id);
            if (dept == null) return NotFound();
            return View(dept);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            return View(new Department());
        }

        // POST: Departments/Create
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _deptRepo.Add(department);
                return RedirectToAction("Index");
            }
            return View(department);
        }

        // GET: Departments/Edit/5
        public IActionResult Edit(int id)
        {
            var dept = _deptRepo.GetByID(id);
            if (dept == null) return NotFound();
            return View("Edit", dept); // reuse Create view for editing
        }

        // POST: Departments/Edit/5
        [HttpPost]
        public IActionResult Edit(Department updatedDept)
        {
            if (ModelState.IsValid)
            {
                _deptRepo.Update(updatedDept);
                return RedirectToAction("Index");
            }
            return View(updatedDept);
        }

        // GET: Departments/Delete/5
        public IActionResult Delete(int id)
        {
            var dept = _deptRepo.GetByID(id);
            if (dept != null)
            {
                _deptRepo.Delete(dept);
            }
            return RedirectToAction("Index");
        }
    }
}
