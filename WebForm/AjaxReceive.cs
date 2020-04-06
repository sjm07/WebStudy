using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Services;

namespace WebForm
{
    public partial class AjaxReceive : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string method = Request.QueryString["Method"];
                if (!string.IsNullOrEmpty(method))
                {
                    MethodInfo methodInfo = this.GetType().GetMethod(method);
                    methodInfo.Invoke(this, null);
                }
            }
        }

        
        public void Decrypt()
        {
            JsEncryptHelper jsHelper = new JsEncryptHelper();
            string a = Request["username"] + "";
            string b = Request["passwd"] + "";
            a = jsHelper.Decrypt(a);
            b = jsHelper.Decrypt(b);
            Response.Write(new { success = true, a = a, b = b });
            Response.End();
        }
    }
}