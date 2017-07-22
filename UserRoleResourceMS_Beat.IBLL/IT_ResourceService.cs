using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRoleResourceMS_Beat.Model;

namespace UserRoleResourceMS_Beat.IBLL
{
    public interface IT_ResourceService:IBaseService<T_Resource>
    {
        //实现模糊查询接口
        IQueryable<T_Resource> LoadDataActionInfo(ModelDataQuery resourceInfo);
        //实现删除权限接口
        int DeleteResourceInfo(List<int> ResourceID);
    }
}
