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
    public class ChefController : Controller
    {
        RestaurantlyContext Db = new RestaurantlyContext();
        // GET: Product


        public ActionResult ChefList(string searcText)
        {
            List<Chef> values;
            if (searcText != null)
            {
                values = Db.Chefs.Where(x => x.Name.Contains(searcText)).ToList();
                return View(values);
            }

            var value = Db.Chefs.ToList();

            return View(value);
        }
        [HttpGet]
        public ActionResult ChefCreate()
        {


            return View();
        }
        [HttpPost]
        public ActionResult ChefCreate(Chef model, HttpPostedFileBase ImageFile)
        {
            if (ImageFile != null && ImageFile.ContentLength > 0)
            {
                // Resmi kaydetme yolu (Sunucuda "Uploads/Chefs" klasörüne kaydedilecek)
                string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(ImageFile.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Template/Restaurantly/assets/img/chefs/"), fileName);

                // Dosyayı sunucuya kaydet
                ImageFile.SaveAs(path);

                // Veritabanına resmin yolunu kaydet
                model.ImageUrl = "/Template/Restaurantly/assets/img/chefs/" + fileName;
            }

            Db.Chefs.Add(model);
            Db.SaveChanges();
            return RedirectToAction("ChefList");
        }
        [HttpGet]
        public ActionResult ChefEdit(int id)
        {
            var value = Db.Chefs.Find(id);

            return View(value);
        }

        [HttpPost]
        public ActionResult ChefEdit(Chef model, HttpPostedFileBase ImageFile)
        {
            var values = Db.Chefs.Find(model.ChefId);
            if (values == null)
            {
                return HttpNotFound();
            }

            values.Name = model.Name;
            values.title = model.title;

            // Eğer yeni bir fotoğraf seçildiyse
            if (ImageFile != null && ImageFile.ContentLength > 0)
            {
                try
                {
                    // Yeni dosya ismini oluştur
                    string fileExtension = System.IO.Path.GetExtension(ImageFile.FileName);
                    string fileName = Guid.NewGuid().ToString() + fileExtension;
                    string folderPath = Server.MapPath("~/Template/Restaurantly/assets/img/chefs/");

                    // Klasör yoksa oluştur
                    if (!System.IO.Directory.Exists(folderPath))
                    {
                        System.IO.Directory.CreateDirectory(folderPath);
                    }

                    // Tam dosya yolu
                    string filePath = System.IO.Path.Combine(folderPath, fileName);

                    // Eski dosyayı sil
                    if (!string.IsNullOrEmpty(values.ImageUrl))
                    {
                        string oldFilePath = Server.MapPath(values.ImageUrl);
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    // Yeni fotoğrafı kaydet
                    ImageFile.SaveAs(filePath);

                    // Veritabanına yeni resmin yolunu kaydet
                    values.ImageUrl = "/Template/Restaurantly/assets/img/chefs/" + fileName;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Hata: " + ex.Message);
                }
            }

            Db.SaveChanges();
            return RedirectToAction("ChefList");
        }


        public ActionResult ChefDelete(int id)
        {
            var value = Db.Chefs.Find(id);
            Db.Chefs.Remove(value);
            Db.SaveChanges();
            return RedirectToAction("ChefList");
        }
    }
}