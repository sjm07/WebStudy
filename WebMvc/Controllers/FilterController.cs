using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMvc.App_Start;

namespace WebMvc.Controllers
{
    /// <summary>
    /// 拦截器Controller
    /// </summary>
    public class FilterController : Controller
    {
        [ExceptionFilter]
        [ActionFilter]
        [ResultFillter]
        [AuthorizationFilter(isHavePower = true)]
        public ActionResult Index()
        {
            System.Threading.Thread.Sleep(2000);
            //int b = int.Parse("a");
            ViewBag.Test = "sjm";
            System.Threading.Thread.Sleep(2000);
            ViewData.ModelState.AddModelError("errorMsg", "测试错误消息，在前端显示");
            return View();
        }


    }
}