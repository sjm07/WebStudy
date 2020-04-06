using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.App_Start
{
    /// <summary>
    /// 授权拦截器接口
    /// 该拦截器专门用来判断权限，判断时候有权限执行后面的Action，此接口在任何拦截器之前执行
    /// </summary>
    public class AuthorizationFilter : FilterAttribute, IAuthorizationFilter
    {
        #region Property

        /// <summary>
        /// 是否有权限
        /// </summary>
        public bool isHavePower { get; set; }

        #endregion

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            #region 验证不通过

            //暂时让代码验证通过
            if (!isHavePower)
            {
                ContentResult Content = new ContentResult();
                Content.Content = "<script type='text/javascript'>alert('权限验证不通过！');history.go(-1);</script>";
                //filterContext.Result = Content;
                filterContext.Result = new RedirectResult("/Home/Index");
            }

            #endregion
        }
    }
}