//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Group5__iCLOTHINGApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.ItemDelivery = new HashSet<ItemDelivery>();
            this.Cataloging = new HashSet<Cataloging>();
        }
        [StringLength(10, MinimumLength = 10, ErrorMessage = "ID must be 10 characters.")]
        [Required(ErrorMessage = "ID required")]
        [Display(Name = "ID")]
        [Index(IsUnique = true)]
        public string productID { get; set; }

        [Required(ErrorMessage = "Name required")]
        [Display(Name = "Name")]
        public string productName { get; set; }

        [Display(Name = "Description")]
        public string productDescription { get; set; }

        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal productPrice { get; set; }

        [Display(Name = "Quantity")]
        public int productQty { get; set; }
        public string categoryID { get; set; }
        public string cartID { get; set; }
    
        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ItemDelivery> ItemDelivery { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cataloging> Cataloging { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
    }
}
