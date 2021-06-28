using JD.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Application.DailyTasks
{
    public class DoSignGetCashDto : IDoSignBaseDto
    {
        public int code { get; set; }
        public dataModel data { get; set; }
        public class dataModel
        {
            public int bizCode { get; set; }
            public string bizMsg { get; set; }
            public resultModel result { get; set; }
            public class resultModel
            {
                public decimal signCash { get; set; }
            }
            public bool success { get; set; }
        }
        public string msg { get; set; }

        /// <summary>
        /// 根据JD接口返回的信息，封装，得到简化的信息
        /// </summary>
        /// <param name="actionName">接口的描述信息</param>
        /// <returns></returns>
        public CusResponseDto GetCusResponse(string actionName = "")
        {
            JDDailyTaskEnum jdDailyTaskEnum = JDDailyTaskEnum.DoSignGetCash;
            JDAwardTypeEnum jdAwardTypeEnum = JDAwardTypeEnum.现金红包;
            actionName = string.IsNullOrEmpty(actionName) ? jdDailyTaskEnum.GetDesc() : actionName;

            try
            {
                //失败
                if (code != 0) return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, msg);
                //今天已经签过到啦\r\n明天继续哦~
                if (!data.success) return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, data.bizMsg);
                //成功
                return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, data.result.signCash, jdAwardTypeEnum);
            }
            catch (Exception ex)
            {
                return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, ex.Message);
            }

            //return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, null, jdAwardTypeEnum);
        }
    }
}
