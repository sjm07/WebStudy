using Dapper;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Log;
using Util.Model;
using Util.XData;

namespace Util.Dapper
{
    public class DBHelperMySQL
    {
        #region Property

        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["MySqlConn"] != null ?
                                         ConfigurationManager.ConnectionStrings["MySqlConn"].ToString() : string.Empty;

        #endregion

        #region Method

        #region 按数据集方式获得数据

        /// <summary>
        /// 获得Table数据
        /// </summary> 
        /// <param name="sql"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string sql, out string msg)
        {
            DateTime beforDT = DateTime.Now;
            DataTable dt = new DataTable();
            try
            {
                #region 数据合法性校验 
                
                if (string.IsNullOrEmpty(sql))
                {
                    msg = "Sql语句为空！";
                    return dt;
                }

                if (string.IsNullOrEmpty(ConnectionString))
                {
                    msg = "数据库链接串为空！";
                    return dt;
                }

                #endregion

                #region 获得数据

                using (IDbConnection connection = new MySqlConnection(ConnectionString))
                {
                    msg = "获取数据成功";
                    var reader = connection.ExecuteReader(sql);
                    dt.Load(reader);

                    Logger.Info("查询开始==========================================================");
                    Logger.Info("查询时间：" + DateTime.Now.Subtract(beforDT).TotalMilliseconds.ToString() + " 毫秒");
                    Logger.Info("GetDataTable()请求SQL:" + sql);
                    Logger.Info("查询结束==========================================================");

                    return dt;
                }

                #endregion
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                Logger.Error("异常信息开始==========================================================");
                Logger.Error("查询时间：" + DateTime.Now.Subtract(beforDT).TotalMilliseconds.ToString() + " 毫秒");
                Logger.Error("GetDataTable()请求SQL:" + sql);
                Logger.Error("GetDataTable()异常信息:" + ex);
                Logger.Error("异常信息结束==========================================================");
                return dt;
            }
        }

        /// <summary>
        /// 获得数据集合
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string sql, out string msg)
        {
            DateTime beforDT = DateTime.Now;
            try
            {
                #region 数据合法性校验 
                
                if (string.IsNullOrEmpty(sql))
                {
                    msg = "Sql语句为空！";
                    return null;
                }

                if (string.IsNullOrEmpty(ConnectionString))
                {
                    msg = "数据库链接串为空！";
                    return null;
                }

                #endregion

                #region 获得数据

                using (IDbConnection connection = new MySqlConnection(ConnectionString))
                {
                    msg = "数据获取成功!";
                    DataSet dataSet = new XDataSet();
                    IDataReader reader = connection.ExecuteReader(sql);
                    dataSet.Load(reader, LoadOption.OverwriteChanges, null, new DataTable[] { });

                    Logger.Info("查询开始==========================================================");
                    Logger.Info("查询时间：" + DateTime.Now.Subtract(beforDT).TotalMilliseconds.ToString() + " 毫秒");
                    Logger.Info("GetDataSet()请求SQL:" + sql);
                    Logger.Info("查询结束==========================================================");

                    return dataSet;
                }

                #endregion
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                Logger.Error("异常信息开始==========================================================");
                Logger.Error("查询时间：" + DateTime.Now.Subtract(beforDT).TotalMilliseconds.ToString() + " 毫秒");
                Logger.Error("GetDataSet()请求SQL:" + sql);
                Logger.Error("GetDataSet()异常信息:" + ex);
                Logger.Error("异常信息结束==========================================================");
                return null;
            }
        }

        #endregion

        #region 按实体类方式获得数据

        /// <summary>
        /// 通过主键获得实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static T GetEntityById<T>(string id, out string msg) where T : class
        {
            DateTime beforDT = DateTime.Now;
            try
            {
                #region 1.数据合法性校验
                
                if (string.IsNullOrEmpty(id))
                {
                    msg = "主键Id为空！";
                    return default(T);
                }

                if (string.IsNullOrEmpty(ConnectionString))
                {
                    msg = "数据库链接串为空！";
                    return default(T);
                }

                #endregion

                #region 2.获得数据

                using (IDbConnection connection = new MySqlConnection(ConnectionString))
                {
                    msg = "获取数据成功";
                    T model = connection.Get<T>(id);
                    Logger.Info("查询开始==========================================================");
                    Logger.Info("查询时间：" + DateTime.Now.Subtract(beforDT).TotalMilliseconds.ToString() + " 毫秒");
                    Logger.Info("GetEntityById<T>()请求条件:" + id);
                    Logger.Info("查询结束==========================================================");
                    return model;
                }  

                #endregion
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                Logger.Error("异常信息开始==========================================================");
                Logger.Error("查询时间：" + DateTime.Now.Subtract(beforDT).TotalMilliseconds.ToString() + " 毫秒");
                Logger.Error("GetEntityById<T>()请求条件:" + id);
                Logger.Error("GetEntityById<T>()异常信息:" + ex.Message);
                Logger.Error("异常信息结束==========================================================");
                return default(T);
            }
        }

        /// <summary>
        /// 获得单条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static T GetSingleList<T>(string sql, out string msg)
        {
            DateTime beforDT = DateTime.Now;
            try
            {
                #region 1.数据合法性校验

                if (string.IsNullOrEmpty(sql))
                {
                    msg = "Sql语句为空！";
                    return default(T);
                }

                if (string.IsNullOrEmpty(ConnectionString))
                {
                    msg = "数据库链接串为空！";
                    return default(T);
                }

                #endregion

                #region 2.获得数据

                using (IDbConnection connection = new MySqlConnection(ConnectionString))
                {
                    msg = "获取数据成功";
                    List<T> list = connection.Query<T>(sql).AsList();
                    Logger.Info("查询开始==========================================================");
                    Logger.Info("查询时间：" + DateTime.Now.Subtract(beforDT).TotalMilliseconds.ToString() + " 毫秒");
                    Logger.Info("GetSingleList<T>()请求SQL:" + sql);
                    Logger.Info("查询结束==========================================================");
                    if (list.Count > 0)
                    {
                        return list[0];
                    }
                    else
                    {
                        return default(T);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                Logger.Error("异常信息开始==========================================================");
                Logger.Error("查询时间：" + DateTime.Now.Subtract(beforDT).TotalMilliseconds.ToString() + " 毫秒");
                Logger.Error("GetSingleList<T>()请求SQL:" + sql);
                Logger.Error("GetSingleList<T>()异常信息:" + ex.Message);
                Logger.Error("异常信息结束==========================================================");
                return default(T);
            }
        }

        /// <summary>
        /// 获得多条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static List<T> GetMoreList<T>(string sql, out string msg)
        {
            DateTime beforDT = DateTime.Now;
            try
            {
                #region 1.数据合法性校验

                if (string.IsNullOrEmpty(sql))
                {
                    msg = "Sql语句为空！";
                    return null;
                }

                if (string.IsNullOrEmpty(ConnectionString))
                {
                    msg = "数据库链接串为空！";
                    return null;
                }

                #endregion

                #region 2.获得数据

                using (IDbConnection connection = new MySqlConnection(ConnectionString))
                {
                    msg = "获取数据成功";
                    Logger.Info("查询开始==========================================================");
                    Logger.Info("查询时间：" + DateTime.Now.Subtract(beforDT).TotalMilliseconds.ToString() + " 毫秒");
                    Logger.Info("GetMoreList<T>()请求SQL:" + sql);
                    Logger.Info("查询结束==========================================================");
                    return connection.Query<T>(sql).AsList();
                }

                #endregion
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                Logger.Error("异常信息开始==========================================================");
                Logger.Error("查询时间：" + DateTime.Now.Subtract(beforDT).TotalMilliseconds.ToString() + " 毫秒");
                Logger.Error("GetMoreList<T>()请求SQL:" + sql);
                Logger.Error("GetMoreList<T>()异常信息:" + ex.Message);
                Logger.Error("异常信息结束==========================================================");
                return null;
            }
        }

        /// <summary>
        /// 获得多条数据－带分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static Tuple<List<T>, int> GetDataList_Page<T>(string sql, out string msg)
        {
            DateTime beforDT = DateTime.Now;
            try
            {
                using (IDbConnection connection = new MySqlConnection(ConnectionString))
                {
                    try
                    {
                        msg = "数据获取成功!";
                        List<T> rows = null;
                        int totalCount = 0;
                        using (var queryMulti = connection.QueryMultiple(sql))
                        {
                            if (!queryMulti.IsConsumed)
                            {
                                rows = queryMulti.Read<T>() as List<T>;
                                totalCount = queryMulti.Read<int>().Single();
                            }
                        }

                        Logger.Info("查询开始==========================================================");
                        Logger.Info("查询时间：" + DateTime.Now.Subtract(beforDT).TotalMilliseconds.ToString() + " 毫秒");
                        Logger.Info("GetDataList_Page<T>()请求SQL:" + sql);
                        Logger.Info("查询结束==========================================================");
                        return new Tuple<List<T>, int>(rows, totalCount);
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                        Logger.Error(ex.Message);
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                Logger.Error("异常信息开始==========================================================");
                Logger.Error("查询时间：" + DateTime.Now.Subtract(beforDT).TotalMilliseconds.ToString() + " 毫秒");
                Logger.Error("GetDataList_Page<T>()请求SQL:" + sql);
                Logger.Error("GetDataList_Page<T>()异常信息:" + ex.Message);
                Logger.Error("异常信息结束==========================================================");
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表-带分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="countSql"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static Tuple<List<T>, int> GetDataList_Page<T>(string sql, string countSql, out string msg)
        {
            DateTime beforDT = DateTime.Now;
            try
            {
                using (IDbConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    IDbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        msg = "数据获取成功!";
                        var rows = connection.Query<T>(sql, transaction) as List<T>;
                        var totalCount = connection.Query<int>(countSql, transaction).Single();
                        transaction.Commit();
                        Logger.Info("查询开始==========================================================");
                        Logger.Info("查询时间：" + DateTime.Now.Subtract(beforDT).TotalMilliseconds.ToString() + " 毫秒");
                        Logger.Info("GetDataList_Page<T>()请求SQL:" + sql);
                        Logger.Info("查询结束==========================================================");
                        return new Tuple<List<T>, int>(rows, totalCount);
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                        Logger.Error(ex.Message);
                        transaction.Rollback();
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                Logger.Error("异常信息开始==========================================================");
                Logger.Error("查询时间：" + DateTime.Now.Subtract(beforDT).TotalMilliseconds.ToString() + " 毫秒");
                Logger.Error("GetDataList_Page<T>()请求SQL:" + sql);
                Logger.Error("GetDataList_Page<T>()异常信息:" + ex.Message);
                Logger.Error("异常信息结束==========================================================");
                return null;
            }
        }

        #endregion

        #region 多表事务执行

        /// <summary>
        /// 采用事务执行SQL语句（支持insert、Update、Delete三种数据）
        /// </summary>
        /// <param name="lsSqls"></param>
        /// <returns></returns>
        public static JObject TransactionExcute(List<string> lsSqls)
        {
            JObject resultObj = new JObject();
            resultObj["flag"] = false;
            resultObj["msg"] = string.Empty;
            try
            {
                using (IDbConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    IDbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        foreach (string sql in lsSqls)
                        {
                            connection.Execute(sql, null, transaction);
                        }
                        transaction.Commit();
                        resultObj["flag"] = true;
                    }
                    catch (Exception ex)
                    {
                        resultObj["msg"] = ex.Message;
                        Logger.Error(ex.Message);
                        transaction.Rollback();
                    }
                }
            }
            catch (Exception ex)
            {
                resultObj["msg"] = ex.Message;
                Logger.Error(ex.Message);
            }
            return resultObj;
        }

        /// <summary>
        /// 带参数（参数是实体类）SQL语句执行数据（支持insert、Update、Delete三种数据）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="agencyModels"></param>
        /// <returns></returns>
        public static JObject TransactionExcuteModel(List<AgencyModel> agencyModels)
        {
            JObject resultObj = new JObject();
            resultObj["flag"] = false;
            resultObj["msg"] = string.Empty;
            try
            {
                using (IDbConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    IDbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        foreach (AgencyModel agencyModel in agencyModels)
                        {
                            connection.Execute(agencyModel.sql, agencyModel.model, transaction);
                        }
                        transaction.Commit();
                        resultObj["flag"] = true;
                    }
                    catch (Exception ex)
                    {
                        resultObj["msg"] = ex.Message;
                        Logger.Error(ex.Message);
                        transaction.Rollback();
                    }
                }
            }
            catch (Exception ex)
            {
                resultObj["msg"] = ex.Message;
                Logger.Error(ex.Message);
            }
            return resultObj;
        }

        #endregion

        #region 按SQL语句执行数据

        /// <summary>
        /// 带SQL语句执行数据（支持Insert、Update、Delete三种数据）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static int Execute(string sql, out string msg)
        {
            DateTime beforDT = DateTime.Now;
            int result = 0;
            msg = string.Empty;
            try
            {
                using (IDbConnection connection = new MySqlConnection(ConnectionString))
                {
                    result = connection.Execute(sql);
                }
                Logger.Info("查询开始==========================================================");
                Logger.Info("查询时间：" + DateTime.Now.Subtract(beforDT).TotalMilliseconds.ToString() + " 毫秒");
                Logger.Info("Execute(string sql)请求SQL:" + sql);
                Logger.Info("查询结束==========================================================");
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                Logger.Error("异常信息开始==========================================================");
                Logger.Error("查询时间：" + DateTime.Now.Subtract(beforDT).TotalMilliseconds.ToString() + " 毫秒");
                Logger.Error("Execute(string sql)请求SQL:" + sql);
                Logger.Error("Execute(string sql)异常信息:" + ex.Message);
                Logger.Error("异常信息结束==========================================================");
            }

            return result;
        }

        /// <summary>
        /// 带参数SQL语句执行数据（支持insert、Update、Delete三种数据）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Execute<T>(string sql, T model, out string msg)
        {
            DateTime beforDT = DateTime.Now;
            int result = 0;
            msg = string.Empty;
            try
            {
                using (IDbConnection connection = new MySqlConnection(ConnectionString))
                {
                    result = connection.Execute(sql, model);
                }
                Logger.Info("查询开始==========================================================");
                Logger.Info("查询时间：" + DateTime.Now.Subtract(beforDT).TotalMilliseconds.ToString() + " 毫秒");
                Logger.Info("Execute<T>(string sql, T model)请求SQL:" + sql);
                Logger.Info("Execute<T>(string sql, T model)参数model:" + Helper.JsonHelper.ObjectToJson(model));
                Logger.Info("查询结束==========================================================");
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                Logger.Error("异常信息开始==========================================================");
                Logger.Error("查询时间：" + DateTime.Now.Subtract(beforDT).TotalMilliseconds.ToString() + " 毫秒");
                Logger.Error("Execute<T>(string sql, T model)请求SQL:" + sql);
                Logger.Error("Execute<T>(string sql, T model)参数model:" + Helper.JsonHelper.ObjectToJson(model));
                Logger.Error("Execute<T>(string sql, T model)异常信息:" + ex.Message);
                Logger.Error("异常信息结束==========================================================");
            }

            return result;
        }

        #endregion

        #region 按实体类执行数据

        /// <summary>
        /// 直接通过实体类Insert
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static long Insert_Model<T>(T model, out string msg) where T : class
        {
            msg = string.Empty;
            DateTime beforDT = DateTime.Now;
            //long result = BizData.NullValue;
            long result = 0;
            try
            {
                using (IDbConnection connection = new MySqlConnection(ConnectionString))
                {
                    result = connection.Insert<T>(model);
                }
                Logger.Info("查询开始==========================================================");
                Logger.Info("查询时间：" + DateTime.Now.Subtract(beforDT).TotalMilliseconds.ToString() + " 毫秒");
                Logger.Info("Insert_Model<T>()参数model:" + Helper.JsonHelper.ObjectToJson(model));
                Logger.Info("查询结束==========================================================");
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                Logger.Error("异常信息开始==========================================================");
                Logger.Error("查询时间：" + DateTime.Now.Subtract(beforDT).TotalMilliseconds.ToString() + " 毫秒");
                Logger.Error("Insert_Model<T>()参数model:" + Helper.JsonHelper.ObjectToJson(model));
                Logger.Error("Insert_Model<T>()异常信息:" + ex.Message);
                Logger.Error("异常信息结束==========================================================");
            }

            return result;
        }

        /// <summary>
        /// 通过实体类更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool Update_Model<T>(T model, out string msg) where T : class
        {
            msg = string.Empty;
            DateTime beforDT = DateTime.Now;
            bool isSuccess = false;
            try
            {
                using (IDbConnection connection = new MySqlConnection(ConnectionString))
                {
                    isSuccess = connection.Update<T>(model);
                }
                Logger.Info("更新开始==========================================================");
                Logger.Info("更新时间：" + DateTime.Now.Subtract(beforDT).TotalMilliseconds.ToString() + " 毫秒");
                Logger.Info("Update_Model<T>()参数model:" + Helper.JsonHelper.ObjectToJson(model));
                Logger.Info("更新结束==========================================================");
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                Logger.Error("异常信息开始==========================================================");
                Logger.Error("更新时间：" + DateTime.Now.Subtract(beforDT).TotalMilliseconds.ToString() + " 毫秒");
                Logger.Error("Update_Model<T>()参数model:" + Helper.JsonHelper.ObjectToJson(model));
                Logger.Error("Update_Model<T>()异常信息:" + ex.Message);
                Logger.Error("异常信息结束==========================================================");
            }

            return isSuccess;
        }

        #endregion

        #region 单列值

        /// <summary>
        /// 获得单列值
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static JObject GetSingleColValue(string sql)
        {
            DateTime beforDT = DateTime.Now;
            JObject resultObj = new JObject();
            resultObj["flag"] = false;//接口是否正确
            resultObj["msg"] = string.Empty;//错误信息

            try
            {
                #region 1.数据合法性校验 

                if (string.IsNullOrEmpty(sql))
                {
                    resultObj["msg"] = "Sql语句为空！";
                    return resultObj;
                }

                if (string.IsNullOrEmpty(ConnectionString))
                {
                    resultObj["msg"] = "数据库链接串为空！";
                    return resultObj;
                }

                #endregion

                IDbConnection connection = new MySqlConnection(ConnectionString);
                var reader = connection.ExecuteScalar(sql);
                if (reader == null)
                {
                    resultObj["flag"] = true;
                    resultObj["singlecolvalue"] = string.Empty;
                }
                else
                {
                    string singleColValue = reader.ToString();
                    resultObj["flag"] = true;
                    resultObj["singlecolvalue"] = singleColValue;
                }
                return resultObj;
            }
            catch (Exception ex)
            {
                resultObj["flag"] = false;
                resultObj["msg"] = ex.Message;
                Logger.Error("异常信息开始==========================================================");
                Logger.Error("查询时间：" + DateTime.Now.Subtract(beforDT).TotalMilliseconds.ToString() + " 毫秒");
                Logger.Error("GetComputeValue()请求SQL:" + sql);
                Logger.Error("GetComputeValue()异常信息:" + ex.Message);
                Logger.Error("异常信息结束==========================================================");
                return resultObj;
            }
        }

        #endregion

        #endregion
    }
}
