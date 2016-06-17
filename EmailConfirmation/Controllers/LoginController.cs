using EmailConfirmation.Context;
using EmailConfirmation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmailConfirmation.Controllers
{
    public class LoginController : Controller
    {
        UserContext context = new UserContext();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User UserObj)
        {
            try
            {
                using (var context = new UserContext())
                {
                    var getUser = (from s in context.User where s.Email == UserObj.Email select s).FirstOrDefault();
                    if (getUser != null)
                    {
                        if (getUser.Status != false)
                        {
                            //Session["UserEmail"] = getUser.Email;
                            return Content("You are loggedin");
                        }
                    }
                    Session["UserEmail"] = getUser.Email;
                    return View("Confimation");
                }
                
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        public ActionResult Confimation()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Confimation(User UserObj)
        {
            UserObj.Email = Session["UserEmail"].ToString();
            var email = Session["UserEmail"].ToString();
            var validateKey = (from s in context.User where UserObj.Email == email && s.Key == UserObj.Key select s).FirstOrDefault();
            if(validateKey != null)
            {
                var result = context.User.FirstOrDefault(s => s.Status == false);
                result.Status = true;
                context.SaveChanges();
                return Content("Your account has been activeted");
            }

            return Content("You are not registered");
        }
    }
}