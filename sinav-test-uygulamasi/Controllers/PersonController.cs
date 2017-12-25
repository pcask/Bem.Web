using sinav_test_uygulamasi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sinav_test_uygulamasi.Controllers
{
    public class PersonController : BaseController
    {
        // GET: Person
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PersonsExams()
        {
            var examList = new List<Sinav>();
            string connstr = "Data Source=DESKTOP-S3O5AOR;Initial Catalog=SinavYonetim;Integrated Security=True";


            using (var connection = new SqlConnection(connstr))
            {
                var command = new SqlCommand("SELECT * FROM Exams WHERE Id IN (SELECT DISTINCT ExamId FROM Answers WHERE PersonId = @PersonId)", connection);
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