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
    public class OrderStatusController : Controller
    {
        private Group5_iCLOTHINGDBEntities db = new Group5_iCLOTHINGDBEntities();

        // GET: OrderStatus
        public ActionResult Index()
        {
            var orderStatus = db.OrderStatus.Include(os => os.ShoppingCart.Select(sc => sc.Customer)).ToList();
            return View(orderStatus);
        }


        // GET: OrderStatus/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderStatus orderStatus = db.OrderStatus.Find(id);
            if (orderStatus == null)
            {
                return HttpNotFound();
            }
            return View(orderStatus);
        }

        // GET: OrderStatus/Create
        public ActionResult Create()
        {
            ViewBag.cartID = new SelectList(db.ShoppingCart, "cartID", "customerID");
            return View();
        }

        // POST: OrderStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "statusID,status,statusDate,cartID")] OrderStatus orderStatus)
        {
            if (ModelState.IsValid)
            {
                db.OrderStatus.Add(orderStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cartID = new SelectList(db.ShoppingCart, "cartID", "customerID", orderStatus.cartID);
            return View(orderStatus);
        }

        // GET: OrderStatus/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderStatus orderStatus = db.OrderStatus.Find(id);
            if (orderStatus == null)
            {
                return HttpNotFound();
            }
            ViewBag.cartID = new SelectList(db.ShoppingCart, "cartID", "customerID", orderStatus.cartID);
            return View(orderStatus);
        }

        // POST: OrderStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "statusID,status,statusDate,cartID")] OrderStatus orderStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cartID = new SelectList(db.ShoppingCart, "cartID", "customerID", orderStatus.cartID);
            return View(orderStatus);
        }

        //Update the order status
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmOrder(string cartID)
        {
            try
            {
                if (string.IsNullOrEmpty(cartID))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid Cart ID");
                }

                var shoppingCart = db.ShoppingCart.Include("OrderStatus").FirstOrDefault(sc => sc.cartID == cartID);
                if (shoppingCart == null)
                {
                    return HttpNotFound("Shopping Cart not found.");
                }

                // Initialize a random instance outside the loop
                var random = new Random();
                var statusID = "";

                // Generate a random 10-character string for statusID and check for uniqueness
                do
                {
                    statusID = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 10)
                        .Select(s => s[random.Next(s.Length)]).ToArray());
                } while (db.OrderStatus.Any(os => os.statusID == statusID)); // Check if the generated ID already exists

                // Create a new OrderStatus indicating the order is confirmed
                var newOrderStatus = new OrderStatus
                {
                    statusID = statusID,
                    status = "Confirmed",
                    statusDate = DateTime.Now,
                    cartID = cartID
                };

                db.OrderStatus.Add(newOrderStatus);
                db.SaveChanges();

                TempData["ConfirmationMessage"] = "Order has been confirmed successfully.";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log the exception details
                // For now, we'll just return the exception message back to the view to understand the issue.
                // In production, you would log the exception and return a user-friendly error message.
                return Content("An error occurred: " + ex.Message);
            }
        }






        // GET: OrderStatus/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderStatus orderStatus = db.OrderStatus.Find(id);
            if (orderStatus == null)
            {
                return HttpNotFound();
            }
            return View(orderStatus);
        }

        // POST: OrderStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            OrderStatus orderStatus = db.OrderStatus.Find(id);
            db.OrderStatus.Remove(orderStatus);
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