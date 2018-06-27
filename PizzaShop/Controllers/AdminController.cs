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
            //if (product.Price < 0)
            //{
            //    ModelState.AddModelError(string.Empty, "Preis darf nicht kleiner als Null sein");
            //    product.Price = db.Products.Find(product.ID).Price;
            //    return View(product);
            //}
            if (ModelState.IsValid)
            {
                var dbProd = db.Products.Find(product.ID);
                dbProd.CategoryID = product.SelectedCategoryID;
                dbProd.Name = product.Name;
                dbProd.Price = product.Price;
                dbProd.IsInSortiment = product.IsInSortiment;

                
                //var x = db.ProductHasAllergens.Where(a => allergens.Contains(a.ProductID)).ToList();
                //foreach (var i in allergens)
                //{
                //    if (x.FirstOrDefault(a => a.AllergenID == i) == null)
                //    {
                //        x
                //    }
                //}
                

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Ungültige Daten");
            }
            return View(product);
        }

        [HttpGet]
        public ActionResult CreateProduct()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login");
            }
            EditProductViewModel product = new EditProductViewModel(db.Categories.ToList(), db.Allergens.ToList());
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct(EditProductViewModel product)
        {
            //if (product.Price < 0)
            //{
            //    ModelState.AddModelError(string.Empty, "Preis darf nicht kleiner als Null sein");
            //    product.Price = db.Products.Find(product.ID).Price;
            //    return View(product);
            //}
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login");
            }
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
            else
            {
                ModelState.AddModelError(string.Empty, "Ungültige Daten");
            }
            return View(product);
        }

        [HttpPost]
        public ActionResult RemoveProduct(int id)
        {
            var product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return null;
        }

        public ActionResult ListCustomers()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login");
            }
            List<Customer> customers = db.Customers.ToList();
            return View(customers);
        }

        [HttpGet]
        public ActionResult EditCustomer(int? id)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login");
            }
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Customer customer = db.Customers.Find(id);

            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCustomer(EditCustomerViewModel customer)
        {
            if (ModelState.IsValid)
            {
                var dbCust = db.Customers.Find(customer.Id);
                dbCust.Firstname = customer.Firstname;
                dbCust.Lastname = customer.Lastname;
                dbCust.Street = customer.Street;
                dbCust.Housenumber = customer.Housenumber;
                dbCust.City = customer.City;
                dbCust.Zipcode = customer.Zipcode;
                dbCust.PhoneNumber = customer.PhoneNumber;
                dbCust.EmailAddress = customer.EmailAddress;
                db.SaveChanges();

                db.SaveChanges();
                return RedirectToAction("ListCustomers");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Ungültige Daten");
            }
            return View(customer);
        }

        [HttpPost]
        public ActionResult RemoveCustomer(int id)
        {
            var customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return null;
        }
    }
}