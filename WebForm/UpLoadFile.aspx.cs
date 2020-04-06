using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm
{
    public partial class UpLoadFile : System.Web.UI.Page
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

        public void Save()
        {
            HttpFileCollection _file = HttpContext.Current.Request.Files;

            string username =  Request["username"].ToString();

            long size = _file[0].ContentLength;
            //图片类型   
            string type = _file[0].ContentType;
            //图片名  
            string name = _file[0].FileName;

            //图片格式   
            string _tp = System.IO.Path.GetExtension(name);

            //创建新的图片名
            string saveName = DateTime.Now.ToString("yyyyMMdd") + Guid.NewGuid().ToString() + _tp;

            string code_path = System.Web.HttpContext.Current.Server.MapPath("/UpLoadImage/");
            DirectoryInfo dInfo = new DirectoryInfo(code_path);
            if (!dInfo.Exists)
            {
                dInfo.Create();
            }

            //string qcFullPath = Path.Combine(code_path, qcName);

            //寻找图片夹目录，没有则创建
            //string path = context.Server.MapPath("~/Uploadfileorimg/");

            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(path);
            //    DirectoryInfo dirInfo = new DirectoryInfo(path);
            //    dirInfo.Attributes = FileAttributes.Normal & FileAttributes.Directory;
            //}
            //保存图片到指定路径
            _file[0].SaveAs(code_path + saveName);
        }
    }
}