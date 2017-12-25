function Kisi(isim, soyIsim, tcKimlik) {
    this.Isim = isim;
    this.SoyIsim = soyIsim;
    this.TcKimlikNo = tcKimlik;
}

function Soru(metin, dogruCevap, secenekler, sira, puan) {
    var _dogruCevap = dogruCevap;
    var _puan = puan;

    this.Metin = metin;
    this.Sira = sira;
    this.Secenekler = secenekler;
    this.VerileCevap = null;
    this.PuaniSoyle = function () {
        return _puan;
    }

    this.Cevapla = function (cevap) {
        this.VerileCevap = cevap;
    }

    this.CevapDogrumu = function () {
        return _dogruCevap === this.VerileCevap;
    }

    this.AldigiPuanSoyle = function () {
        return (this.CevapDogrumu()) ? _puan : 0;
    }
}

function Sinav(baslik, sure, aciklama) {
    var sinavAktifMi = false;

    this.Sure = sure;
    this.Baslik = baslik;
    this.Aciklama = aciklama;
    this.Kisi = new Kisi();
    this.Sorular = [];

    this.Basla = function () {
        sinavAktifMi = true;
    };

    this.Bitir = function () {
        sinavAktifMi = false
    };

    this.PuanHesapla = function () {
        var toplam = 0;

        for (var i = 0; i < this.Sorular.length; i++) {
            toplam += this.Sorular[i].AldigiPuanSoyle();
        }

        return toplam;
    };

    this.SoruEkle = function (soru) {
        this.Sorular.push(soru);
    };
}

var sinav = new Sinav('Deneme Sınavı', 1, 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec eu mattis lorem, ac iaculis elit. Nam et facilisis arcu. Suspendisse mollis quam quam, ac scelerisque urna viverra at. In eget consectetur lectus, sit amet consectetur orci. Fusce ullamcorper imperdiet quam iaculis hendrerit. Morbi ultricies facilisis purus, eget venenatis dui varius ut. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed dictum convallis ligula, non iaculis lacus fringilla id.');
sinav.Kisi = new Kisi("Ahmet", "Hamdi", "12345678978");

sinav.SoruEkle(new Soru("<h3>Dünya yuvarlak mıdır?</h3>", 2, ["Evet", "Hayır", "Fikrim Yok"], 0, 10));
sinav.SoruEkle(new Soru("<h3>Ay Dünya'nın uydusu mudur?</h3>", 0, ["Doğru", "Yanlış"], 1, 10));
sinav.SoruEkle(new Soru("<h3>Javascript en güzel programlama dilidir?</h3>", 1, ["Farklı", "Farksız"], 2, 10));
sinav.SoruEkle(new Soru("<h3>Su sağlıktır?</h3>", 0, ["Dandanakan", "Erikli"], 3, 10));
sinav.SoruEkle(new Soru("<h3>Sigara faydalıdır?</h3>", 1, ["Marlboro", "Marlyn Monroe"], 4, 10));
sinav.SoruEkle(new Soru("<h3>Welcome to the course?</h3>", 1, ["For instance", "To Death"], 5, 10));
sinav.SoruEkle(new Soru("<h3>Im in the eye of the storm?</h3>", 1, ["Rambo 3", "One Piece"], 6, 10));
sinav.SoruEkle(new Soru("<h3>Just waiting for the moment?</h3>", 1, ["Zoro's Fight", "Shank's Appearence"], 7, 10));


//sinav.Sorular[0].Cevapla(0);//d
//sinav.Sorular[1].Cevapla(1);//y
//sinav.Sorular[2].Cevapla(1);//d
//sinav.Sorular[3].Cevapla(0);//d
//sinav.Sorular[4].Cevapla(0);//y