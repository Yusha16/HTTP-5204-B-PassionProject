using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DeckBuilder.Models
{
    public class Deck
    {
        [Key]
        public int DeckID { get; set; }

        //A Deck has a name
        public string DeckName { get; set; }

        //A Deck can have at least 50 cards (Many to Many Relationship)
        public ICollection<Card> Cards { get; set; }
    }
}