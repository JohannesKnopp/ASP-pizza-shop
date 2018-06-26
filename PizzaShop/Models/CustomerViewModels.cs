using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /*
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
        */
    }

    public class RegisterViewModel
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First name required")]
        public string Firstname { get; set; }

        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name required")]
        public string Lastname { get; set; }

        [Display(Name = "Street")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Street required")]
        public string Street { get; set; }

        [Display(Name = "Housenumber")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Housenumber required")]
        public string Housenumber { get; set; }

        [Display(Name = "City")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "City required")]
        public string City { get; set; }

        [Display(Name = "Postal Code")]
        [DataType(DataType.PostalCode)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Postal code required")]
        public string Zipcode { get; set; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone number required")]
        public string PhoneNumber { get; set; }

        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage ="Must be valid E-Mail Address")]
        public string EmailAddress { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
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

        [Display(Name = "First Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First name required")]
        public string Firstname { get; set; }

        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name required")]
        public string Lastname { get; set; }

        [Display(Name = "Street")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Street required")]
        public string Street { get; set; }

        [Display(Name = "Housenumber")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Housenumber required")]
        public string Housenumber { get; set; }

        [Display(Name = "City")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "City required")]
        public string City { get; set; }

        [Display(Name = "Postal Code")]
        [DataType(DataType.PostalCode)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Postal code required")]
        public string Zipcode { get; set; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone number required")]
        public string PhoneNumber { get; set; }

        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage ="Must be valid E-Mail Address")]
        public string EmailAddress { get; set; }

        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Enter Password to save changes!")]
        public string ConfirmationPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmNewPassword { get; set; }
    }
}
