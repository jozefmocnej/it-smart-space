using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Net;
using System.Threading.Tasks;
using SmartSpaceWeb.Models;

namespace SmartSpaceWeb.Controllers
{
    public class SensorController : Controller
    {
        // GET: Sensor
        public ActionResult Index()
        {
            var items = DocumentDBRepository<Sensor>.GetItems(d => (d.Id != null));
            return View(items);
        }

        public ActionResult Create()
        {
            return View("AddSensor");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Type,AtLocation,Place,Timestamp,Status,Contatore")] Sensor item)
        {
            if (ModelState.IsValid)
            {
                await DocumentDBRepository<Sensor>.CreateItemAsync(item);
                return RedirectToAction("Index");
            }
            return View(item);
        }

    }
}