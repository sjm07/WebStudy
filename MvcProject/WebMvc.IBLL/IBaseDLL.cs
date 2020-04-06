using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebMvc.IBLL
{
    public interface IBaseDLL<T> where T : class
    {
        #region Method

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        T Insert(T model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Update(T model);

        int Delete(T model);

        IQueryable<T> Find(Expression<Func<T, bool>> exp = null);

        int SaveChanges();

        #endregion
    }
}
