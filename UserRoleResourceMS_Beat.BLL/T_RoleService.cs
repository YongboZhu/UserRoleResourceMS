using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRoleResourceMS_Beat.Model;
using UserRoleResourceMS_Beat.IBLL;
using UserRoleResourceMS_Beat.DAL;
using UserRoleResourceMS_Beat.IDAL;

namespace UserRoleResourceMS_Beat.BLL
{
    public partial class T_RoleService:BaseService<T_Role>,IT_RoleService
    {
        IT_RoleRepository role = new T_RoleRepository();
        public IQueryable<T_Role> LoadRoleInfo(ModelDataQuery roleInfo)
        {
            //首先查询出所有的数据
            var temp = role.LoadEntities(c => true);

            //判断角色名称
            if (!string.IsNullOrEmpty(roleInfo.RoleName))
            {
                temp = temp.Where<T_Role>(c => c.RoleName.Contains(roleInfo.RoleName));
            }

            //获取总数total
            roleInfo.total = temp.Count();

            //获取总数返回
            return temp.Skip<T_Role>(roleInfo.pageSize * (roleInfo.pageIndex - 1)).Take<T_Role>(roleInfo.pageSize).AsQueryable();
            //return temp.Skip<T_Role>(roleInfo.pageSize * (roleInfo.pageIndex - 1)).Take(roleInfo.pageSize);
        }
        public int DeleteRoleInfo(List<int> DeleteUserInfoRoleName)
        {
            foreach (var ID in DeleteUserInfoRoleName)
            {
                role.DeleteEntities(new T_Role() { RoleID = ID });
            }
            return 1;
        }

    }
}
