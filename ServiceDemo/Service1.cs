using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDemo
{
    public partial class Service1 : ServiceBase
    {

        ILog _log = LogManager.GetLogger("s");
        public Service1()
        {
            InitializeComponent();
            _log.Info("开始");
        }

        protected override void OnStart(string[] args)
        {
            _log.Info("启动");
        }

        protected override void OnStop()
        {
        }
    }
}
