using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDeom
{
    class Program
    {
        public delegate void PrintDelegate(string inStr);

        static void Main(string[] args)
        {
            #region 委托事例

            //FDelegate();

            #endregion

            #region 索引器事例

            FIndex();

            #endregion

            #region 事件事例

            //FEvent();

            #endregion
        }

        /// <summary>
        /// 索引器事例
        /// </summary>
        static void FIndex()
        {
            IndexDemo indexDemo = new IndexDemo();
            indexDemo[0] = 100;
            indexDemo[1] = 200;
            indexDemo["0"] = "索引器Test";
            Console.WriteLine("索引器：" + indexDemo[0]);
            Console.WriteLine("索引器重载：" + indexDemo["0"]);
            Console.ReadKey();
        }

        /// <summary>
        /// 委托事例
        /// </summary>
        static void FDelegate()
        {
            PrintDelegate printDelegate;
            printDelegate = DelegateStudy.PrintDelegateStr;
            printDelegate("sjm");
            printDelegate += DelegateStudy.SecPrintDelegateStr;
            printDelegate("sec");
            Action<string> action = DelegateStudy.PrintDelegateStr;
            action("action delegate");
            Func<string, string> func = DelegateStudy.ThirdPrintDelegateStr;
            Console.WriteLine("测试打印 func " + func("third test"));
            Console.ReadLine();
        }

        /// <summary>
        /// 事件事例
        /// </summary>
        static void FEvent()
        {
            RunSports runSports = new RunSports();
            Judgment judgment = new Judgment();
            //judgment.eventRun += new Judgment.delegateRun(runSports.Run);
            judgment.eventRun += runSports.Run;
            judgment.Begin();
        }
    }
}
