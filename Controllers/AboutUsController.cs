using Group5__iCLOTHINGApp.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Group5__iCLOTHINGApp.Controllers
{
    public class AboutUsController : Controller
    {
        private Group5_iCLOTHINGDBEntities db = new Group5_iCLOTHINGDBEntities();
        private string adminIDAboutUs = "1234567890"; // AboutUs's adminID will always be the same since there is only 1 AboutUs instance

        // GET: AboutUs
        public ActionResult Index()
        {
            if (TempData["error"] != null)
            {
                ViewBag.Notifications = TempData["error"];
            }
            AboutUs aboutUs = db.AboutUs.FirstOrDefault(x => x.adminID == adminIDAboutUs); // Get 1st matching (and only) AboutUs with adminID == 1234567890
            if (aboutUs == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View("Details", aboutUs);
            } 
        }

        // GET: AboutUs/Details/1234567890
        public ActionResult Details()
        {
            AboutUs aboutUs = db.AboutUs.Find(adminIDAboutUs); // Get 1st matching (and only) AboutUs with adminID == 1234567890
            if (aboutUs == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(aboutUs);
            }
        }

        // GET: AboutUs/Edit/5
        public ActionResult Edit()
        {
            AboutUs aboutUs = db.AboutUs.Find(adminIDAboutUs); // Get 1st matching (and only) AboutUs with adminID == 1234567890
            if (aboutUs == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(aboutUs);
            }
        }

        // POST: AboutUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "companyAddress,companyShippingPolicy,companyReturnPolicy,companyContactInfo,companyBusinessDescription,adminID")] AboutUs aboutUs)
        {

            if (ModelState.IsValid)
            {
                db.Entry(aboutUs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            ViewBag.adminID = new SelectList(db.Administrator, "adminID", "adminName", aboutUs.adminID);
            return View(aboutUs);
        }
    }
}
       
