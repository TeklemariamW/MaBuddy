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
    public class TipsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tips
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tips.ToListAsync());
        }

        [Authorize(Roles = "Admin")]
        // GET: Tips/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tips/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TipTitle,TipText")] Tips tips)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tips);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(tips);
        }

        // POST: Tips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tips = await _context.Tips.FindAsync(id);
            _context.Tips.Remove(tips);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool TipsExists(int id)
        {
            return _context.Tips.Any(e => e.Id == id);
        }
    }
}