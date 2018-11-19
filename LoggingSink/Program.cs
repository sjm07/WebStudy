using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Appender;
using log4net.Core;
using System.Runtime.Remoting;
using System.IO;

namespace LoggingSink
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            RemotingConfiguration.Configure(AppDomain.CurrentDomain.BaseDirectory + @"ConfigFile\Remoting.config", false);
            Console.ReadKey();
        }
    }
}
