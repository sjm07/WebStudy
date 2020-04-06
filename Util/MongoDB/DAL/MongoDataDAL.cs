using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Log;
using Util.MongoDB.DBUtility;

namespace Util.MongoDB.DAL
{
    public class MongoDataDAL
    {
        #region 文件集合

        /// <summary>
        /// 按条件获得指定集合信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryObj"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static List<T> GetCollectionInfo<T>(JObject queryObj, out string msg)
        {
            try
            {
                #region 1.组织查询条件

                string collection_name = "fs.files"; //集合名称
                if (queryObj.Property("collection_name") != null)
                {
                    collection_name = queryObj["collection_name"].ToString();
                }

                var filterBuilder = Builders<T>.Filter;
                var filter = filterBuilder.Empty;
                if (queryObj.Property("_id") != null)
                {
                    filter = filter & filterBuilder.Eq("_id", new ObjectId(queryObj["_id"].ToString()));
                }

                if (queryObj.Property("files_id") != null)
                {
                    filter = filter & filterBuilder.Eq("files_id", new ObjectId(queryObj["files_id"].ToString()));
                }

                if (queryObj.Property("filename") != null)
                {
                    filter = filter & filterBuilder.Eq("filename", queryObj["filename"].ToString());
                }

                #endregion

                #region 2.获得数据

                return DBHelperMongoDB.GetDataList<T>(collection_name, filter, out msg);

                #endregion
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                Logger.Error(ex);
                return null;
            }
        }
        
        /// <summary>
        /// 新增数据到Mongodb
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string InsertFile(FileStream fileStream, JObject insertObj, out string msg)
        {
            try
            {
                return DBHelperMongoDB.InsertFile(fileStream, insertObj, out msg);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                Logger.Error(ex);
                return string.Empty;
            }
        }

        #endregion
    }
}
