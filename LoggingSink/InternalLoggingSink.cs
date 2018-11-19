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
    /// 内容接口日志记录器
    /// 2018-11-05
    /// sjm
    /// </summary>
    public class InternalLoggingSink : MarshalByRefObject, RemotingAppender.IRemoteLoggingSink
    {
        public InternalLoggingSink()
        {
            ILoggerRepository repository = LogManager.CreateRepository("InternalRepository");
            log4net.Config.XmlConfigurator.ConfigureAndWatch(repository, new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"ConfigFile\log4net_internal.config"));
            log4net.LogManager.GetRepository(repository.Name).PluginMap.Add(new log4net.Plugin.RemoteLoggingServerPlugin("LoggingSink.Internal"));
        }

        public void LogEvents(LoggingEvent[] events)
        {
            throw new NotImplementedException();
        }
    }
}
