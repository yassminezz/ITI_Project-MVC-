using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ItiProject_ms1.Models;

namespace ItiProject_ms1.Controllers
{
    public class CoursesController : Controller
    {
        public UniDbContext context = new UniDbContext();

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var lst = await context.courses.ToListAsync();
            return View(lst);
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var d = await context.courses.FirstOrDefaultAsync(d => d.Id == id);
            return View(d);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            Course crs = new Course();
            return View(crs);
        }

        // POST: Courses/Create
        [HttpPost]
        public async Task<IActionResult> Create(Course crs)
        {
            if (ModelState.IsValid)
            {
                context.Add(crs);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(crs);
        }

        // GET: Courses/Edit/5
        public IActionResult Edit(int id)
        {
            var crs = context.courses.FirstOrDefault(d => d.Id == id);
            if (crs == null) return NotFound();
            return View("Edit", crs); // reuse Create view for editing
        }

        // POST: Courses/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(Course updatedCrs)
        {
            var crs = await context.courses.FirstOrDefaultAsync(d => d.Id == updatedCrs.Id);
            if (crs != null)
            {
                crs.Name = updatedCrs.Name;
                crs.Degree = updatedCrs.Degree;
                crs.MinimumDegree = updatedCrs.MinimumDegree;
                crs.Hours = updatedCrs.Hours;
                crs.DeptId = updatedCrs.DeptId;
                await context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var d = await context.courses.FirstOrDefaultAsync(d => d.Id == id);
            if (d != null)
            {
                context.courses.Remove(d);
                await context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
