using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.App_Start
{
    /// <summary>
    /// Result拦截器接口
    /// </summary>
    public class ResultFillter : FilterAttribute, IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            filterContext.Controller.ViewData["ResultAfterTime"] = filterContext.Controller + ":执行Result后" + DateTime.Now;
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.Controller.ViewData["ResultBeforeTime"] = filterContext.Controller + ":执行Result前" + DateTime.Now;
        }
    }
}