using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDeom
{
    class Judgment
    {
        public delegate void delegateRun();
        public event delegateRun eventRun;
        public void Begin()
        {
            eventRun();
        }
    }

    class RunSports
    {
        public void Run()
        {
            Console.WriteLine("运动员开始比赛！");
            Console.ReadKey();
        }
    }
}
