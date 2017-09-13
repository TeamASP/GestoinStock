using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StockApp.Models;

namespace StockApp.Controllers
{
    public class EntrerStockController : Controller
    {
        private stockfaesdbEntities dc = new stockfaesdbEntities();
        //
        // GET: /EntrerStock/
        public ActionResult CreateEntrer()
        {
            return View();
        }

        public JsonResult getProductCategories()
        {
            List<TB_categorie> categories = new List<TB_categorie>();
            //using (stockfaesdbEntities dc = new stockfaesdbEntities())
            {
                categories = dc.TB_categorie.OrderBy(a => a.Nom_categorie).ToList();
            }
            return new JsonResult { Data = categories, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult getProducts(int categoryID)
        {
            List<TB_articles> products = new List<TB_articles>();
            //using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                products = dc.TB_articles.Where(a => a.Id_categorie.Equals(categoryID)).OrderBy(a => a.Nom_articles).ToList();
            }
            return new JsonResult { Data = products, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
	}
}