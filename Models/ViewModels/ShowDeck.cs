using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeckBuilder.Models.ViewModels
{
    public class ShowDeck
    {
        //The specific Deck
        public virtual Deck deck { get; set; }

        //A list for every card they have
        public List<Card> cards { get; set; }

        public List<int> cardQuantity { get; set; }

        //A list for every card
        public List<Card> allCards { get; set; }
    }
}