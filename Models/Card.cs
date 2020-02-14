using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeckBuilder.Models
{
    public class Card
    {
        [Key]
        public int CardID { get; set; }

        //A Card has a name
        public string CardName { get; set; }

        //A Card has a colour (Yellow, Green, Red, Blue)
        public string CardColour { get; set; }

        //A Card can have only one series (One to Many Relationship)
        public int SeriesID { get; set; }
        [ForeignKey("SeriesID")]
        public virtual Series Series { get; set; }

        //A Card can have up to two trait (Many to Many Relationship) Change to FK
        public ICollection<Trait> Traits { get; set; }

        /*
         * Attempt to have 2 Foreign Key to reference a Trait Primary Key
         * 
        public int? TraitID1 { get; set; }
        [ForeignKey("TraitID1")]
        [InverseProperty("Cards")]
        public virtual Trait Trait1 { get; set; }

        public int? TraitID2 { get; set; }
        [ForeignKey("TraitID2")]
        public virtual Trait Trait2 { get; set; }
        */
        /*
         * Attempt to have 2 Foreign Key to reference a Trait Primary Key
         * 
        //A Card can have up to two trait (One to Many Relationship)
        [ForeignKey("Trait1")]
        public int? TraitID1 { get; set; }
        public virtual Trait Trait1 { get; set; }

        //A Card can have up to two trait (One to Many Relationship)
        [ForeignKey("Trait2")]
        public int? TraitID2 { get; set; }
        public virtual Trait Trait2 { get; set; }
        */

        //A Card can be in any number of decks (Many to Many Relationship)
        public ICollection<Deck> Decks { get; set; }
    }
}