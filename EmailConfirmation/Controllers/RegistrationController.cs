using EmailConfirmation.Context;
using EmailConfirmation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmailConfirmation.ClassHelper;
using System.Threading.Tasks;
using System.Globalization;
using System.Web.Hosting;
using System.IO;

namespace EmailConfirmation.Controllers
{
    public class RegistrationController : Controller
    {
        UserContext context = new UserContext();
        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }

        public static async Task<string> EmailTemplate(string template)
        {
            var templateFilePath = HostingEnvironment.MapPath("~/Content/templates/") + template + ".cshtml";
            StreamReader objStreamreaderFile = new StreamReader(templateFilePath);
            var body = await objStreamreaderFile.ReadToEndAsync();
            objStreamreaderFile.Close();
            return body;
        }

        //[HttpPost]
        //public ActionResult Index(User UserObj)
        //{
        //    KeyGenerator classhelper = new KeyGenerator();
        //    if (ModelState.IsValid)
        //    {
        //        UserObj.Status = false;
        //        UserObj.Key = KeyGenerator.GeneratePassword(10);
        //        context.User.Add(UserObj);
        //        context.SaveChanges();

        //        return Content("Successfull");
        //    }
        //    return Content("Not Done");
        //}

        public async Task<ActionResult> SendEmail(User UserObj)
        {
            KeyGenerator classhelper = new KeyGenerator();
            if (ModelState.IsValid)
            {
                UserObj.Status = false;
                UserObj.Key = KeyGenerator.GeneratePassword(10);
                context.User.Add(UserObj);
                context.SaveChanges();

                var message = await EmailTemplate("WelcomeEmail");
                message = message.Replace("@ViewBag.Name", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(UserObj.Name));
                message = message.Replace("@ViewBag.Key",UserObj.Key);
                await ManageEmailService.SendingEmailAsync(UserObj.Email, "Welcome!", message);
                //return View("EmailSent");
                return Content("Check your Email");
            }
            return Content("Not Done!!");
        }
    }
}