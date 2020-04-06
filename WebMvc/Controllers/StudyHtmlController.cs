using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Controllers
{
    public class StudyHtmlController : Controller
    {
        // GET: StudyHtml
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult Other()
        {
            return View();
        }
    }
}