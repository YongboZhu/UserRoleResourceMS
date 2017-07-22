using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRoleResourceMS_Beat.Model
{
    public class ModelDataQuery
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public int total { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string ResourceName { get; set; }
    }
}
