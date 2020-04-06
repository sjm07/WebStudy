using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Log;

namespace Util.MongoDB.DBUtility
{
    public class DBHelperMongoDB
    {
        //private static readonly string mongodbConnString = ConfigurationManager.ConnectionStrings["mongodbConn"] != null ?
        //                                 ConfigurationManager.ConnectionStrings["mongodbConn"].ToString() : "mongodb://skj009:jbinfo2019@192.168.137.105:27017/admin";

        private static readonly string mongodbConnString = ConfigurationManager.ConnectionStrings["mongodbConn"] != null ?
                                     ConfigurationManager.ConnectionStrings["mongodbConn"].ToString() : "mongodb://study:study@localhost:27017/studydb";

        #region 按BsonDocument方式封装

        /// <summary>
        /// 获得实例化后数据库
        /// </summary>
        /// <returns></returns>
        public IMongoDatabase GetDataBaseInstance_bson()
        {
            try
            {
                #region 1.数据合法性校验

                if (string.IsNullOrEmpty(mongodbConnString))
                {
                    return null;
                }

                #endregion

                #region 2.读取数据库

                // 读取连接字符串
                var mongoUrl = new MongoUrlBuilder(mongodbConnString);

                // 获取数据库名称
                string databaseName = mongoUrl.DatabaseName;

                // 创建并实例化客户端
                MongoClient _client = new MongoClient(mongoUrl.ToMongoUrl());

                // 根据数据库名称实例化数据库
                IMongoDatabase _database = _client.GetDatabase(databaseName);

                return _database;

                #endregion
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return null;
            }
        }

        /// <summary>
        /// 获得指定的集合
        /// </summary>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public IMongoCollection<BsonDocument> GetCollection_bson(string collectionName)
        {
            try
            {
                // 根据集合名称获取集合
                var collection = GetDataBaseInstance_bson().GetCollection<BsonDocument>(collectionName);
                return collection;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return null;
            }
        }

        public List<T> Get<T>()
        {
            List<T> lst = new List<T>();
            try
            {
                var collection = GetCollection_bson("fs.files");
                var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId("5cb033a166af380da6571bb2"));//完全匹配 ;
                List<BsonDocument> ls_bson = collection.Find(filter).ToList<BsonDocument>();

                foreach (var bson in ls_bson)
                {
                    var entity = BsonSerializer.Deserialize<T>(bson);
                    lst.Add(entity);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return lst;
        }

        #endregion

        #region 按自定义方式封装

        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static IMongoDatabase GetDataBaseInstance(out string msg)
        {
            try
            {
                #region 1.数据合法性校验

                msg = string.Empty;
                if (string.IsNullOrEmpty(mongodbConnString))
                {
                    msg = "数据库链接串为空";
                    return null;
                }

                #endregion

                #region 2.读取数据库

                // 读取连接字符串
                var mongoUrl = new MongoUrlBuilder(mongodbConnString);

                // 获取数据库名称
                string databaseName = mongoUrl.DatabaseName;

                // 创建并实例化客户端
                MongoClient _client = new MongoClient(mongoUrl.ToMongoUrl());

                // 根据数据库名称实例化数据库
                IMongoDatabase _database = _client.GetDatabase(databaseName);

                return _database;

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
        /// 获得集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private static IMongoCollection<T> GetCollection<T>(string collectionName, out string msg)
        {
            try
            {
                // 根据集合名称获取集合
                var collection = GetDataBaseInstance(out msg).GetCollection<T>(collectionName);
                return collection;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                Logger.Error(ex);
                return null;
            }
        }

        /// <summary>
        /// 根据指定条件获得集合内容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="filter"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static List<T> GetDataList<T>(string collectionName, FilterDefinition<T> filter, out string msg)
        {
            try
            {
                #region 1.数据合法性校验

                if (string.IsNullOrEmpty(collectionName))
                {
                    msg = "集合名称不允许为空！";
                    return null;
                }

                #endregion

                #region 2.获得数据

                // 获得集合
                var collection = GetCollection<T>(collectionName, out msg);
                if (collection == null)
                {
                    return null;
                }

                return collection.Find(filter).ToList<T>();

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
        /// <param name="insertObj"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string InsertFile(FileStream fileStream, JObject insertObj, out string msg)
        {
            try
            {
                #region 获得数据

                string collection_name = "fs"; //集合名称
                string file_name = string.Empty, ext = string.Empty;
                if (insertObj.Property("file_name") != null)
                {
                    file_name = insertObj["file_name"].ToString();
                }
                if (insertObj.Property("ext") != null)
                {
                    ext = insertObj["ext"].ToString();
                }
                if (insertObj.Property("collection_name") != null)
                {
                    collection_name = insertObj["collection_name"].ToString();
                }

                #endregion

                #region 上传

                IMongoDatabase _database = GetDataBaseInstance(out msg);

                //初始化GridFSBucket
                var bucket = new GridFSBucket(_database, new GridFSBucketOptions
                {
                    BucketName = collection_name,//设置根节点名
                    ChunkSizeBytes = 1024 * 256,   //设置块的大小为256KB
                    WriteConcern = WriteConcern.WMajority,     //写入确认级别为majority
                    ReadPreference = ReadPreference.Secondary  //优先从从节点读取
                });

                ObjectId fileId;
                fileId = bucket.UploadFromStream(filename: file_name + "." + ext, source: fileStream, options: null);
                return fileId.ToString();

                #endregion
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
