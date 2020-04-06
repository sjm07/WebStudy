using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMvc.App_Start;
using WebMvc.Model.DB;
using WebMvc.Model.Query;
using WebMvc.Models;

namespace WebMvc.Controllers
{
    public class TestController : Controller
    {
        private CLogin _cLogin;
        private CLogin_Query _cLogin_Query;

        /// <summary>
        /// 构造函数通过查找方式（GetFromFac）给其它注册类赋值
        /// Global.asax.cs文件中可以不用注册这个Controller
        /// </summary>
        public TestController()
        {
            this._cLogin = AutoFacConfig.GetFromFac<CLogin>();
            this._cLogin_Query = AutoFacConfig.GetFromFac<CLogin_Query>();
        }

        /// <summary>
        /// 构造函数中定义了其它注册类对象的情况，
        /// Global.asax.cs文件中必须同时注册了这个Controller，否则报错
        /// </summary>
        /// <param name="clogin"></param>
        /// <param name="cLogin_query"></param>
        //public TestController(CLogin clogin, CLogin_Query cLogin_query)
        //{
        //    this._cLogin = clogin;
        //    this._cLogin_Query = cLogin_query;
        //}

        // GET: Test
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Index(TestModel testModel)
        {
            _cLogin.user_id = "sjm";
            _cLogin_Query.username = "苏继明";
            testModel.result = testModel.num1 + testModel.num2;
            ViewBag.User_ID = _cLogin.user_id;
            ViewData["username"] = _cLogin_Query.username;
            return View(testModel);
        }

        public ActionResult RedirectToAction()
        {
            return RedirectToAction("q","Test");
        }

        public ActionResult Content()
        {
            return Redirect("q");
            //return Content("content");
        }

        public ActionResult myAction()
        {
            Dictionary<string, string> aa = new Dictionary<string, string>();


            return Json(aa, JsonRequestBehavior.AllowGet);
        }

        public ActionResult q()
        {
            return View();
        }

        public ActionResult A()
        {
            return View();
        }

        
    }
}