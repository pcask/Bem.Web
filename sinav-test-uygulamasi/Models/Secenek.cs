namespace sinav_test_uygulamasi.Models
{
    public class Secenek
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Soru Question { get; set; }
    }
}