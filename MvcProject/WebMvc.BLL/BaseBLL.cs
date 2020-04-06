using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebMvc.IBLL;
using WebMvc.IDAL;

namespace WebMvc.BLL
{
    public class BaseBLL<T> : IBaseDLL<T> where T : class
    {
        #region Property

        protected IBaseDAL<T> dal = null;

        #endregion

        #region Method

        public T Insert(T model)
        {
            return dal.Insert(model);
        }

        public int Update(T model)
        {
            return dal.Update(model);
        }

        public int Delete(T model)
        {
            return dal.Delete(model);
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> exp = null)
        {
            return dal.Find(exp);
        }

        public int SaveChanges()
        {
            return dal.SaveChanges();
        }

        #endregion
    }
}
