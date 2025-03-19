using AcunMedya.Restaurantly.Context;
using AcunMedya.Restaurantly.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AcunMedya.Restaurantly.Controllers
{
    [Authorize]
    public class ServiceController : Controller
    {
        RestaurantlyContext Db = new RestaurantlyContext();
        // GET: Category

        public ActionResult ServiceList(string searcText)
        {

            List<Service> values;
            if (searcText != null)
            {
                values = Db.Services.Where(x => x.Title.Contains(searcText)).ToList();
                return View(values);
            }
            var value = Db.Services.ToList();
            //ViewBag.username = Session["a"];
            return View(value);
        }
        [HttpGet]
        public ActionResult ServiceCreate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ServiceCreate(Service model)
        {
            Db.Services.Add(model);
            Db.SaveChanges();
            return RedirectToAction("ServiceList");
        }

        [HttpGet]
        public ActionResult ServiceEdit(int id)
        {
            var value = Db.Services.Find(id);
            return View(value);
        }

        [HttpPost]
        public ActionResult ServiceEdit(Service model)
        {
            var values = Db.Services.Find(model.ServiceId);
            values.Title = model.Title;
            values.Description = model.Description;
            Db.SaveChanges();
            return RedirectToAction("ServiceList");
        }

        public ActionResult ServiceDelete(int id)
        {
            var value = Db.Services.Find(id);
            Db.Services.Remove(value);
            Db.SaveChanges();
            return RedirectToAction("ServiceList");
        }

    }
}