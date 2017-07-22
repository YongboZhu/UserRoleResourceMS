using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserRoleResourceMS_Beat.Model;
using UserRoleResourceMS_Beat.IBLL;
using UserRoleResourceMS_Beat.BLL;

namespace UserRoleResourceMS_Beat.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        IT_UserService _iuserinfo = new T_UserService();

        public ActionResult Index()
        {
            var UName = Request.Cookies["UserName"] == null ? "" : Request.Cookies["UserName"].Value;
            ViewBag.UserName = UName;
            return View();
        }


        //检查用户登录信息
        public ActionResult CheckUserLogin(T_User userinfo, string Code)
        {
            //如果有用户名的话，保存到Cookies中
            if (userinfo.UserName != null)
            {
                Response.Cookies["UserName"].Value = userinfo.UserName;
                Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(7);
            }

            //验证用户输入的验证码和系统的验证码是否相同
            string sessionCode = Session["ValidateCode"] == null ? new Guid().ToString() : Session["ValidateCode"].ToString();
            Session["ValidateCode"] = new Guid();
            if (sessionCode != Code)
            {
                return Content("请输入正确的验证码！");
            }

            T_User uinfo = _iuserinfo.checkUserLogin(userinfo);

            //检验用户名密码是否正确
            if (uinfo != null)
            {
                Session["T_User"] = uinfo;
                return Content("OK");
            }
            else
            {
                return Content("用户名密码错误，请您仔细检查!");
            }

        }


        public ActionResult CheckCode()
        {
            //生成验证码
            ValidateCode validateCode = new ValidateCode();
            string code = validateCode.CreateValidateCode(4);
            Session["ValidateCode"] = code;
            byte[] bytes = validateCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }
    }
}
