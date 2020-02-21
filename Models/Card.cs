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

        //PicID can be 0 or 1+ indicating (1+) => has picture (0)=> no picture
        //Content/Cards/{PicID}.{PicExtension}
        public int PicID { get; set; }
        //.jpg, .gif. .png, .jpeg
        public string PicExtension { get; set; }

        //A Card can have only one series (One to Many Relationship)
        public int SeriesID { get; set; }
        [ForeignKey("SeriesID")]
        public virtual Series Series { get; set; }

        //A Card can have up to two trait (Many to Many Relationship)
        public ICollection<Trait> Traits { get; set; }

        //A Card can be in any number of decks (Many to Many Relationship)
        public ICollection<Deck> Decks { get; set; }
    }
}