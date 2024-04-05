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

    public partial class UserPassword
    {
        public string userID { get; set; }

        [StringLength(30, ErrorMessage = "Max length of username is 30 characters.")]
        [Required(ErrorMessage = "Username required")]
        [Display(Name = "Username")]
        [Index(IsUnique = true)]

        public string userAccountName { get; set; }

        [StringLength(30, ErrorMessage = "Max length of password is 30 characters.")]
        [Required(ErrorMessage = "Password required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string userEncryptedPassword { get; set; }

        public int passwordExpiryTime { get; set; }

        [Display(Name = "Account Expire date")]
        [DataType(DataType.Date)]
        public System.DateTime userAccountExpiryDate { get; set; }

        [Display(Name = "Administrator")]
        public bool isAdmin { get; set; }
    
        public virtual Customer Customer { get; set; }
    }
}