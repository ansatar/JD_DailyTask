using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Application.DailyTasks
{
    public class QuerySecKillingDto
    {
        public int code { get; set; }
        public resultModel result { get; set; }
        public class resultModel
        {
            public string button { get; set; }
            public string projectId { get; set; }
            public string taskId { get; set; }
        }
    }
}
