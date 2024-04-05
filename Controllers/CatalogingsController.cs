using Group5__iCLOTHINGApp.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace Group5__iCLOTHINGApp.Controllers
{
    public class CatalogingsController : Controller
    {
        private Group5_iCLOTHINGDBEntities db = new Group5_iCLOTHINGDBEntities();

        // GET: Catalogings
        public ActionResult Index(string sort)
        {
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
    }
}
