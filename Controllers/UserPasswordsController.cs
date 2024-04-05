using Group5__iCLOTHINGApp.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Group5__iCLOTHINGApp.Controllers
{
    public class UserPasswordsController : Controller
    {
        private Group5_iCLOTHINGDBEntities db = new Group5_iCLOTHINGDBEntities();

        // GET: UserPasswords
        public ActionResult Index()
        {
            return View();
        }

            // GET: UserPasswords/Login
            public ActionResult Login()
        {
                ViewBag.userID = new SelectList(db.Customer, "customerID", "customerName");
                UserPassword model = new UserPassword();
                model.passwordExpiryTime = 10000;
                DateTime expiryDate = DateTime.Today.AddDays(model.passwordExpiryTime);
                model.userAccountExpiryDate = expiryDate;
                return View(model);
        }

        // GET: UserPasswords/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPassword userPassword = db.UserPassword.Find(id);
            if (userPassword == null)
            {
                return HttpNotFound();
            }
            return View(userPassword);
        }

        // GET: UserPasswords/Create
        public ActionResult Create()
        {
            ViewBag.userID = new SelectList(db.Customer, "customerID", "customerName");
            UserPassword model = new UserPassword();

            // Default passwordExpiryTime = 100000 days after creation day of UserPassword instance
            model.passwordExpiryTime = 100000;
            DateTime expiryDate = DateTime.Today.AddDays(model.passwordExpiryTime);
            model.userAccountExpiryDate = expiryDate;
            model.isAdmin = false; // Default value for isAdmin is false
            return View(model);
        }

        // POST: UserPasswords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userID,userAccountName,userEncryptedPassword,passwordExpiryTime,userAccountExpiryDate,isAdmin")] UserPassword userPassword)
        {
            if (ModelState.IsValid)
            {
                if (db.UserPassword.Any(x => x.userAccountName == userPassword.userAccountName) || db.UserPassword.Any(x => x.userID == userPassword.userID))
                {
                    ViewBag.Notification = "user account already exists";
                    return View();
                }
                else
                {
                    db.UserPassword.Add(userPassword);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
            }

            ViewBag.userID = new SelectList(db.Customer, "customerID", "customerName", userPassword.userID);
            return View(userPassword);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "userID,userAccountName,userEncryptedPassword,passwordExpiryTime,userAccountExpiryDate,isAdmin")] UserPassword userPassword)
        {
            if (db.UserPassword.Any(x => x.userAccountName == userPassword.userAccountName))
            {
                UserPassword user = db.UserPassword.FirstOrDefault(x => x.userAccountName == userPassword.userAccountName); // Get 1st matching UserPassword with userAccountName

                if (user != null && user.userEncryptedPassword == userPassword.userEncryptedPassword)
                {
                    db.SaveChanges();
                    Session["userIDSS"] = user.userID.ToString();
                    Session["userAccountNameSS"] = user.userAccountName.ToString();
                    Session["userEncryptedPasswordSS"] = user.userEncryptedPassword.ToString();
                    Session["passwordExpiryTimeSS"] = user.passwordExpiryTime;
                    Session["userAccountExpiryDateSS"] = user.userAccountExpiryDate;
                    Session["isAdminSS"] = (bool)user.isAdmin;

                    Session["customerNameSS"] = db.Customer.FirstOrDefault(x => x.customerID == user.userID).customerName.ToString();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Notification = "incorrect username or password";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "UserPasswords");
        }

        // GET: UserPasswords/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPassword userPassword = db.UserPassword.Find(id);
            if (userPassword == null)
            {
                return HttpNotFound();
            }
            ViewBag.userID = new SelectList(db.Customer, "customerID", "customerName", userPassword.userID);
            return View(userPassword);
        }

        // POST: UserPasswords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userID,userAccountName,userEncryptedPassword,passwordExpiryTime,userAccountExpiryDate,isAdmin")] UserPassword userPassword)
        {
            if (ModelState.IsValid)
            {
                // Assign values that user can't edit in View here
                userPassword.passwordExpiryTime = (int) Session["passwordExpiryTimeSS"];
                userPassword.userAccountExpiryDate = (DateTime)Session["userAccountExpiryDateSS"];
                if(Session["isAdminSS"] is bool)
                {
                    userPassword.isAdmin = (bool)Session["isAdminSS"];
                }

                db.Entry(userPassword).State = EntityState.Modified;
                db.SaveChanges();
                Session["userAccountNameSS"] = userPassword.userAccountName.ToString();
                Session["userEncryptedPasswordSS"] = userPassword.userEncryptedPassword.ToString();
                if (userPassword.isAdmin)
                {
                    Session["isAdminSS"] = true;
                }
                else
                {
                    Session["isAdminSS"] = false;
                }

                return RedirectToAction("Index");
            }
            ViewBag.userID = new SelectList(db.Customer, "customerID", "customerName", userPassword.userID);
            return View(userPassword);
        }

        // GET: UserPasswords/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPassword userPassword = db.UserPassword.Find(id);
            if (userPassword == null)
            {
                return HttpNotFound();
            }
            return View(userPassword);
        }

        // POST: UserPasswords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UserPassword userPassword = db.UserPassword.Find(id);
            db.UserPassword.Remove(userPassword);
            db.SaveChanges();
            return RedirectToAction("Logout");
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
