using JD.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Application.DailyTasks
{
    public class DoSignShakeDto : IDoSignBaseDto
    {
        public string message { get; set; }
        public string officialDocument { get; set; }
        public string resultCode { get; set; }
        public string resultTips { get; set; }
        public bool success { get; set; }
        public dataModel data { get; set; }
        public class dataModel
        {
            public luckyBoxModel luckyBox { get; set; }
            public class luckyBoxModel
            {
                /// <summary>
                /// 剩余的免费次数
                /// </summary>
                public int freeTimes { get; set; }
            }
            public prizeBeanModel prizeBean { get; set; }
            public class prizeBeanModel
            {
                public int count { get; set; }
            }
        }

        /// <summary>
        /// 根据JD接口返回的信息，封装，得到简化的信息
        /// </summary>
        /// <param name="actionName">接口的描述信息</param>
        /// <returns></returns>
        public CusResponseDto GetCusResponse(string actionName = "")
        {
            JDDailyTaskEnum jdDailyTaskEnum = JDDailyTaskEnum.DoSignShake;
            JDAwardTypeEnum jdAwardTypeEnum = JDAwardTypeEnum.京豆;
            actionName = string.IsNullOrEmpty(actionName) ? jdDailyTaskEnum.GetDesc() : actionName;

            try
            {
                if (success) return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, (data.prizeBean == null ? 0 : data.prizeBean.count), jdAwardTypeEnum);
                if (resultCode == "9000005") return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, "免费次数已用完");
                return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, resultTips);
            }
            catch (Exception ex)
            {
                return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, ex.Message);
            }

            //return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, null, jdAwardTypeEnum);
        }
    }
}
