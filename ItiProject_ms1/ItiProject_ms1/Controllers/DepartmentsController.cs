using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ItiProject_ms1.Models;

namespace ItiProject_ms1.Controllers
{
    public class DepartmentsController : Controller
    {
        public UniDbContext context = new UniDbContext();

        // GET: Departments
        public async Task<IActionResult> Index()
        {
            var lst = await context.departments.ToListAsync();
            return View(lst);
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var d = await context.departments.FirstOrDefaultAsync(d => d.Id == id);
            return View(d);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            Department dept = new Department();
            return View(dept);
        }

        // POST: Departments/Create
        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                context.Add(department);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        // GET: Departments/Edit/5
        public IActionResult Edit(int id)
        {
            var dept = context.departments.FirstOrDefault(d => d.Id == id);
            if (dept == null) return NotFound();
            return View("Edit", dept); // reuse Create view for editing
        }

        // POST: Departments/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(Department updatedDept)
        {
            var dept = await context.departments.FirstOrDefaultAsync(d => d.Id == updatedDept.Id);
            if (dept != null)
            {
                dept.Name = updatedDept.Name;
                dept.ManagerName = updatedDept.ManagerName;
                await context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var d = await context.departments.FirstOrDefaultAsync(d => d.Id == id);
            if (d != null)
            {
                context.departments.Remove(d);
                await context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
