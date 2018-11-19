using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.WorkOrderManage
{
    /// <summary>
    /// 工单结单后业务处理
    /// Message = Update
    /// 异步
    /// </summary>
    public class hsicrm_wo_workordertest_post:  IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
                //base.Initialize(serviceProvider);

                //IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
                //ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
                //IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                //IOrganizationService service = serviceFactory.CreateOrganizationService(null);

                IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
                IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                IOrganizationService service = serviceFactory.CreateOrganizationService(null);
                ITracingService tracingservice = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

                string a = "plugin";

                tracingservice.Trace("test trace");

                //MyTrace b = new MyTrace();
                //b.Trace("sjm");

                //int b = Int32.Parse(a);

                //throw new Exception("plugin test");
        }
    }
}
