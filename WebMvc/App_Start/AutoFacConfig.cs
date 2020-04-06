using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using WebMvc.Model.DB;

namespace WebMvc.App_Start
{
    public class AutoFacConfig
    {
        static IContainer container;
        public static void Register()
        {
            ContainerBuilder builder = new ContainerBuilder();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            //注册Model层
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(CLogin)));

            //告诉autofac框架注册数据仓储层所在程序集中的所有类的对象实例
            Assembly respAss = Assembly.Load("WebMvc.DAL");
            //创建respAss中的所有类的instance以此类的实现接口存储
            builder.RegisterTypes(respAss.GetTypes()).AsImplementedInterfaces();

            //告诉autofac框架注册业务逻辑层所在程序集中的所有类的对象实例
            Assembly serpAss = Assembly.Load("WebMvc.BLL");
            //创建serAss中的所有类的instance以此类的实现接口存储
            builder.RegisterTypes(serpAss.GetTypes()).AsImplementedInterfaces();

            container = builder.Build();
            DependencyResolver.SetResolver(new  AutofacDependencyResolver(container));//生成容器并提供给MVC
        }

        /// <summary>
        /// 从容器中获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static T GetFromFac<T>()
        {
            return container.Resolve<T>();
        }
    }
}