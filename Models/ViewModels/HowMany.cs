using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeckBuilder.Models.ViewModels
{
    public class HowMany
    {
        public virtual int DeckID { get; set; }

        public virtual int CardID { get; set; }

        public virtual int CurrentQuantity { get; set; }

        public virtual string UpdateMethod { get; set; }

    }
}