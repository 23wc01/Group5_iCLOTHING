using Group5__iCLOTHINGApp.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Group5__iCLOTHINGApp.Controllers
{
    public class ProductsController : Controller
    {
        private Group5_iCLOTHINGDBEntities db = new Group5_iCLOTHINGDBEntities();

        // GET: Products
        public ActionResult Index(string searchString)
        {
            var product = db.Product.Include(p => p.Brand).Include(p => p.Category).Include(p => p.ShoppingCart);
            // List<Product> products = product.ToList();

            return View(db.Product.Where(x => x.productName.Contains(searchString) || searchString == null).ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.productID = new SelectList(db.Brand, "brandID", "brandName");
            ViewBag.categoryID = new SelectList(db.Category, "categoryID", "categoryName");
            ViewBag.cartID = new SelectList(db.ShoppingCart, "cartID", "customerID");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "productID,productName,productDescription,productPrice,productQty,categoryID,cartID")] Product product)
        {
            if (ModelState.IsValid)
            {
                if (db.Product.Any(x => x.productID == product.productID))
                {
                    ViewBag.Notifications = "ID already exists";
                }
                else
                {
                    db.Product.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.productID = new SelectList(db.Brand, "brandID", "brandName", product.productID);
            ViewBag.categoryID = new SelectList(db.Category, "categoryID", "categoryName", product.categoryID);
            ViewBag.cartID = new SelectList(db.ShoppingCart, "cartID", "customerID", product.cartID);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.productID = new SelectList(db.Brand, "brandID", "brandName", product.productID);
            ViewBag.categoryID = new SelectList(db.Category, "categoryID", "categoryName", product.categoryID);
            ViewBag.cartID = new SelectList(db.ShoppingCart, "cartID", "customerID", product.cartID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "productID,productName,productDescription,productPrice,productQty,categoryID,cartID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.productID = new SelectList(db.Brand, "brandID", "brandName", product.productID);
            ViewBag.categoryID = new SelectList(db.Category, "categoryID", "categoryName", product.categoryID);
            ViewBag.cartID = new SelectList(db.ShoppingCart, "cartID", "customerID", product.cartID);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
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
