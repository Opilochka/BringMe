using BringMe.Data;
using BringMe.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BringMe.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            db = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Catalog()
        {
            return View(db.Products);
        }

        [HttpPost]
        public IActionResult Delivery(int value)
        {

            return RedirectToAction("Delivery", "ProfileBuyer", new {value = value});

        }
    }
}