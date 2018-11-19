using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;

namespace Util.Log
{
    public class Logger
    {
        private static readonly ILog _log = LogManager.GetLogger("Logger");

        private static ILog Log
        {
            get
            {
                return _log;
            }
        }

        public static void Info(object message)
        {
            if (Log.IsInfoEnabled)
            {
                Log.Info(message);
            }
        }

        public static void Debug(object message)
        {
            if (Log.IsDebugEnabled)
            {
                Log.Debug(message);
            }
        }

        public static void Error(HttpActionContext ActionContext, Exception ex)
        {
            if (Log.IsErrorEnabled)
            {
                try
                {
                    Log.Error(string.Format("ControllerName: {0} \r\n ActionName:{1} \r\n UserHostAddress: {2} \r\n AbsoluteUri:{3} \r\n Request:{4} \r\n 错误内容：{5}"
                        , ActionContext.ActionDescriptor.ActionName
                        , ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName
                        , HttpContext.Current.Request.UserHostAddress
                        , HttpContext.Current.Request.Url.AbsoluteUri
                        , GetRequestValues(ActionContext)
                        , ex));
                }
                catch
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 读取request 的提交内容
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        /// <returns></returns>
        public static string GetRequestValues(HttpActionContext actionExecutedContext)
        {
            #region 这段代码貌似跟返回结果无关，但是注释掉之后，结果一直为空，求解释。。。
            Stream stream = actionExecutedContext.Request.Content.ReadAsStreamAsync().Result;
            Encoding encoding = Encoding.UTF8;
            /*
                这个StreamReader不能关闭，也不能dispose， 关了就傻逼了
                因为你关掉后，后面的管道  或拦截器就没办法读取了
            */
            var reader = new StreamReader(stream, encoding);
            string result = reader.ReadToEnd();
            /*
            这里也要注意：   stream.Position = 0;
            当你读取完之后必须把stream的位置设为开始
            因为request和response读取完以后Position到最后一个位置，交给下一个方法处理的时候就会读不到内容了。
            */
            stream.Position = 0;
            #endregion

            return actionExecutedContext.Request.Content.ReadAsStringAsync().Result;
        }
    }
}
