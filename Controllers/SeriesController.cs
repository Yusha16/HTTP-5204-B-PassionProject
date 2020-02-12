using System;
using System.Collections.Generic;
using System.Data;
//required for SqlParameter class
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DeckBuilder.Data;
using DeckBuilder.Models;
using System.Diagnostics;

namespace DeckBuilder.Controllers
{
    public class SeriesController : Controller
    {
        private DeckBuilderContext db = new DeckBuilderContext();

        // GET: Series
        public ActionResult Index()
        {
            return View();
        }

        // GET: Series
        public ActionResult List()
        {
            List<Series> series = db.Series.SqlQuery("SELECT * FROM Series").ToList();
            return View(series);
        }

        //Show Series Detail
        public ActionResult Show(int? id)
        {
            //Debug Purpose to see if we are getting the id
            Debug.WriteLine("I'm pulling data of " + id.ToString());

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Query statement to get the specific Species
            string query = "SELECT * from series where seriesid=@SeriesID";
            SqlParameter sqlparam = new SqlParameter("@SeriesID", id);

            //Get the Specific Series
            Series selectedSeries = db.Series.SqlQuery(query, sqlparam).FirstOrDefault();

            if (selectedSeries == null)
            {
                return HttpNotFound();
            }

            //Show the result
            return View(selectedSeries);
        }

        //When user lands on the Add page (not submitting form)
        public ActionResult Add()
        {
            return View();
        }

        //When user submits the form to add a new Series
        [HttpPost]
        public ActionResult Add(string SeriesName, string SeriesCode)
        {
            //Debug Purpose to see if we are getting the data
            Debug.WriteLine("I'm pulling data of " + SeriesName + " and " + SeriesCode);

            //The query to add a new Series
            string query = "INSERT INTO series (SeriesName, SeriesCode) VALUES (@SeriesName, @SeriesCode)";
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@SeriesName", SeriesName);
            sqlparams[1] = new SqlParameter("@SeriesCode", SeriesCode);

            //Run the sql command
            db.Database.ExecuteSqlCommand(query, sqlparams);

            //Go back to the list of Series to see the added Series
            return RedirectToAction("List");
        }

        //When user lands on the Update page (not submitting form)
        public ActionResult Update(int id)
        {
            //Debug Purpose to see if we are getting the id
            Debug.WriteLine("I'm pulling data of " + id.ToString());

            //Query statement to select the specific Series
            string query = "SELECT * FROM series WHERE SeriesID = @SeriesID";
            SqlParameter sqlparam = new SqlParameter("@SeriesID", id);

            //The query is returning a list, so we only want the first one
            Series selectedseries = db.Series.SqlQuery(query, sqlparam).FirstOrDefault();

            //read the species data
            return View(selectedseries);
        }

        //When user submits the form to update the specific Series
        [HttpPost]
        public ActionResult Update(int id, string SeriesName, string SeriesCode)
        {
            //Debug Purpose to see if we are getting the data
            Debug.WriteLine("I'm pulling data of " + id.ToString() + ", " + SeriesName + ", and " + SeriesCode);

            //Query statement to update the specific Series
            string query = "UPDATE series";
            query += " SET SeriesName = @SeriesName, SeriesCode = @SeriesCode";
            query += " WHERE SeriesID = @SeriesID";

            SqlParameter[] sqlparams = new SqlParameter[3];
            sqlparams[0] = new SqlParameter("@SeriesName", SeriesName);
            sqlparams[1] = new SqlParameter("@SeriesCode", SeriesCode);
            sqlparams[2] = new SqlParameter("@SeriesID", id);

            //Execute query command
            db.Database.ExecuteSqlCommand(query, sqlparams);

            //Go back to the list of Series to see our changes
            return RedirectToAction("List");
        }

        //When user clicks on the Delete button (hidden form submission)
        [HttpPost]
        public ActionResult Delete(int id)
        {
            //Debug Purpose to see if we are getting the id
            Debug.WriteLine("I'm pulling data of " + id.ToString());

            //Query statement to delete the specific Series
            string query = "DELETE FROM series WHERE SeriesID = @SeriesID";

            SqlParameter sqlparam = new SqlParameter("@SeriesID", id);

            //Execute query command
            db.Database.ExecuteSqlCommand(query, sqlparam);

            //Go back to List of Series
            return RedirectToAction("List");
        }
    }
}