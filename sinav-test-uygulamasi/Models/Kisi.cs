using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sinav_test_uygulamasi.Models
{
    public class Kisi
    {
        public string Isim { get; set; }
        public string Soyisim { get; set; }
        public string TcKimlikNo { get; set; }
        public List<SinavSonuc> SinavSonuclari { get; set; }
    }
}