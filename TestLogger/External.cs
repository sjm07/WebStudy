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
    public class External : MarshalByRefObject, RemotingAppender.IRemoteLoggingSink
    {
        //public External()
        //{
        //    log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"ConfigFile\log4net1.config"));
        //    log4net.LogManager.GetRepository().PluginMap.Add(new log4net.Plugin.RemoteLoggingServerPlugin("TestLogger.wem"));
        //    ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().GetType());
        //    _log.Info("External 接口");
        //    //Console.ReadKey();
        //}

        public External()
        {
            ILoggerRepository repository = LogManager.CreateRepository("NETCoreRepository_wem");
            log4net.Config.XmlConfigurator.ConfigureAndWatch(repository, new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"ConfigFile\log4net1.config"));
            log4net.LogManager.GetRepository(repository.Name).PluginMap.Add(new log4net.Plugin.RemoteLoggingServerPlugin("TestLogger.wem"));
            ILog _log = LogManager.GetLogger(repository.Name,"Logger");
            _log.Info("External 接口");
            //Console.ReadKey();
        }

        public void LogEvents(LoggingEvent[] events)
        {
            throw new NotImplementedException();
        }
    }
}
