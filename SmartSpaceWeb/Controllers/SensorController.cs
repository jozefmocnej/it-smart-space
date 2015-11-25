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
            var items = DocumentDBRepository<Sensor>.GetItems(d => (true));
            return View(items);
        }

        public ActionResult KitchenIndex()
        {
            var items = DocumentDBRepository<Sensor>.GetItems(d => (d.Place == "kitchen"));
            /*DateTime newest = new DateTime();
            foreach (Sensor s in items)
            {
                items.
            }*/
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

        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Sensor item = (Sensor)DocumentDBRepository<Sensor>.GetItem(d => d.Id == id);

            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Type,AtLocation,Place,Timestamp,Status,Contatore")] Sensor item)
        {
            if (ModelState.IsValid)
            {
                await DocumentDBRepository<Sensor>.UpdateItemAsync(item.Id, item);
                return RedirectToAction("Index");
            }

            return View(item);
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Sensor item = DocumentDBRepository<Sensor>.GetItem(x => x.Id == id);
            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        // To protect against Cross-Site Request Forgery, validate that the anti-forgery token was received and is valid
        // for more details on preventing see http://go.microsoft.com/fwlink/?LinkID=517254
        [ValidateAntiForgeryToken]

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        public async Task<ActionResult> DeleteConfirmed([Bind(Include = "Id")] string id)
        {
            await DocumentDBRepository<Sensor>.DeleteItemAsync(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(string id)
        {
            Sensor item = DocumentDBRepository<Sensor>.GetItem(x => x.Id == id);
            return View(item);
        }

    }
}