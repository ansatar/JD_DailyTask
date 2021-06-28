using JD.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Application.DailyTasks
{
    public class DoSignShopDto : IDoSignBaseDto
    {
        public int code { get; set; }
        public string msg { get; set; }
        public int subCode { get; set; }
        public string subCodeMsg { get; set; }
        public string noAwardTxt { get; set; }
        public decimal? jdBean { get; set; } = 1;
        public List<awardListModel> awardList { get; set; }
        public class awardListModel
        {
            public string text { get; set; }
            public int type { get; set; }
        }

        public CusResponseDto GetCusResponse(string actionName = "")
        {
            JDDailyTaskEnum jdDailyTaskEnum = JDDailyTaskEnum.DoSignShop;
            JDAwardTypeEnum jdAwardTypeEnum = JDAwardTypeEnum.京豆;
            actionName = string.IsNullOrEmpty(actionName) ? jdDailyTaskEnum.GetDesc() : actionName;

            try
            {
                //失败
                if (code != 0) return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, msg);
                if (subCode != 0) return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, subCodeMsg);
                if (!string.IsNullOrEmpty(noAwardTxt)) return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, noAwardTxt);
                //成功
                if (awardList != null && awardList.Count > 0) jdBean = awardList.Sum(s => s.type);
                return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, jdBean, jdAwardTypeEnum);
            }
            catch (Exception ex)
            {
                return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, ex.Message);
            }

            return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, null, jdAwardTypeEnum);
        }
    }
}
