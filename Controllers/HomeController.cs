using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using mabuddy.Data;
using Microsoft.AspNetCore.Mvc;
using mabuddy.Models;
using mabuddy.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace mabuddy.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _db;
        private UserManager<ApplicationUser> _um;
        private RoleManager<IdentityRole> _rm;

        public HomeController(ApplicationDbContext db, UserManager<ApplicationUser> um, RoleManager<IdentityRole> rm)
        {
            _db = db;
            _um = um;
            _rm = rm;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ViewResult> Faq()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Introduction()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> ChooseLevel()
        {
            return View();
        }

        [Authorize]
        // [HttpGet("{id}")]
        public async Task<IActionResult> Subjects()
        {
            return View();
        }

        public async Task<IActionResult> GradeE()
        {
            var level = 1;
            var currentUser = await _um.GetUserAsync(User);
            currentUser.Level = level;

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Subjects));
        }

        public async Task<IActionResult> GradeC()
        {
            var level = 2;
            var currentUser = await _um.GetUserAsync(User);
            currentUser.Level = level;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Subjects));
        }

        public async Task<IActionResult> GradeA()
        {
            var level = 3;
            var currentUser = await _um.GetUserAsync(User);
            currentUser.Level = level;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Subjects));
        }

        [Authorize]
        public async Task<IActionResult> Lessons()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            var vm = await _db.Users.ToListAsync();
            return View(vm.OrderByOrdinal(b => b.Nickname));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}