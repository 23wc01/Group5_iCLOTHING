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
    
    public partial class OrderStatus
    {
        public string statusID { get; set; }
        public string status { get; set; }
        public System.DateTime statusDate { get; set; }
        public string cartID { get; set; }
    
        public virtual ShoppingCart ShoppingCart { get; set; }
    }
}