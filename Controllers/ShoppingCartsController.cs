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
    public class ShoppingCartsController : Controller
    {
        private Group5_iCLOTHINGDBEntities db = new Group5_iCLOTHINGDBEntities();

        // GET: ShoppingCarts
        public ActionResult Index()
        {
            if (Session["userIDSS"] == null)
            {
                // If user is not logged in, redirect to login page
                return RedirectToAction("Login", "UserPasswords");
            }

            var userID = Session["userIDSS"].ToString(); // Keep userID as string
            var shoppingCartItems = db.ShoppingCart.Include(s => s.Customer)
                                 .Where(s => s.customerID == userID);

            // Handle potential nulls in the calculation of totalCost
            var totalCost = shoppingCartItems
                .Sum(item => (decimal?)item.cartProductPrice * item.cartProductQty) ?? 0m;

            // Pass the total cost to the view
            ViewBag.TotalCost = totalCost;

            return View(shoppingCartItems.ToList());
        }


        // GET: ShoppingCarts/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCart shoppingCart = db.ShoppingCart.Find(id);
            if (shoppingCart == null)
            {
                return HttpNotFound();
            }
            return View(shoppingCart);
        }

        // GET: ShoppingCarts/Create
        public ActionResult Create()
        {
            ViewBag.SelectedProductID = new SelectList(db.Product, "productID", "productName");
            return View();
        }


        // POST: ShoppingCarts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cartID,cartProductQty")] ShoppingCart shoppingCart, string SelectedProductID)
        {
            if (ModelState.IsValid)
            {
                // Fetch the logged-in user's ID from the session
                var loggedInUserId = Session["userIDSS"]?.ToString();
                if (string.IsNullOrEmpty(loggedInUserId))
                {
                    // Handle the case where there is no user logged in
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "You must be logged in to create a cart.");
                }

                var product = db.Product.FirstOrDefault(p => p.productID == SelectedProductID);
                if (product != null)
                {
                    shoppingCart.customerID = loggedInUserId; // Set the customerID to the logged-in user's ID
                    shoppingCart.cartProductPrice = product.productPrice; // Set price based on the selected product

                    db.ShoppingCart.Add(shoppingCart);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid product selection.");
                }
            }

            // Prepare the dropdown lists again in case of returning to the view
            ViewBag.SelectedProductID = new SelectList(db.Product, "productID", "productName");

            return View(shoppingCart);
        }



        // GET: ShoppingCarts/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCart shoppingCart = db.ShoppingCart.Find(id);
            if (shoppingCart == null)
            {
                return HttpNotFound();
            }
            // Assuming you use a session variable "userIDSS" to track the currently logged in user.
            var currentUserId = Session["userIDSS"]?.ToString();
            if (shoppingCart.customerID != currentUserId)
            {
                // Optional: Return an error view or redirect if the cart does not belong to the current user
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            ViewBag.SelectedProductID = new SelectList(db.Product, "productID", "productName", shoppingCart.Product.FirstOrDefault()?.productID); // Assuming a cart can have multiple products and you're editing the first one for simplicity
            ViewBag.customerID = new SelectList(db.Customer, "customerID", "customerName", shoppingCart.customerID);

            return View(shoppingCart);
        }


        // POST: ShoppingCarts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cartID,cartProductQty")] ShoppingCart shoppingCart, string SelectedProductID)
        {
            if (ModelState.IsValid)
            {
                var existingCart = db.ShoppingCart.Find(shoppingCart.cartID);
                if (existingCart != null)
                {
                    var product = db.Product.FirstOrDefault(p => p.productID == SelectedProductID);
                    if (product != null)
                    {
                        // Update the product quantity
                        existingCart.cartProductQty = shoppingCart.cartProductQty;

                        // Update the product price based on the selected product
                        // This ensures the price is always in sync with the product's current price
                        existingCart.cartProductPrice = product.productPrice;

                        db.Entry(existingCart).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid product selection.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Shopping Cart not found.");
                }
            }

            // Prepare the dropdown list and model state for returning to the view
            ViewBag.SelectedProductID = new SelectList(db.Product, "productID", "productName", SelectedProductID);
            return View(shoppingCart);
        }



        // GET: ShoppingCarts/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCart shoppingCart = db.ShoppingCart.Find(id);
            if (shoppingCart == null)
            {
                return HttpNotFound();
            }
            return View(shoppingCart);
        }


        // Action to display all carts for admin
        public ActionResult AllCarts()
        {
            var carts = db.ShoppingCart.Include(c => c.Customer).ToList();
            var totals = carts.GroupBy(cart => cart.customerID)
                              .ToDictionary(group => group.Key, group => group.Sum(item => item.cartProductPrice * item.cartProductQty));

            ViewBag.Totals = totals; // Pass the totals dictionary to the view
            return View(carts);
        }


        // POST: ShoppingCarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ShoppingCart shoppingCart = db.ShoppingCart.Find(id);
            db.ShoppingCart.Remove(shoppingCart);
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
