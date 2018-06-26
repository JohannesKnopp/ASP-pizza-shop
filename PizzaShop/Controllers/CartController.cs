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



        [HttpPost]
        public ActionResult AddProduct(int productId, int quantity, int[] extras)
        {

            var product = db.Products.Find(productId);
            List<Product> toppings = new List<Product>();
            if (extras != null) {
                toppings = db.Products.Where(p => extras.Contains(p.ID)).ToList();
            }

            var productVM = new CartViewModel
            {
                ProductID = product.ID,
                Name = product.Name,
                Price = product.Price,
                Quantity = quantity,
                Tax = product.Category.Tax,
                Toppings = toppings
            };
            var cart = Session["cart"] as List<CartViewModel>;
            cart.Add(productVM);
            Session["cart"] = cart;

            return null;
        }

        [HttpPost]
        public ActionResult RemoveProduct()
        {
            return null;
        }

        public ActionResult CartSummary()
        {
            var cart = Session["cart"] as List<CartViewModel>;
            return View(cart);
        }

        public ActionResult PartialCartSummary()
        {
            var cart = Session["cart"] as List<CartViewModel>;
            return PartialView(cart);
        }
    }
}