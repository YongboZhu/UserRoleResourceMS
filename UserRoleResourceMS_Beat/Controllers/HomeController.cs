using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserRoleResourceMS_Beat.Model;

namespace UserRoleResourceMS_Beat.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            T_User userinfo = Session["T_User"] as T_User;
            if (userinfo != null)
            {
                ViewBag.UserName = userinfo.UserName;
            }

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
