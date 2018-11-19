using log4net.Appender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Core;
using System.Runtime.Remoting;
using System.IO;
using log4net;
using log4net.Repository;

namespace TestLogger
{
    public class Internal : MarshalByRefObject, RemotingAppender.IRemoteLoggingSink
    {
        //public Internal()
        //{
        //    log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"ConfigFile\log4net.config"));
        //    log4net.LogManager.GetRepository().PluginMap.Add(new log4net.Plugin.RemoteLoggingServerPlugin("TestLogger.rem"));
        //    ILog _log = LogManager.GetLogger("Logger");
        //    _log.Info("Internal 接口");
        //    //Console.ReadKey();
        //}

        public Internal()
        {
            ILoggerRepository repository = LogManager.CreateRepository("NETCoreRepository");
            log4net.Config.XmlConfigurator.ConfigureAndWatch(repository,new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"ConfigFile\log4net.config"));
            log4net.LogManager.GetRepository(repository.Name).PluginMap.Add(new log4net.Plugin.RemoteLoggingServerPlugin("TestLogger.rem"));
            ILog _log = LogManager.GetLogger(repository.Name,"Logger");
            _log.Info("Internal 接口");
            //Console.ReadKey();
        }

        public void LogEvents(LoggingEvent[] events)
        {
            throw new NotImplementedException();
        }
    }
}
