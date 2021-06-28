using JD.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Application.DailyTasks
{
    public class DoSignLotteryDrawDto : IDoSignBaseDto
    {
        public int code { get; set; }
        public string errorCode { get; set; }
        public string errorMessage { get; set; }
        public dataModel data { get; set; }
        public class dataModel
        {
            public int isWinner { get; set; }
            public int chances { get; set; }
            public string toastTxt { get; set; }
            public int prizeSendNumber { get; set; }
        }

        /// <summary>
        /// 根据JD接口返回的信息，封装，得到简化的信息
        /// </summary>
        /// <param name="actionName">接口的描述信息</param>
        /// <returns></returns>
        public CusResponseDto GetCusResponse(string actionName = "")
        {
            JDDailyTaskEnum jdDailyTaskEnum = JDDailyTaskEnum.DoSignLotteryDraw;
            JDAwardTypeEnum jdAwardTypeEnum = JDAwardTypeEnum.京豆;
            actionName = string.IsNullOrEmpty(actionName) ? jdDailyTaskEnum.GetDesc() : actionName;

            try
            {
                if (code != 0 || errorCode != null) return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, errorMessage);
                if (data.prizeSendNumber == 0) return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, data.toastTxt);
                return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, data.prizeSendNumber, jdAwardTypeEnum);
            }
            catch (Exception ex)
            {
                return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, ex.Message);
            }

            //return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, null, jdAwardTypeEnum);
        }
    }
}
