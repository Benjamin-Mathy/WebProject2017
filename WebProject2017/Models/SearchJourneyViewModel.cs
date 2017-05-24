using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebProject2017Server.Modèle;

namespace WebProject2017.Models
{
    public class SearchJourneyViewModel
    {
        public List<Journey> Result { get; set; }
        public List<Journey> JourneysUser { get; set; }
    }
}