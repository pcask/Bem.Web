using sinav_test_uygulamasi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sinav_test_uygulamasi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ContentResult Mrb()
        {
            return Content("<h1>Merhaba</h1>");
        }

        [HttpGet]
        public JsonResult Sinav()
        {
            var sinav = SinavGetir();

            var sorular = sinav.Sorular.Select(s => new
            {
                s.Metin,
                s.Secenekler,
                s.Puan,
                s.Sira
            }).ToList();

            return Json(new {
                sinav.Baslik, sinav.Aciklama, sinav.Sure, Sorular = sorular
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SinavSonuc()
        {
            var sinav = SinavGetir();
            return Json(sinav, JsonRequestBehavior.AllowGet);
        }

        private static Sinav SinavGetir()
        {
            var sinav = new Sinav
            {
                Baslik = "Deneme Sınavı",
                Aciklama = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec eu mattis lorem, ac iaculis elit. Nam et facilisis arcu. Suspendisse mollis quam quam, ac scelerisque urna viverra at. In eget consectetur lectus, sit amet consectetur orci. Fusce ullamcorper imperdiet quam iaculis hendrerit. Morbi ultricies facilisis purus, eget venenatis dui varius ut. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed dictum convallis ligula, non iaculis lacus fringilla id.",
                Sure = 10,
                Sorular = new List<Soru>()
            };

            //sinav.Sorular.Add(new Soru
            //{
            //    Metin = "Dünya düz müdür?",
            //    Secenekler = new List<string> { "Evet", "Hayır" },
            //    DogruCevap = 0,
            //    Puan = 10,
            //    Sira = 1
            //});

            //sinav.Sorular.Add(new Soru
            //{
            //    Metin = "Ay dünyanın uydusu mudur?",
            //    Secenekler = new List<string> { "Evet", "Hayır" },
            //    DogruCevap = 0,
            //    Puan = 10,
            //    Sira = 1
            //});

            //sinav.Sorular.Add(new Soru
            //{
            //    Metin = "Javascript en iyi programlama dilidir?",
            //    Secenekler = new List<string> { "Evet", "Hayır" },
            //    DogruCevap = 1,
            //    Puan = 10,
            //    Sira = 1
            //});

            return sinav;
        }
    }
}