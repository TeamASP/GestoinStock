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
    public class ControllerLotEntrer : Controller
    {
        private stockfaesdbEntities db = new stockfaesdbEntities();

        // GET: /ControllerLotEntrer/
        public ActionResult Index()
        {
            var tb_lot_entrestock = db.TB_lot_entrestock.Include(t => t.TB_articles).Include(t => t.TB_bonEntre);
            return View(tb_lot_entrestock.ToList());
        }

        // GET: /ControllerLotEntrer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_lot_entrestock tb_lot_entrestock = db.TB_lot_entrestock.Find(id);
            if (tb_lot_entrestock == null)
            {
                return HttpNotFound();
            }
            return View(tb_lot_entrestock);
        }

        // GET: /ControllerLotEntrer/Create
        public ActionResult Create()
        {
            ViewBag.Id_articles = new SelectList(db.TB_articles, "Id_articles", "Nom_articles");
            ViewBag.Id_bon_entrestock = new SelectList(db.TB_bonEntre, "Id_bon_entrestock", "Description");
            return View();
        }

        // POST: /ControllerLotEntrer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id_lot_entrestock,Date_entrer,Id_articles,Id_bon_entrestock,Quantite")] TB_lot_entrestock tb_lot_entrestock)
        {
            if (ModelState.IsValid)
            {
                db.TB_lot_entrestock.Add(tb_lot_entrestock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_articles = new SelectList(db.TB_articles, "Id_articles", "Nom_articles", tb_lot_entrestock.Id_articles);
            ViewBag.Id_bon_entrestock = new SelectList(db.TB_bonEntre, "Id_bon_entrestock", "Description", tb_lot_entrestock.Id_bon_entrestock);
            return View(tb_lot_entrestock);
        }

        // GET: /ControllerLotEntrer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_lot_entrestock tb_lot_entrestock = db.TB_lot_entrestock.Find(id);
            if (tb_lot_entrestock == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_articles = new SelectList(db.TB_articles, "Id_articles", "Nom_articles", tb_lot_entrestock.Id_articles);
            ViewBag.Id_bon_entrestock = new SelectList(db.TB_bonEntre, "Id_bon_entrestock", "Description", tb_lot_entrestock.Id_bon_entrestock);
            return View(tb_lot_entrestock);
        }

        // POST: /ControllerLotEntrer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id_lot_entrestock,Date_entrer,Id_articles,Id_bon_entrestock,Quantite")] TB_lot_entrestock tb_lot_entrestock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_lot_entrestock).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_articles = new SelectList(db.TB_articles, "Id_articles", "Nom_articles", tb_lot_entrestock.Id_articles);
            ViewBag.Id_bon_entrestock = new SelectList(db.TB_bonEntre, "Id_bon_entrestock", "Description", tb_lot_entrestock.Id_bon_entrestock);
            return View(tb_lot_entrestock);
        }

        // GET: /ControllerLotEntrer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_lot_entrestock tb_lot_entrestock = db.TB_lot_entrestock.Find(id);
            if (tb_lot_entrestock == null)
            {
                return HttpNotFound();
            }
            return View(tb_lot_entrestock);
        }

        // POST: /ControllerLotEntrer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TB_lot_entrestock tb_lot_entrestock = db.TB_lot_entrestock.Find(id);
            db.TB_lot_entrestock.Remove(tb_lot_entrestock);
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
