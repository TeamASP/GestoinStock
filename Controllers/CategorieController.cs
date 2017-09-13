using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StockApp.Models;
using PagedList;


namespace StockApp.Controllers
{
    public class CategorieController : Controller
    {
        private stockfaesdbEntities db = new stockfaesdbEntities();

        // GET: /Categorie/
        public ActionResult Index(string sortOrder, string searchCategorie, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Nom_categorie" : "";


            if (searchCategorie != null)
            {
                page = 1;
            }
            else
            {
                searchCategorie = currentFilter;
            }

            ViewBag.CurrentFilter = searchCategorie;

            var categorie = from cate in db.TB_categorie select cate;

            if (!String.IsNullOrEmpty(searchCategorie))
            {
                categorie = categorie.Where(s => s.Nom_categorie.Contains(searchCategorie));
                // || s.FirstMidName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Nom_categorie":
                    categorie = categorie.OrderByDescending(s => s.Nom_categorie);
                    break;
                default:
                    categorie = categorie.OrderBy(s => s.Nom_categorie);
                    break;
            }

            //return View(db.TB_categorie.ToList());
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(categorie.ToPagedList(pageNumber, pageSize));
            //return View(categorie.ToList());
        }

        // GET: /Categorie/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_categorie tb_categorie = db.TB_categorie.Find(id);
            if (tb_categorie == null)
            {
                return HttpNotFound();
            }
            return View(tb_categorie);
        }

        // GET: /Categorie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Categorie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id_categorie,Nom_categorie,Description,DateCreer,CreerPar")] TB_categorie tb_categorie)
        {
            if (ModelState.IsValid)
            {
                tb_categorie.DateCreer = DateTime.Now;
                tb_categorie.CreerPar = "Admin".ToString();
                db.TB_categorie.Add(tb_categorie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_categorie);
        }

        // GET: /Categorie/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_categorie tb_categorie = db.TB_categorie.Find(id);
            if (tb_categorie == null)
            {
                return HttpNotFound();
            }
            return View(tb_categorie);
        }

        // POST: /Categorie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id_categorie,Nom_categorie,Description,DateCreer,CreerPar")] TB_categorie tb_categorie)
        {
            if (ModelState.IsValid)
            {
                tb_categorie.DateCreer = DateTime.Now;
                tb_categorie.CreerPar = "Admin".ToString();
                db.Entry(tb_categorie).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["message"]=string.Format("{0} Enregistrement reusit",tb_categorie.Nom_categorie);
                return RedirectToAction("Index");
            }
            return View(tb_categorie);
        }

        // GET: /Categorie/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_categorie tb_categorie = db.TB_categorie.Find(id);
            if (tb_categorie == null)
            {
                return HttpNotFound();
            }
            return View(tb_categorie);
        }

        // POST: /Categorie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //TB_categorie tb_categorie = db.TB_categorie.Include(i => i.TB_articles).Where(i => i.Id_categorie == id).Single();
            //tb_categorie.TB_articles = null;
            //db.TB_categorie.Remove(tb_categorie);
            //var article = db.TB_articles.Where(d => d.Id_categorie == id).SingleOrDefault(); 
            //if (article != null)
            //{
            //    article.Id_categorie = null;
            //}

           
            TB_categorie tb_categorie = db.TB_categorie.Find(id);
            db.TB_categorie.Remove(tb_categorie);
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
