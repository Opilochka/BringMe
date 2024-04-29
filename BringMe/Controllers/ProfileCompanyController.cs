using BringMe.Data;
using BringMe.Migrations;
using BringMe.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace BringMe.Controllers
{
    [Authorize(Roles = "company")]
    public class ProfileCompanyController : Controller
    {
        ApplicationDbContext db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public ProfileCompanyController(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            db = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var name = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(name);
            ViewBag.Message = user.Id;
            return View(db.Products);
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var name = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(name);
            ViewBag.Message = user.Id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(FormProduct product)
        {
            if (ModelState.IsValid)
            {
                var filename = ContentDispositionHeaderValue
                                    .Parse(product.Picture.ContentDisposition)
                                    .FileName
                                    .Trim('"');
                filename = Path.Combine("wwwroot/Content/", $@"{filename}");
                if (Directory.Exists("wwwroot/Content/"))
                {
                    using (FileStream fs = System.IO.File.Create(filename))
                    {
                        product.Picture.CopyTo(fs);
                        fs.Flush();
                    }
                }
                product.ImageUrl = "/Content/" + product.Picture.FileName;
                var name = User.Identity.Name;
                var user = await _userManager.FindByNameAsync(name);
                Product productBD = new Product
                {
                    Name = product.Name,
                    ImageUrl = product.ImageUrl,
                    Description = product.Description,
                    Mass = product.Mass,
                    Size = product.Size,
                    Price = product.Price,
                    IdUser = user.Id
                };
                db.Products.Add(productBD);
                db.SaveChanges();
                return RedirectToAction("Index", "ProfileCompany");
            }

            return View(product);
        }


        [HttpGet]
        public async Task<IActionResult> CreateStorageHouse()
        {
            var name = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(name);
            ViewBag.Message = user.Id;
            return View(db.Localities);
        }
        [HttpPost]
        public async Task<IActionResult> CreateStorageHouse(Models.Locality locality)
        {
            var name = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(name);
            locality.IdUser = user.Id;
            if (ModelState.IsValid)
            {
                db.Localities.Add(locality);
                db.SaveChanges();
                return RedirectToAction("Index", "ProfileCompany");
            }

            return View(locality);
        }

        [HttpGet]
        public async Task<IActionResult> CreateRoads()
        {
            List<Models.Locality> temp = new List<Models.Locality>();
            foreach (var locality in db.Localities)
            {
                temp.Add(locality);
            }
            var name = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(name);
            ViewBag.Message = user.Id;
            ViewBag.Countries = temp;
            List<Way> ways = new List<Way>();
            foreach(var way in db.Ways)
            {
                if(way.IdUser == user.Id)
                {
                    ways.Add(way);
                }
            }
            ViewBag.MyList = ways;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoads(Way way)
        {
            var name = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(name);
            way.IdUser = user.Id;
            if (ModelState.IsValid)
            {
                db.Ways.Add(way);
                db.SaveChanges();
                return RedirectToAction("Index", "ProfileCompany");
            }

            return RedirectToAction("CreateRoads", "ProfileCompany");
        }

        /*[ValidateAntiForgeryToken]*/
        [HttpPost]
        public async Task<IActionResult> Logout(string b)
        {
            await _signInManager.SignOutAsync();
            HttpContext.Response.Cookies.Delete("localhost");

            return RedirectToAction("Index", "Home");
        }

    }
}
