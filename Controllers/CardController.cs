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
using DeckBuilder.Models.ViewModels;
using System.Diagnostics;

namespace PassionProject.Controllers
{
    public class CardController : Controller
    {
        // GET: Card
        public ActionResult Index()
        {
            return View();
        }

        private DeckBuilderContext db = new DeckBuilderContext();

        // GET: Card
        public ActionResult List()
        {
            List<Card> cards = db.Cards.SqlQuery("SELECT * FROM Cards").ToList();
            return View(cards);
        }

        //Show Card Detail
        public ActionResult Show(int? id)
        {
            //Debug Purpose to see if we are getting the id
            Debug.WriteLine("I'm pulling data of " + id.ToString());

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Query statement to get the specific Card
            string query = "SELECT * FROM cards WHERE cardid=@CardID";
            SqlParameter sqlparam = new SqlParameter("@CardID", id);

            //Get the Specific Card
            Card selectedCard = db.Cards.SqlQuery(query, sqlparam).FirstOrDefault();

            //Get the all the trait that the card has
            string aside_query = "SELECT * FROM Traits INNER JOIN TraitCards ON Traits.TraitID = TraitCards.Trait_TraitID WHERE TraitCards.Card_CardID=@id";
            var fk_parameter = new SqlParameter("@id", id);
            List<Trait> traits = db.Traits.SqlQuery(aside_query, fk_parameter).ToList();

            //ViewModel for the AddCard
            ShowCard viewmodel = new ShowCard();
            viewmodel.card = selectedCard;
            viewmodel.traits = traits;

            if (selectedCard == null)
            {
                return HttpNotFound();
            }

            //Show the result
            return View(viewmodel);
        }

        //When user lands on the Add page (not submitting form)
        public ActionResult Add()
        {
            List<Series> series = db.Series.SqlQuery("SELECT * FROM Series").ToList();
            List<Trait> traits = db.Traits.SqlQuery("SELECT * FROM Traits").ToList();

            //ViewModel for the AddCard
            AddCard viewmodel = new AddCard();
            viewmodel.allSeries = series;
            viewmodel.allTraits = traits;

            return View(viewmodel);
        }

        //When user submits the form to add a new Card
        [HttpPost]
        public ActionResult Add(string CardName, string CardColour, int SeriesID, int TraitID1 = -1, int TraitID2 = -1)
        {
            //Debug Purpose to see if we are getting the data
            Debug.WriteLine("I'm pulling data of " + CardName + ", " + CardColour + ", " + SeriesID + ", " + TraitID1  + ", and " + TraitID2);

            //The query to add a new Trait
            string query = "INSERT INTO cards (CardName, CardColour, SeriesID) VALUES (@CardName, @CardColour, @SeriesID)";
            SqlParameter[] sqlparams = new SqlParameter[3];
            sqlparams[0] = new SqlParameter("@CardName", CardName);
            sqlparams[1] = new SqlParameter("@CardColour", CardColour);
            sqlparams[2] = new SqlParameter("@SeriesID", SeriesID);

            //Run the sql command
            db.Database.ExecuteSqlCommand(query, sqlparams);

            //Get the id
            List<Card> cards = db.Cards.SqlQuery("SELECT * FROM Cards").ToList();
            int id = cards[cards.Count - 1].CardID;

            if (TraitID1 != -1) { 
                //Add the trait to the Card
                query = "INSERT INTO TraitCards (Trait_TraitID, Card_CardID) VALUES (@CardID, @TraitID)";
                sqlparams = new SqlParameter[2];
                sqlparams[0] = new SqlParameter("@CardID", id);
                sqlparams[1] = new SqlParameter("@TraitID", TraitID1);
                db.Database.ExecuteSqlCommand(query, sqlparams);
            }
            if (TraitID2 != -1 && TraitID1 != TraitID2)
            {
                //Add the trait to the Card
                query = "INSERT INTO TraitCards (Trait_TraitID, Card_CardID) VALUES (@CardID, @TraitID)";
                sqlparams = new SqlParameter[2];
                sqlparams[0] = new SqlParameter("@CardID", id);
                sqlparams[1] = new SqlParameter("@TraitID", TraitID2);
                db.Database.ExecuteSqlCommand(query, sqlparams);
            }

            //Go back to the list of Trait to see the added Trait
            return RedirectToAction("List");
        }

        //When user lands on the Update page (not submitting form)
        public ActionResult Update(int id)
        {
            //Debug Purpose to see if we are getting the id
            Debug.WriteLine("I'm pulling data of " + id.ToString());

            //Query statement to select the specific Card
            string query = "SELECT * FROM cards WHERE CardID = @CardID";
            SqlParameter sqlparam = new SqlParameter("@CardID", id);

            //The query is returning a list, so we only want the first one
            Card selectedCard = db.Cards.SqlQuery(query, sqlparam).FirstOrDefault();

            UpdateCard viewModel = new UpdateCard();
            viewModel.card = selectedCard;

            List<Series> allSeries = db.Series.SqlQuery("SELECT * FROM Series").ToList();
            List<Trait> allTraits = db.Traits.SqlQuery("SELECT * FROM Traits").ToList();
            viewModel.allSeries = allSeries;
            viewModel.allTraits = allTraits;

            //Get the all the trait that the card has
            string aside_query = "SELECT * FROM Traits INNER JOIN TraitCards ON Traits.TraitID = TraitCards.Trait_TraitID WHERE TraitCards.Card_CardID=@id";
            var fk_parameter = new SqlParameter("@id", id);
            List<Trait> traits = db.Traits.SqlQuery(aside_query, fk_parameter).ToList();
            viewModel.traits = traits;

            //read the view model data
            return View(viewModel);
        }

        //When user submits the form to update the specific Card
        [HttpPost]
        public ActionResult Update(int id, string CardName, string CardColour, int SeriesID, int TraitID1 = -1, int TraitID2 = -1)
        {
            //Debug Purpose to see if we are getting the data
            Debug.WriteLine("I'm pulling data of " + CardName + ", " + CardColour + ", " + SeriesID + ", " + TraitID1 + ", and " + TraitID2);

            //Query statement to update the specific Card
            string query = "UPDATE cards";
            query += " SET CardName = @CardName, CardColour = @CardColour, SeriesID = @SeriesID";
            query += " WHERE CardID = @CardID";

            SqlParameter[] sqlparams = new SqlParameter[4];
            sqlparams[0] = new SqlParameter("@CardName", CardName);
            sqlparams[1] = new SqlParameter("@CardColour", CardColour);
            sqlparams[2] = new SqlParameter("@SeriesID", SeriesID);
            sqlparams[3] = new SqlParameter("@CardID", id);

            //Execute query command
            db.Database.ExecuteSqlCommand(query, sqlparams);

            //Delete Add





            //Go back to the list of Trait to see our changes
            return RedirectToAction("List");
        }


    }
}