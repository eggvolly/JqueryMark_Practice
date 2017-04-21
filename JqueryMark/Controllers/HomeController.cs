using JqueryMark.ActionModel;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JqueryMark.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login(LoginActionModel actionModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Contact", actionModel);
            }

            using (var context = new PrincipalContext(ContextType.Machine))
            {
                bool exist = context.ValidateCredentials(actionModel.UserName, actionModel.UserPassword);
                if (exist)
                {
                    return Content("Login success!");
                }
                else
                {
                    return Content("Login failed!");
                }
            }
        }
    }
}