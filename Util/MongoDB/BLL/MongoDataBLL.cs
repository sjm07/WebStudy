using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Log;
using Util.MongoDB.DAL;

namespace Util.MongoDB.BLL
{
    public class MongoDataBLL
    {
        #region 单例

        private static MongoDataBLL _instance = null;
        private static readonly object Instancelock = new object();

        private MongoDataBLL()
        {
        }

        public static MongoDataBLL Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Instancelock)
                    {
                        if (_instance == null)
                        {
                            _instance = new MongoDataBLL();
                        }
                    }
                }

                return _instance;
            }
        }

        #endregion

        #region Method 

        /// <summary>
        /// 按条件获得指定集合信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryObj"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static List<T> GetCollectionInfo<T>(JObject queryObj, out string msg)
        {
            List<T> ls_data = null;
            try
            {
                #region 1.获得数据

                ls_data = MongoDataDAL.GetCollectionInfo<T>(queryObj, out msg);

                #endregion
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                Logger.Error(ex);
            }

            return ls_data;
        }

        /// <summary>
        /// 新增数据到Mongodb
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string InsertFile(FileStream fileStream, JObject insertObj, out string msg)
        {
            /*mongodb主键*/
            string _id = string.Empty;
            try
            {
                _id = MongoDataDAL.InsertFile(fileStream, insertObj, out msg);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                Logger.Error(ex);
            }

            return _id;
        }

        #endregion
    }
}
