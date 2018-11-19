using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class DapperController : ApiController
    {
        [HttpGet]
        [Route("Dapper/Query")]
        public int Query()
        {
            WorkOrder woOrder = new WorkOrder();
            woOrder.hsicrm_workorderid = "QD19042220000002";
            string connString = string.Empty;
            ConnectionStringSettings conSettings = ConfigurationManager.ConnectionStrings["WoConnString"];
            if (conSettings != null)
            {
                connString = conSettings.ConnectionString;
            }

            
            using (IDbConnection connection = new SqlConnection(connString))
            {
                //List<WorkOrder> ls = connection.Query<WorkOrder>("select * from hsicrm_wo_workordertestBase where hsicrm_workorderid = @hsicrm_workorderid", woOrder).ToList();
                List<WorkOrder> ls = connection.Query<WorkOrder>("select * from hsicrm_wo_workordertestBase where hsicrm_workorderid = @hsicrm_workorderid and statecode = @statecode",
                                new { hsicrm_workorderid = "QD19042220000002",statecode = 0 }).ToList();

                return ls.Count;
            }
        }
    }
}
