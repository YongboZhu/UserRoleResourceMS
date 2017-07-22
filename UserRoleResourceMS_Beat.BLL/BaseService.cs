using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRoleResourceMS_Beat.DAL;
using UserRoleResourceMS_Beat.IBLL;
using UserRoleResourceMS_Beat.IDAL;
using UserRoleResourceMS_Beat.Model;

namespace UserRoleResourceMS_Beat.BLL
{
    public class BaseService<T>where T:class,new()
    {
        BaseRepository<T> IBR = new BaseRepository<T>();
        //添加
        public T AddEntities(T entity)
        {

            var addentity = IBR.AddEntities(entity);
            if (addentity != null)
            {
                return addentity;
            }
            else
            {
                return null;
            }
        }

        //修改
        public bool UpdateEntities(T entity)
        {
            var updateEntity = IBR.UpdateEntities(entity);
            if (updateEntity)
            {
                return updateEntity;
            }
            else
            {
                return false;
            }
        }


        //修改
        public bool DeleteEntities(T entity)
        {
            var deleteEntity =IBR.DeleteEntities(entity);
            return deleteEntity;
        }


        //查询
        public IQueryable<T> LoadEntities(Func<T, bool> wherelambda)
        {
            return IBR.LoadEntities(wherelambda);
        }


        //分页
        public IQueryable<T> LoadPagerEntities<S>(int pageSize, int pageIndex,
             out int total, Func<T, bool> whereLambda, bool isAsc, Func<T, S> orderByLambda)
        {
            return IBR.LoadPagerEntities(pageSize, pageIndex, out total, whereLambda, isAsc, orderByLambda);

        }
    }
}
