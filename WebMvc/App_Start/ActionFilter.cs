using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.App_Start
{
    /// <summary>
    /// Action拦截器接口
    /// </summary>
    public class ActionFilter : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.Controller.ViewData["OutTime"] = filterContext.Controller + ":执行Action后" + DateTime.Now;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewData["InTime"] = filterContext.Controller + ":执行Action前" + DateTime.Now;
        }
    }
}