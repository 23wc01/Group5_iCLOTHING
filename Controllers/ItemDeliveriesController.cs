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
    public class ItemDeliveriesController : Controller
    {
        private Group5_iCLOTHINGDBEntities db = new Group5_iCLOTHINGDBEntities();

        // GET: ItemDeliveries
        public ActionResult Index()
        {
            var itemDelivery = db.ItemDelivery.Include(i => i.Customer).Include(i => i.Product);
            return View(itemDelivery.ToList());
        }

        // GET: ItemDeliveries/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemDelivery itemDelivery = db.ItemDelivery.Find(id);
            if (itemDelivery == null)
            {
                return HttpNotFound();
            }
            return View(itemDelivery);
        }

        // GET: ItemDeliveries/Create
        public ActionResult Create()
        {
            ViewBag.customerID = new SelectList(db.Customer, "customerID", "customerName");
            ViewBag.productID = new SelectList(db.Product, "productID", "productName");
            return View();
        }

        // POST: ItemDeliveries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "stickerID,stickerDate,productID,customerID")] ItemDelivery itemDelivery)
        {
            if (ModelState.IsValid)
            {
                db.ItemDelivery.Add(itemDelivery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.customerID = new SelectList(db.Customer, "customerID", "customerName", itemDelivery.customerID);
            ViewBag.productID = new SelectList(db.Product, "productID", "productName", itemDelivery.productID);
            return View(itemDelivery);
        }

        // GET: ItemDeliveries/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemDelivery itemDelivery = db.ItemDelivery.Find(id);
            if (itemDelivery == null)
            {
                return HttpNotFound();
            }
            ViewBag.customerID = new SelectList(db.Customer, "customerID", "customerName", itemDelivery.customerID);
            ViewBag.productID = new SelectList(db.Product, "productID", "productName", itemDelivery.productID);
            return View(itemDelivery);
        }

        // POST: ItemDeliveries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "stickerID,stickerDate,productID,customerID")] ItemDelivery itemDelivery)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemDelivery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customerID = new SelectList(db.Customer, "customerID", "customerName", itemDelivery.customerID);
            ViewBag.productID = new SelectList(db.Product, "productID", "productName", itemDelivery.productID);
            return View(itemDelivery);
        }

        // GET: ItemDeliveries/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemDelivery itemDelivery = db.ItemDelivery.Find(id);
            if (itemDelivery == null)
            {
                return HttpNotFound();
            }
            return View(itemDelivery);
        }

        // POST: ItemDeliveries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ItemDelivery itemDelivery = db.ItemDelivery.Find(id);
            db.ItemDelivery.Remove(itemDelivery);
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
