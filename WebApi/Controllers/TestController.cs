using Dapper;
using Newtonsoft.Json.Linq;
using RemotingLogApp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Util.Log;
using WebApi.BLL;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class TestController : ApiController
    {
        [HttpGet]
        [Route("Test/CreateLogger")]
        public string CreateLogger(string strInfo)
        {
            Logger.Info(strInfo);
            return strInfo;
        }

        [HttpGet]
        [Route("Test/RemotingLogger")]
        public string RemotingLogger(string strInfo)
        {
            try
            {
                Logger.Info(strInfo);
                //RemotingLog.RemotingLog obj = new RemotingLog.RemotingLog();
                //int c = obj.Add(1, 2);
                //return c.ToString();

                string a = "b";
                int c = Int32.Parse(a);
            }
            catch (Exception ex)
            {
                Logger.Error(ActionContext, ex);
            }

            return strInfo + "sjm";
        }

        [HttpPost]
        [Route("Test/testObj")]
        public JObject testObj([FromBody]JObject inObj)
        {
            JObject obj = new JObject();
            obj["msg"] = "成功";
            obj["flag"] = true;
            return obj;
        }

        [HttpGet]
        [Route("Test/AsyncTest")]
        public void AsyncTest()
        {
            //AsyncTestBLL.AsyncTest();
            AsyncTestBLL.MainThread();
        }

        [HttpGet]
        [Route("Test/a")]
        public void a()
        {
            DataTable aaa = new DataTable();
            DataColumn dc = aaa.Columns.Add();
            dc.ColumnName = "Name";
            dc.DataType = typeof(string);
            
            DataRow dr = aaa.NewRow();
            dr["Name"] = "sjm";
            aaa.Rows.Add(dr);

            IList<Class1> studentList = new List<Class1>() {
                    new Class1() {  Name = "John" },
                    new Class1() {  Name = "Moin" },
                    new Class1() {  Name = "Bill" },
                    new Class1() {  Name = "Ram" },
                    new Class1() {  Name = "Ron" }
                    };


            var q = from aa in studentList
                    select aa.Name;


            var s = q.ToList();
        }
    }
}
