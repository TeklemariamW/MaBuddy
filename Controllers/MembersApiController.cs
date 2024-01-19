using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mabuddy.Data;
using mabuddy.Models;
using mabuddy.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace mabuddy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersApiController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public MembersApiController(ApplicationDbContext context,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // GET: api/ProgressesApi
        [HttpGet]
        public async Task<IActionResult> GetMembers()
        {
            var vms = new List<Users_in_Role_ViewModel>();

            var userList = await _context.Users.ToListAsync();
            foreach (var user in userList)
            {
                var vm = new Users_in_Role_ViewModel();
                vm.User = user;
                vm.Role = _userManager.GetRolesAsync(user).Result.SingleOrDefault();
                vms.Add(vm);
            }

            //_um.GetRolesAsync()
            return Ok(vms.OrderBy(b => b.User.Nickname).ToList());
        }


        // POST: api/ProgressesApi
        [HttpPost("{id}")]
        public async Task<ActionResult<IEnumerable<Questions>>> PostProgress(string id,
            Users_in_Role_ViewModel usermodel)
        {
            if (_userManager.GetUserId(User) == id)
                return RedirectToAction("Index", "Members");
            if (usermodel.Role == null)
                return RedirectToAction("Index", "Members");


            var user = await _userManager.FindByIdAsync(id);


            var newRole = usermodel.Role;

            var oldRole = _userManager.GetRolesAsync(user).Result.SingleOrDefault();
            if (oldRole != null)
            {
                _userManager.RemoveFromRoleAsync(user, oldRole).Wait();
                _userManager.AddToRoleAsync(user, newRole).Wait();
            }

            return RedirectToAction("Index", "Members");
        }
    }
}