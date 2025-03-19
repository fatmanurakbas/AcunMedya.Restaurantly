using AcunMedya.Restaurantly.Context;
using AcunMedya.Restaurantly.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AcunMedya.Restaurantly.Controllers
{
    public class DefaultController : Controller
    {
        RestaurantlyContext Db = new RestaurantlyContext();
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult PartialHead()
        {
            return PartialView();
        }

        public PartialViewResult PartialTop()
        {
            ViewBag.Call = Db.Adresss.Select(x => x.Call).FirstOrDefault();
            ViewBag.OpenHours = Db.Adresss.Select(x => x.OpenHours).FirstOrDefault();
            return PartialView();
        }
        public PartialViewResult PartialNavbar()
        {
            return PartialView();
        }

        public PartialViewResult PartialFeature()
        {
            ViewBag.SubTitle = Db.Features.Select(x => x.SubTitle).FirstOrDefault();
            ViewBag.Title = Db.Features.Select(x => x.Title).FirstOrDefault();
            ViewBag.VideUrl = Db.Features.Select(x => x.VideoUrl).FirstOrDefault();
            return PartialView();
        }

        public PartialViewResult PartialAbout()
        {
            ViewBag.Title = Db.Abouts.Select(x => x.Title).FirstOrDefault();
            ViewBag.Description = Db.Abouts.Select(x => x.Description).FirstOrDefault();
            ViewBag.imageUrl = Db.Abouts.Select(x => x.imageUrl).FirstOrDefault();
            return PartialView();
        }
        public PartialViewResult PartialService()
        {
            var value = Db.Services.ToList();
            return PartialView(value);
        }

        public PartialViewResult PartialMenu()
        {
            var value = Db.Products.ToList();
            return PartialView(value);
        }

        public PartialViewResult PartialContact()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult ContactAdd(Contact model)
        {
            model.SendDate = DateTime.Now;
            model.IsRead = false;
            Db.Contacts.Add(model);
            Db.SaveChanges();
            ViewBag.Message = "Mesaj Başarılı";
            return View("PartialContact");

        }
        public PartialViewResult PartialSpecial()
        {
            var value = Db.Specials.ToList();
            return PartialView(value);
        }
        public PartialViewResult PartialTestimonial()
        {
            var value = Db.Testimonials.ToList();
            return PartialView(value);
        }
        
        public PartialViewResult PartialChef()
        {
            var value = Db.Chefs.ToList();
            return PartialView(value);
        }
        public PartialViewResult PartialAdress()
        {
            ViewBag.Location = Db.Adresss.Select(x => x.Location).FirstOrDefault();
            ViewBag.OpenHours = Db.Adresss.Select(x => x.OpenHours).FirstOrDefault();
            ViewBag.Email = Db.Adresss.Select(x => x.Email).FirstOrDefault();
            ViewBag.Call = Db.Adresss.Select(x => x.Call).FirstOrDefault();
            return PartialView();

        }
        public PartialViewResult PartialFooter()
        {          
            return PartialView();
        }

        [HttpGet]
        public PartialViewResult PartialReservation()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult ReservationAdd(Reservation model)
        {
            model.ReservationDate = DateTime.Now;
            model.ReservationStatus = "Pending"; // Başlangıçta "Pending" yapabilirsiniz.

            Db.Reservations.Add(model);
            Db.SaveChanges();

            ViewBag.Message = "Rezervasyon Başarılı";
            return PartialView("PartialReservation");
        }
        public PartialViewResult PartialEvent()
        {
            var value = Db.Events.ToList();
            return PartialView(value);
        }
        public PartialViewResult PartialGallery()
        {
            var value = Db.Galery.ToList();
            return PartialView(value);
        }


    }
}