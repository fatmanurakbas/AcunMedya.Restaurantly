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
    public class ContactController : Controller
    {
        RestaurantlyContext Db = new RestaurantlyContext();
        // GET: Category

        public ActionResult ContactList(string searcText)
        {
            List<Contact> values;
            if (searcText != null)
            {
                values = Db.Contacts.Where(x => x.Name.Contains(searcText)).ToList();
                return View(values);
            }

            var value = Db.Contacts.ToList();
            return View(value);
        }
        [HttpGet]
        public ActionResult ContactCreate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ContactCreate(Contact model)
        {
            Db.Contacts.Add(model);
            Db.SaveChanges();
            return RedirectToAction("ContactList");
        }

        [HttpGet]
        public ActionResult ContactEdit(int id)
        {
            var value = Db.Contacts.Find(id);
            return View(value);
        }

        [HttpPost]
        public ActionResult ContactEdit(Contact model)
        {
            var values = Db.Contacts.Find(model.ContactId);
            values.Name = model.Name;
            values.Email = model.Email;
            values.Subject = model.Subject;
            values.Message = model.Message;
            values.IsRead = model.IsRead;
            values.SendDate = model.SendDate;

            Db.SaveChanges();
            return RedirectToAction("ContactList");
        }

        public ActionResult ContactDelete(int id)
        {
            var value = Db.Contacts.Find(id);
            Db.Contacts.Remove(value);
            Db.SaveChanges();
            return RedirectToAction("ContactList");
        }

    }
}