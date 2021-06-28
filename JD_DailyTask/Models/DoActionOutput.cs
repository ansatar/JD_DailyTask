using JD.Application;
using JD.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JD_DailyTask.Models
{
    public class DoActionOutput
    {
        public JDDailyTaskEnum ActionIndex { get; set; }
        public string ActionName { get; set; }
        public bool Success { get; set; }
        public string ErrMsg { get; set; }
        public decimal? AwardCount { get; set; }
        public JDAwardTypeEnum AwardType { get; set; }

        public static DoActionOutput Instance(JDDailyTaskEnum actionIndex, string actionName, bool success, string errMsg, decimal? beanCount, JDAwardTypeEnum awardType)
        {
            var model = new DoActionOutput();

            model.Success = success;
            model.ErrMsg = errMsg;
            model.ActionIndex = actionIndex;
            model.ActionName = actionName;
            model.AwardCount = beanCount;
            model.AwardType = awardType;

            return model;
        }

        public static DoActionOutput IsSuccess(JDDailyTaskEnum actionIndex, string actionName, decimal? beanCount, JDAwardTypeEnum awardType)
        {
            return Instance(actionIndex, actionName, true, string.Empty, beanCount, awardType);
        }

        public static DoActionOutput IsFail(JDDailyTaskEnum actionIndex, string actionName, string errMsg)
        {
            return Instance(actionIndex, actionName, false, errMsg, 0, JDAwardTypeEnum.京豆);
        }
    }
}
