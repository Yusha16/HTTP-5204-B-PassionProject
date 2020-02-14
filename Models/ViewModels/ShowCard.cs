using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeckBuilder.Models.ViewModels
{
    public class ShowCard
    {
        //The specific Card
        public virtual Card card { get; set; }
        
        //A list for every trait they have
        public List<Trait> traits { get; set; }
    }
}