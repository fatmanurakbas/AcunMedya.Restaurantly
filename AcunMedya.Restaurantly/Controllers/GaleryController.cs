using AcunMedya.Restaurantly.Context;
using AcunMedya.Restaurantly.Entities;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AcunMedya.Restaurantly.Controllers
{
    [Authorize]
    public class GaleryController : Controller
    {
        RestaurantlyContext Db = new RestaurantlyContext();

        public ActionResult GaleryList()
        {
            var value = Db.Galery.ToList();
            return View(value);
        }

        [HttpGet]
        public ActionResult GaleryCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GaleryCreate(Galery model, HttpPostedFileBase ImageFile)
        {
            if (ImageFile != null && ImageFile.ContentLength > 0)
            {
                string fileName = Path.GetFileName(ImageFile.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Template/Restaurantly/assets/img/gallery/"), fileName);
                ImageFile.SaveAs(filePath);
                model.ImageUrl = "/Template/Restaurantly/assets/img/gallery/" + fileName;
            }

            Db.Galery.Add(model);
            Db.SaveChanges();
            return RedirectToAction("GaleryList");
        }

        [HttpGet]
        public ActionResult GaleryEdit(int id)
        {
            var value = Db.Galery.Find(id);
            return View(value);
        }

        [HttpPost]
        public ActionResult GaleryEdit(Galery model, HttpPostedFileBase ImageFile)
        {
            var values = Db.Galery.Find(model.GaleryId);
            if (values != null)
            {
                values.Title = model.Title;

                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(ImageFile.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Template/Restaurantly/assets/img/gallery/"), fileName);
                    ImageFile.SaveAs(filePath);
                    values.ImageUrl = "/Template/Restaurantly/assets/img/gallery/" + fileName;
                }

                Db.SaveChanges();
            }
            return RedirectToAction("GaleryList");
        }

        public ActionResult GaleryDelete(int id)
        {
            var value = Db.Galery.Find(id);
            if (value != null)
            {
                Db.Galery.Remove(value);
                Db.SaveChanges();
            }
            return RedirectToAction("GaleryList");
        }
    }
}
