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
    public class ReservationController : Controller
    {
        RestaurantlyContext Db = new RestaurantlyContext();
        // GET: Category

        public ActionResult ReservationList(string searcText)
        {
            List<Reservation> values;
            if (searcText != null)
            {
                values = Db.Reservations.Where(x => x.Name.Contains(searcText)).ToList();
                return View(values);
            }

            var value = Db.Reservations.ToList();
            return View(value);
        }
        [HttpGet]
        public ActionResult ReservationCreate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ReservationCreate(Reservation model)
        {
            Db.Reservations.Add(model);
            Db.SaveChanges();
            return RedirectToAction("ReservationList");
        }

        [HttpGet]
        public ActionResult ReservationEdit(int id)
        {
            var value = Db.Reservations.Find(id);
            return View(value);
        }

        [HttpPost]
        public ActionResult ReservationEdit(Reservation model)
        {
            var values = Db.Reservations.Find(model.ReservationId);
            values.Name = model.Name;
            values.Email = model.Email;
            values.Phone = model.Phone;
            values.Description = model.Description;
            values.ReservationDate = model.ReservationDate;
            values.Time = model.Time;
            values.GuestCount = model.GuestCount;
            values.ReservationStatus = model.ReservationStatus;

            
            Db.SaveChanges();
            return RedirectToAction("ReservationList");
        }

        public ActionResult ReservationDelete(int id)
        {
            var value = Db.Reservations.Find(id);
            Db.Reservations.Remove(value);
            Db.SaveChanges();
            return RedirectToAction("ReservationList");
        }

    }
}