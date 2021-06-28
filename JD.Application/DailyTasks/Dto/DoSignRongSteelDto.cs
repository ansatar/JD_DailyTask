using JD.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Application.DailyTasks
{
    public class DoSignRongSteelDto : IDoSignBaseDto
    {
        public int resultCode { get; set; }
        public string resultMsg { get; set; }
        public resultDataModel resultData { get; set; }
        public class resultDataModel
        {
            public resBusiDataModel resBusiData { get; set; }
            public string resBusiMsg { get; set; }
            public int resBusiCode { get; set; }
            public class resBusiDataModel
            {
                public decimal baseReward { get; set; }
            }
        }

        /// <summary>
        /// 根据JD接口返回的信息，封装，得到简化的信息
        /// </summary>
        /// <param name="actionName">接口的描述信息</param>
        /// <returns></returns>
        public CusResponseDto GetCusResponse(string actionName = "")
        {
            JDDailyTaskEnum jdDailyTaskEnum = JDDailyTaskEnum.DoSignRongSteel;
            JDAwardTypeEnum jdAwardTypeEnum = JDAwardTypeEnum.钢镚;
            actionName = string.IsNullOrEmpty(actionName) ? jdDailyTaskEnum.GetDesc() : actionName;

            try
            {
                //失败
                if (resultCode != 0) return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, resultMsg);
                //已经领取过
                if (resultData.resBusiCode != 0) return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, resultData.resBusiMsg);
                //成功
                return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, resultData.resBusiData.baseReward, jdAwardTypeEnum);

            }
            catch (Exception ex)
            {
                return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, ex.Message);
            }

            //return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, null, jdAwardTypeEnum);
        }
    }
}
