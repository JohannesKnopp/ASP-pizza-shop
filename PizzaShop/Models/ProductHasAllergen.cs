//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PizzaShop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductHasAllergen
    {
        public int ProductID { get; set; }
        public int AllergenID { get; set; }
        public Nullable<System.DateTime> LastChanged { get; set; }
    
        public virtual Allergen Allergen { get; set; }
        public virtual Product Product { get; set; }
    }
}
