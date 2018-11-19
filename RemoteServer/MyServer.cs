using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace RemoteServer
{
    class MyServer
    {
        [STAThread]
        static void Main(string[] args)
        {
            RemotingConfiguration.Configure("RemoteServer.exe.config");
            Console.ReadLine();
        }
    }
}
