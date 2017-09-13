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
    public class DirectionController : Controller
    {
        private stockfaesdbEntities db = new stockfaesdbEntities();

        // GET: /Direction/
        public ActionResult Index(string sortOrder, string searchDirection, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Nom_direction" : "";


            if (searchDirection != null)
            {
                page = 1;
            }
            else
            {
                searchDirection = currentFilter;
            }

            ViewBag.CurrentFilter = searchDirection;

            var direction = from direct in db.TB_direction select direct;

            if (!String.IsNullOrEmpty(searchDirection))
            {
                direction = direction.Where(s => s.Nom_direction.Contains(searchDirection));
                // || s.FirstMidName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Nom_direction":
                    direction = direction.OrderByDescending(s => s.Nom_direction);
                    break;
                default:
                    direction = direction.OrderBy(s => s.Nom_direction);
                    break;
            }

            //return View(db.TB_categorie.ToList());
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(direction.ToPagedList(pageNumber, pageSize));
            //return View(categorie.ToList());
            //return View(db.TB_direction.ToList());
        }

        // GET: /Direction/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_direction tb_direction = db.TB_direction.Find(id);
            if (tb_direction == null)
            {
                return HttpNotFound();
            }
            return View(tb_direction);
        }

        // GET: /Direction/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Direction/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id_direction,Nom_direction,Description,DateCreer,CreerPar")] TB_direction tb_direction)
        {
            if (ModelState.IsValid)
            {
                tb_direction.DateCreer = DateTime.Now;
                tb_direction.CreerPar = "Admin".ToString();
                db.TB_direction.Add(tb_direction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_direction);
        }

        // GET: /Direction/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_direction tb_direction = db.TB_direction.Find(id);
            if (tb_direction == null)
            {
                return HttpNotFound();
            }
            return View(tb_direction);
        }

        // POST: /Direction/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id_direction,Nom_direction,Description,DateCreer,CreerPar")] TB_direction tb_direction)
        {
            if (ModelState.IsValid)
            {
                tb_direction.DateCreer = DateTime.Now;
                tb_direction.CreerPar = "Admin".ToString();
                db.Entry(tb_direction).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["message"] = string.Format("{0} Enregistrement reusit", tb_direction.Nom_direction);
                return RedirectToAction("Index");
            }
            return View(tb_direction);
        }

        // GET: /Direction/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_direction tb_direction = db.TB_direction.Find(id);
            if (tb_direction == null)
            {
                return HttpNotFound();
            }
            return View(tb_direction);
        }

        // POST: /Direction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TB_direction tb_direction = db.TB_direction.Find(id);
            db.TB_direction.Remove(tb_direction);
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
