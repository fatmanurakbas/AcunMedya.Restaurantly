using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcunMedya.Restaurantly.Context;
using AcunMedya.Restaurantly.Entities;

namespace AcunMedya.Restaurantly.Controllers
{
    [Authorize]
        public class AdressController : Controller
        {
  
            RestaurantlyContext Db = new RestaurantlyContext();
            // GET: Category
            public ActionResult AdressList(string searcText)
            {
            List<Adress> values;
            if (searcText != null)
            {
                values = Db.Adresss.Where(x => x.Location.Contains(searcText)).ToList();
                return View(values);
            }
            var value = Db.Adresss.ToList();
                return View(value);
            }
            [HttpGet]
            public ActionResult AdressCreate()
            {
                return View();
            }
        [HttpPost]
        public ActionResult AdressCreate(Adress model)
            {
                Db.Adresss.Add(model);
                Db.SaveChanges();
                return RedirectToAction("AdressList");
            }

            [HttpGet]
            public ActionResult AdressEdit(int id)
            {
                var value = Db.Adresss.Find(id);
                return View(value);
            }

            [HttpPost]
            public ActionResult AdressEdit(Adress model)
            {
                var values = Db.Adresss.Find(model.AdressId);
                values.Location= model.Location;
                values.OpenHours = model.OpenHours;
                values.Email = model.Email;
                values.Call= model.Call;

            Db.SaveChanges();
                return RedirectToAction("AdressList");
            }

            public ActionResult AdressDelete(int id)
            {
                var value = Db.Adresss.Find(id);
                Db.Adresss.Remove(value);
                Db.SaveChanges();
                return RedirectToAction("AdressList");
            }

        
    }
}