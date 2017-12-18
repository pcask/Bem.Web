using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class TiyatroController : Controller
    {
        // GET: Tiyatro
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Guncel()
        {
            var tiyatroListesi = new List<Etkinlik>();

            foreach (var tiyatro in EtkinlikRepository.ListeyiDoldur())
            {
                if (tiyatro.EtkinlikTuru == EtkinlikTuru.Tiyatro)
                {
                    tiyatroListesi.Add(tiyatro);
                }
            }

            return View("~/views/_shared/guncel.cshtml", tiyatroListesi);
        }
    }
}