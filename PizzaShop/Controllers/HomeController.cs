using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PizzaShop.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.Message = "Wir sind ein kleines Familienunternehmen aus dem Süden Italiens. 1989 ist mein Vater nach Wien gezogen und hat " +
                "2014 diese Pizzeria eröffnet. Ich hoffe Ihnen gefällt Ihr Aufenthalt!";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Kontakt";

            return View();
        }
    }
}