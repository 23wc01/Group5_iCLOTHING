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
    public class EmailsController : Controller
    {
        private Group5_iCLOTHINGDBEntities db = new Group5_iCLOTHINGDBEntities();

        // GET: Emails
        public ActionResult Index()
        {
            //var email = db.Email.Include(e => e.Administrator).Include(e => e.Customer);
            //return View(email.ToList());

            if (Session["userIDSS"] != null)
            {
                if ((bool)Session["isAdminSS"])
                {
                    var email = db.Email.Include(e => e.Administrator).Include(e => e.Customer);
                    return View(email.ToList());
                }
                else
                {
                    string customerID = (string)Session["userIDSS"];
                    var emails = db.Email.Where(e => e.customerID == customerID);
                    return View(emails.ToList());
                }
            }
            else
            {
                return RedirectToAction("Login", "UserPasswords");
            }
        }

        // GET: Emails/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Email email = db.Email.Find(id);
            if (email == null)
            {
                return HttpNotFound();
            }
            return View(email);
        }

        // GET: Emails/Create
        public ActionResult Create()
        {
            ViewBag.adminID = new SelectList(db.Administrator, "adminID", "adminName");
            ViewBag.customerID = new SelectList(db.Customer, "customerID", "customerName");
            return View();
        }

        // POST: Emails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "emailNo,emailDate,emailSubject,emailBody,adminID,customerID")] Email email)
        {
            if (ModelState.IsValid)
            {
                db.Email.Add(email);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.adminID = new SelectList(db.Administrator, "adminID", "adminName", email.adminID);
            ViewBag.customerID = new SelectList(db.Customer, "customerID", "customerName", email.customerID);
            return View(email);
        }

        // GET: Emails/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Email email = db.Email.Find(id);
            if (email == null)
            {
                return HttpNotFound();
            }
            ViewBag.adminID = new SelectList(db.Administrator, "adminID", "adminName", email.adminID);
            ViewBag.customerID = new SelectList(db.Customer, "customerID", "customerName", email.customerID);
            return View(email);
        }

        // POST: Emails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "emailNo,emailDate,emailSubject,emailBody,adminID,customerID")] Email email)
        {
            if (ModelState.IsValid)
            {
                db.Entry(email).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.adminID = new SelectList(db.Administrator, "adminID", "adminName", email.adminID);
            ViewBag.customerID = new SelectList(db.Customer, "customerID", "customerName", email.customerID);
            return View(email);
        }

        // GET: Emails/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Email email = db.Email.Find(id);
            if (email == null)
            {
                return HttpNotFound();
            }
            return View(email);
        }

        // POST: Emails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Email email = db.Email.Find(id);
            db.Email.Remove(email);
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
