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
    public class ResourceController : BaseController
    {
        IT_ResourceService t_resourceservice = new T_ResourceService();
        // GET: /Resource/

        public ActionResult Index()
        {
            return View();
        }

        //得到所有的用户权限信息
        public ActionResult GetActionUserInfoShow()
        {
            //json数据：{total:"",row:""}
            //
            //分页的数据
            //

            int pageIndex = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            int pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);


            ////SearchName
            string txtResourceName = Request["txtResourceName"];

            //处理分页过滤事件
            ModelDataQuery pm = new ModelDataQuery();
            pm.pageIndex = pageIndex;
            pm.pageSize = pageSize;
            pm.ResourceName = txtResourceName;
            pm.total = 0;

            ////放置依赖刷新
            var data = from u in t_resourceservice.LoadDataActionInfo(pm)
                       select new { u.ResourceID, u.ResourceName, u.ResDescription, u.CreateOperatorID, u.CreateOperatorName, u.CreateTime, u.ModiOperatorID, u.ModiOperatorName, u.ModiTime, u.Notes };


            ////var data = _userInfoService.LoadPagerEntities(pageSize, pageIndex, out total, u => true, true, u => u.ID);

            var result = new { total = pm.total, rows = data };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 添加权限信息
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        public ActionResult AddResourceInfo(T_Resource resource)
        {
            T_User userinfo = Session["T_User"] as T_User;
            //实现对用户的添加信息
            var resourceinfo = t_resourceservice.LoadEntities(u => u.ResourceID > 0);
            List<int> resourceinf = new List<int>();
            foreach (var i in resourceinfo)
            {
                resourceinf.Add(i.ResourceID);
            }
            int maxid = resourceinf.Max();
            resource.ResourceID = maxid + 1;
            resource.CreateOperatorID = userinfo.UserID ;
            resource.CreateOperatorName = userinfo.UserName ;
            resource.CreateTime = DateTime.Now;
            resource.ModiOperatorID = "";
            resource.ModiOperatorName = "";
            resource.ModiTime = DateTime.Now;
            var t = t_resourceservice.AddEntities(resource);
            if (t != null)
            {
                return Content("OK");
            }
            else
            {
                return Content("Error");
            }
        }
        /// <summary>
        /// 绑定用户权限问题
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult BindResourceInfo(int ResourceID)
        {
            var jsonData = t_resourceservice.LoadEntities(c => c.ResourceID == ResourceID).FirstOrDefault();
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// 对权限信息进行修改
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        public ActionResult UpdateResourceInfo(T_Resource resource)
        {
            //T_ResourceService rs = new T_ResourceService();
            T_Resource num = t_resourceservice.LoadEntities(c => c.ResourceID == resource.ResourceID).FirstOrDefault();
            if (num == null)
            {
                return Content("修改错误，请您检查");
            }
            num.ResourceName = resource.ResourceName;
            num.ResDescription = resource.ResDescription;
            num.Notes = resource.Notes;
            t_resourceservice.UpdateEntities(num);
            return Content("OK");
        }
        /// <summary>
        /// 多选删除用户信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult DeleteResourceInfo(string ID)
        {
            if (string.IsNullOrEmpty(ID))
            {
                return Content("请选择您要删除的数据");
            }
            var deleteID = ID.Split(',');
            //定义List集合存放这些需要删除的数据
            List<int> list = new List<int>();
            foreach (var dID in deleteID)
            {
                list.Add(Convert.ToInt32(dID));
            }

            //实现删除多条数据的方法
            if (t_resourceservice.DeleteResourceInfo(list) > 0)
            {
                return Content("OK");
            }
            return Content("删除失败，请您检查");
        }
    }
}
