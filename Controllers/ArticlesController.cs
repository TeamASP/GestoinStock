using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StockApp.Models;
using System.Data.Entity.Infrastructure;
using StockApp.ViewModel;
using PagedList;



namespace StockApp.Controllers
{
    public class ArticlesController : Controller
    {
        //private ViewModel 
        private stockfaesdbEntities db = new stockfaesdbEntities();

        // GET: /Articles/
        public ActionResult Index(string sortOrder, string searchArticles, string currentFilter, int? page)
        {
           ViewBag.CurrentSort = sortOrder;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Nom_articles" : "";
            if (searchArticles != null)
            {
                page = 1;
            }
            else
            {
                searchArticles = currentFilter;
            }

            ViewBag.CurrentFilter = searchArticles;

            var articles = from article in db.TB_articles select article;

            if (!String.IsNullOrEmpty(searchArticles))
            {
                articles = articles.Where(s => s.Nom_articles.Contains(searchArticles));
                // || s.FirstMidName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Nom_articles":
                    articles = articles.OrderByDescending(s => s.Nom_articles);
                    break;
                default:
                    articles = articles.OrderBy(s => s.Nom_articles);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(articles.ToPagedList(pageNumber, pageSize));
            
           //return View(db.TB_articles.ToList());
           // return View(articl.ToList());
        }

        // GET: /Articles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_articles tb_articles = db.TB_articles.Find(id);
            if (tb_articles == null)
            {
                return HttpNotFound();
            }
            return View(tb_articles);
        }

        // GET: /Articles/Create
        public ActionResult Create()
        {
            PopulateCategorieDropDownList();
            //ViewBag.Id_categorie = new SelectList(db.TB_categorie, "Id_categorie", "Nom_categorie");

            return View();
        }

        // POST: /Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id_articles,Nom_articles,Qte_alerte,Description,Id_categorie,DateCreer,CreerPar")] TB_articles tb_articles)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    tb_articles.DateCreer = DateTime.Now;
                    tb_articles.CreerPar = "Admin".ToString();
                    db.TB_articles.Add(tb_articles);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            catch (RetryLimitExceededException /* dex */)
            { //Log the error (uncomment dex variable name and add a line here to write a log.) 
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateCategorieDropDownList(tb_articles.Id_categorie);
            //ViewBag.Id_categorie = new SelectList(db.TB_categorie, "Id_categorie", "Nom_categorie", tb_articles.Id_categorie);

            return View(tb_articles);
        }

        // GET: /Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_articles tb_articles = db.TB_articles.Find(id);
            if (tb_articles == null)
            {
                return HttpNotFound();
            }
            //ViewBag.Id_categorie = new SelectList(db.TB_categorie, "Id_categorie", "Nom_categorie", tb_articles.Id_categorie);
            PopulateCategorieDropDownList(tb_articles.Id_categorie);

            return View(tb_articles);
        }

        // POST: /Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id_articles,Nom_articles,Qte_alerte,Description,Id_categorie,DateCreer,CreerPar")] TB_articles tb_articles)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    tb_articles.DateCreer = DateTime.Now;
                    tb_articles.CreerPar = "Admin".ToString();
                    db.Entry(tb_articles).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            { //Log the error (uncomment dex variable name and add a line here to write a log.) 
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateCategorieDropDownList(tb_articles.Id_categorie);

            return View(tb_articles);
        }

        private void PopulateCategorieDropDownList(object selectedCategorie = null)
        {
            var CategorieQuery = from d in db.TB_categorie orderby d.Nom_categorie select d;
            ViewBag.Id_categorie = new SelectList(CategorieQuery, "Id_categorie", "Nom_categorie", selectedCategorie);
        }


        // GET: /Articles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_articles tb_articles = db.TB_articles.Find(id);
            if (tb_articles == null)
            {
                return HttpNotFound();
            }
            return View(tb_articles);
        }

        // POST: /Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            TB_articles tb_articles = db.TB_articles.Find(id);
            db.TB_articles.Remove(tb_articles);
            db.SaveChanges();
            return RedirectToAction("Index");
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
