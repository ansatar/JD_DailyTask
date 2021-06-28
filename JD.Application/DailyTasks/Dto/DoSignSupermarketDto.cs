using JD.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Application.DailyTasks
{
    public class DoSignSupermarketDto : IDoSignBaseDto
    {
        public int code { get; set; }
        public TempModel data { get; set; }
        public string msg { get; set; }
        public class TempModel
        {
            public int bizCode { get; set; }
            public string bizMsg { get; set; }
            public bool success { get; set; }
        }

        /// <summary>
        /// 根据JD接口返回的信息，封装，得到简化的信息
        /// </summary>
        /// <param name="actionName">接口的描述信息</param>
        /// <returns></returns>
        public CusResponseDto GetCusResponse(string actionName = "")
        {
            JDDailyTaskEnum jdDailyTaskEnum = JDDailyTaskEnum.DoSignSupermarket;
            JDAwardTypeEnum jdAwardTypeEnum = JDAwardTypeEnum.京豆;
            actionName = string.IsNullOrEmpty(actionName) ? jdDailyTaskEnum.GetDesc() : actionName;

            try
            {
                //失败
                if (code != 0) return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, msg);
                //当日已签到
                if (data != null && !data.success) return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, data.bizMsg);
                //成功
                return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, 0, jdAwardTypeEnum);
            }
            catch (Exception ex)
            {
                return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, ex.Message);
            }

            //return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, null, jdAwardTypeEnum);
        }
    }
}
