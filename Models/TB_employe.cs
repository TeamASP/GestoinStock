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
    
    public partial class TB_employe
    {
        public int Id_employe { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public Nullable<int> Id_direction { get; set; }
        public Nullable<System.DateTime> DateCreer { get; set; }
        public string CreerPar { get; set; }
        public string Description { get; set; }
    
        public virtual TB_direction TB_direction { get; set; }
    }
}
