using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sinav_test_uygulamasi.Models
{
    public class Sinav
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public int Sure { get; set; }
        public List<Soru> Sorular { get; set; }
        public List<SinavSonuc> SinavSonuclari { get; set; }
        public DateTime StartDate { get; set; }
    }
}