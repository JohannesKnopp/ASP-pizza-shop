using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Benutzername")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Passwort")]
        public string Password { get; set; }

        /*
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
        */
    }

    public class RegisterViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Vorname")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vorname ist verpflichtend")]
        public string Firstname { get; set; }

        [Display(Name = "Nachname")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nachname ist verpflichtend")]
        public string Lastname { get; set; }

        [Display(Name = "Straße")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Straße ist verpflichtend")]
        public string Street { get; set; }

        [Display(Name = "Hausnummer")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Hausnummer ist verpflichtend")]
        public string Housenumber { get; set; }

        [Display(Name = "Stadt")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Stadt ist verpflichtend")]
        public string City { get; set; }

        [Display(Name = "Postleitzahl")]
        [DataType(DataType.PostalCode)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Postleitzahl ist verpflichtend")]
        public string Zipcode { get; set; }

        [Display(Name = "Telefonnummer")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Telefonnummer ist verpflichtend")]
        public string PhoneNumber { get; set; }

        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage ="Muss gültige E-Mail Adresse sein")]
        public string EmailAddress { get; set; }

        [Required]
        [Display(Name = "Benutzername")]
        [RegularExpression("^[a-zA-Z0-9]+")]
        public string Username { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = "Das Passwort muss zwischen 8 und 40 Zeichen sein", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Passwort")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Passwort bestätigen")]
        [Compare("Password", ErrorMessage = "Passwörter stimmen nicht überein")]
        public string ConfirmPassword { get; set; }
    }

    public class EditCustomerViewModel
    {

        public EditCustomerViewModel() { }

        public EditCustomerViewModel(Customer c)
        {
            this.Firstname = c.Firstname;
            this.Lastname = c.Lastname;
            this.Street = c.Street;
            this.Housenumber = c.Housenumber;
            this.City = c.City;
            this.Zipcode = c.Zipcode;
            this.PhoneNumber = c.PhoneNumber;
            this.EmailAddress = c.EmailAddress;
            this.Username = c.Username;
            this.ConfirmationPassword = "";
        }

        public int Id { get; set; }

        [Display(Name = "Vorname")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vorname ist verpflichtend")]
        public string Firstname { get; set; }

        [Display(Name = "Nachname")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nachname ist verpflichtend")]
        public string Lastname { get; set; }

        [Display(Name = "Straße")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Straße ist verpflichtend")]
        public string Street { get; set; }

        [Display(Name = "Hausnummer")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Hausnummer ist verpflichtend")]
        public string Housenumber { get; set; }

        [Display(Name = "Stadt")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Stadt ist verpflichtend")]
        public string City { get; set; }

        [Display(Name = "Postleitzahl")]
        [DataType(DataType.PostalCode)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Postleitzahl ist verpflichtend")]
        public string Zipcode { get; set; }

        [Display(Name = "Telefonnummer")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Telefonnummer ist verpflichtend")]
        public string PhoneNumber { get; set; }

        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Muss gültige E-Mail Adresse sein")]
        public string EmailAddress { get; set; }

        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Bestätige Passwort um Änderungen zu speichern")]
        public string ConfirmationPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Altes Passwort")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = "Das Passwort muss zwischen 8 und 40 Zeichen sein", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Neues Passwort")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Bestätige neues Passwort")]
        [Compare("NewPassword", ErrorMessage = "Die Passwörter stimmen nicht überein")]
        public string ConfirmNewPassword { get; set; }
    }
}
