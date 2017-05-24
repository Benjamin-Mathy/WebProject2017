using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using WebProject2017.Models;
using WebProject2017Server.EF.Mappers;
using WebProject2017Server.Modèle;

namespace WebProject2017.Controllers
{
    public class JourneyController : Controller
    {
        // GET: Journey
        public ActionResult SearchJourney()
        {
            SearchJourneyViewModel model = new SearchJourneyViewModel();
            model.Result = new List<Journey>();
            using(EFJourneyMapper mapper = new EFJourneyMapper("WebProject2017-DBb"))
            {               
                model.Result = mapper.GetAll();               
               
            }
            model.JourneysUser = new List<Journey>();
            if (((User)Session["User"]) != null)
            {
                foreach (Journey j in model.Result)
                {
                    if (j.Driver.ID == ((User)Session["User"]).ID)
                    {
                        model.JourneysUser.Add(j);
                    }
                }
            }
            return View(model);
        }
        public ActionResult AddJourney()
        {
            if (((User)Session["User"]) != null)
            {
                return View();
            }
            return RedirectToAction("Sign_In_Out","Users");
        }
        // POST: JOURNEY
        [HttpPost]
        public ActionResult AddJourney(Journey journey)
        {
            if (ModelState.IsValid)
            {
                using (EFJourneyMapper mapper = new EFJourneyMapper("WebProject2017-DBb"))
                {
                    try
                    {
                        journey.Driver = (User)Session["User"];
                        journey = mapper.AddorUpdate(journey);
                        return RedirectToAction("Profil","Users");
                    }
                    catch (Exception ex)
                    {
                        ViewBag.MessageErreur = ex.Message;
                    }
                }
            }
            return View(journey);
        }
        [HttpPost]
        public ActionResult Profil(SearchJourneyViewModel model)
        {            
            return View(model);
        }
    }
}