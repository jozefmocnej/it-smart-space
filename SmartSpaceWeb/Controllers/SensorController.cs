﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Net;
using System.Threading.Tasks;
using SmartSpaceWeb.Models;
using System.Globalization;
using System.Web.UI.WebControls;

namespace SmartSpaceWeb.Controllers
{
    public class SensorController : Controller
    {
        // GET: Sensor
        public ActionResult Index()
        {
            var model = new SensorSearch();
            var locations = DocumentDBRepository<Sensor>.GetItems().Select(t => t.AtLocation).AsEnumerable().Distinct();
            var types = DocumentDBRepository<Sensor>.GetItems().Select(t => t.Type).AsEnumerable().Distinct();
            foreach (var location in locations)
            {
                model.Locations.Add(new SelectListItem { Text = location, Value = location });
            }

            foreach (var type in types)
            {
                model.Types.Add(new SelectListItem { Text = type, Value = type });
            }
            return View(model);
        }

        public PartialViewResult IndexPartial(SensorSearch model)
        {
            var items = DocumentDBRepository<Sensor>.GetItems();
            if (model != null)
            {
                if (model.AtLocation != null)
                {
                    items = items.Where(x => x.AtLocation == model.AtLocation);
            }
                if (model.Type != null)
            {
                    items = items.Where(y => y.Type == model.Type);
            }
            }
            var itemList = items.ToList();
            foreach (Sensor sensor in itemList)
            {
                //simple fix because of datatime inconsistency
                if(sensor.Timestamp.Length<15)
                sensor.Timestamp = (ConvertFromUnixTimestamp(System.Convert.ToDouble(sensor.Timestamp)).ToString());
            }
            return PartialView("_IndexPartial", itemList);
        }


        public ActionResult RoomView(string room)
        {
            if (room == null)
            { room = ""; }
            ViewBag.room = room;
            return View();
        }

        public PartialViewResult _SensorPartial(string room)
        {
            if ((room == null) || (room == "KITCHEN"))
            { room = ""; }

            DateTime time = DateTime.Now.AddHours(-1);

            //var items = DocumentDBRepository<Sensor>.GetItems(d => d.AtLocation == room).GroupBy(r => r.Type).Select(g => g.OrderByDescending(x => x.Timestamp).FirstOrDefault());
            var items = DocumentDBRepository<Sensor>.GetItems(d => d.AtLocation == room).AsEnumerable().GroupBy(r => r.Type).Select(g => g.OrderByDescending(x => x.Timestamp).FirstOrDefault()).ToList();

            var alarms = DocumentDBRepository<Alarm>.GetItemsCol2(d => (d.AtLocation == room));

            int val;
            foreach (Sensor s in items)
            {
                val = Int32.Parse(s.Status, System.Globalization.NumberStyles.HexNumber);
                s.Flag = 0;
                foreach (Alarm a in alarms)
                {
                    if ((s.IdDevice == a.IdDevice) && (s.Place == a.Place) && (s.Type == a.Type))
                    {
                        if (val < a.Min) { s.Flag = -1; }
                        else if (val > a.Max) { s.Flag = 1; }
                    }
                }
                if (s.Timestamp.Length < 15)
                {
                    s.Timestamp = (ConvertFromUnixTimestamp(System.Convert.ToDouble(s.Timestamp)).ToString());
                }
            }

            ViewBag.time = time;
            return PartialView(items);
        }


        public ActionResult Create()
        {
            return View("AddSensor");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,IdDevice,Type,AtLocation,Place,Timestamp,Status,Counter")] Sensor item)
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


        // HELPING METHODS 
        public static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }

        public static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        private async Task<IEnumerable<Sensor>> DataConversion(IEnumerable<Sensor> items)
        {
            string t = "dd/MM/yyyy HH:mm:ss";

            foreach (Sensor sen in items)
            {


                DateTime dt;
                if (DateTime.TryParseExact(sen.Timestamp, t, CultureInfo.InvariantCulture,
                                           DateTimeStyles.None,
                                           out dt))
                {
                    sen.Timestamp = System.Convert.ToString(ConvertToUnixTimestamp(dt));
                    await Edit(sen);
                }

            }
            return items;
        }



    }
}