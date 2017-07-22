using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRoleResourceMS_Beat.Model;

namespace UserRoleResourceMS_Beat.IBLL
{
    public interface IT_RoleService:IBaseService<T_Role>
    {
        IQueryable<T_Role> LoadRoleInfo(ModelDataQuery roleInfo);
        //删除Role
        int DeleteRoleInfo(List<int> DeleteUserInfoRoleName);
    }
}
