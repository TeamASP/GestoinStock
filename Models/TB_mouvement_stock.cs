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
    
    public partial class TB_mouvement_stock
    {
        public int Id_mouvement { get; set; }
        public Nullable<System.DateTime> DateCreer { get; set; }
        public Nullable<int> Type_mouvement { get; set; }
        public Nullable<int> Quantite { get; set; }
        public string CreerPar { get; set; }
        public Nullable<int> Id_article { get; set; }
    }
}
