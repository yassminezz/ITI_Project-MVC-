using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ItiProject_ms1.Models;

namespace ItiProject_ms1.Controllers
{
    public class InstructorsController : Controller
    {
        public UniDbContext context = new UniDbContext();

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var lst = await context.instructors.ToListAsync();
            return View(lst);
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var d = await context.instructors.FirstOrDefaultAsync(d => d.Id == id);
            return View(d);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            Instructor ins = new Instructor();
            return View(ins);
        }

        // POST: Courses/Create
        [HttpPost]
        public async Task<IActionResult> Create(Instructor ins)
        {
            if (ModelState.IsValid)
            {
                context.Add(ins);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(ins);
        }

        // GET: Courses/Edit/5
        public IActionResult Edit(int id)
        {
            var ins = context.instructors.FirstOrDefault(d => d.Id == id);
            if (ins == null) return NotFound();
            return View("Edit", ins); 
        }

        // POST: Courses/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(Instructor updatedins)
        {
            var ins = await context.instructors.FirstOrDefaultAsync(d => d.Id == updatedins.Id);
            if (ins != null)
            {
                ins.Name = updatedins.Name;
                ins.Address = updatedins.Address;
                ins.Salary= updatedins.Salary;
                ins.DeptId = updatedins.DeptId;  
                await context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var d = await context.instructors.FirstOrDefaultAsync(d => d.Id == id);
            if (d != null)
            {
                context.instructors.Remove(d);
                await context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
