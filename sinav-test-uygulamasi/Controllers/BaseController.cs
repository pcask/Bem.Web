using sinav_test_uygulamasi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sinav_test_uygulamasi.Controllers
{
    public class BaseController : Controller
    {
        internal Sinav SinavBul(string url)
        {
            string connstr = "Data Source=DESKTOP-S3O5AOR;Initial Catalog=SinavYonetim;Integrated Security=True";
            using (var connection = new SqlConnection(connstr))
            {
                var command = new SqlCommand("SELECT * FROM Exams WHERE Url = @url", connection);
                command.Parameters.AddWithValue("@url", url);
                connection.Open();

                using (var rdr = command.ExecuteReader())
                {
                    rdr.Read();

                    return new Sinav { Baslik = rdr["title"].ToString(), Aciklama = rdr["description"].ToString(), StartDate = (DateTime)rdr["startDate"], Sure = Convert.ToInt32(rdr["Duration"]) };
                }
            }
        }
    }
}