using JD.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Application.DailyTasks
{
    public class CusResponseDto
    {
        public JDDailyTaskEnum ActionIndex { get; set; }
        public string ActionName { get; set; }
        public bool Success { get; set; }
        public string ErrMsg { get; set; }
        public decimal? AwardCount { get; set; }
        public JDAwardTypeEnum AwardType { get; set; }

        public static CusResponseDto Instance(JDDailyTaskEnum actionIndex, string actionName, bool success, string errMsg, decimal? beanCount, JDAwardTypeEnum awardType)
        {
            var model = new CusResponseDto();

            model.ActionIndex = actionIndex;
            model.ActionName = actionName;
            model.Success = success;
            model.ErrMsg = errMsg;
            model.AwardCount = beanCount;
            model.AwardType = awardType;

            return model;
        }

        public static CusResponseDto IsSuccess(JDDailyTaskEnum actionIndex, string actionName, decimal? beanCount, JDAwardTypeEnum awardType)
        {
            return Instance(actionIndex, actionName, true, string.Empty, beanCount, awardType);
        }

        public static CusResponseDto IsFail(JDDailyTaskEnum actionIndex, string actionName, string errMsg)
        {
            return Instance(actionIndex, actionName, false, errMsg, 0, JDAwardTypeEnum.京豆);
        }
    }
}
