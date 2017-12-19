using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class MuzikController : Controller
    {
        // GET: Tiyatro
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Guncel()
        {
            var muzikListesi = new List<Etkinlik>();

            foreach (var muzik in EtkinlikRepository.ListeyiDoldur())
            {
                if (muzik.EtkinlikTuru == EtkinlikTuru.Muzik)
                {
                    muzikListesi.Add(muzik);
                }
            }

            return View("~/views/_shared/guncel.cshtml", muzikListesi);
        }
    }
}