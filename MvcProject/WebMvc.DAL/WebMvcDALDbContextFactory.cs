using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using WebMvc.Model;

namespace WebMvcDAL
{
    class WebMvcDALDbContextFactory
    {
        /// <summary>
        /// 获取当前EF上下文的唯一实例
        /// </summary>
        /// <returns></returns>
        public static DbContext GetCurrentThreadInstance()
        {
            DbContext obj = CallContext.GetData(typeof(WebMvcDALDbContextFactory).FullName) as DbContext;
            if (obj == null)
            {
                obj = new WebMvcDbContext();
                CallContext.SetData(typeof(WebMvcDALDbContextFactory).FullName, obj);
            }
            return obj;
        }
    }
}
