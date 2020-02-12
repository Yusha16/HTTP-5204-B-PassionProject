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
    }
}