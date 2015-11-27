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
    public class AlarmController : Controller
    {
        // GET: Alarm
        public ActionResult AlarmIndex()
        {
            var items = DocumentDBRepository<Alarm>.GetItemsCol2(d => (true));
            return View(items);
        }

        public ActionResult Create()
        {
            var model = new SensorSearch();
            var locations = DocumentDBRepository<Sensor>.GetItems().Select(t => t.AtLocation).AsEnumerable().Distinct();
            var types = DocumentDBRepository<Sensor>.GetItems().Select(t => t.Type).AsEnumerable().Distinct();

            ViewBag.SensorDataLocation = new SelectList(locations);
            ViewBag.SensorDataType = new SelectList(types);

            return View("Create");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,IdDevice,Type,AtLocation,Place,Min,Max")] Alarm item)
        {
            if (ModelState.IsValid)
            {
                await DocumentDBRepository<Alarm>.CreateItemAsync2(item);
                return RedirectToAction("AlarmIndex");
            }
            return View(item);
        }

        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Alarm item = (Alarm)DocumentDBRepository<Alarm>.GetItem2(d => d.Id == id);

            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,IdDevice,Type,AtLocation,Place,Min,Max")] Alarm item)
        {
            if (ModelState.IsValid)
            {
                await DocumentDBRepository<Alarm>.UpdateItemAsync2(item.Id, item);
                return RedirectToAction("AlarmIndex");
            }

            return View(item);
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Alarm item = DocumentDBRepository<Alarm>.GetItem2(x => x.Id == id);
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
            await DocumentDBRepository<Alarm>.DeleteItemAsync2(id);
            return RedirectToAction("AlarmIndex");
        }

        public ActionResult Details(string id)
        {
            Alarm item = DocumentDBRepository<Alarm>.GetItem2(x => x.Id == id);
            return View(item);
        }

    }
}