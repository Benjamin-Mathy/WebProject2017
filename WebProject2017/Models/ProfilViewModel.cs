using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebProject2017Server.Modèle;

namespace WebProject2017.Models
{
    public class ProfilViewModel
    {
        public User User { get; set; }
        public List<Journey> Journeys { get; set; }

    }
}