using AcunMedya.Restaurantly.Context;
using AcunMedya.Restaurantly.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
public class SpecialController : Controller
{
    RestaurantlyContext Db = new RestaurantlyContext();

    public ActionResult SpecialList(string searcText)
    {
        List<Special> values;
        if (searcText != null)
        {
            values = Db.Specials.Where(x => x.Title.Contains(searcText)).ToList();
            return View(values);
        }
        var value = Db.Specials.ToList();
        return View(value);
    }

    [HttpGet]
    public ActionResult SpecialCreate()
    {
        return View();
    }

    [HttpPost]
    public ActionResult SpecialCreate(Special model, HttpPostedFileBase ImageFile)
    {
        if (ImageFile != null && ImageFile.ContentLength > 0)
        {
            string fileName = Path.GetFileName(ImageFile.FileName);
            string filePath = Path.Combine(Server.MapPath("~/Template/Restaurantly/assets/img/"), fileName);
            ImageFile.SaveAs(filePath);
            model.ImageUrl = "/Template/Restaurantly/assets/img/" + fileName;
        }

        Db.Specials.Add(model);
        Db.SaveChanges();
        return RedirectToAction("SpecialList");
    }

    [HttpGet]
    public ActionResult SpecialEdit(int id)
    {
        var value = Db.Specials.Find(id);
        return View(value);
    }

    [HttpPost]
    public ActionResult SpecialEdit(Special model, HttpPostedFileBase ImageFile)
    {
        var values = Db.Specials.Find(model.SpecialId);
        if (ImageFile != null && ImageFile.ContentLength > 0)
        {
            string fileName = Path.GetFileName(ImageFile.FileName);
            string filePath = Path.Combine(Server.MapPath("~/Template/Restaurantly/assets/img/"), fileName);
            ImageFile.SaveAs(filePath);
            values.ImageUrl = "/Template/Restaurantly/assets/img/" + fileName;
        }
        values.Title = model.Title;
        values.ShortDescription = model.ShortDescription;
        values.FullDescription = model.FullDescription;
        Db.SaveChanges();
        return RedirectToAction("SpecialList");
    }

    public ActionResult SpecialDelete(int id)
    {
        var value = Db.Specials.Find(id);
        Db.Specials.Remove(value);
        Db.SaveChanges();
        return RedirectToAction("SpecialList");
    }
}

