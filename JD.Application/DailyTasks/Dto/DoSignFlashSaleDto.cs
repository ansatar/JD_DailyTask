using JD.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Application.DailyTasks
{
    public class DoSignFlashSaleDto : IDoSignBaseDto
    {
        public resultModel result { get; set; }
        public class resultModel
        {
            public string msg { get; set; }
            public int code { get; set; }
            public int jdBeanNum { get; set; }
        }
        public int code { get; set; }

        /// <summary>
        /// 根据JD接口返回的信息，封装，得到简化的信息
        /// </summary>
        /// <param name="actionName">接口的描述信息</param>
        /// <returns></returns>
        public CusResponseDto GetCusResponse(string actionName = "")
        {
            JDDailyTaskEnum jdDailyTaskEnum = JDDailyTaskEnum.DoSignFlashSale;
            JDAwardTypeEnum jdAwardTypeEnum = JDAwardTypeEnum.京豆;
            actionName = string.IsNullOrEmpty(actionName) ? jdDailyTaskEnum.GetDesc() : actionName;

            try
            {
                //失败
                if (code != 0) return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, "未知原因");
                //成功
                if (result.code == 0) return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, result.jdBeanNum, jdAwardTypeEnum);
                //数据失联了，刷新看看吧~
                return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, result.msg);
            }
            catch (Exception ex)
            {
                return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, ex.Message);
            }

            //return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, null, jdAwardTypeEnum);
        }
    }
}
