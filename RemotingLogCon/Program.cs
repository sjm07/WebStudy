using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Appender;
using log4net.Core;
using System.Runtime.Remoting;

namespace RemotingLogCon
{
    public class Program : MarshalByRefObject, log4net.Appender.RemotingAppender.IRemoteLoggingSink
    {
        //public ServerLoggingContext Context;

        private static readonly ILog Logger =

            LogManager.GetLogger(typeof(Program));

        private bool _shutdown = false;

        //public override object InitializeLifetimeService()
        //{
        //    return null;
        //}

        //public void Shutdown()
        //{
        //    lock (this)
        //    { 
        //        _shutdown = true;
        //    }
        //}

        [STAThread]
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo("log4net.config"));
            RemotingConfiguration.Configure("Remoting.config");
            log4net.LogManager.GetRepository().PluginMap.Add(new log4net.Plugin.RemoteLoggingServerPlugin("RemotingLogCon"));

            ILog _log = LogManager.GetLogger("Logger");
            _log.Info("Main sjm");
            Console.ReadLine();
        }

        void RemotingAppender.IRemoteLoggingSink.LogEvents(LoggingEvent[] events)
        {
            Logger.Info("LogEvents");
            if (_shutdown || (events == null)) return;
            lock (this)
            {
                //using (Context = new ServerLoggingContext())
                //{
                foreach (LoggingEvent le in events)
                {
                    //if (LoggingHelper.LevelFromString(le.Level.Name) == LoggingLevels.Fatal)
                    Logger.Logger.Log(le);
                }
                //}
            }
        }
    }
}
