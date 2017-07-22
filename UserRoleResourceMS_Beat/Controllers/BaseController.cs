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
        public class BaseController : Controller
        {
            IT_UserService _userInfoService = new T_UserService();
            //定义一个基类的UserInfo对象
            public T_User CurrentUserInfo { get; set; }

            //<summary>
            //重写基类在Action之前执行的方法
            //</summary>
            //<param name="filterContext"></param>
            protected override void OnActionExecuting(ActionExecutingContext filterContext)
            {

                //#region -----校验用户是否登录进入网站的-----
                //base.OnActionExecuting(filterContext);
                //CurrentUserInfo = Session["T_User"] as T_User;

                //检验用户是否已经登录，如果登录则不执行，否则则执行下面的跳转代码
                //if (CurrentUserInfo == null)
                //{
                //    Response.Redirect("/Login/Index");
                //}
                //#endregion


                //#region ------//留个接口------
                //if (CurrentUserInfo.UserName == "admin")
                //{
                //    return;
                //}
                //#endregion

            }


            public void EndRequest()
            {
                Response.Redirect("/Error.html");
            }
        }
}
