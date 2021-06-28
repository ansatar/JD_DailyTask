using JD.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Application.DailyTasks
{
    public class DoSignLuckyLotteryDto : IDoSignBaseDto
    {
        public int resultCode { get; set;}
        public string resultMsg { get; set; }
        public resultDataModel resultData { get; set; }
        public class resultDataModel
        {
            public int resultCode { get; set; }
            public string resultMsg { get; set; }
            public dataModel data { get; set; }
            public class dataModel
            {
                public List<rewardListModel> rewardList { get; set; }
                public class rewardListModel
                {
                    public string rewardName { get; set; }
                    public int rewardCount { get; set; }
                }
                public int rewardStatus { get; set; }
            }
        }

        /// <summary>
        /// 根据JD接口返回的信息，封装，得到简化的信息
        /// </summary>
        /// <param name="actionName">接口的描述信息</param>
        /// <returns></returns>
        public CusResponseDto GetCusResponse(string actionName = "")
        {
            JDDailyTaskEnum jdDailyTaskEnum = JDDailyTaskEnum.DoSignLuckyLottery;
            JDAwardTypeEnum jdAwardTypeEnum = JDAwardTypeEnum.京豆;
            actionName = string.IsNullOrEmpty(actionName) ? jdDailyTaskEnum.GetDesc() : actionName;

            try
            {
                //失败
                if (resultCode != 0) return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, resultMsg);
                //今日已签到，请勿重复签到！
                if (resultData.resultCode != 0) return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, resultData.resultMsg);
                //成功
                return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, resultData.data.rewardList.Sum(s => s.rewardCount), jdAwardTypeEnum);
            }
            catch (Exception ex)
            {
                return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, ex.Message);
            }

            //return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, null, jdAwardTypeEnum);
        }
    }
}
