using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DeckBuilder.Models
{
    public class Series
    {
        [Key]
        public int SeriesID { get; set; }

        //A Series has a name
        public string SeriesName { get; set; }

        //A Series has a special code in them
        public string SeriesCode { get; set; }

        //A series can have many cards (One to Many Relationship)
        public ICollection<Card> Cards { get; set; }

    }
}