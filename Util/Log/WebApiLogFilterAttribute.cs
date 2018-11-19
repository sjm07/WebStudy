using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Util.Log
{
    /// <summary>
    /// 日志记录过滤器，主要用于跟踪问题，正常情况下不需要使用
    /// </summary>
    public class WebApiLogFilterAttribute: ActionFilterAttribute
    {
        private static readonly string enterTimeKey = "enterTime";
        private static readonly string requestArgsKey = "requestArgs";

        /// <summary>
        /// action执行前
        /// </summary>
        /// <param name="actionContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            if (SkipLogging(actionContext))
            {
                await base.OnActionExecutingAsync(actionContext, cancellationToken);
            }

            //记录进入请求的时间
            actionContext.Request.Properties[enterTimeKey] = DateTime.Now.ToBinary();
            actionContext.Request.Properties[requestArgsKey] = await Task.Run(() => (GetRequestValues(actionContext)));
            await base.OnActionExecutingAsync(actionContext, cancellationToken);
        }

        /// <summary>
        /// action执行后
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override async Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            object beginTime = null;
            object inArgs = null;
            if (actionExecutedContext.Request.Properties.TryGetValue(enterTimeKey, out beginTime))
            {
                DateTime enterTime = DateTime.FromBinary(Convert.ToInt64(beginTime)); //获取action开始执行时间
                double costTime = (DateTime.Now - enterTime).TotalMilliseconds; //执行所花费毫稍数
                HttpRequestMessage requestMessage = actionExecutedContext.Request;
                string requestMethod = requestMessage.Method.ToString(); //请求方式
                string url = requestMessage.RequestUri.AbsoluteUri; //请求绝对地址
                string ip = requestMessage.RequestUri.Host; //请求Ip地址
                string port = requestMessage.RequestUri.Port.ToString(); //请求端口号
                string controllerName = actionExecutedContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName; //控制器名称
                string actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName; //action名称
                string requestParamaters = string.Empty;
                if (actionExecutedContext.Request.Properties.TryGetValue(requestArgsKey, out inArgs))
                {
                    requestParamaters = inArgs.ToString();
                }

                //获取response响应的结果
                string executeResult = await Task.Run(()=> GetResponseValues(actionExecutedContext));
            }

             base.OnActionExecuted(actionExecutedContext);
        }

        /// <summary>
        /// action执行后
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        //public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        //{
        //    string actonName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
        //    if (actionExecutedContext.Exception != null)
        //    {
        //        //LogHelper.Error(actonName + "退出执行异常：", actionExecutedContext.Exception);
        //        //LogHelper.Error(actonName + "异常参数：", Utils.SerializeObject(actionExecutedContext.ActionContext.ActionArguments));
        //    }
        //    else
        //    {
        //        //LogHelper.Warn(actonName + "成功退出执行：", Utils.SerializeObject(actionExecutedContext.ActionContext.ActionArguments));
        //        //LogHelper.Warn(actonName + "响应：", actionExecutedContext.Response.Content.ReadAsStringAsync().Result);

        //        string a = GetRequestValues(actionExecutedContext);
        //        Logger.Info("异常！！");
        //    }
        //    base.OnActionExecuted(actionExecutedContext);
        //}

        /// <summary>
        /// 读取request 的提交内容
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        //public string GetRequestValues(HttpActionContext actionContext)
        //{
        //    string result = null;
        //    foreach (var arg in actionContext.ActionArguments)
        //    {
        //        result += $"key={arg.Key};";
        //        result += $"value={JsonConvert.SerializeObject(arg.Value)};{Environment.NewLine}";
        //    }

        //    return result;
        //}

        public JObject GetRequestValues(HttpActionContext actionContext)
        {
            JObject resultObj = new JObject();
            foreach (var arg in actionContext.ActionArguments)
            {
                resultObj[arg.Key] = $"{ JsonConvert.SerializeObject(arg.Value)}";

                

                //result += $"key={arg.Key};";
                //result += $"value={JsonConvert.SerializeObject(arg.Value)};{Environment.NewLine}";
            }
            return resultObj;
        }

        /// <summary>
        /// 读取request 的提交内容
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        /// <returns></returns>
        public string GetRequestValues(HttpActionExecutedContext actionExecutedContext)
        {
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
            return actionExecutedContext.Request.Content.ReadAsStringAsync().Result;
        }

        /// <summary>
        /// 读取action返回的result
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        /// <returns></returns>
        public string GetResponseValues(HttpActionExecutedContext actionExecutedContext)
        {
            Stream stream = actionExecutedContext.Response.Content.ReadAsStreamAsync().Result;
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
            return result;
        }

        /// <summary>
        /// 判断类和方法头上的特性是否要进行Action拦截
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        private static bool SkipLogging(HttpActionContext actionContext)
        {
            return actionContext.ActionDescriptor.GetCustomAttributes<NoLogAttribute>().Any() || actionContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<NoLogAttribute>().Any();
        }
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true)]
    public class NoLogAttribute : Attribute
    {
    }
}
