using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Application.DailyTasks
{
    public class QueryLotteryDrawDto
    {
        public int code { get; set; }
        public dataModel data { get; set; }
        public class dataModel
        {
            /// <summary>
            /// 转盘剩余次数
            /// </summary>
            public int lotteryCount { get; set; }
            public string lotteryCode { get; set; }
        }
    }
}
