using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mabuddy.Data;
using mabuddy.Models;
using Microsoft.AspNetCore.Authorization;

namespace mabuddy.Controllers
{
    [Authorize]
    public class AddLessonController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AddLessonController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AddLesson
        public async Task<IActionResult> Index()
        {
            return View(await _context.AddLesson.ToListAsync());
        }

        //Mathematics 1
        public async Task<IActionResult> Derivation()
        {
            return View(await _context.AddLesson.ToListAsync());
        }

        public async Task<IActionResult> Integration()
        {
            return View(await _context.AddLesson.ToListAsync());
        }

        public async Task<IActionResult> Limits()
        {
            return View(await _context.AddLesson.ToListAsync());
        }

        public async Task<IActionResult> ComplexNumbers()
        {
            return View(await _context.AddLesson.ToListAsync());
        }

        //Mathematics 2
        public async Task<IActionResult> PartialDerivation()
        {
            return View(await _context.AddLesson.ToListAsync());
        }

        public async Task<IActionResult> SequenceAndSeries()
        {
            return View(await _context.AddLesson.ToListAsync());
        }

        //Mathematics 3
        public async Task<IActionResult> ApplicationOfDerivation()
        {
            return View(await _context.AddLesson.ToListAsync());
        }

        public async Task<IActionResult> ApplicationOfIntegration()
        {
            return View(await _context.AddLesson.ToListAsync());
        }

        public async Task<IActionResult> InverseFunctions()
        {
            return View(await _context.AddLesson.ToListAsync());
        }


        [Authorize(Roles = "Admin")]
        // GET: AddLesson/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AddLesson/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LessonTitle,LessonUrl,Level,Subject")]
            AddLesson addLesson)
        {
            if (ModelState.IsValid)
            {
                _context.Add(addLesson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(addLesson);
        }

        // POST: AddLesson/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var addLesson = await _context.AddLesson.FindAsync(id);
            _context.AddLesson.Remove(addLesson);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}