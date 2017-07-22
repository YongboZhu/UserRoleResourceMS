using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRoleResourceMS_Beat.Model;

namespace UserRoleResourceMS_Beat.IBLL
{
    public interface IT_UserService:IBaseService<T_User>
    {
        //检查用户登录信息
        T_User checkUserLogin(T_User userinfo);
        //加载用户查询的数据
        IQueryable<T_User> LoadSearchData(ModelDataQuery query);
        //删除用户信息
        int DeleteUserInfo(List<string> DeleteUserInfoID);
        bool SetUserInfoRole(string userID, List<int> roleIDList);
    }
}
