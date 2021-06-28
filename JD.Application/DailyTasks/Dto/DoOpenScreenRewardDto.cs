using JD.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Application.DailyTasks
{
    public class DoOpenScreenRewardDto : IDoSignBaseDto
    {
        public int resultCode { get; set; }
        public string resultMsg { get; set; }
        public resultDataModel resultData { get; set; }
        public class resultDataModel
        {
            public int resultCode { get; set; }
            public dataModel data { get; set; }
            public class dataModel
            {
                public int rewardCode { get; set; }
                public decimal? rewardAmount { get; set; }
            }
            public bool isSuccess { get; set; }
            public string resultMsg { get; set; }
        }

        /// <summary>
        /// 根据JD接口返回的信息，封装，得到简化的信息
        /// </summary>
        /// <param name="actionName">接口的描述信息</param>
        /// <returns></returns>
        public CusResponseDto GetCusResponse(string actionName = "")
        {
            JDDailyTaskEnum jdDailyTaskEnum = JDDailyTaskEnum.DoOpenScreenReward;
            JDAwardTypeEnum jdAwardTypeEnum = JDAwardTypeEnum.金贴;
            actionName = string.IsNullOrEmpty(actionName) ? jdDailyTaskEnum.GetDesc() : actionName;

            try
            {
                //失败
                if (resultCode != 0) return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, resultMsg);
                //失败
                if (!resultData.isSuccess) return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, resultData.resultMsg);
                //成功
                return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, resultData.data.rewardAmount, jdAwardTypeEnum);
            }
            catch (Exception ex)
            {
                return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, ex.Message);
            }

            //return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, null, jdAwardTypeEnum);
        }
    }
}
