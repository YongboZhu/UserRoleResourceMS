using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRoleResourceMS_Beat.IDAL
{
    public partial interface IBaseRepository<T> where T : class,new()
    {
        T AddEntities(T entity);
        bool UpdateEntities(T entity);
        bool DeleteEntities(T entity);
        IQueryable<T> LoadEntities(Func<T, bool> wherelambda);
        IQueryable<T> LoadPagerEntities<S>(int pageSize, int pageIndex, out int total, Func<T, bool> whereLambda, bool isAsc, Func<T, S> orderByLambda);
    }
}
