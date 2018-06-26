using PizzaShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PizzaShop.Controllers
{
    public class CartController : Controller
    {

        PizzaShopEntities db = new PizzaShopEntities();

        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }



        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(int id, int quantity, )
        {
            var cart = Session["cart"] as List<CartViewModel>;
            var sessionProd = cart.Where(p => p.ProductID == id).FirstOrDefault();
            if(sessionProd == null)
            {
                Product dbProd = db.Products.Where(p => p.ID == id).FirstOrDefault();
                var toppings = new ICollection<Product>;
                foreach(int i in toppingID)
                {

                }
                CartViewModel product = new { ProductID = id, Name = dbProd.Name, Quantity = quantity, Price = dbProd.Price, Tax = dbProd.Category.Tax, Toppings = )}
                cart.Add();
                Session["cart"] = cart;
            }
            else
            {
                sessionProd.Quantity += product.Quantity;
            }
            return RedirectToAction("Index", "Customer");
        }
        */
    }
}