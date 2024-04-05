using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Group5__iCLOTHINGApp.Models;

namespace Group5__iCLOTHINGApp.Controllers
{
    public class UserQueriesController : Controller
    {
        private Group5_iCLOTHINGDBEntities db = new Group5_iCLOTHINGDBEntities();

        // GET: UserQueries
        public ActionResult Index()
        {
            var userQuery = db.UserQuery.Include(u => u.Customer);
            return View(userQuery.ToList());
        }

        // GET: UserQueries/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserQuery userQuery = db.UserQuery.Find(id);
            if (userQuery == null)
            {
                return HttpNotFound();
            }
            return View(userQuery);
        }

        // GET: UserQueries/Create
        public ActionResult Create()
        {
            ViewBag.customerID = new SelectList(db.Customer, "customerID", "customerName");
            UserQuery model = new UserQuery();
            model.queryDate = DateTime.Now;
            return View(model);
        }
        // POST: UserQueries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "queryNo,queryDate,queryDescription,customerID")] UserQuery userQuery)
        {
            if (ModelState.IsValid)
            {
                if (db.UserQuery.Any(x => x.queryNo == userQuery.queryNo))
                {
                    ViewBag.Notifications = "Query # already exists";
                    return View("Create");
                }
                else
                {
                    userQuery.queryDate = DateTime.Now;
                    db.UserQuery.Add(userQuery);
                    db.SaveChanges();
                    Session["customerIDSS"] = userQuery.customerID.ToString();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Notifications = "Invalid input. Ensure Query # isn't taken";
            return RedirectToAction("Create");
        }
        // GET: ShoppingCarts/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserQuery userQuery = db.UserQuery.Find(id);
            if (userQuery == null)
            {
                return HttpNotFound();
            }
            ViewBag.customerID = new SelectList(db.Customer, "customerID", "customerName", userQuery.customerID);
            return View(userQuery);
        }

        // POST: UserQuery/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "queryNo,queryDate,queryDescription,customerID")] UserQuery userQuery)
        {
            if (ModelState.IsValid)
            {
                userQuery.queryDate = DateTime.Now;
                db.Entry(userQuery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Notifications = "Invalid input";
            ViewBag.customerID = new SelectList(db.Customer, "customerID", "customerName", userQuery.customerID);
            return View(userQuery);
        }

        // GET: UserQueries/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserQuery userQuery = db.UserQuery.Find(id);
            if (userQuery == null)
            {
                return HttpNotFound();
            }
            return View(userQuery);
        }

        // POST: UserQueries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UserQuery userQuery = db.UserQuery.Find(id);
            db.UserQuery.Remove(userQuery);
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
