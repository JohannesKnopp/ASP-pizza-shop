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
            return View(mainProducts);
        }

    }
}