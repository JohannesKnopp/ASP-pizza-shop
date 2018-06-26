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

            var cart = Session["cart"] as List<CartViewModel>;
            var uniqueId = Convert.ToInt32(Session["cartNextID"]);
            var productVM = new CartViewModel
            {
                UniqueID = uniqueId,
                ProductID = product.ID,
                Name = product.Name,
                Price = product.Price,
                Quantity = quantity,
                Tax = product.Category.Tax,
                Toppings = toppings
            };

            Session["cartNextID"] = uniqueId + 1;
            cart.Add(productVM);
            Session["cart"] = cart;

            return null;
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int uniqueId)
        {
            var cart = Session["cart"] as List<CartViewModel>;
            var productToRemove = cart.SingleOrDefault(u => u.UniqueID == uniqueId);
            cart.Remove(productToRemove);
            Session["cart"] = cart;

            ViewBag.TotalPrice = cart.Sum(p => p.FullPrice);

            return null;
        }

        public ActionResult CartSummary()
        {
            var cart = Session["cart"] as List<CartViewModel>;
            ViewBag.TotalPrice = cart.Sum(p => p.FullPrice);
            return View(cart);
        }
    }
}