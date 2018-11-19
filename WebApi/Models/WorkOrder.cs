using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class WorkOrder
    {
        public Guid hsicrm_wo_workordertestid { get; set; }

        public string hsicrm_workorderid { get; set; }

        public string hsicrm_workorderstatuscode { get; set; }

        public string hsicrm_regioncode { get; set; }
    }
}