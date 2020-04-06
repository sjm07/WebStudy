using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebMvc.IDAL
{
    /// <summary>
    /// 基接口
    /// </summary>
    public interface IBaseDAL<T> where T : class
    {
        #region Method

        /// <summary>
        /// 新增
        /// </summary>
        T Insert(T model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        int Update(T model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        int Delete(T model);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        IQueryable<T> Find(Expression<Func<T, bool>> exp = null);

        int SaveChanges();

        #endregion
    }
}
