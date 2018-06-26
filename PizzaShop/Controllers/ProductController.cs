using Newtonsoft.Json;
using Omu.ValueInjecter;
using PizzaShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PizzaShop.Controllers
{
    public class ProductController : Controller
    {

        PizzaShopEntities db = new PizzaShopEntities();

        // GET: Product
        public ActionResult Index()
        {
            var mainProducts = db.Categories.Include("Products").Where(c => c.Name != "Extras").ToList();
            var extras = db.Products.Where(p => p.Category.Name.Equals("Extras")).ToList();
            ViewBag.Extras = extras;

            var memberList = extras.Select(x =>
            {
                var dto = new ProductDto()
                {
                    ID = x.ID,
                    Name = x.Name,
                    Price = x.Price
                };
                return dto;
            }).ToList();

            ViewBag.JsonExtras = JsonConvert.SerializeObject(memberList);
            return View(mainProducts);
        }

    }
}