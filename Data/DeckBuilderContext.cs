using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace DeckBuilder.Data
{
    public class DeckBuilderContext : DbContext
    {
        public DeckBuilderContext() : base("name=DeckBuilderContext")
        {
        }

        public System.Data.Entity.DbSet<DeckBuilder.Models.Deck> Decks { get; set; }

        public System.Data.Entity.DbSet<DeckBuilder.Models.Card> Cards { get; set; }

        public System.Data.Entity.DbSet<DeckBuilder.Models.Series> Series { get; set; }

        public System.Data.Entity.DbSet<DeckBuilder.Models.Trait> Trait { get; set; }
    }
}