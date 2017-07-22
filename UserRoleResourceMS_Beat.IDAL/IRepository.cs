 

using UserRoleResourceMS_Beat.Model;

namespace UserRoleResourceMS_Beat.IDAL
{
   
	
    public partial interface IT_ResourceRepository :IBaseRepository<T_Resource>
    {         
    }
	
    public partial interface IT_RoleRepository :IBaseRepository<T_Role>
    {         
    }
	
    public partial interface IT_RoleResourceRepository :IBaseRepository<T_RoleResource>
    {         
    }
	
    public partial interface IT_UserRepository :IBaseRepository<T_User>
    {         
    }
	
    public partial interface IT_UserRoleRepository :IBaseRepository<T_UserRole>
    {         
    }
	
}