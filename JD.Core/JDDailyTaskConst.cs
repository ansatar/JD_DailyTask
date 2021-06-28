using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Core
{
    public class JDDailyTaskConst
    {
        /// <summary>
        /// 执行任务的间隔时间ms（为了不频繁调用京东接口）
        /// </summary>
        public static int StepMilliSeconds => 1000 * (new Random().Next(1, 5));
    }
}
