using JD.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JD.Application.DailyTasks
{
    public class DoSignSecKillingDto : IDoSignBaseDto
    {
        public string msg { get; set; }
        public int code { get; set; }
        public string subCode { get; set; }
        public rewardsInfoModel rewardsInfo { get; set; }
        public class rewardsInfoModel
        {
            public object successRewards { get; set; }
            //public successRewardsModel successRewards { get; set; }
            //public class successRewardsModel
            //{
            //    public int
            //}
        }
        //public object assignmentInfo { get; set; }

        /// <summary>
        /// 根据JD接口返回的信息，封装，得到简化的信息
        /// </summary>
        /// <param name="actionName">接口的描述信息</param>
        /// <returns></returns>
        public CusResponseDto GetCusResponse(string actionName = "")
        {
            JDDailyTaskEnum jdDailyTaskEnum = JDDailyTaskEnum.DoSignSecKilling;
            JDAwardTypeEnum jdAwardTypeEnum = JDAwardTypeEnum.现金红包;
            actionName = string.IsNullOrEmpty(actionName) ? jdDailyTaskEnum.GetDesc() : actionName;

            try
            {
                //本场红包已经抢过啦~
                if (code != 0) return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, msg);

                //成功
                if (code == 0 && subCode == "0")
                {
                    //正则
                    Regex MyRegex = new Regex(
                                        "discount\": (?<value>.*?),",
                                      RegexOptions.IgnoreCase
                                      | RegexOptions.CultureInvariant
                                      | RegexOptions.IgnorePatternWhitespace
                                      | RegexOptions.Compiled
                                      );
                    Match m = MyRegex.Match(rewardsInfo.successRewards.ToString());
                    string val = m.Groups.GetValueOrDefault("value").Value.Trim() ?? "0";

                    return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, Convert.ToDecimal(val), jdAwardTypeEnum);
                }

                return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, msg);
            }
            catch (Exception ex)
            {
                return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, ex.Message);
            }

            //return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, null, jdAwardTypeEnum);
        }
    }
}
