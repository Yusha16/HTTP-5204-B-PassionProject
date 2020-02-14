using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeckBuilder.Models.ViewModels
{
    public class UpdateCard
    {
        //The specific Card
        public virtual Card card { get; set; }

        //A list for every trait they have
        public List<Trait> traits { get; set; }

        //Used for the drop down list of the all the possible Series
        public List<Series> allSeries { get; set; }

        //Used for the drop down list of the all the possible Traits
        public List<Trait> allTraits { get; set; }

    }
}