using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRoleResourceMS_Beat.Model;
using UserRoleResourceMS_Beat.IDAL;
using UserRoleResourceMS_Beat.DAL;
using UserRoleResourceMS_Beat.IBLL;

namespace UserRoleResourceMS_Beat.BLL
{
    public partial class T_UserService:BaseService<T_User>,IT_UserService
    {
        IT_UserRepository user = new T_UserRepository();
        IT_UserRoleRepository userrole = new T_UserRoleRepository();
        public T_User checkUserLogin(T_User userinfo)
        {
            //判断用户的用户名密码是否正确
            return user.LoadEntities(u => u.UserName == userinfo.UserName && u.Password == userinfo.Password)
                 .FirstOrDefault();
        }

        public IQueryable<T_User> LoadSearchData(ModelDataQuery query)
        {
            
            //拿到所有的数据
            var temp = user.LoadEntities(u => true);
            //进行的过滤
            if (!string.IsNullOrEmpty(query.UserName))
            {
                temp = temp.Where<T_User>(u => u.UserName.Contains(query.UserName));
            }
            //返回总数
            query.total = temp.Count();

            //做分页查询
            return temp.Skip<T_User>(query.pageSize * (query.pageIndex - 1)).Take<T_User>(query.pageSize).AsQueryable();
            //return temp.Skip(query.pageSize * (query.pageIndex - 1)).Take(query.pageSize).AsQueryable();
        }

        public int DeleteUserInfo(List<string> DeleteUserInfoID)
        {
            foreach (var ID in DeleteUserInfoID)
            {
                user.DeleteEntities(new T_User() { UserID = ID });
            }
            return 1;
        }
        public bool SetUserInfoRole(string userID, List<int> roleIDList)
        {
            //首先根据用户ID，查询出用户的信息
            var currentUser = user.LoadEntities(c => c.UserID == userID).FirstOrDefault();
            if (currentUser == null)
            {
                return false;
            }
            //得到角色表中的数据全部返回
            var listRoles = currentUser.T_UserRole.ToList();
            ///处理清空原来的数据，用户的和角色的中间表信息
            for (int i = 0; i < listRoles.Count; i++)
            {
                userrole.DeleteEntities(listRoles[i]);
            }

            //在此重新将数据加载会数据库
            foreach (var roleID in roleIDList)
            {
                T_UserRole rUserInfoRole = new T_UserRole();
                rUserInfoRole.RoleID = roleID;
                rUserInfoRole.UserID = userID;
                userrole.AddEntities(rUserInfoRole);
            }
            return true;
        }

    }
}
