using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Model
{
    /// <summary>
    /// 中间代理类（为使用事务提供）
    /// </summary>
    public class AgencyModel
    {
        public string sql;
        public object model;
    }
}
