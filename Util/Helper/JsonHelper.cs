using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Util.Log;

namespace Util.Helper
{
    public class JsonHelper
    {
        public static T JsonConvertObj<T>(string content) where T : class
        {
            T obj = null;
            try
            {
                obj = JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception ex)
            {
                Logger.Error("==========================================================");
                Logger.Error("JsonHelper.JsonConvertObj：" + ex.Message);
                Logger.Error("参数:" + content);
                Logger.Error("==========================================================");
                Logger.Error(ex);
            }

            return obj;
        }

        /// <summary>
        /// 对象转换为Json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToJson(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// ListJson转换为dataTable
        /// </summary>
        /// <param name="ListJson"></param>
        /// <returns></returns>
        public static DataTable JsonConvertTable(string ListJson)
        {
            return JsonConvertObj<DataTable>(ListJson);
        }
    }
}
