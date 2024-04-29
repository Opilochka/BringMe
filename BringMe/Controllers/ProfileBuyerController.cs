using BringMe.Data;
using BringMe.Migrations;
using BringMe.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.IO;

namespace BringMe.Controllers
{
    [Authorize(Roles = "buyer")]
    public class ProfileBuyerController : Controller
    {
        ApplicationDbContext db;
        private readonly UserManager<IdentityUser> _userManager;
        List<Way> ways;
        public List<List<Way>> route;
        public List<ViewsDeliveryAll> deliveryAll;
        public int IdProduct;
        private readonly SignInManager<IdentityUser> _signInManager;


        public ProfileBuyerController(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            db = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Delivery(int value)
        {
            IdProduct = value;
            ViewBag.Id = value;
            return View(db.Localities);
        }

        [HttpGet]
        public async Task<IActionResult> ProfileDelivery()
        {
            List<Product> products = new List<Product>();
            List<Delivery> deliveryTemp = new List<Delivery>();
            List<Way> wayTemp = new List<Way>();
            List<DeliveryWay> deliveryWayTemp = new List<DeliveryWay>();

            List<string> str = new List<string>();
            var name = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(name);
            foreach (var delivery in db.Delivery)
            {
                deliveryTemp.Add(delivery);
            }

            foreach (var way in db.Ways)
            {
                wayTemp.Add(way);
            }

            foreach (var deliveryWay in db.DeliveryWays)
            {
                deliveryWayTemp.Add(deliveryWay);
            }

            foreach (var delivery in deliveryTemp)
            {
                foreach (var product in db.Products)
                {
                    if ((delivery.IdProduct == product.Id) && (delivery.IdUser == user.Id)) 
                    {
                        products.Add(product);
                    } 

                }
            }

            foreach (var delivery in deliveryTemp)
            {
                foreach (var deliveryWay in db.DeliveryWays)
                {
                    if ((delivery.Id == deliveryWay.IdDelivery))
                    {
                        deliveryWayTemp.Add(deliveryWay);
                    }

                }
            }

            foreach(var deliveryWay in deliveryWayTemp)
            {
                foreach(var way in wayTemp)
                {
                    if(way.Id == deliveryWay.IdWay)
                    {
                        foreach(var loc in db.Localities)
                        {
                            if(way.IdLocalityA == loc.Id)
                            {
                                str.Add(loc.Address);
                            }
                        }
                    }
                }
            }
           
            ViewBag.MyList = str.Distinct().ToList(); 
            return View(products);
        }


        [HttpPost]
        public async Task<IActionResult> ProfileDelivery(int value)
        {
            Delivery deliveryDb = new Delivery();
            List<DeliveryWayTest> wayTemp = new List<DeliveryWayTest>();
            int index = value + db.DeliveryTest.First().Id;
            foreach (var delivery in db.DeliveryTest)
            {
                if(delivery.Id == index)
                {
                    deliveryDb.IdUser = delivery.IdUser;
                    deliveryDb.IdProduct = delivery.IdProduct;
                    deliveryDb.DateTime = delivery.DateTime;
                    deliveryDb.Price = delivery.Price;
                    deliveryDb.Status = delivery.Status;
                    deliveryDb.Type = delivery.Type;
                }
            }
            db.Delivery.Add(deliveryDb);
            db.SaveChanges();
            foreach (var way in db.DeliveryWaysTest)
            {
                wayTemp.Add(way);
            }

            foreach(var way in wayTemp)
            {
                DeliveryWay temp = new DeliveryWay
                {
                    IdDelivery = way.IdDelivery,
                    IdWay = way.IdWay,
                    Status = way.Status
                };
                db.DeliveryWays.Add(temp);
               
            }
            db.SaveChanges();

            foreach (var way in db.DeliveryWaysTest)
            {
                db.DeliveryWaysTest.Remove(way);
            }
            foreach(var way in db.DeliveryTest)
            {
                db.DeliveryTest.Remove(way);
            }
            
            db.SaveChanges();
            return RedirectToAction("ProfileDelivery", "ProfileBuyer");
        }

        

        [HttpPost]
        public async Task<IActionResult> Delivery(ViewsDelivery delivery)
        {
           
            GetWaysAsync(delivery);
            
            ViewBag.Countries = new List<ViewsDeliveryAll>(deliveryAll);
            
            var name = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(name);
            
            foreach (var del in deliveryAll)
            {
                DeliveryTest deliverytemp = new DeliveryTest
                {
                    IdProduct = delivery.create_delivery_button,
                    IdUser = user.Id,
                    Price = del.Price,
                    DateTime = del.DateTime,
                    Type = del.Type
                };
                db.DeliveryTest.Add(deliverytemp);
                db.SaveChanges();
            }

            foreach (var way in route)
             {
                 foreach (var way2 in way)
                 {
                    
                     DeliveryWayTest temp = new DeliveryWayTest();
                     temp.IdWay = way2.Id;
                     temp.IdDelivery = route.IndexOf(way);
                     db.DeliveryWaysTest.Add(temp);
                     db.SaveChanges();
                 }
             }
           
            return View(db.Localities);
        }

        public void GetWaysAsync(ViewsDelivery delivery)
        {
            List<Way> buff = new List<Way>();
            route = new List<List<Way>>();
            deliveryAll = new List<ViewsDeliveryAll>();
            ways = new List<Way>();
            Way temp;
            route.Clear();
            deliveryAll.Clear();
            ViewsDeliveryAll elementWithMinPrice;
            ViewsDeliveryAll elementWithMinTime;

            List<Way> tempD = new List<Way>();
            int index = 0;
            int LocalityA = delivery.PickupPoint;

            int price = 0;
            int time = 0;
           
            int i = 0;

            foreach (var way in db.Ways) 
            {
                if(way.Transport == delivery.TypeTransport)
                {
                    ways.Add(way);
                }
            }
            bool[] visited = new bool[ways.Count];

            

            foreach (var way in ways)
            {
                i = ways.IndexOf(way);
                if ((way.IdLocalityA == LocalityA || way.IdLocalityB == LocalityA )&& !visited[i])
                {

                    if (way.IdLocalityA == LocalityA)
                    {
                        DFS(i, way, buff, visited, way.IdLocalityB);
                    }
                    else
                    {
                        DFS(i, way, buff, visited, way.IdLocalityA);
                    }
                } 
            }

            foreach (var way in route)
            {
                ViewsDeliveryAll deliveryTemp = new ViewsDeliveryAll();
                foreach (var w in way)
                {
                    price += w.Price;
                    time += w.Time;
                }
                
                deliveryTemp.Price = price;
                deliveryTemp.Adress = delivery.PickupPoint;
                deliveryTemp.Type = delivery.TypeTransport;
                DateTime dateTime = DateTime.Now;
                dateTime = dateTime.AddHours(time);
                deliveryTemp.DateTime = dateTime;
                deliveryAll.Add(deliveryTemp);
                price = 0;
                time = 0;
            }

            if (delivery.DeliverySelect == "cheap")
            {
                elementWithMinPrice = deliveryAll.OrderBy(v => v.Price).FirstOrDefault();
                index = deliveryAll.IndexOf(elementWithMinPrice);
                tempD = route[index];
                route.Clear();
                deliveryAll.Clear();
                route.Add(tempD);
                deliveryAll.Add(elementWithMinPrice);
            }
            else
            {
                if(delivery.DeliverySelect == "fast")
                {
                    elementWithMinTime = deliveryAll.OrderBy(v => v.DateTime).FirstOrDefault();
                    index = deliveryAll.IndexOf(elementWithMinTime);
                    tempD = route[index];
                    route.Clear();
                    deliveryAll.Clear();
                    route.Add(tempD);
                    deliveryAll.Add(elementWithMinTime);
                }
                else
                {
                    foreach (var del in deliveryAll)
                    {
                        if (del.Price > delivery.PriceRange)
                        {
                            deliveryAll.Remove(del);
                            route.RemoveAt(deliveryAll.IndexOf(del));
                        }
                        else
                        {
                            if (del.DateTime > delivery.DataDelivery)
                            {
                                deliveryAll.Add(del);
                                route.RemoveAt(deliveryAll.IndexOf(del));
                            }
                        }
                    }
                }
                
            } 
        }
        public void DFS(int i,Way wayBuff, List<Way> buff, bool[] visited, int LocalityA)
        {
            List<Way> buff2 = new List<Way>(buff);
            visited[i] = true;
            buff.Add(wayBuff);
            
            foreach (var way in ways)
            {
                i = ways.IndexOf(way);
                if ((way.IdLocalityA == LocalityA || way.IdLocalityB == LocalityA) && !visited[i] && (way != wayBuff))
                {
                    
                    if (way.IdLocalityA == LocalityA)
                    {
                        DFS(i, way, buff, visited, way.IdLocalityB); 
                    }
                    else
                    {
                        DFS(i, way, buff, visited, way.IdLocalityA);
                    }
                }  
            }
            visited[i] = false;
            route.Add(new List<Way>(buff));
            buff.Remove(wayBuff);
 
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string b)
        {
            await _signInManager.SignOutAsync();
            HttpContext.Response.Cookies.Delete("localhost");

            return RedirectToAction("Index", "Home");
        }
    }
}
