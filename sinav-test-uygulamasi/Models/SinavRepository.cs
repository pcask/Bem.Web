using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace sinav_test_uygulamasi.Models
{
    public class SinavRepository
    {
        //private static Dictionary<string, Sinav> SinavGetir()
        //{
        //    //Dictionary<string, Sinav> sinavlar = new Dictionary<string, Sinav>();

        //    //sinavlar.Add("deneme-sinavi-1",Sinav1());
        //    //sinavlar.Add("deneme-sinavi-2", Sinav2());

        //    //return sinavlar;
        //}

        //private static Sinav Sinav1()
        //{
        //    var sinav = new Sinav
        //    {
        //        Baslik = "Deneme Sınavı",
        //        Aciklama = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec eu mattis lorem, ac iaculis elit. Nam et facilisis arcu. Suspendisse mollis quam quam, ac scelerisque urna viverra at. In eget consectetur lectus, sit amet consectetur orci. Fusce ullamcorper imperdiet quam iaculis hendrerit. Morbi ultricies facilisis purus, eget venenatis dui varius ut. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed dictum convallis ligula, non iaculis lacus fringilla id.",
        //        Sure = 10,
        //        Sorular = new List<Soru>()
        //    };

        //    sinav.Sorular.Add(new Soru
        //    {
        //        Metin = "Dünya düz müdür?",
        //        Secenekler = new List<string> { "Evet", "Hayır" },
        //        DogruCevap = 0,
        //        Puan = 10,
        //        Sira = 1
        //    });

        //    sinav.Sorular.Add(new Soru
        //    {
        //        Metin = "Ay dünyanın uydusu mudur?",
        //        Secenekler = new List<string> { "Evet", "Hayır" },
        //        DogruCevap = 0,
        //        Puan = 10,
        //        Sira = 1
        //    });

        //    sinav.Sorular.Add(new Soru
        //    {
        //        Metin = "Javascript en iyi programlama dilidir?",
        //        Secenekler = new List<string> { "Evet", "Hayır" },
        //        DogruCevap = 1,
        //        Puan = 10,
        //        Sira = 1
        //    });

        //    return sinav;
        //}

        private static SqlConnection CreateConnection()
        {
            // Ev
            return new SqlConnection(@"Data Source=PCASK\MSSQLSERVER2016D;Initial Catalog=SinavYonetim;Integrated Security=True");

            // Sinif
            //return new SqlConnection(@"Data Source=DESKTOP-S3O5AOR;Initial Catalog=SinavYonetim;Integrated Security=True");
        }

        internal static List<Soru> SinavaAitSorulariGetir(string sinavAdi)
        {
            using (var conn = CreateConnection())
            {
                var command = new SqlCommand("SELECT * FROM Questions as q WHERE q.SinavId IN(SELECT e.Id  FROM[SinavYonetim].[dbo].[Exams] as e  WHERE e.Url = @url)", conn);
                command.Parameters.AddWithValue("@url", sinavAdi);

                conn.Open();

                List<Soru> sorular = new List<Soru>();
                using (var rdr = command.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        sorular.Add(new Soru
                        {
                            Id = Convert.ToInt32(rdr["Id"]),
                            Metin = rdr["text"].ToString(),
                            Secenekler = new List<Secenek>()
                        });
                    }
                }


                foreach (var soru in sorular)
                {
                    var commandQ = new SqlCommand("SELECT * FROM Options WHERE QuestionId =  @qId", conn);
                    commandQ.Parameters.AddWithValue("@qId", soru.Id);

                    using (var rdr = commandQ.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            soru.Secenekler.Add(new Secenek { Id = Convert.ToInt32(rdr["Id"]), Text = rdr["Text"].ToString() });
                        }
                    }
                }

                return sorular;
            }
        }

        //private static Sinav Sinav2()
        //{
        //    var sinav = new Sinav
        //    {
        //        Baslik = "Deneme Sınavı 2",
        //        Aciklama = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec eu mattis lorem, ac iaculis elit. Nam et facilisis arcu. Suspendisse mollis quam quam, ac scelerisque urna viverra at. In eget consectetur lectus, sit amet consectetur orci.",
        //        Sure = 10,
        //        Sorular = new List<Soru>()
        //    };

        //    sinav.Sorular.Add(new Soru
        //    {
        //        Metin = "Dünya düz müdür?",
        //        Secenekler = new List<string> { "Evet", "Hayır" },
        //        DogruCevap = 0,
        //        Puan = 10,
        //        Sira = 1
        //    });

        //    sinav.Sorular.Add(new Soru
        //    {
        //        Metin = "Ay dünyanın uydusu mudur?",
        //        Secenekler = new List<string> { "Evet", "Hayır" },
        //        DogruCevap = 0,
        //        Puan = 10,
        //        Sira = 1
        //    });

        //    sinav.Sorular.Add(new Soru
        //    {
        //        Metin = "Javascript en iyi programlama dilidir?",
        //        Secenekler = new List<string> { "Evet", "Hayır" },
        //        DogruCevap = 1,
        //        Puan = 10,
        //        Sira = 1
        //    });

        //    return sinav;
        //}


        public static Sinav SinavBul(string key)
        {
            var sinav = new Sinav();

            using (var connection = CreateConnection())
            {
                var command = new SqlCommand("SELECT * FROM Exams WHERE Url = @url", connection);
                command.Parameters.AddWithValue("@url", key);
                connection.Open();

                using (var rdr = command.ExecuteReader())
                {
                    rdr.Read();

                    sinav = new Sinav {Id = Convert.ToInt32(rdr["Id"]), Baslik = rdr["title"].ToString(), Aciklama = rdr["description"].ToString(), StartDate = (DateTime)rdr["startDate"], Sure = Convert.ToInt32(rdr["Duration"]) };

                    return sinav;
                }
            }
        }

        public static List<Secenek> KayitliCevaplariGetir(int sinavId, int kullaniciId)
        {
            using(var con = CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Answers WHERE ExamId = @eId AND PersonId = @pId", con);
                cmd.Parameters.AddWithValue("@eId", sinavId);
                cmd.Parameters.AddWithValue("@pId", kullaniciId);

                con.Open();

               SqlDataReader sdr = cmd.ExecuteReader();

                List<Secenek> secenekler = new List<Secenek>();

                while (sdr.Read())
                {
                    secenekler.Add(new Secenek
                    {
                        Id = Convert.ToInt32(sdr["OptionId"]),
                        Question = new Soru { Id = Convert.ToInt32(sdr["QuestionId"]) }
                    });
                }

                return secenekler;
            }
        }

        public static bool SoruyaCevapVer(int sinavId, int soruId, int secenekId, int kullaniciId)
        {
            using (var connection = CreateConnection())
            {
                var command = new SqlCommand("SELECT Id FROM Answers WHERE PersonId = @pId AND QuestionId= @qId AND ExamId = @eId", connection);
                command.Parameters.AddWithValue("@pId", kullaniciId);
                command.Parameters.AddWithValue("@eId", sinavId);
                command.Parameters.AddWithValue("@qId", soruId);
                connection.Open();


                int id = Convert.ToInt32(command.ExecuteScalar());
                if(id < 1)
                {
                    var insertCommand = new SqlCommand("INSERT INTO Answers (ExamId, PersonId, QuestionId, OptionId) VALUES (@eId, @pId, @qId, @oId)", connection);

                    insertCommand.Parameters.AddWithValue("@pId", kullaniciId);
                    insertCommand.Parameters.AddWithValue("@eId", sinavId);
                    insertCommand.Parameters.AddWithValue("@qId", soruId);
                    insertCommand.Parameters.AddWithValue("@oId", secenekId);

                    return insertCommand.ExecuteNonQuery() >0;
                }
                else
                {
                    var updateCommand = new SqlCommand("UPDATE Answers SET OptionId =  @oId WHERE Id=@id", connection);

                    updateCommand.Parameters.AddWithValue("@id", id);
                    updateCommand.Parameters.AddWithValue("@oId", secenekId);

                    return updateCommand.ExecuteNonQuery()>0;
                }
            }
        }
    }
}