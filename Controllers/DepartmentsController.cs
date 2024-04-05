using Group5__iCLOTHINGApp.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Group5__iCLOTHINGApp.Controllers
{
    public class DepartmentsController : Controller
    {
        private Group5_iCLOTHINGDBEntities db = new Group5_iCLOTHINGDBEntities();

        // GET: Departments
        public ActionResult Index(string sort)
        {
        // var department = db.Department.Include(d => d.Cataloging);
            List<Department> departments;
            if (sort != null && sort.ToString() == "descending")
            { // Sorts by Department name in descending order if specified
                departments = db.Department.OrderByDescending(x => x.departmentName).ToList();
            }
            else
            { // Sorts by Department name in ascending order by default
                departments = db.Department.OrderBy(x => x.departmentName).ToList();
            }
            return View(departments);
        }
        
        // GET: Departments/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Department.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            ViewBag.departmentID = new SelectList(db.Cataloging, "departmentID", "categoryID");
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "departmentID,departmentName,departmentDescription")] Department department)
        {
            if (ModelState.IsValid)
            {
                if (db.Department.Any(x => x.departmentID == department.departmentID))
                {
                    ViewBag.Notifications = "ID already exists";
                }
                else
                {
                    db.Department.Add(department);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.departmentID = new SelectList(db.Cataloging, "departmentID", "categoryID", department.departmentID);
            return View(department);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Department.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            ViewBag.departmentID = new SelectList(db.Cataloging, "departmentID", "categoryID", department.departmentID);
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "departmentID,departmentName,departmentDescription")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.departmentID = new SelectList(db.Cataloging, "departmentID", "categoryID", department.departmentID);
            return View(department);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Department.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Department department = db.Department.Find(id);
            db.Department.Remove(department);
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
