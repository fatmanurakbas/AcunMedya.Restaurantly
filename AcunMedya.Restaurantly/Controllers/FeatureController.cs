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
    public class FeatureController : Controller
    {
        RestaurantlyContext Db = new RestaurantlyContext();
        // GET: Product


        public ActionResult FeatureList()
        {

            var value = Db.Features.ToList();

            return View(value);
        }
        [HttpGet]
        public ActionResult FeatureCreate()
        {


            return View();
        }
        [HttpPost]
        public ActionResult FeatureCreate(Feature model)
        {
            Db.Features.Add(model);
            Db.SaveChanges();
            return RedirectToAction("FeatureList");
        }

        [HttpGet]
        public ActionResult FeatureEdit(int id)
        {
            var value = Db.Features.Find(id);

            return View(value);
        }

        [HttpPost]
        public ActionResult FeatureEdit(Feature model)
        {
            var values = Db.Features.Find(model.FeatureId);
            values.SubTitle = model.SubTitle;
            values.Title = model.Title;
            values.VideoUrl = model.VideoUrl;
            values.ImageUrl = model.ImageUrl;
            Db.SaveChanges();
            return RedirectToAction("FeatureList");
        }

        public ActionResult FeatureDelete(int id)
        {
            var value = Db.Features.Find(id);
            Db.Features.Remove(value);
            Db.SaveChanges();
            return RedirectToAction("FeatureList");
        }
    }
}