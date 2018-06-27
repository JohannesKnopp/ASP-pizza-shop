using PizzaShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PizzaShop.Controllers
{
    public class AdminController : Controller
    {

        PizzaShopEntities db = new PizzaShopEntities();

        
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login");
            }
            List<Product> products = db.Products.ToList();
            return View(products);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Administrator admin)
        {
            if (ModelState.IsValid)
            {
                var dbAdmin = db.Administrators.Where(a => a.Username.Equals(admin.Username)).FirstOrDefault();
                if (dbAdmin != null)
                {
                    var loginHash = Cryptography.Hash(admin.PasswordHash);
                    if (dbAdmin.PasswordHash.Equals(loginHash) == false)
                    {
                        ModelState.AddModelError(string.Empty, "Benutzername und Passwort stimmen nicht überein");
                    }
                    else
                    {
                        Session["Admin"] = dbAdmin.Username;
                        return RedirectToAction("Index", "Admin");
                    }
                }
            }
            return View();
        }

        public ActionResult LogOff()
        {
            Session.RemoveAll();
            Session.Clear();
            return RedirectToAction("Login", "Admin");
        }


        [HttpGet]
        public ActionResult EditProduct(int? id)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login");
            }
            if(id == null)
            {
                return RedirectToAction("Index");
            }
            Product product = db.Products.Find(id);
            List<Category> categories = db.Categories.ToList();
            List<Allergen> allergens = db.Allergens.ToList();

            EditProductViewModel prodVM = new EditProductViewModel(product, categories, allergens);
            return View(prodVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(EditProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                var dbProd = db.Products.Find(product.ID);
                dbProd.CategoryID = product.SelectedCategoryID;
                dbProd.Name = product.Name;
                dbProd.Price = product.Price;
                dbProd.IsInSortiment = product.IsInSortiment;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpGet]
        public ActionResult CreateProduct()
        {
            EditProductViewModel product = new EditProductViewModel(db.Categories.ToList(), db.Allergens.ToList());
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct(EditProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                Product p = new Product
                {
                    Name = product.Name,
                    CategoryID = product.SelectedCategoryID,
                    IsInSortiment = product.IsInSortiment,
                    Price = product.Price
                };
                db.Products.Add(p);

                db.SaveChanges();
                return RedirectToAction("ListProducts");
            }
            return View(product);
        }
    }
}