using PizzaShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet;

namespace PizzaShop.Controllers
{
    public class CustomerController : Controller
    {

        PizzaShopEntities db = new PizzaShopEntities();

        public CustomerController() { }

        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel customer)
        {
            if (ModelState.IsValid)
            {
                if (getCustomerByUsername(customer.Username) != null)
                {
                    ModelState.AddModelError(string.Empty, "Benutzername bereits vergeben.");
                    return View(customer);
                }

                Customer c = new Customer
                {
                    Firstname = customer.Firstname,
                    Lastname = customer.Lastname,
                    Street = customer.Street,
                    Housenumber = customer.Housenumber,
                    City = customer.City,
                    Zipcode = customer.Zipcode,
                    PhoneNumber = customer.PhoneNumber,
                    EmailAddress = customer.EmailAddress,
                    Username = customer.Username
                };
                c.PasswordHash = Cryptography.Hash(customer.Password);
                db.Customers.Add(c);
                db.SaveChanges();
                return View("Login");
            }
            return View(customer);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel customer)
        {
            if (ModelState.IsValid) {
                var dbCust = getCustomerByUsername(customer.Username);
                if (dbCust != null)
                {
                    var loginHash = Cryptography.Hash(customer.Password);
                    if (dbCust.PasswordHash.Equals(loginHash) == false)
                    {
                        ModelState.AddModelError(string.Empty, "Benutzername und Passwort stimmen nicht überein");
                    }
                    else
                    {
                        Session["CurrentCustomerName"] = dbCust.Firstname + " " + dbCust.Lastname;
                        Session["CurrentCustomerId"] = dbCust.ID;
                        Session["CurrentCustomerUsername"] = dbCust.Username;
                        return RedirectToAction("Index", "Product");
                    }
                }
            }
            return View();
        }

        public ActionResult LogOff()
        {
            Session.RemoveAll();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Edit()
        {
            if(Session["CurrentCustomerID"] == null)
            {
                return RedirectToAction("Login");
            }
            Customer dbCust = getCustomerByUsername(Session["CurrentCustomerUsername"].ToString());
            EditCustomerViewModel customer = new EditCustomerViewModel(dbCust);
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditCustomerViewModel customer)
        {
            if (ModelState.IsValid)
            {
                var dbCust = getCustomerByUsername(Session["CurrentCustomerUsername"].ToString());
                var pwdHash = Cryptography.Hash(customer.ConfirmationPassword);
                if (dbCust.PasswordHash.Equals(pwdHash))
                {
                    dbCust.Firstname = customer.Firstname;
                    dbCust.Lastname = customer.Lastname;
                    dbCust.Street = customer.Street;
                    dbCust.Housenumber = customer.Housenumber;
                    dbCust.City = customer.City;
                    dbCust.Zipcode = customer.Zipcode;
                    dbCust.PhoneNumber = customer.PhoneNumber;
                    dbCust.EmailAddress = customer.EmailAddress;
                    db.SaveChanges();
                    return RedirectToAction("Edit");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Ungültiges Passwort");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Ungültige Daten");
            }
            return View("Edit");
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            if (Session["CurrentCustomerID"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel passwords)
        {
            if (ModelState.IsValid)
            {
                if (passwords.OldPassword.Equals(passwords.NewPassword))
                {
                    ModelState.AddModelError(string.Empty, "New and Old password can't be the same.");
                    return RedirectToAction("ChangePassword");
                }
                var dbCust = getCustomerByUsername(Session["CurrentCustomerUsername"].ToString());
                if (Cryptography.Compare(passwords.OldPassword, dbCust.PasswordHash))
                {
                    var newHash = Cryptography.Hash(passwords.NewPassword);
                    dbCust.PasswordHash = newHash;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Incorrect Password");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "All fields required");
            }
            return RedirectToAction("ChangePassword");
        }

        private Customer getCustomerByUsername(string username)
        {
            var customer = db.Customers.Where(c => c.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            return customer;
        }

        private Customer getCustomerById(int id)
        {
            var customer = db.Customers.Where(c => c.ID == id).FirstOrDefault();
            return customer;
        }
    }
}