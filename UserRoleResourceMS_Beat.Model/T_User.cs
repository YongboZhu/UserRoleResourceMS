//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace UserRoleResourceMS_Beat.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_User
    {
        public T_User()
        {
            this.T_UserRole = new HashSet<T_UserRole>();
        }
    
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CreateOperatorID { get; set; }
        public string CreateOperatorName { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string ModiOperatorID { get; set; }
        public string ModiOperatorName { get; set; }
        public System.DateTime ModiTime { get; set; }
        public string Notes { get; set; }
    
        public virtual ICollection<T_UserRole> T_UserRole { get; set; }
    }
}
