using JqueryMark.ActionModel;
using JqueryMark.ViewModel;
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
                return Content("Model is invalid!");
            }

            try
            {
                //ContextType.Domain, "Domain Name", "Container", 加密方式, 伺服器帳號, 伺服器密碼
                using (var context = new PrincipalContext(ContextType.Machine, null, null, ContextOptions.Negotiate, "", ""))
                {
                    bool exist = context.ValidateCredentials(actionModel.UserName, actionModel.UserPassword);

                    if (exist)
                    {
                        var identity = UserPrincipal.FindByIdentity(context, IdentityType.Name, actionModel.UserName);

                        if (identity != null && !identity.IsAccountLockedOut())
                        {
                            return Content("登入成功！");
                        }
                        else
                        {
                            return Content("讀取登入者資料失敗，或者此帳號目前被鎖定。");
                        }
                    }
                    else
                    {
                        return Content("指定的使用者帳號及密碼並無有效的內容。");
                    }
                }
            }
            catch
            {
                return Content("與Active Directory連線失敗，或其他系統錯誤。");
            }
        }

        public ActionResult GetAll()
        {
            try
            {
                using (var context = new PrincipalContext(ContextType.Machine))
                {
                    using(var user = new UserPrincipal(context))
                    {
                        using (var searcher = new PrincipalSearcher(user))
                        {
                            var resultList = searcher.FindAll().ToList();
                            List<UserIdentityViewModel> viewModel = new List<UserIdentityViewModel>();
                            foreach (var item in resultList)
                            {
                                var tmp = new UserIdentityViewModel()
                                {
                                    Name = item.Name,
                                    DisplayName = item.DisplayName,
                                    Description = item.Description
                                };
                                viewModel.Add(tmp);
                            }
                            return PartialView("_GetAll", viewModel);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}