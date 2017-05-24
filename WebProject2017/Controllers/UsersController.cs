using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using WebProject2017.Models;
using WebProject2017Server.EF.Mappers;
using WebProject2017Server.Modèle;

namespace WebProject2017.Controllers
{
    public class UsersController : Controller
    {
       
        // GET
        public ActionResult Profil()
        {
            if (((User)Session["User"]) != null)
            {
                ProfilViewModel vm;
                using (EFUserMapper mapper = new EFUserMapper("WebProject2017-DBb"))
                {
                    vm = new ProfilViewModel()
                    {
                        User = mapper.FindBy(((User)Session["User"]).ID),
                        Journeys = new List<Journey>()
                    };
                }
                return View(vm);
            }
            else
            {
                return RedirectToAction("Sign_In_Out");
            }            
        }
        public ActionResult Sign_In_Out()
        {
            return View(new SignInModelView() { Login = "login", Password = "12345" });
        }
        public ActionResult Sign_Up()
        {
            return View(new User() { Login = "login", Password = "12345", LastName = "bologne", Name = "sauce", Email = "email@gamil.com", Phone = "0475859565" });
        }
        public ActionResult ProfilEditor()
        {
            User user;
            using (EFUserMapper mapper = new EFUserMapper("WebProject2017-DBb"))
            {
                user = mapper.FindBy(((User)Session["User"]).ID);
            }
            return View(user);
        }
        public ActionResult AddressEditor()
        {
            User user;
            using (EFUserMapper mapper = new EFUserMapper("WebProject2017-DBb"))
            {
                user = mapper.FindBy(((User)Session["User"]).ID);
            }
            return View(user.Address);
        }

        //Post
        [HttpPost]
        public ActionResult Sign_In_Out(SignInModelView user)
        {
 
            if (ModelState.IsValid)
            {
                using (EFUserMapper mapper = new EFUserMapper("WebProject2017-DBb"))
                {
                    Expression<Func<User, bool>> expression = u => u.Login == user.Login && u.Password == user.Password;
                    List<User> uss = mapper.FindBy(expression).ToList<User>();

                    if (uss.Count > 0)
                    {
                        User us = uss.First();
                        Session.Add("User", us);
                        return RedirectToAction("Profil");
                    }
                    else
                    {
                        ViewBag.MessageErreur = "idenfiant incorrect";
                    }
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult Sign_Up(User user)
        {
            
            if (ModelState.IsValid)
            {
                using (EFUserMapper mapper = new EFUserMapper("WebProject2017-DBb"))
                {
                    try
                    {
                        user = mapper.AddorUpdate(user);
                        Session.Add("User", user);
                        return RedirectToAction("Profil");
                    }
                    catch(Exception ex)
                    {
                        ViewBag.MessageErreur = ex.Message;
                    }
                }
            }
            return View(user);
        }
        [HttpPost]
        public ActionResult Profil(ProfilViewModel model)
        {

            return RedirectToAction("ProfilEditor", new { user = (User)model.User }); ;
        }
        [HttpPost]
        public ActionResult Profila(ProfilViewModel model)
        {

            return RedirectToAction("AddressEditor", new { user = (User)model.User }); ;
        }
        [HttpPost]
        public ActionResult ProfilEditor(User user)
        {
            
            if (ModelState.IsValid)
            {
                using (EFUserMapper mapper = new EFUserMapper("WebProject2017-DBb"))
                {
                    user.ID = (int)Session["UserId"];
                    mapper.AddorUpdate(user);
                    return RedirectToAction("Profil");
                }
            }
            return View(user);
        }

    }
}