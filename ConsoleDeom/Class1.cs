using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDeom
{
    public class Class1
    {
        public Class1()
        {
            Console.WriteLine("Class1");
            PrintFields();
        }

        public virtual void PrintFields() { }
    }

    public class Class2 : Class1
    {
        int x = 1;
        int y;
        public Class2()
        {
            Console.WriteLine("Class2");
            y = -1;
            Console.ReadLine();
        }

        public override void PrintFields()
        {
            Console.WriteLine(x);
            Console.WriteLine(y);
            //Console.ReadLine();
        }
    }
}
