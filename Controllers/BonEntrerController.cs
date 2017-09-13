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
    public class BonEntrerController : Controller
    {
        private stockfaesdbEntities db = new stockfaesdbEntities();

        public ActionResult Create() 
        {
            PopulateLivraisonDropDownList();

           // PopulateCategorieDropDownList1();

            ViewBag.Livraison = db.TB_livraison.ToList();
            ViewBag.Categorie = db.TB_categorie.ToList();
            ViewBag.Articles = new SelectList(db.TB_articles
                            .Where(c => c.Id_articles == 0), "Id_articles", "Nom_articles").ToList();
            //ViewBag.City = new SelectList(entities.Cities
            //                .Where(c => c.ID == 0), "ID", "CityName").ToList();

            return View();
        }

        private void PopulateLivraisonDropDownList(object selectedlivraison = null)
        {
            var LivraisonQuery = from d in db.TB_livraison orderby d.Code_fiche select d;
            ViewBag.Id_livraison = new SelectList(LivraisonQuery, "Id_livraison", "Code_fiche", selectedlivraison);
        }

        private void PopulateCategorieDropDownList1(object selectedlivraison = null)
        {
            var LivraisonQuery = from d in db.TB_categorie orderby d.Nom_categorie select d;
            ViewBag.Id_categorie = new SelectList(LivraisonQuery, "Id_categorie", "Nom_categorie", selectedlivraison);
        }


        public ActionResult FillCity(int state)
        {
            var cities = db.TB_articles.Where(c => c.Id_categorie == state);
            return Json(cities, JsonRequestBehavior.AllowGet);
        }
        // GET: /BonEntrer/
        public ActionResult Index()
        {
            var tb_bonentre = db.TB_bonEntre.Include(t => t.TB_livraison);
            return View(tb_bonentre.ToList());

        }

        
        // GET: /BonEntrer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_bonEntre tb_bonentre = db.TB_bonEntre.Find(id);
            if (tb_bonentre == null)
            {
                return HttpNotFound();
            }
            return View(tb_bonentre);

            //return this.RedirectToAction("LotEntrer/Details", "LotEntrerController");
        }



        //// GET: /BonEntrer/Create
        //public ActionResult Create()
        //{
        ////    ViewBag.Id_livraison = new SelectList(db.TB_livraison, "Id_livraison", "Code_fiche");
        ////    return View();

        //PopulateLivraisonDropDownList();

        //   // PopulateCategorieDropDownList1();

        //    ViewBag.Livraison = db.TB_livraison.ToList();
        //    ViewBag.Categorie = db.TB_categorie.ToList();
        //    ViewBag.Articles = new SelectList(db.TB_articles
        //                    .Where(c => c.Id_articles == 0), "Id_articles", "Nom_articles").ToList();
        //    //ViewBag.City = new SelectList(entities.Cities
        //    //                .Where(c => c.ID == 0), "ID", "CityName").ToList();

        //    return View();
        //}

        //// POST: /BonEntrer/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id_bon_entrestock,Date_entre,Description,Id_livraison,DateCreer,CreerPar")] TB_bonEntre tb_bonentre)
        public JsonResult Save(TB_bonEntre tb_bonentre)
    {

        try
        {
            if (ModelState.IsValid)
            {

                // If sales main has SalesID then we can understand we have existing sales Information
                // So we need to Perform Update Operation

                // Perform Update
                //if (tb_bonentre.Id_bon_entrestock > 0)
                //{

                //    var CurrentsalesSUb = db.TB_lot_entrestock.Where(p => p.Id_bon_entrestock == tb_bonentre.Id_bon_entrestock);

                //    foreach (TB_lot_entrestock ss in CurrentsalesSUb)
                //        db.TB_lot_entrestock.Remove(ss);

                //    foreach (TB_lot_entrestock ss in tb_bonentre.TB_lot_entrestock)
                //        db.TB_lot_entrestock.Add(ss);

                //    db.Entry(tb_bonentre).State = System.Data.Entity.EntityState.Modified;
                //}
                //Perform Save
                //else
                //{
                    db.TB_bonEntre.Add(tb_bonentre);
                //}

                db.SaveChanges();

                // If Sucess== 1 then Save/Update Successfull else there it has Exception
                return Json(new { Success = 1, BonentrerID = tb_bonentre.Id_bon_entrestock, ex = "" });
                
               
            }
        }
        catch (Exception ex)
        {
            // If Sucess== 0 then Unable to perform Save/Update Operation and send Exception to View as JSON
            return Json(new { Success = 0, ex = ex.Message.ToString() });
        }

        return Json(new { Success = 0, ex = new Exception("les informations ne sont enregistrer").Message.ToString() });
            //if (ModelState.IsValid)
            //{
            //    db.TB_bonEntre.Add(tb_bonentre);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.Id_livraison = new SelectList(db.TB_livraison, "Id_livraison", "Code_fiche", tb_bonentre.Id_livraison);
            //return View(tb_bonentre);
        }

        //public ActionResult Edit(int id)
        //{
        //    ViewBag.Title = "Edit";
        //    TB_bonEntre salesmain = db.TB_bonEntre.Find(id);

        //    //Call Create View
        //    return View("Create", salesmain);
        //}

        // GET: /BonEntrer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_bonEntre tb_bonentre = db.TB_bonEntre.Find(id);
            if (tb_bonentre == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_livraison = new SelectList(db.TB_livraison, "Id_livraison", "Code_fiche", tb_bonentre.Id_livraison);
            return View(tb_bonentre);
        }

        // POST: /BonEntrer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_bon_entrestock,Date_entre,Description,Id_livraison,DateCreer,CreerPar")] TB_bonEntre tb_bonentre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_bonentre).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_livraison = new SelectList(db.TB_livraison, "Id_livraison", "Code_fiche", tb_bonentre.Id_livraison);
            return View(tb_bonentre);
        }

        //// GET: /BonEntrer/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TB_bonEntre tb_bonentre = db.TB_bonEntre.Find(id);
        //    if (tb_bonentre == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tb_bonentre);
        //}

        //// POST: /BonEntrer/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    TB_bonEntre tb_bonentre = db.TB_bonEntre.Find(id);
        //    db.TB_bonEntre.Remove(tb_bonentre);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        //------------------------------------------------------------------------



        public JsonResult LoadArticles(string CategorieID, string TempData)
        {
            if (HttpContext.Request.IsAjaxRequest() && CategorieID != "")
            {
                int CategID = Convert.ToInt32(CategorieID);
                var StatesData = db.TB_articles
                                 .Where(c => c.Id_categorie == CategID).ToList()
                                 .Select(m => new SelectListItem()
                                 {
                                     Text = m.Nom_articles,
                                     Value = m.Id_articles.ToString()
                                 });
                return Json(StatesData, JsonRequestBehavior.AllowGet);
            }
            return null;
        }


        public JsonResult Loadcategorie()
        {
            //if (HttpContext.Request.IsAjaxRequest() && categID != "")
           // {
               // int CatID = Convert.ToInt32(categID);
               // CatID=0;
                var CategorieData = db.TB_categorie
                                 //.Where(c => c.Id_categorie != CatID && c.Id_categorie > 0).ToList()
                                 .Select(m => new SelectListItem()
                                 {
                                     Text = m.Nom_categorie,
                                     Value = m.Id_categorie.ToString()
                                 });
                return Json(CategorieData, JsonRequestBehavior.AllowGet);
            //}
            //return null;
        }

       

        //[AcceptVerbs(HttpVerbs.Get)]
        //public JsonResult LoadCitiesByStateID(string StateID)
        //{
        //    if (HttpContext.Request.IsAjaxRequest() && StateID != "")
        //    {
        //        int StID = Convert.ToInt32(StateID);
        //        var CitisData = entities.Cities
        //                        .Where(m => m.StateID == StID).ToList()
        //                        .Select(c => new SelectListItem()
        //                        {
        //                            Text = c.CityName.ToString(),
        //                            Value = c.ID.ToString(),
        //                        });
        //        return Json(CitisData, JsonRequestBehavior.AllowGet);
        //    }
        //    return null;
        //}


        public JsonResult GetCountries()
        {

            //using (MyDatabaseEntities context = new MyDatabaseEntities())
            {
                var ret = db.TB_categorie.Select(x => new { x.Id_categorie, x.Nom_categorie }).ToList();
                return Json(ret, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult GetStates(int countryId)
        {
            //using (MyDatabaseEntities context = new MyDatabaseEntities())
            {
                var ret = db.TB_articles.Where(x => x.Id_categorie == countryId).Select(x => new { x.Id_articles, x.Nom_articles }).ToList();
                return Json(ret);
            }
        }
      

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

