using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin
{
    public class PluginBase
    {
        #region 相关服务

        //调试沙箱插件使用的跟踪服务
        protected ITracingService tracingservice;

        //插件的上下文
        protected IPluginExecutionContext context;

        //组织服务工厂
        protected IOrganizationServiceFactory serviceFactory;

        //组织服务
        protected IOrganizationService service;

        protected OrganizationServiceContext orgServiceContext;

        //相关记录
        protected Entity targer;
        protected Entity pretarger;
        protected Entity posttarger;

        protected EntityReference targerref;
        protected EntityReferenceCollection targerrefc;
        protected Relationship targerrel;

        //触发操作
        protected bool isCreate;
        protected bool isUpdate;
        protected bool isDelete;

        #endregion

        public void Initialize(IServiceProvider serviceProvider)
        {
            #region 相关服务的初始化

            tracingservice = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            service = serviceFactory.CreateOrganizationService(null);
            orgServiceContext = new OrganizationServiceContext(service);

            //Entity preTarger =  (Entity)context.PreEntityImages["AliasImage"];
            //Entity postTarger = (Entity)context.PostEntityImages["AliasImage"];


            if (context.InputParameters.Contains("Target"))
            {
                if (context.InputParameters["Target"] is Entity)
                {
                    targer = (Entity)context.InputParameters["Target"];
                }
                else if (context.InputParameters["Target"] is EntityReference)
                {
                    targerref = (EntityReference)context.InputParameters["Target"];
                }
                else
                { }

                
            }

            #endregion
        }
    }
}
