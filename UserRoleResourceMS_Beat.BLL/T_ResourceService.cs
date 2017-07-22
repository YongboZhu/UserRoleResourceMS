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

    public partial class T_ResourceService:BaseService<T_Resource>,IT_ResourceService
    {
        IT_ResourceRepository resource = new T_ResourceRepository();
        public IQueryable<T_Resource> LoadDataActionInfo(ModelDataQuery resourceInfo)
        {
            //首先拿到所有的数据
            var temp = resource.LoadEntities(u => true);

            //然后进行权限名称过滤
            if (!string.IsNullOrEmpty(resourceInfo.ResourceName))
            {
                temp = temp.Where<T_Resource>(c => c.ResourceName.Contains(resourceInfo.ResourceName));
            }
            //返回总数
            resourceInfo.total = temp.Count();

            //最后实现分页
            return temp.Skip<T_Resource>(resourceInfo.pageSize * (resourceInfo.pageIndex - 1)).Take<T_Resource>(resourceInfo.pageSize).AsQueryable();
        }

        //删除权限信息
        public int DeleteResourceInfo(List<int> ResourceID)
        {
            foreach (var ID in ResourceID)
            {
                resource.DeleteEntities(new T_Resource { ResourceID = ID });
            }
            return 1; 
        }
    }
}
