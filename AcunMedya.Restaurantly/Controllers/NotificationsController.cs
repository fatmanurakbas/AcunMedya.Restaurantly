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
    public class NotificationsController : Controller
    {
        RestaurantlyContext Db = new RestaurantlyContext();
        // GET: Category

        public ActionResult NotificationList(string searcText)
        {
            List<Notification> values;
            if (searcText != null)
            {
                values = Db.Notifications.Where(x => x.Title.Contains(searcText)).ToList();
                return View(values);
            }

            var value = Db.Notifications.ToList();
            return View(value);
        }
        [HttpGet]
        public ActionResult NotificationCreate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NotificationCreate(Notification model)
        {
            Db.Notifications.Add(model);
            Db.SaveChanges();
            return RedirectToAction("NotificationList");
        }

        [HttpGet]
        public ActionResult NotificationEdit(int id)
        {
            var value = Db.Notifications.Find(id);
            return View(value);
        }

        [HttpPost]
        public ActionResult NotificationEdit(Notification model)
        {
            var values = Db.Notifications.Find(model.NotificationId);
            values.Title = model.Title;
            values.Time = model.Time;
            values.Icon = model.Icon;
            values.Iconcolor = model.Iconcolor;
            values.IsRead = model.IsRead;
            Db.SaveChanges();
            return RedirectToAction("NotificationList");
        }

        public ActionResult NotificationDelete(int id)
        {
            var value = Db.Notifications.Find(id);
            Db.Notifications.Remove(value);
            Db.SaveChanges();
            return RedirectToAction("NotificationList");
        }

    }
}