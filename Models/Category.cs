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

    public partial class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            this.Cataloging = new HashSet<Cataloging>();
            this.Product = new HashSet<Product>();
        }
        [StringLength(10, MinimumLength = 10, ErrorMessage = "ID must be 10 characters.")]
        [Required(ErrorMessage = "ID required")]
        [Display(Name = "ID")]
        [Index(IsUnique = true)]
        public string categoryID { get; set; }

        [StringLength(50, ErrorMessage = "Max length of name is 50 characters.")]
        [Required(ErrorMessage = "Name required")]
        [Display(Name = "Name")]
        public string categoryName { get; set; }

        [Display(Name = "Description")]
        [StringLength(int.MaxValue, MinimumLength = 1, ErrorMessage = "Description can't be empty")]
        public string categoryDescription { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cataloging> Cataloging { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Product { get; set; }
    }
}