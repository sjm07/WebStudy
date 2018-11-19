using log4net.Appender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Core;
using log4net.Repository;
using log4net;
using System.IO;

namespace LoggingSink
{
    /// <summary>
    /// 工单接入日志记录器
    /// 2018-11-05
    /// sjm
    /// </summary>
    public class AccessLoggingSink: MarshalByRefObject, RemotingAppender.IRemoteLoggingSink
    {
        public AccessLoggingSink()
        {
            ILoggerRepository repository = LogManager.CreateRepository("AccessRepository");
            log4net.Config.XmlConfigurator.ConfigureAndWatch(repository, new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"ConfigFile\log4net_access.config"));
            log4net.LogManager.GetRepository(repository.Name).PluginMap.Add(new log4net.Plugin.RemoteLoggingServerPlugin("LoggingSink.Access"));
        }

        public void LogEvents(LoggingEvent[] events)
        {
            throw new NotImplementedException();
        }
    }
}
