using JD.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Application.DailyTasks
{
    public class DoSignCashDto : IDoSignBaseDto
    {
        public int code { get; set; }
        public bool success { get; set; }
        public string busiCode { get; set; }
        public string message { get; set; }
        public resultModel result { get; set; }
        public class resultModel
        {
            public signResultModel signResult { get; set; }
            public class signResultModel
            {
                public string msg { get; set; }
                //public int showCode { get; set; }
                public string bizCode { get; set; }
                //public int errCode { get; set; }
                public signDataModel signData { get; set; }
                public class signDataModel
                {
                    public decimal amount { get; set; }
                }
                public string sessionId { get; set; }
                public bool success { get; set; }
            }
        }

        /// <summary>
        /// 根据JD接口返回的信息，封装，得到简化的信息
        /// </summary>
        /// <param name="actionName">接口的描述信息</param>
        /// <returns></returns>
        public CusResponseDto GetCusResponse(string actionName = "")
        {
            JDDailyTaskEnum jdDailyTaskEnum = JDDailyTaskEnum.DoSignCash;
            JDAwardTypeEnum jdAwardTypeEnum = JDAwardTypeEnum.现金红包;
            actionName = string.IsNullOrEmpty(actionName) ? jdDailyTaskEnum.GetDesc() : actionName;

            try
            {
                //失败
                if (code != 0) return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, message);
                //大人，您今天已经完成签到了哦~请明天再来吧~
                if (!result.signResult.success) return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, result.signResult.msg);
                //成功
                return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, result.signResult.signData.amount, jdAwardTypeEnum);
            }
            catch (Exception ex)
            {
                return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, ex.Message);
            }

            //return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, null, jdAwardTypeEnum);
        }
    }
}
