using Invoicer.Models;
using Invoicer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PizzaShop.Models;

namespace PizzaShop.Controllers
{
    public class OrderController : Controller
    {

        PizzaShopEntities db = new PizzaShopEntities();

        // GET: Order
        public ActionResult Index()
        {
            var cart = Session["cart"] as List<CartViewModel>;
            ViewBag.TotalPrice = cart.Sum(p => p.FullPrice);
            if(Session["CurrentCustomerID"] == null)
            {
                return RedirectToAction("Login", "Customer");
            }
            else if(ViewBag.TotalPrice < 12)
            {
                ModelState.AddModelError(string.Empty, "Mindestbestellwert (12,00€) nicht erreicht");
                return RedirectToAction("CartSummary", "Cart");
            }
            return View(cart);
        }

        public ActionResult Finished()
        {
            var cart = Session["cart"] as List<CartViewModel>;
            var customerId = Convert.ToInt32(Session["CurrentCustomerID"]);
            var orderDate = DateTime.Now;

            Order order = new Order
            {
                CustomerID = customerId,
                Status = 2,
                OrderDate = orderDate
            };

            db.Orders.Add(order);
            db.SaveChanges();

            var dbOrder = db.Orders.OrderByDescending(o => o.ID).First();

            foreach(var p in cart)
            {
                OrderHasProduct ohp = new OrderHasProduct { OrderID = dbOrder.ID, ProductID = p.ProductID, Quantity = p.Quantity };
                db.OrderHasProducts.Add(ohp);
                foreach(var t in p.Toppings)
                {
                    OrderHasProduct oht = new OrderHasProduct { OrderID = dbOrder.ID, ProductID = t.ID, Quantity = 1 };
                    db.OrderHasProducts.Add(oht);
                }
            }
            db.SaveChanges();

            return View();
        }

        public void PrintPDF()
        {








            /*
            new InvoicerApi(SizeOption.A4, OrientationOption.Landscape, "£")
                .TextColor("#CC0000")
                .BackColor("#FFD6CC")
                .Items(new List<ItemRow> {
                    ItemRow.Make("Nexus 6", "Midnight Blue", (decimal)1, 20, (decimal)166.66, (decimal)199.99),
                    ItemRow.Make("24 Months (£22.50pm)", "100 minutes, Unlimited texts, 100 MB data 3G plan with 3GB of UK Wi-Fi", (decimal)1, 20, (decimal)360.00, (decimal)432.00),
                    ItemRow.Make("Special Offer", "Free case (blue)", (decimal)1, 0, (decimal)0, (decimal)0),
                })
                .Totals(new List<TotalRow> {
                    TotalRow.Make("Sub Total", (decimal)526.66),
                    TotalRow.Make("VAT @ 20%", (decimal)105.33),
                    TotalRow.Make("Total", (decimal)631.99, true),
                })
                .Details(new List<DetailRow> {
                    DetailRow.Make("PAYMENT INFORMATION", "Make all cheques payable to Vodafone UK Limited.", "", "If you have any questions concerning this invoice, contact our sales department at sales@vodafone.co.uk.", "", "Thank you for your business.")
                })
                .Footer("http://www.vodafone.co.uk")
                .Save("hi.pdf");
                */
        }
    }
}