using log4net.Appender;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using log4net.Core;

namespace TestLogger
{
    public class Program
    {
        //[STAThread]
        //static void Main(string[] args)
        //{
        //    RemotingConfiguration.Configure(AppDomain.CurrentDomain.BaseDirectory + @"ConfigFile\Remoting.config", false);
        //    log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"ConfigFile\log4net.config"));
        //    log4net.LogManager.GetRepository().PluginMap.Add(new log4net.Plugin.RemoteLoggingServerPlugin("TestLogger.rem"));
        //    Console.ReadKey();
        //}


        [STAThread]
        static void Main(string[] args)
        {
            RemotingConfiguration.Configure(AppDomain.CurrentDomain.BaseDirectory + @"ConfigFile\Remoting.config", false);
            //log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"ConfigFile\log4net.config"));
            //log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"ConfigFile\log4net1.config"));
            //log4net.LogManager.GetRepository().PluginMap.Add(new log4net.Plugin.RemoteLoggingServerPlugin("TestLogger.rem"));
            //log4net.LogManager.GetRepository().PluginMap.Add(new log4net.Plugin.RemoteLoggingServerPlugin("TestLogger.wem"));
            Console.ReadKey();
        }
    }
}
