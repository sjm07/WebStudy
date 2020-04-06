using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.XData
{
    public class XLoadAdapter : DataAdapter
    {
        public XLoadAdapter()
        {
        }

        public int FillFromReader(DataSet ds, IDataReader dataReader, int startRecord, int maxRecords)
        {
            return this.Fill(ds, "Table", dataReader, startRecord, maxRecords);
        }
    }
}
