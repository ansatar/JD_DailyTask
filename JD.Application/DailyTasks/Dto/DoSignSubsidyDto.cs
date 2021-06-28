using JD.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Application.DailyTasks
{
    public class DoSignSubsidyDto : IDoSignBaseDto
    {
        public int resultCode { get; set; }
        public string resultMsg { get; set; }
        public resultDataModel resultData { get; set; }
        public class resultDataModel
        {
            public string msg { get; set; }
            public int code { get; set; }
            public dataModel data { get; set; }
            public class dataModel
            {
                public int thisAmount { get; set; }
                public int continuityDays { get; set; }
                public decimal thisAmountStr { get; set; }
            }
        }

        /// <summary>
        /// 根据JD接口返回的信息，封装，得到简化的信息
        /// </summary>
        /// <param name="actionName">接口的描述信息</param>
        /// <returns></returns>
        public CusResponseDto GetCusResponse(string actionName = "")
        {
            JDDailyTaskEnum jdDailyTaskEnum = JDDailyTaskEnum.DoSignSubsidy;
            JDAwardTypeEnum jdAwardTypeEnum = JDAwardTypeEnum.金贴;
            actionName = string.IsNullOrEmpty(actionName) ? jdDailyTaskEnum.GetDesc() : actionName;

            try
            {
                //失败
                if (resultCode != 0) return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, resultMsg);
                //失败
                if (resultData.code != 0) return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, resultData.msg);
                //成功
                if (resultData.data != null) return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, resultData.data.thisAmountStr, jdAwardTypeEnum);
            }
            catch (Exception ex)
            {
                return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, ex.Message);
            }

            return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, null, jdAwardTypeEnum);
        }
    }
}
