using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDeom
{
    public  class DelegateStudy
    {
        public static void PrintDelegateStr(string inStr)
        {
            Console.WriteLine(string.Format(@"测试委托打印，输入值是：{0}", inStr));
            //Console.ReadLine();
        }

        public static void SecPrintDelegateStr(string inStr)
        {
            Console.WriteLine(string.Format(@"Second测试委托打印，输入值是：{0}", inStr));
            //Console.ReadLine();
        }

        public static string ThirdPrintDelegateStr(string inStr)
        {
            return "third delegate";
        }
    }
}
