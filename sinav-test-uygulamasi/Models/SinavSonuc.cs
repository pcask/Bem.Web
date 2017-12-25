using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sinav_test_uygulamasi.Models
{
    public class SinavSonuc
    {
        public Kisi Kisi { get; set; }
        public Sinav Sinav { get; set; }
        public Dictionary<Soru, int> Cevaplar { get; set; }
    }
}