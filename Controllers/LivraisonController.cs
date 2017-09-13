using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StockApp.Models;
using System.IO;
using System.Data.Entity.Infrastructure;
using PagedList;

namespace StockApp.Controllers
{
    public class LivraisonController : Controller
    {
        private stockfaesdbEntities db = new stockfaesdbEntities();

        // GET: /Livraison/
        public ActionResult Index(int page=1)
        {
            int recordsPerPage = 2;

            var list = db.TB_livraison.ToList().ToPagedList(page, recordsPerPage);
            return View(list);
            //return View(db.TB_livraison.ToList());
        }

        //public FileResult Download(string id)
        //{
        //    int _arquivoId = Convert.ToInt32(id);
        //    string contentType = "";
        //    var arquivos =(Server.MapPath("~/Upload"));
        //    string nomeArquivo = (from arquivo in db.TB_livraison
        //                          where arquivo.Id_livraison == _arquivoId
        //                          select arquivo.ImageUrl).FirstOrDefault();
        //    string extensao = Path.GetExtension(nomeArquivo);
        //    string nomeArquivoV = Path.GetFileNameWithoutExtension(nomeArquivo);
        //    if (extensao.Equals(".pdf"))
        //        contentType = "application/pdf";
        //    if (extensao.Equals(".doc"))
        //        contentType = "application/doc";
        //    if (extensao.Equals(".JPG") || extensao.Equals(".GIF") || extensao.Equals(".PNG"))
        //        contentType = "application/image";
        //    return File(nomeArquivo, contentType, nomeArquivoV + extensao);
        //}

        // GET: /Livraison/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_livraison tb_livraison = db.TB_livraison.Find(id);
            if (tb_livraison == null)
            {
                return HttpNotFound();
            }
            return View(tb_livraison);
        }

        // GET: /Livraison/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Livraison/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id_livraison,Code_fiche,Date,Image_fiche,Description,DateCreer,CreerPar")] HttpPostedFileBase file, TB_livraison tb_livraison)
        //public ActionResult Create(IEnumerable<HttpPostedFileBase> file, TB_livraison tb_livraison)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    if (file.ContentLength > 0)
                    {
                        string Upload = Server.MapPath("~/Upload");
                        if (!Directory.Exists(Upload))
                        {
                            Directory.CreateDirectory(Upload);
                        }
                        else
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var guid = Guid.NewGuid().ToString();
                            var path = Path.Combine(Server.MapPath("~/Upload"), guid + fileName);
                            file.SaveAs(path);
                            string fl = path.Substring(path.LastIndexOf("\\"));
                            string[] split = fl.Split('\\');
                            string newpath = split[1];
                            string imagepath = newpath;
                            tb_livraison.Image_fiche = fileName;
                            tb_livraison.ImageUrl = imagepath;
                            tb_livraison.CreerPar = "Admin".ToString();
                            tb_livraison.DateCreer = DateTime.Now;
                            db.TB_livraison.Add(tb_livraison);
                            db.SaveChanges();
                            //files.SaveAs(path);  
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        ViewBag.Message = "specifier le fichier";
                    }  
                    }
                }
           
            catch (Exception ex)
            {
                ViewBag.Message = "Error" + ex.Message.ToString();
            }
            return View(tb_livraison);
           
        }
        // GET: /Livraison/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_livraison tb_livraison = db.TB_livraison.Find(id);
            if (tb_livraison == null)
            {
                return HttpNotFound();
            }

            
            return View(tb_livraison);
        }

        // POST: /Livraison/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_livraison,Code_fiche,Date,Image_fiche,Description,DateCreer,CreerPar,ImgeUrl")] HttpPostedFileBase file, TB_livraison tb_livraison, bool Remenber = false)
        {
            try
            {
               if(Remenber==true)
               {
                if (ModelState.IsValid)
                {

                    
                        if (file.ContentLength>0)
                        {
                            
                                var fileName = Path.GetFileName(file.FileName);
                                var guid = Guid.NewGuid().ToString();
                                var path = Path.Combine(Server.MapPath("~/Upload"), guid + fileName);
                                file.SaveAs(path);
                                string fl = path.Substring(path.LastIndexOf("\\"));
                                string[] split = fl.Split('\\');
                                string newpath = split[1];
                                string imagepath = newpath;
                                tb_livraison.Image_fiche = fileName;
                                tb_livraison.ImageUrl = imagepath;
                                tb_livraison.DateCreer = DateTime.Now;
                                tb_livraison.CreerPar = "Admin".ToString();

                                db.Entry(tb_livraison).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                                return RedirectToAction("Index");
                     }
                        else
                        {
                            ViewBag.Message = "specifier le fichier";
                        }  
                }
                    }
               
                  
               else
                {

                    if (ModelState.IsValid)
                    {
                       
                        tb_livraison.DateCreer = DateTime.Now;
                        tb_livraison.CreerPar = "Admin".ToString();

                        db.Entry(tb_livraison).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");

                    }
                }
             }
           
           
            catch (Exception ex)
            {
                ViewBag.Message = "Erreur" + ex.Message.ToString();
            }
            return View(tb_livraison);
               
            }
  
      
        //================= methode download =================
        //public FileResult Download(String p, String d)
        //{
        //    try
        //    {
        //        string contentType = string.Empty;
        //        string dossier = Path.Combine(Server.MapPath("~/Upload/"), p);
        //        string extenssion = Path.GetExtension(dossier);

        //        if (extenssion.Equals(".pdf"))
        //            contentType = "application/pdf";
        //        if (extenssion.Equals(".doc"))
        //            contentType = "application/doc";
        //        if (extenssion.Equals(".JPG") || extenssion.Equals(".GIF") || extenssion.Equals(".PNG"))
        //            contentType = "application/image";



        //        return File(dossier, contentType, d);
        //        //return File(dossier, d);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Erreur"+ex);
        //    }
        //}

        public FileResult Download(string fileName)
        {
            var filepath = System.IO.Path.Combine(Server.MapPath("~/Upload/"), fileName);
            return File(filepath, MimeMapping.GetMimeMapping(filepath), fileName);
        }

        
        // GET: /Livraison/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_livraison tb_livraison = db.TB_livraison.Find(id);
            if (tb_livraison == null)
            {
                return HttpNotFound();
            }
            return View(tb_livraison);
        }

        // POST: /Livraison/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TB_livraison tb_livraison = db.TB_livraison.Find(id);

            String path = Path.Combine(Server.MapPath("~/Upload/"),tb_livraison.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            
            db.TB_livraison.Remove(tb_livraison);
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
