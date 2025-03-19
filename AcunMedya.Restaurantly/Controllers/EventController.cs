using AcunMedya.Restaurantly.Context;
using AcunMedya.Restaurantly.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AcunMedya.Restaurantly.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        RestaurantlyContext Db = new RestaurantlyContext();

        public ActionResult EventList(string searcText)
        {
            List<Event> values;
            if (searcText != null)
            {
                values = Db.Events.Where(x => x.Name.Contains(searcText)).ToList();
                return View(values);
            }
            var value = Db.Events.ToList();
            return View(value);
        }

        [HttpGet]
        public ActionResult EventCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EventCreate(Event model, HttpPostedFileBase ImageFile)
        {
            if (ImageFile != null && ImageFile.ContentLength > 0)
            {
                string fileName = Path.GetFileName(ImageFile.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Template/Restaurantly/assets/img/"), fileName);
                ImageFile.SaveAs(filePath);
                model.ImageUrl = "/Template/Restaurantly/assets/img/" + fileName;
            }

            Db.Events.Add(model);
            Db.SaveChanges();
            return RedirectToAction("EventList");
        }

        [HttpGet]
        public ActionResult EventEdit(int id)
        {
            var value = Db.Events.Find(id);
            return View(value);
        }

        [HttpPost]
        public ActionResult EventEdit(Event model, HttpPostedFileBase ImageFile)
        {
            var values = Db.Events.Find(model.EventId);
            if (values != null)
            {
                values.Title = model.Title;
                values.Name = model.Name;
                values.Price = model.Price;
                values.Description = model.Description;

                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(ImageFile.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Template/Restaurantly/assets/img/"), fileName);
                    ImageFile.SaveAs(filePath);
                    values.ImageUrl = "/Template/Restaurantly/assets/img/" + fileName;
                }

                Db.SaveChanges();
            }
            return RedirectToAction("EventList");
        }

        public ActionResult EventDelete(int id)
        {
            var value = Db.Events.Find(id);
            if (value != null)
            {
                Db.Events.Remove(value);
                Db.SaveChanges();
            }
            return RedirectToAction("EventList");
        }
    }
}
