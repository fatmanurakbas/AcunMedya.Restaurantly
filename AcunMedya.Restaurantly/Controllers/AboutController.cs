using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcunMedya.Restaurantly.Context;
using AcunMedya.Restaurantly.Entities;

namespace AcunMedya.Restaurantly.Controllers
{
    public class AboutController : Controller
    {
        RestaurantlyContext Db = new RestaurantlyContext();

        public ActionResult AboutList(string searcText)

        {
            List<About> values;
            if (searcText != null)
            {
                values = Db.Abouts.Where(x => x.Title.Contains(searcText)).ToList();
                return View(values);
            }
            var value = Db.Abouts.ToList();
            return View(value);
        }

        [HttpGet]
        public ActionResult AboutCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AboutCreate(About model, HttpPostedFileBase ImageFile)
        {
            if (ImageFile != null && ImageFile.ContentLength > 0)
            {
                string fileName = Path.GetFileName(ImageFile.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Template/Restaurantly/assets/img/"), fileName);
                ImageFile.SaveAs(filePath);
                model.imageUrl = "/Template/Restaurantly/assets/img/" + fileName;
            }

            Db.Abouts.Add(model);
            Db.SaveChanges();
            return RedirectToAction("AboutList");
        }

        [HttpGet]
        public ActionResult AboutEdit(int id)
        {
            var value = Db.Abouts.Find(id);
            return View(value);
        }

        [HttpPost]
        public ActionResult AboutEdit(About model, HttpPostedFileBase ImageFile)
        {
            var values = Db.Abouts.Find(model.AboutId);
            values.Title = model.Title;
            values.Description = model.Description;

            if (ImageFile != null && ImageFile.ContentLength > 0)
            {
                string fileName = Path.GetFileName(ImageFile.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Template/Restaurantly/assets/img/"), fileName);
                ImageFile.SaveAs(filePath);
                values.imageUrl = "/Template/Restaurantly/assets/img/" + fileName;
            }

            Db.SaveChanges();
            return RedirectToAction("AboutList");
        }

        public ActionResult AboutDelete(int id)
        {
            var value = Db.Abouts.Find(id);
            Db.Abouts.Remove(value);
            Db.SaveChanges();
            return RedirectToAction("AboutList");
        }
    }
}
