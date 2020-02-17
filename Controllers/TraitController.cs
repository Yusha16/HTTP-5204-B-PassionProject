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
    public class TraitController : Controller
    {
        private DeckBuilderContext db = new DeckBuilderContext();

        // GET: Trait
        public ActionResult Index()
        {
            return View();
        }

        // GET: Trait
        public ActionResult List()
        {
            List<Trait> traits = db.Traits.SqlQuery("SELECT * FROM Traits").ToList();
            return View(traits);
        }

        //Show Trait Detail
        public ActionResult Show(int? id)
        {
            //Debug Purpose to see if we are getting the id
            Debug.WriteLine("I'm pulling data of " + id.ToString());

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Query statement to get the specific Species
            string query = "SELECT * FROM traits WHERE traitid=@TraitID";
            SqlParameter sqlparam = new SqlParameter("@TraitID", id);

            //Get the Specific Series
            Trait selectedTrait = db.Traits.SqlQuery(query, sqlparam).FirstOrDefault();

            if (selectedTrait == null)
            {
                return HttpNotFound();
            }

            //Show the result
            return View(selectedTrait);
        }

        //When user lands on the Add page (not submitting form)
        public ActionResult Add()
        {
            return View();
        }

        //When user submits the form to add a new Trait
        [HttpPost]
        public ActionResult Add(string TraitName)
        {
            //Debug Purpose to see if we are getting the data
            Debug.WriteLine("I'm pulling data of " + TraitName);

            //The query to add a new Trait
            string query = "INSERT INTO traits (TraitName) VALUES (@TraitName)";
            SqlParameter sqlparam = new SqlParameter("@TraitName", TraitName);

            //Run the sql command
            db.Database.ExecuteSqlCommand(query, sqlparam);

            //Go back to the list of Trait to see the added Trait
            return RedirectToAction("List");
        }

        //When user lands on the Update page (not submitting form)
        public ActionResult Update(int id)
        {
            //Debug Purpose to see if we are getting the id
            Debug.WriteLine("I'm pulling data of " + id.ToString());

            //Query statement to select the specific Trait
            string query = "SELECT * FROM traits WHERE TraitID = @TraitID";
            SqlParameter sqlparam = new SqlParameter("@TraitID", id);

            //The query is returning a list, so we only want the first one
            Trait selectedTrait = db.Traits.SqlQuery(query, sqlparam).FirstOrDefault();

            //read the trait data
            return View(selectedTrait);
        }

        //When user submits the form to update the specific Trait
        [HttpPost]
        public ActionResult Update(int id, string TraitName)
        {
            //Debug Purpose to see if we are getting the data
            Debug.WriteLine("I'm pulling data of " + id.ToString() + ", " + TraitName);

            //Query statement to update the specific Trait
            string query = "UPDATE traits";
            query += " SET TraitName = @TraitName";
            query += " WHERE TraitID = @TraitID";

            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@TraitName", TraitName);
            sqlparams[1] = new SqlParameter("@TraitID", id);

            //Execute query command
            db.Database.ExecuteSqlCommand(query, sqlparams);

            //Go back to the list of Trait to see our changes
            return RedirectToAction("List");
        }

        //When user clicks on the Delete button (hidden form submission)
        [HttpPost]
        public ActionResult Delete(int id)
        {
            //Debug Purpose to see if we are getting the id
            Debug.WriteLine("I'm pulling data of " + id.ToString());

            //Query statement to delete the specific Trait
            string query = "DELETE FROM traits WHERE TraitID = @TraitID";

            SqlParameter sqlparam = new SqlParameter("@TraitID", id);

            //Execute query command
            db.Database.ExecuteSqlCommand(query, sqlparam);

            //Go back to List of Trait
            return RedirectToAction("List");
        }
    }
}