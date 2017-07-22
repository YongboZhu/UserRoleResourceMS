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
    public class RoleController : BaseController
    {
        IT_RoleService _roleService = new T_RoleService();
        IT_ResourceService _resourceservice=new T_ResourceService();
        IT_RoleResourceService _iroleresourceservice = new T_RoleResourceService();
        // GET: /Role/
        public ActionResult Index()
        {
            //var userrole = _roleService.LoadEntities(u => true);
            //List<T_Role> trole = new List<T_Role>();
            //foreach (var role in userrole)
            //{
            //    trole.Add(role);
            //}
            //ViewData.Model = trole;
            return View();
        }

        /// <summary>
        /// 实现对用户角色的绑定
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllUserRoleInfo()
        {
            //首先获取从前台传递过来的参数
            int pageIndex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pageSize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);

            //获取从前台传递过来的需要多条件模糊查询的数据
            string txtSerachRoleName = Request["txtSerachRoleName"];


            //定义对象，得到所有的参数
            ModelDataQuery roleInfo = new ModelDataQuery();
            roleInfo.pageIndex = pageIndex;
            roleInfo.pageSize = pageSize;
            roleInfo.RoleName = txtSerachRoleName;
            roleInfo.total = 0;

            //获取所有的总数输入
            var data = from n in _roleService.LoadRoleInfo(roleInfo)
                       select new { n.RoleID, n.RoleName, n.RoleDescription,n.Notes };
            //var data = _roleService.LoadPagerEntities(pageSize, pageIndex, out total, c => true, true, d => d.ID);

            var jsonResult = new { total = roleInfo.total, rows = data };

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 实现对用户角色的添加信息
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>

        public ActionResult AddUserRoleInfo(T_Role role)
        {
            T_User roleinfo = Session["T_User"] as T_User;
            //实现对用户的添加信息
            var rolein = _roleService.LoadEntities(u => u.RoleID > 0);
            List<int> roleinf = new List<int>();
            foreach (var i in rolein)
            {
                roleinf.Add(i.RoleID);
            }
            int maxid = roleinf.Max();
            role.RoleID = maxid+1 ;
            role.CreateOperatorID = roleinfo.UserID;
            role.CreateOperatorName = roleinfo.UserName ;
            role.CreateTime = System.DateTime.Now;
            role.ModiOperatorID = "";
            role.ModiOperatorName = "";
            role.ModiTime = System.DateTime.Now;
            if (_roleService.AddEntities(role) != null)
            {
                return Content("OK");
            }
            else
            {
                return Content("fail");
            }

        }

        /// <summary>
        /// 实现对用户角色的绑定信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult BindUserRoleInfo(int RoleID)
        {
            var BindUserRoleInfoJson = _roleService.LoadEntities(c => c.RoleID == RoleID).FirstOrDefault();

            return Json(BindUserRoleInfoJson, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改用户角色信息
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <returns></returns>
        public ActionResult UpdataRole(T_Role roleInfo)
        {
            T_User user = Session["T_User"] as T_User;
            //查询出Role实体对象
            T_Role EditRole = _roleService.LoadEntities(c => c.RoleID == roleInfo.RoleID).FirstOrDefault();

            EditRole.RoleName = roleInfo.RoleName;
            EditRole.RoleDescription = roleInfo.RoleDescription;
            EditRole.ModiOperatorID = user.UserID;
            EditRole.ModiOperatorName = user.UserName;
            EditRole.ModiTime = DateTime.Now;
            EditRole.Notes = roleInfo.Notes;
            //查询出实体对象然后修改
            _roleService.UpdateEntities(EditRole);
            return Content("OK");
        }

        /// <summary>
        /// 删除用户角色信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult DeleteUserRoleInfo(string RoleID)
        {
            if (string.IsNullOrEmpty(RoleID))
            {
                return Content("请选择您要删除的数据");
            }

            //截取传递过来的字符串显示成数组形式
            var deleteID = RoleID.Split(',');
            //定义数组存放删除的ID
            List<int> deleteIDList = new List<int>();
            foreach (var dID in deleteID)
            {
                deleteIDList.Add(Convert.ToInt32(dID));
            }

            if (_roleService.DeleteRoleInfo(deleteIDList) > 0)
            {
                return Content("OK");
            }
            return Content("删除失败，请您检查");
        }
        public ActionResult SetResourceInfo()
        {
            var BindResourceInfoJson = _resourceservice.LoadEntities(u => true);
            List<T_Resource> roleinfo = new List<T_Resource>();
            foreach (var role in BindResourceInfoJson)
            {
                roleinfo.Add(role);
            }
            return Json(roleinfo, JsonRequestBehavior.AllowGet);

        }
        public ActionResult AddRoleResourse(T_RoleResource roleresource)
        {
            int maxid = 0;
            T_RoleResource rolerece = new T_RoleResource();
            var rre = _iroleresourceservice.LoadEntities(u => u.RoleID == roleresource.RoleID);
            foreach (var roler in rre)
            {
                if (roler.ResourceID == roleresource.ResourceID)
                {
                    return Content("Failed");
                }
            }
            var relreo = _iroleresourceservice.LoadEntities(u => u.SID > 0);
            List<int> rresource = new List<int>();
            foreach (var i in relreo)
            {
                rresource.Add(i.SID);
            }
            maxid = rresource.Max();
            rolerece.SID = maxid + 1;
            rolerece.RoleID = roleresource.RoleID;
            rolerece.ResourceID = roleresource.ResourceID;
            var EntityRoleResource = _iroleresourceservice.AddEntities(rolerece);
            if (EntityRoleResource != null)
            {
                return Content("OK");
            }
            else
            {
                return Content("Failed");
            }
        }
    }
}
