//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StockApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TB_user
    {
        public long ID_User { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Login { get; set; }
        public string Motdepasse { get; set; }
        public string Role { get; set; }
        public Nullable<System.DateTime> DateCreer { get; set; }
        public string CreerPar { get; set; }
    }
}
