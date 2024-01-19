using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace mabuddy.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MembersController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}