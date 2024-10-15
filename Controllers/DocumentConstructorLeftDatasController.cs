using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class DocumentConstructorLeftDatasController : Controller
    {
        private DocumentConstructorContext db = new DocumentConstructorContext();

        // GET: DocumentConstructorLeftDatas
        public ActionResult Index()
        {
            var documentConstructorLeftDatas = db.DocumentConstructorLeftDatas.Include(d => d.DocumentConstructor);
            return View(documentConstructorLeftDatas.ToList());
        }

        // GET: DocumentConstructorLeftDatas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentConstructorLeftData documentConstructorLeftData = db.DocumentConstructorLeftDatas.Find(id);
            if (documentConstructorLeftData == null)
            {
                return HttpNotFound();
            }
            return View(documentConstructorLeftData);
        }

        // GET: DocumentConstructorLeftDatas/Create
        public ActionResult Create()
        {
            ViewBag.DocumentConstructorId = new SelectList(db.DocumentConstructors, "DocumentConstructorId", "Header");
            return View();
        }

        // POST: DocumentConstructorLeftDatas/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "DocumentConstructorLeftDataId,Title,Npp,SizeTitle,DocumentConstructorId")] DocumentConstructorLeftData documentConstructorLeftData)
        {
            if (ModelState.IsValid)
            {
                var maxNpp = db.DocumentConstructorLeftDatas.Max(i => (int?)i.Npp) ?? 0;
                documentConstructorLeftData.Npp = maxNpp + 1;
                documentConstructorLeftData.DocumentConstructorId = 4;
                db.DocumentConstructorLeftDatas.Add(documentConstructorLeftData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DocumentConstructorId = new SelectList(db.DocumentConstructors, "DocumentConstructorId", "Header", documentConstructorLeftData.DocumentConstructorId);
            return View(documentConstructorLeftData);
        }

        // GET: DocumentConstructorLeftDatas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentConstructorLeftData documentConstructorLeftData = db.DocumentConstructorLeftDatas.Find(id);
            if (documentConstructorLeftData == null)
            {
                return HttpNotFound();
            }
            ViewBag.DocumentConstructorId = new SelectList(db.DocumentConstructors, "DocumentConstructorId", "Header", documentConstructorLeftData.DocumentConstructorId);
            return View(documentConstructorLeftData);
        }

        // POST: DocumentConstructorLeftDatas/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        public ActionResult Edit([Bind(Include = "DocumentConstructorLeftDataId,Title,Npp,SizeTitle,DocumentConstructorId")] DocumentConstructorLeftData documentConstructorLeftData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(documentConstructorLeftData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DocumentConstructorId = new SelectList(db.DocumentConstructors, "DocumentConstructorId", "Header", documentConstructorLeftData.DocumentConstructorId);
            return View(documentConstructorLeftData);
        }

        // GET: DocumentConstructorLeftDatas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentConstructorLeftData documentConstructorLeftData = db.DocumentConstructorLeftDatas.Find(id);
            if (documentConstructorLeftData == null)
            {
                return HttpNotFound();
            }
            return View(documentConstructorLeftData);
        }

        // POST: DocumentConstructorLeftDatas/Delete/5
        [HttpPost, ActionName("Delete")]
    
        public ActionResult DeleteConfirmed(int id)
        {
            
            DocumentConstructorLeftData documentConstructorLeftData = db.DocumentConstructorLeftDatas.Find(id);
            var itemsToUpdate = db.DocumentConstructorLeftDatas.Where(i => i.Npp > documentConstructorLeftData.Npp).ToList();
            foreach (var item in itemsToUpdate)
            {
                item.Npp--;
            }

            db.DocumentConstructorLeftDatas.Remove(documentConstructorLeftData);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ChangeNppUp(int id)
        {

            // У NPP уникальные значения так как при создании нового элемента npp = maxNpp + 1;
            // Меняем Npp у элементов

            var item = db.DocumentConstructorLeftDatas.Find(id);

            var previousItem = db.DocumentConstructorLeftDatas
                .Where(i => i.Npp < item.Npp)
                .OrderByDescending(i => i.Npp)
                .FirstOrDefault();

            if (previousItem != null && previousItem.Npp < item.Npp)
            {

                // Меняем местами Npp
                int tempNpp = item.Npp;
                item.Npp = previousItem.Npp;
                previousItem.Npp = tempNpp;

                db.SaveChanges();
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ChangeNppDown(int id)
        {
            // У NPP уникальные значения так как при создании нового элемента npp = maxNpp + 1;
            // Меняем Npp у элементов

            var item = db.DocumentConstructorLeftDatas.Find(id);

            var previousItem = db.DocumentConstructorLeftDatas
                .Where(i => i.Npp > item.Npp)
                .OrderBy(i => i.Npp)
                .FirstOrDefault();

            if (previousItem != null && previousItem.Npp > item.Npp)
            {

                // Меняем местами Npp
                int tempNpp = item.Npp;
                item.Npp = previousItem.Npp;
                previousItem.Npp = tempNpp;

                db.SaveChanges();
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet, ActionName("ChangeSizeTitle")]
        public ActionResult ChangeSizeTitle(int id, int changeNumber)
        {

            var item = db.DocumentConstructorLeftDatas.Find(id);
            if ((item.SizeTitle + changeNumber) > 6 || (item.SizeTitle + changeNumber) < 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                item.SizeTitle += changeNumber;
                db.SaveChanges();
                return RedirectToAction("Index");
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
