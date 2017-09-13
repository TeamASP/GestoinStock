using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StockApp.Models;

namespace StockApp.ViewModel
{
    public class ViewModel
    {
        public IEnumerable<TB_articles> Articles { get; set; }
        public IEnumerable<TB_categorie> Categories { get; set; }
    }
}