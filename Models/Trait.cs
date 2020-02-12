using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DeckBuilder.Models
{
    public class Trait
    {
        [Key]
        public int TraitID { get; set; }

        //A Trait has a name
        public string TraitName { get; set; }

        //A Trait can have many cards (Many to Many Relationship)
        public ICollection<Card> Cards { get; set; }
    }
}