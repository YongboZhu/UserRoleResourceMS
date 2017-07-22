using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserRoleResourceMS_Beat.BLL;
using UserRoleResourceMS_Beat.IBLL;
using UserRoleResourceMS_Beat.Model;

namespace UserRoleResourceMS_Beat.Controllers
{
    public class UserInfoController : BaseController
    {
        //GET: /UserInfo/
        IT_UserService _iUserInfoService = new T_UserService();
        IT_RoleService _itroleservice = new T_RoleService();
        IT_UserRoleService _iuserrole = new T_UserRoleService();

        public ActionResult Index()
        { 
            return View();
        }
        //得到用户的所有信息
        public ActionResult GetAllUserInfos()
        {

            //json数据：{total:"",row:""}
            //分页的数据

            int pageIndex = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            int pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);


            ////SearchName
            string txtSearchUserName = Request["txtSerachUserName"];

            //处理分页过滤事件
            ModelDataQuery pm = new ModelDataQuery();
            pm.pageIndex = pageIndex;
            pm.pageSize = pageSize;
            pm.UserName = txtSearchUserName;
            pm.total = 0;

            //放置依赖刷新
            var data = from u in _iUserInfoService.LoadSearchData(pm)
                       select new { u.UserID, u.UserName, u.Password, u.CreateOperatorID, u.CreateOperatorName, u.CreateTime, u.ModiOperatorID, u.ModiOperatorName, u.ModiTime, u.Notes };

            var result = new { total = pm.total, rows = data };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // 注册用户
        public ActionResult Regist(T_User userinfo)
        {
            T_User userin = Session["T_User"] as T_User;

            //给表中的默认字段赋值
            userinfo.CreateOperatorID = userin.UserID ;
            userinfo.CreateOperatorName = userin.UserName ;
            userinfo.CreateTime = DateTime.Now;
            userinfo.ModiOperatorID = "";
            userinfo.ModiOperatorName = "";
            userinfo.ModiTime = DateTime.Now;
            //在这里需要用到枚举类型，不要写0

            _iUserInfoService.AddEntities(userinfo);
            return Content("OK");

        }

        // 删除用户
        public ActionResult DeleteUserInfo(string deleteUserInfoID, string UserName)
        {
            //首先确认是哪个用户登录进来的，如果此用户正在登录系统，则不允许删除此用户
            T_User UInfo = Session["T_User"] as T_User;
            var LoginUName = UInfo.UserName;

            var UIdsName = UserName.Split(',');
            List<string> deleteUName = new List<string>();
            foreach (var Name in UIdsName)
            {
                deleteUName.Add(Name);
            }
            if (deleteUName.Contains(LoginUName))
            {
                return Content("含有正在使用的用户，禁止删除");
            }

            if (string.IsNullOrEmpty(deleteUserInfoID))
            {
                return Content("请选择您要删除的数据");
            }
            //截取传递过来的字符串的数字信息
            var idsStr = deleteUserInfoID.Split(',');

            List<string> deleteIDList = new List<string>();

            foreach (var ID in idsStr)
            {
                deleteIDList.Add(ID);
            }
            if (_iUserInfoService.DeleteUserInfo(deleteIDList) > 0)
            {
                return Content("OK");
            }
            return Content("删除失败，请您检查");
        }

        // 绑定用户数据
        public ActionResult GetBindDetails(string UserID)
        {
            var BindIDForUpdateTextBox = _iUserInfoService.LoadEntities(u => u.UserID == UserID).FirstOrDefault();
            return Json(BindIDForUpdateTextBox, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public ActionResult UpdateInfo(T_User userInfo)
        {
            T_User useri = Session["T_User"] as T_User;
            //T_User EditUserInfo = new T_User();
            //首先查询出要修改的实体对象
            T_User EditUserInfo = _iUserInfoService.LoadEntities(c => c.UserID == userInfo.UserID).FirstOrDefault();

            //查询出实体对象给重新复制
            EditUserInfo.UserID = userInfo.UserID;
            EditUserInfo.UserName =  userInfo.UserName ;
            EditUserInfo.Password = userInfo.Password;
            EditUserInfo.ModiOperatorID = useri.UserID;
            EditUserInfo.ModiOperatorName = useri.UserName;
            EditUserInfo.ModiTime = DateTime.Now;
            EditUserInfo.Notes = userInfo.Notes;
            _iUserInfoService.UpdateEntities(EditUserInfo);
            return Content("OK");
        }

        /// <summary>
        /// 检查用户名是否已经存在
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public ActionResult CheckUserName(string UserName)
        {
            var UName = _iUserInfoService.LoadEntities(u => u.UserName == UserName).FirstOrDefault();
            if (UName != null)
            {
                return Content("OK");
            }
            else
            {
                return Content("Error");
            }
        }

       public ActionResult  AddUserRole(T_UserRole userrole)
       {
         T_UserRole ur=new T_UserRole();
         var ure = _iuserrole.LoadEntities(u => u.UserID == userrole.UserID);
         foreach (var userr in ure)
         {
             if (userr.RoleID == userrole.RoleID)
             {
                 return Content("Failed");
             }
         }
         var userre = _iuserrole.LoadEntities(u => u.SID > 0);
         List<int> urole= new List<int>();
         foreach (var i in userre)
         {
             urole.Add(i.SID);
         }
         int maxid = urole.Max();
         ur.SID = maxid + 1;
         ur.UserID=userrole.UserID;
         ur.RoleID=userrole.RoleID;
         var EntityUserRole= _iuserrole.AddEntities(ur);
         if (EntityUserRole != null)
         {
             return Content("OK");
         }
         else
         {
             return Content("添加失败哦！");
         }
      }
     public ActionResult SetRoleInfo()
     {
         var role = _itroleservice.LoadEntities(u => u.RoleID > 0);
         List<T_Role> roleinfo = new List<T_Role>();
         foreach (var i in role)
         {
             roleinfo.Add(i);
         }
         return Json(roleinfo, JsonRequestBehavior.AllowGet);
     }

     //public ActionResult GetUserRoleInfos()
     //{
        
     //}
    }
}
