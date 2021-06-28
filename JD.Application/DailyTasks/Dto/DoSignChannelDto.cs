using JD.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Application.DailyTasks
{
    public class DoSignChannelDto : IDoSignBaseDto
    {
        public string errorCode { get; set; }
        public string errorMessage { get; set; }
        public bool success { get; set; }
        public dataModel data { get; set; }
        public class dataModel
        {
            public bool signSuccess { get; set; }
            public bool hasBean { get; set; }
            public int jdBeanQuantity { get; set; }
        }

        public CusResponseDto GetCusResponse(string actionName = "")
        {
            JDDailyTaskEnum jdDailyTaskEnum = JDDailyTaskEnum.DoSignShop;
            JDAwardTypeEnum jdAwardTypeEnum = JDAwardTypeEnum.京豆;
            actionName = string.IsNullOrEmpty(actionName) ? jdDailyTaskEnum.GetDesc() : actionName;

            try
            {
                //失败
                if (!success) return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, errorMessage);
                //成功
                if (data != null && data.signSuccess && data.hasBean)
                    return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, data.jdBeanQuantity, jdAwardTypeEnum);
                else
                    return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, 0, jdAwardTypeEnum);

            }
            catch (Exception ex)
            {
                return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, ex.Message);
            }

            return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, null, jdAwardTypeEnum);
        }
    }
}
