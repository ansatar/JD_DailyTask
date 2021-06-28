using JD.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Application.DailyTasks
{
    public class DoSignWebcastsDto : IDoSignBaseDto
    {
        public int code { get; set; }
        public int subCode { get; set; }
        public int sum { get; set; }
        public string msg { get; set; }

        /// <summary>
        /// 根据JD接口返回的信息，封装，得到简化的信息
        /// </summary>
        /// <param name="actionName">接口的描述信息</param>
        /// <returns></returns>
        public CusResponseDto GetCusResponse(string actionName = "")
        {
            JDDailyTaskEnum jdDailyTaskEnum = JDDailyTaskEnum.DoSignWebcasts;
            JDAwardTypeEnum jdAwardTypeEnum = JDAwardTypeEnum.京豆;
            actionName = string.IsNullOrEmpty(actionName) ? jdDailyTaskEnum.GetDesc() : actionName;

            try
            {
                //成功
                if (code == 0 && subCode == 0) return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, sum, jdAwardTypeEnum);
                //京豆与你擦肩而过
                if (!string.IsNullOrEmpty(msg)) return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, msg);

            }
            catch (Exception ex)
            {
                return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, ex.Message);
            }

            return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, null, jdAwardTypeEnum);
        }
    }
}
