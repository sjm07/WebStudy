using System.Web;
using System.Web.Mvc;
using WebMvc.App_Start;

namespace WebMvc
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new ActionFilter());
        }
    }
}
