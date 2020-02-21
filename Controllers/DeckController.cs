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

namespace DeckBuilder.Controllers
{
    public class DeckController : Controller
    {
        // GET: Deck
        public ActionResult Index()
        {
            return View();
        }

        private DeckBuilderContext db = new DeckBuilderContext();

        // GET: Deck
        public ActionResult List()
        {
            List<Deck> decks = db.Decks.SqlQuery("SELECT * FROM Decks").ToList();
            return View(decks);
        }

        //Show Deck Detail
        public ActionResult Show(int? id)
        {
            //Debug Purpose to see if we are getting the id
            Debug.WriteLine("I'm pulling data of " + id.ToString());

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Query statement to get the specific Deck
            string query = "SELECT * FROM decks WHERE deckid=@DeckID";
            SqlParameter sqlparam = new SqlParameter("@DeckID", id);

            //Get the Specific Deck
            Deck selectedDeck = db.Decks.SqlQuery(query, sqlparam).FirstOrDefault();

            //Get all the cards that the deck has
            string aside_query = "SELECT * FROM Cards INNER JOIN DeckCards ON Cards.CardID = DeckCards.Card_CardID WHERE DeckCards.Deck_DeckID=@id ORDER BY Quantity DESC";
            var fk_parameter = new SqlParameter("@id", id);
            List<Card> cards = db.Cards.SqlQuery(aside_query, fk_parameter).ToList();

            //Get the Quantity Amount
            aside_query = "SELECT quantity FROM Cards INNER JOIN DeckCards ON Cards.CardID = DeckCards.Card_CardID WHERE DeckCards.Deck_DeckID=@id ORDER BY Quantity DESC";
            fk_parameter = new SqlParameter("@id", id);
            List<int> quantities = db.Database.SqlQuery<int>(aside_query, fk_parameter).ToList();

            //Get all cards
            List<Card> allCards = db.Cards.SqlQuery("SELECT * FROM Cards").ToList();

            //ViewModel for the ShowDeck
            ShowDeck viewModel = new ShowDeck();
            viewModel.deck = selectedDeck;
            viewModel.cards = cards;
            viewModel.cardQuantity = quantities;
            viewModel.allCards = allCards;

            if (selectedDeck == null)
            {
                return HttpNotFound();
            }

            //Show the result
            return View(viewModel);
        }

        /*Attempt for Pop up box asking how many to add/delete*/
        /*
        [HttpPost]
        public ActionResult AskHowMany(int id, int CardID)
        {
            HowMany howMany = new HowMany();
            howMany.DeckID = id;
            howMany.CardID = CardID;

            //Get the Quantity
            string query = "SELECT quantity FROM deckcards WHERE Deck_DeckID=@DeckID AND Card_CardID=@CardID";
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@DeckID", id);
            sqlparams[1] = new SqlParameter("@CardID", CardID);
            int quantity = db.Database.ExecuteSqlCommand(query, sqlparams);
            
            //No such card in your deck then
            if (quantity < 0) {
                quantity = 0;
            }

            howMany.CurrentQuantity = quantity;

            return PartialView("Show", howMany);
        }
        */

        //When user removes a card to the deck (click on remove card)
        [HttpPost]
        public ActionResult RemoveCard(int id, int CardID)
        {
            //Debug Purpose to see if we are getting the data
            Debug.WriteLine("I'm pulling data of " + id.ToString() + ", " + CardID);

            //Get the Quantity Amount
            string query = "SELECT quantity FROM deckcards WHERE Deck_DeckID=@DeckID AND Card_CardID=@CardID";
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@DeckID", id);
            sqlparams[1] = new SqlParameter("@CardID", CardID);
            int quantity = db.Database.SqlQuery<int>(query, sqlparams).FirstOrDefault();

            //Remove the last copy of the card in the deck
            if (quantity == 1)
            {
                //Query statement to delete the specific Deck
                query = "DELETE FROM DeckCards WHERE Deck_DeckID = @DeckID AND Card_CardID = @CardID";
                sqlparams = new SqlParameter[2];
                sqlparams[0] = new SqlParameter("@DeckID", id);
                sqlparams[1] = new SqlParameter("@CardID", CardID);
                db.Database.ExecuteSqlCommand(query, sqlparams);
            }
            //Remove a single copy of the card
            else
            {
                //Update the quantity column
                quantity--;

                //Query statement to update the specific DeckCards
                query = "UPDATE DeckCards";
                query += " SET Quantity = @Quantity";
                query += " WHERE Deck_DeckID = @DeckID AND Card_CardID = @CardID";

                sqlparams = new SqlParameter[3];
                sqlparams[0] = new SqlParameter("@Quantity", quantity);
                sqlparams[1] = new SqlParameter("@DeckID", id);
                sqlparams[2] = new SqlParameter("@CardID", CardID);

                //Execute query command
                db.Database.ExecuteSqlCommand(query, sqlparams);
            }

            //Refresh the page
            return RedirectToAction("Show/" + id);
        }

        //When user adds a card to the deck (click on add card)
        [HttpPost]
        public ActionResult AddCard(int id, int CardID)
        {
            //Debug Purpose to see if we are getting the data
            Debug.WriteLine("I'm pulling data of " + id.ToString() + ", " + CardID);

            //Get the Quantity Amount
            string query = "SELECT quantity FROM deckcards WHERE Deck_DeckID=@DeckID AND Card_CardID=@CardID";
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@DeckID", id);
            sqlparams[1] = new SqlParameter("@CardID", CardID);
            int quantity = db.Database.SqlQuery<int>(query, sqlparams).FirstOrDefault();
            //No such card in your deck then
            if (quantity < 1)
            {
                //Add the new card to deck
                quantity = 1;
                query = "INSERT INTO DeckCards (Deck_DeckID, Card_CardID, Quantity) VALUES (@DeckID, @CardID, @Quantity)";
                sqlparams = new SqlParameter[3];
                sqlparams[0] = new SqlParameter("@DeckID", id);
                sqlparams[1] = new SqlParameter("@CardID", CardID);
                sqlparams[2] = new SqlParameter("@Quantity", 1);
                db.Database.ExecuteSqlCommand(query, sqlparams);
            }
            //Can only have 4 of the same card in a single deck
            else if (quantity < 4) {
                //Update the quantity column
                quantity++; 

                //Query statement to update the specific DeckCards
                query = "UPDATE DeckCards";
                query += " SET Quantity = @Quantity";
                query += " WHERE Deck_DeckID = @DeckID AND Card_CardID = @CardID";

                sqlparams = new SqlParameter[3];
                sqlparams[0] = new SqlParameter("@Quantity", quantity);
                sqlparams[1] = new SqlParameter("@DeckID", id);
                sqlparams[2] = new SqlParameter("@CardID", CardID);

                //Execute query command
                db.Database.ExecuteSqlCommand(query, sqlparams);
            }

            //Refresh the page
            return RedirectToAction("Show/" + id);
        }

        //When user lands on the Add page (not submitting form)
        public ActionResult Add()
        {
            return View();
        }

        //When user submits the form to add a new Deck
        [HttpPost]
        public ActionResult Add(string DeckName)
        {
            //Debug Purpose to see if we are getting the data
            Debug.WriteLine("I'm pulling data of " + DeckName);

            //The query to add a new Deck
            string query = "INSERT INTO decks (DeckName) VALUES (@DeckName)";
            SqlParameter sqlparam = new SqlParameter("@DeckName", DeckName);

            //Run the sql command
            db.Database.ExecuteSqlCommand(query, sqlparam);

            //Go back to the list of Deck to see the added Deck
            return RedirectToAction("List");
        }

        //When user lands on the Update page (not submitting form)
        public ActionResult Update(int id)
        {
            //Debug Purpose to see if we are getting the id
            Debug.WriteLine("I'm pulling data of " + id.ToString());

            //Query statement to select the specific Deck
            string query = "SELECT * FROM decks WHERE DeckID = @DeckID";
            SqlParameter sqlparam = new SqlParameter("@DeckID", id);

            //The query is returning a list, so we only want the first one
            Deck selectedDeck = db.Decks.SqlQuery(query, sqlparam).FirstOrDefault();

            //read the deck data
            return View(selectedDeck);
        }

        //When user submits the form to update the specific Deck
        [HttpPost]
        public ActionResult Update(int id, string DeckName)
        {
            //Debug Purpose to see if we are getting the data
            Debug.WriteLine("I'm pulling data of " + id.ToString() + ", " + DeckName);

            //Query statement to update the specific Deck
            string query = "UPDATE decks";
            query += " SET DeckName = @DeckName";
            query += " WHERE DeckID = @DeckID";

            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@DeckName", DeckName);
            sqlparams[1] = new SqlParameter("@DeckID", id);

            //Execute query command
            db.Database.ExecuteSqlCommand(query, sqlparams);

            //Go back to the list of Deck to see our changes
            return RedirectToAction("List");
        }

        //When user clicks on the Delete button (hidden form submission)
        [HttpPost]
        public ActionResult Delete(int id)
        {
            //Debug Purpose to see if we are getting the id
            Debug.WriteLine("I'm pulling data of " + id.ToString());

            //Query statement to delete the specific Deck
            string query = "DELETE FROM decks WHERE DeckID = @DeckID";

            SqlParameter sqlparam = new SqlParameter("@DeckID", id);

            //Execute query command
            db.Database.ExecuteSqlCommand(query, sqlparam);

            //Go back to List of Deck
            return RedirectToAction("List");
        }
    }
}