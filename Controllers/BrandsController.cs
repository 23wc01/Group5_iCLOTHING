using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Group5__iCLOTHINGApp.Models;

namespace Group5__iCLOTHINGApp.Controllers
{
    public class BrandsController : Controller
    {
        private Group5_iCLOTHINGDBEntities db = new Group5_iCLOTHINGDBEntities();

        // GET: Brands
        public ActionResult Index(string searchString)
        {
            var brand = db.Brand.Include(b => b.Product);
            return View(db.Brand.Where(x => x.brandName.Contains(searchString) || searchString == null).ToList());
        }

        // GET: Brands/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brand.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // GET: Brands/Create
        public ActionResult Create()
        {
            ViewBag.brandID = new SelectList(db.Product, "productID", "productName");
            return View();
        }

        // POST: Brands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "brandID,brandName,brandDescription")] Brand brand)
        {
            if (ModelState.IsValid)
            {
                db.Brand.Add(brand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.brandID = new SelectList(db.Product, "productID", "productName", brand.brandID);
            return View(brand);
        }

        // GET: Brands/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brand.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            ViewBag.brandID = new SelectList(db.Product, "productID", "productName", brand.brandID);
            return View(brand);
        }

        // POST: Brands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "brandID,brandName,brandDescription")] Brand brand)
        {
            if (ModelState.IsValid)
            {
                db.Entry(brand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.brandID = new SelectList(db.Product, "productID", "productName", brand.brandID);
            return View(brand);
        }

        // GET: Brands/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brand.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Brand brand = db.Brand.Find(id);
            db.Brand.Remove(brand);
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
