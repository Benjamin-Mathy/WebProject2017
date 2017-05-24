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
        public ActionResult Profil(int id)
        {
            EFMapper<User> mapper = new EFMapper<User>("WebProject2017-DBb");
            mapper.OpenSession();
            ProfilViewModel vm = new ProfilViewModel()
            {
                User = mapper.FindBy(id),
                Journeys = new List<Journey>()            
            };
            return View(vm);
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
            EFUserMapper mapper = new EFUserMapper("WebProject2017-DBb");
            mapper.OpenSession();
            User user = mapper.FindBy((int)Session["UserId"]);
            return View(user);
        }
        public ActionResult AddressEditor()
        {
            EFUserMapper mapper = new EFUserMapper("WebProject2017-DBb");
            mapper.OpenSession();
            User user = mapper.FindBy((int)Session["UserId"]);
            return View(user.Address);
        }

        //Post
        [HttpPost]
        public ActionResult Sign_In_Out(SignInModelView user)
        {
            EFMapper<User> mapper = new EFMapper<User>("WebProject2017-DBb");
            if (ModelState.IsValid)
            {
                mapper.OpenSession();
                Expression<Func<User, bool>> expression = u => u.Login == user.Login && u.Password == user.Password;
                User us = mapper.FindBy(expression).First();
                if (us != null)
                {

                    Session.Add("UserId", us.ID);
                    return RedirectToAction("Profil", new { id = us.ID });
                }
                else
                {
                    ModelState.AddModelError("idenfiant incorrect", "idenfiant incorrect");
                }             
            }
            return View();
        }
        [HttpPost]
        public ActionResult Sign_Up(User user)
        {
            EFMapper<User> mapper = new EFMapper<User>("WebProject2017-DBb");
            if (ModelState.IsValid)
            {
                user = mapper.AddorUpdate(user);
                Session.Add("UserId", user.ID);
                return RedirectToAction("Profil", new { id = user.ID });
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
            EFMapper<User> mapper = new EFMapper<User>("WebProject2017-DBb");
            if (ModelState.IsValid)
            {
                user.ID = (int)Session["UserId"];
                mapper.AddorUpdate(user);
                mapper.Save();
                return RedirectToAction("Profil", new { id = (long)Session["UserId"]});
            }
            return View(user);
        }

    }
}