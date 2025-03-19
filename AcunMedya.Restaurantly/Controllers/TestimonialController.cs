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
    public class TestimonialController : Controller
    {
        RestaurantlyContext Db = new RestaurantlyContext();

        public ActionResult TestimonialList(string searcText)
        {
            List<Testimonial> values;
            if (searcText != null)
            {
                values = Db.Testimonials.Where(x => x.Name.Contains(searcText)).ToList();
                return View(values);
            }
            var value = Db.Testimonials.ToList();
            return View(value);
        }

        [HttpGet]
        public ActionResult TestimonialCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TestimonialCreate(Testimonial model, HttpPostedFileBase ImageFile)
        {
            if (ImageFile != null && ImageFile.ContentLength > 0)
            {
                string fileName = Path.GetFileName(ImageFile.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Template/Restaurantly/assets/img/testimonials/"), fileName);
                ImageFile.SaveAs(filePath);
                model.ImageUrl = "/Template/Restaurantly/assets/img/testimonials/" + fileName;
            }

            Db.Testimonials.Add(model);
            Db.SaveChanges();
            return RedirectToAction("TestimonialList");
        }

        [HttpGet]
        public ActionResult TestimonialEdit(int id)
        {
            var value = Db.Testimonials.Find(id);
            return View(value);
        }

        [HttpPost]
        public ActionResult TestimonialEdit(Testimonial model, HttpPostedFileBase ImageFile)
        {
            var values = Db.Testimonials.Find(model.TestimonialId);
            values.Name = model.Name;
            values.Title = model.Title;
            values.Description = model.Description;

            if (ImageFile != null && ImageFile.ContentLength > 0)
            {
                string fileName = Path.GetFileName(ImageFile.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Template/Restaurantly/assets/img/testimonials/"), fileName);
                ImageFile.SaveAs(filePath);
                values.ImageUrl = "/Template/Restaurantly/assets/img/testimonials/" + fileName;
            }

            Db.SaveChanges();
            return RedirectToAction("TestimonialList");
        }

        public ActionResult TestimonialDelete(int id)
        {
            var value = Db.Testimonials.Find(id);
            if (!string.IsNullOrEmpty(value.ImageUrl))
            {
                string filePath = Server.MapPath(value.ImageUrl);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            Db.Testimonials.Remove(value);
            Db.SaveChanges();
            return RedirectToAction("TestimonialList");
        }
    }
}
