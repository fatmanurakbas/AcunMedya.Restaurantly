using AcunMedya.Restaurantly.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AcunMedya.Restaurantly.Controllers
{
    [Authorize]
    public class DahboardController : Controller
    {
        RestaurantlyContext Db = new RestaurantlyContext();

        // GET: Dahboard
        public ActionResult Index()
        {
            ViewBag.Productcount = Db.Products.Count();
            ViewBag.CategoryCount = Db.Categories.Count();
            ViewBag.Chefcount = Db.Chefs.Count();
            ViewBag.SpecialCount = Db.Contacts.Count();

            // Ortalama Ürün Fiyatı
            var averagePrice = Db.Products.Average(p => p.Price);
            ViewBag.AverageProductPrice = averagePrice;

            // En Çok Tercih Edilen Kategori
            var topCategory = Db.Products
                                 .GroupBy(p => p.CategoryId)
                                 .OrderByDescending(g => g.Count())
                                 .FirstOrDefault();
            var topCategoryName = topCategory != null ? Db.Categories.FirstOrDefault(c => c.CategoryId == topCategory.Key)?.CategoryName : "Yok";
            ViewBag.TopCategory = topCategoryName;

            // En Fazla Rezervasyon Yapılan Tarih
            var topReservationDate = Db.Reservations
                                       .GroupBy(r => r.ReservationDate)
                                       .OrderByDescending(g => g.Count())
                                       .FirstOrDefault();
            ViewBag.TopReservationDate = topReservationDate?.Key.ToString("dd/MM/yyyy");

            // En Çok Sipariş Edilen Ürün
            var topOrderedProduct = Db.Products.GroupBy(p=>p.ProductId)
                                      .OrderByDescending(g => g.Count())
                                      .FirstOrDefault();
            var topOrderedProductName = topOrderedProduct != null ? Db.Products.FirstOrDefault(p => p.ProductId == topOrderedProduct.Key)?.Name : "Yok";
            ViewBag.TopOrderedProduct = topOrderedProductName;

            return View();
        }

        public PartialViewResult ReservasionPartial()
        {
            var values = Db.Reservations.ToList();
            return PartialView(values);
        }
    }
}