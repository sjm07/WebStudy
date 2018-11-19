using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDeom
{
    /// <summary>
    /// 索引器
    /// </summary>
    class IndexDemo
    {
        private int[] _Array = new int[10];
        private Hashtable hsTable = new Hashtable();

        public int this[int index]
        {
            get
            {
                return _Array[index];
            }
            set
            {
                _Array[index] = value;
            }
        }

        public string this[string index]
        {
            get
            {
                return hsTable[index].ToString();
            }
            set
            {
                hsTable.Add(index,value);
            }
        }
    }
}
