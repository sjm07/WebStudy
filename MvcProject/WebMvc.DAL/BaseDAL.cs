using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebMvc.IDAL;
using WebMvc.Model;

namespace WebMvcDAL
{
    public class BaseDAL<T> : IBaseDAL<T> where T : class
    {
        WebMvcDbContext webDb = WebMvcDbContextFactory.GetCurrentInstance();
        DbContext db = WebMvcDALDbContextFactory.GetCurrentThreadInstance();

        public BaseDAL()
        {
            _context = WebMvcDbContextFactory.GetCurrentInstance();
        }

        #region Property

        private WebMvcDbContext _context;
        public WebMvcDbContext dbContext
        {
            get { return _context; }
            private set {
                _context = value;
            }
        }

        #endregion

        #region Method

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public T Insert(T model)
        {
            dbContext.Entry<T>(model).State = EntityState.Added;
            if (dbContext.SaveChanges() > 0)
                dbContext.Entry<T>(model).State = EntityState.Detached;
            return model;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        public int Update(T model)
        {
            dbContext.Entry<T>(model).State = EntityState.Modified;
            if (dbContext.SaveChanges() > 0)
                dbContext.Entry<T>(model).State = EntityState.Detached;
            return dbContext.SaveChanges();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        public int Delete(T model)
        {
            //db.Set<T>().Remove(model);
            // EF 6.0写法
            dbContext.Entry<T>(model).State = EntityState.Deleted;
            return dbContext.SaveChanges();
        }

        private IQueryable<T> Filter(Expression<Func<T, bool>> exp)
        {
            var dbSet = dbContext.Set<T>().AsQueryable();
            if (exp != null)
                dbSet = dbSet.Where(exp);
            return dbSet;
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> exp = null)
        {
            return Filter(exp);
        }

        public int SaveChanges()
        {
            return dbContext.SaveChanges();
        }

        #endregion
    }
}
