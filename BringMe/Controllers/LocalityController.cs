using BringMe.Data;
using BringMe.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BringMe.Controllers
{
    public class LocalityController : Controller
    {
        ApplicationDbContext db;
        private readonly UserManager<IdentityUser> _userManager;

        public LocalityController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            db = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var name = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(name);
            ViewBag.Message = user.Id;
            return View(db.Localities);
        }

        [HttpPost]
        public async Task<IActionResult> Index(Locality locality)
        {
            var name = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(name);
            locality.IdUser = user.Id;
            if (ModelState.IsValid)
            {
                db.Localities.Add(locality);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(locality);
        }
    }
}
