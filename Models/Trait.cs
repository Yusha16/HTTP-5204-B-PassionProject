using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        //A Trait can have many cards (One to Many Relationship)
        //[InverseProperty("Trait1")]
        //public ICollection<Card> Cards1 { get; set; }

        //[InverseProperty("Trait2")]
        //public ICollection<Card> Cards2 { get; set; }
    }
}