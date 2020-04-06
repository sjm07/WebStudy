using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.App_Start
{
    /// <summary>
    /// 异常拦截器接口
    /// </summary>
    public class ExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
            filterContext.Controller.ViewData["ErrorMessage"] = ex;
            filterContext.Result = new ViewResult()
            {
                ViewName = "Error",
                ViewData = filterContext.Controller.ViewData
            };
            filterContext.ExceptionHandled = true;

        }
    }
}