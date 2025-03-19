using AcunMedya.Restaurantly.Context;
using AcunMedya.Restaurantly.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AcunMedya.Restaurantly.Controllers
{
    public class AdminLayoutController : Controller
    {
        RestaurantlyContext Db = new RestaurantlyContext();

        // GET: AdminLayout
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult PartialHead()
        {
            return PartialView();
        }
        public PartialViewResult PartialNavbar()
        {
            // Okunmamış bildirimleri çek
            var unreadNotifications = Db.Notifications.Where(x => x.IsRead == "False").ToList();

            // Okunmamış mesajları çek (Son 5 mesaj)
            var unreadMessages = Db.Contacts
                .Where(m => !m.IsRead)
                .OrderByDescending(m => m.SendDate)
                .Take(5)
                .ToList();

            // ViewModel ile verileri birleştir
            var model = new NavbarViewModel
            {
                Notifications = unreadNotifications,
                UnreadMessages = unreadMessages,
                UnreadMessageCount = unreadMessages.Count,
                UnreadNotificationCount = unreadNotifications.Count
            };

            return PartialView(model);
        }
        // Mesajı okundu olarak işaretleme fonksiyonu
        public ActionResult MarkAsRead(int id)
        {
            var message = Db.Contacts.FirstOrDefault(m => m.ContactId == id);
            if (message != null)
            {
                message.IsRead = true;
                Db.SaveChanges();
            }
            return RedirectToAction("PartialNavbar");
        }

        public PartialViewResult PartialSidebar()
        {
            return PartialView();
        }
        public PartialViewResult PartialFooter()
        {
            return PartialView();
        }
        public PartialViewResult PartialScript()
        {
            return PartialView();
        }
        public ActionResult NotificationStatusChangeToTrue(int id)
        {
            var value = Db.Notifications.Find(id);
            value.IsRead = "True";
            Db.SaveChanges();
            return RedirectToAction("ProductList", "Product");
        }
    }
}