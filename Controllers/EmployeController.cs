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
using Microsoft.AspNet.Identity;

namespace StockApp.Views
{
    public class EmployeController : Controller
    {
        private stockfaesdbEntities db = new stockfaesdbEntities();

        // GET: /Employe/
        public ActionResult Index(string sortOrder, string searchEmploye, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Nom" : "";
            if (searchEmploye != null)
            {
                page = 1;
            }
            else
            {
                searchEmploye = currentFilter;
            }

            ViewBag.CurrentFilter = searchEmploye;


             // var articles = from article in db.TB_articles select article;
            var tb_employe = db.TB_employe.Include(t => t.TB_direction);

            if (!String.IsNullOrEmpty(searchEmploye))
            {
                tb_employe = tb_employe.Where(s => s.Nom.Contains(searchEmploye));
                // || s.FirstMidName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Nom":
                    tb_employe = tb_employe.OrderByDescending(s => s.Nom);
                    break;
                default:
                    tb_employe = tb_employe.OrderBy(s => s.Nom);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(tb_employe.ToPagedList(pageNumber, pageSize));
            //var tb_employe = db.TB_employe.Include(t => t.TB_direction);
            //return View(tb_employe.ToList());
        }

        // GET: /Employe/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_employe tb_employe = db.TB_employe.Find(id);
            if (tb_employe == null)
            {
                return HttpNotFound();
            }
            return View(tb_employe);
        }

        // GET: /Employe/Create
        //[StockApp.CustomFilters.AuthLog(Roles = "Utilisateur")]
        public ActionResult Create()
        {
            ViewBag.Id_direction = new SelectList(db.TB_direction, "Id_direction", "Nom_direction");
            return View();
        }

        // POST: /Employe/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id_employe,Nom,Prenom,Email,Telephone,Description,Id_direction,DateCreer,CreerPar")] TB_employe tb_employe)
        {
            if (ModelState.IsValid)
            {
                tb_employe.DateCreer = DateTime.Now;
                tb_employe.CreerPar = User.Identity.GetUserName(); //"Admin";
                db.TB_employe.Add(tb_employe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_direction = new SelectList(db.TB_direction, "Id_direction", "Nom_direction", tb_employe.Id_direction);
            return View(tb_employe);
        }

        // GET: /Employe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_employe tb_employe = db.TB_employe.Find(id);
            if (tb_employe == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_direction = new SelectList(db.TB_direction, "Id_direction", "Nom_direction", tb_employe.Id_direction);
            return View(tb_employe);
        }

        // POST: /Employe/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id_employe,Nom,Prenom,Email,Telephone,Description,Id_direction,DateCreer,CreerPar")] TB_employe tb_employe)
        {
            if (ModelState.IsValid)
            {
                tb_employe.DateCreer = DateTime.Now;
                tb_employe.CreerPar = "Admin";
                db.Entry(tb_employe).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_direction = new SelectList(db.TB_direction, "Id_direction", "Nom_direction", tb_employe.Id_direction);
            return View(tb_employe);
        }

        // GET: /Employe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_employe tb_employe = db.TB_employe.Find(id);
            if (tb_employe == null)
            {
                return HttpNotFound();
            }
            return View(tb_employe);
        }

        // POST: /Employe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TB_employe tb_employe = db.TB_employe.Find(id);
            db.TB_employe.Remove(tb_employe);
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
