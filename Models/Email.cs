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
    
    public partial class Email
    {
        public string emailNo { get; set; }
        public System.DateTime emailDate { get; set; }
        public string emailSubject { get; set; }
        public string emailBody { get; set; }
        public string adminID { get; set; }
        public string customerID { get; set; }
    
        public virtual Administrator Administrator { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
