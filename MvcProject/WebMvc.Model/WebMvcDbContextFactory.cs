using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace WebMvc.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class WebMvcDbContextFactory
    {
        /// <summary>
        /// 获取当前EF上下文的唯一实例
        /// </summary>
        /// <returns></returns>
        public static DbContext GetCurrentThreadInstance()
        {
            DbContext obj = CallContext.GetData(typeof(WebMvcDbContextFactory).FullName) as DbContext;
            if (obj == null)
            {
                obj = new WebMvcDbContext();
                CallContext.SetData(typeof(WebMvcDbContextFactory).FullName, obj);
            }
            return obj;
        }

        public static WebMvcDbContext GetCurrentInstance()
        {
            WebMvcDbContext obj = CallContext.GetData(typeof(WebMvcDbContextFactory).FullName) as WebMvcDbContext;
            if (obj == null)
            {
                obj = new WebMvcDbContext();
                CallContext.SetData(typeof(WebMvcDbContextFactory).FullName, obj);
            }
            return obj;
        }

    }
}
