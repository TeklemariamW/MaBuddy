using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mabuddy.Data;
using mabuddy.Models;
using mabuddy.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace mabuddy.Controllers
{
    [Authorize]
    public class InboxController : Controller
    {
        private ApplicationDbContext _db;
        private UserManager<ApplicationUser> _um;
        private RoleManager<IdentityRole> _rm;

        public InboxController(ApplicationDbContext db, UserManager<ApplicationUser> um, RoleManager<IdentityRole> rm)
        {
            _db = db;
            _um = um;
            _rm = rm;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var pPost = _db.Inbox.Include(blog => blog.ApplicationUser).ToList().OrderByDescending(s => s.Id);
            return View(pPost);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Outbox()
        {
            var pPost = _db.Inbox.Include(blog => blog.ApplicationUser).ToList().OrderByDescending(s => s.Id);
            return View(pPost);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(Inbox inbox)
        {
            if (ModelState.IsValid)
            {
                var user = _um.GetUserAsync(User).Result;
                inbox.ApplicationUserID = user.Id;
                inbox.ApplicationUser = user;
                _db.Add(inbox);
                _db.SaveChanges();

                ModelState.Clear();
            }

            return RedirectToAction("Index", View());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}