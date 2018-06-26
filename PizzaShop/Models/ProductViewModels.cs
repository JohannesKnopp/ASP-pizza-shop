using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PizzaShop.Models
{
    public class CartViewModel
    {
        public CartViewModel() { }

        public int UniqueID { get; set; }
        public int ProductID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int Tax { get; set; }

        public virtual ICollection<Product> Toppings { get; set; }

        public decimal FullPrice
        {
            get
            {
                decimal priceToppings = 0;
                foreach (Product p in Toppings)
                {
                    priceToppings += p.Price;
                }
                return (Price + priceToppings) * Quantity;
            }
        }
    }

    public class EditProductViewModel
    {
        public EditProductViewModel() { }
        public EditProductViewModel(List<Category> categories, List<Allergen> allergens)
        {
            this.SelectedCategoryID = categories.First().ID;
            this._categories = categories;
            this.Allergens = allergens;
        }
        public EditProductViewModel(Product product, List<Category> categories, List<Allergen> allergens)
        {
            this.ID = product.ID;
            this.Name = product.Name;
            this.Price = product.Price;
            this.IsInSortiment = product.IsInSortiment;
            this.Allergens = allergens;
            this.SelectedCategoryID = product.CategoryID;
            this._categories = categories;
        }

        public int ID { get; set; }
        [Required]
        [Display(Name ="Name")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        [Required]
        [Display(Name = "Is in Sortiment?")]
        public bool IsInSortiment { get; set; }
        [Display(Name = "Allergens")]
        public virtual ICollection<ProductHasAllergen> ProductHasAllergens { get; set; }

        public List<Allergen> Allergens;

        private List<Category> _categories;
        [Display(Name = "Category")]
        public int SelectedCategoryID { get; set; }
        public IEnumerable<SelectListItem> Categories
        {
            get { return new SelectList(_categories, "ID", "Name"); }
        }

    }

}