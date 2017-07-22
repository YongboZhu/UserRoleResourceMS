 

using UserRoleResourceMS_Beat.IDAL;
using UserRoleResourceMS_Beat.Model;

namespace UserRoleResourceMS_Beat.DAL
{
   
	

	public partial class T_ResourceRepository :BaseRepository<T_Resource>,IT_ResourceRepository
    {
         
    }

	

	public partial class T_RoleRepository :BaseRepository<T_Role>,IT_RoleRepository
    {
         
    }

	

	public partial class T_RoleResourceRepository :BaseRepository<T_RoleResource>,IT_RoleResourceRepository
    {
         
    }

	

	public partial class T_UserRepository :BaseRepository<T_User>,IT_UserRepository
    {
         
    }

	

	public partial class T_UserRoleRepository :BaseRepository<T_UserRole>,IT_UserRoleRepository
    {
         
    }

	
}