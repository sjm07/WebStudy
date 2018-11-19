using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.WorkOrderManage
{
    public class MyTrace : ITracingService
    {
        public void Trace(string format, params object[] args)
        {
            throw new NotImplementedException();
        }
    }
}
