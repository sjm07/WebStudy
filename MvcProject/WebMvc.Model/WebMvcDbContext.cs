using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMvc.Model.DB;

namespace WebMvc.Model
{
    /// <summary>
    /// 定义本项目数据上下文
    /// </summary>
    public class WebMvcDbContext : DbContext
    {
        /// <summary>
        /// 避免数据库被自动创建或自动迁移
        /// </summary>
        static WebMvcDbContext()
        {
            Database.SetInitializer<WebMvcDbContext>(null);
        }

        public WebMvcDbContext() : base("name=WebMvcDBContext")
        {
            
        }

        #region 上下文集合

        public DbSet<CEmployee> Employees { get; set; }

        #endregion
    }
}
