using Newtonsoft.Json;
using sinav_test_uygulamasi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sinav_test_uygulamasi.Controllers
{
    public class SinavController : BaseController
    {
        // GET: Sinav
        public ActionResult Index(string sinavAdi)
        {
            var sinavSonuc = new SinavSonuc();

            sinavSonuc.Sinav = SinavRepository.SinavBul(sinavAdi);
            sinavSonuc.Kisi = new Kisi
            {
                Isim = "Ahmet",
                Soyisim = "Mehmet",
                TcKimlikNo = "12345678912"
            };
            ViewBag.SinavAdi = sinavAdi;
            return View(sinavSonuc);
        }

        public ActionResult Cevapla(string sinavAdi)
        {
            var sinavSonuc = new SinavSonuc();

            sinavSonuc.Sinav = SinavRepository.SinavBul("deneme-sinavi-1");
            sinavSonuc.Kisi = new Kisi
            {
                Isim = "Ahmet",
                Soyisim = "Mehmet",
                TcKimlikNo = "12345678912"
            };
            ViewBag.SinavAdi = sinavAdi;


            //return Content(JsonConvert.SerializeObject(sinavSonuc));
            return Json(sinavSonuc, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SorulariGetir(string sinavAdi)
        {
            List<Soru> sorular = SinavRepository.SinavaAitSorulariGetir(sinavAdi);

            return Json(sorular, JsonRequestBehavior.AllowGet);

        }

        public JsonResult SoruyaCevapVer(int sinavId, int soruId, int secenekId)
        {
            var result = SinavRepository.SoruyaCevapVer(sinavId, soruId, secenekId, 1);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult KayitliCevaplariGetir(int sinavId)
        {
            List<Secenek> secenekler = SinavRepository.KayitliCevaplariGetir(sinavId, 1);

            return Json(secenekler, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SinavListesi()
        {
            return View();
        }


        public ActionResult SinavDurumu()
        {
            var examList = new List<Sinav>();

            string connstr = "Data Source=DESKTOP-S3O5AOR;Initial Catalog=SinavYonetim;Integrated Security=True";


            using (var connection = new SqlConnection(connstr))
            {
                var command = new SqlCommand("SELECT * FROM Exams", connection);
                connection.Open();

                using (var rdr = command.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        examList.Add(new Sinav { Baslik = rdr["title"].ToString(), Aciklama = rdr["description"].ToString(), StartDate = (DateTime)rdr["startDate"], Sure = Convert.ToInt32(rdr["Duration"]) });
                    }
                }
            }

            return View("~/views/shared/examlist.cshtml", examList);
        }
    }
}