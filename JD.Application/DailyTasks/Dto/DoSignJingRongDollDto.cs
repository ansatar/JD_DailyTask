using JD.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Application.DailyTasks
{
    public class DoSignJingRongDollDto : IDoSignBaseDto
    {
        public int resultCode { get; set; }
        public string resultMsg { get; set; }
        public resultDataModel resultData { get; set; }
        public class resultDataModel
        {
            public int code { get; set; }
            public string msg { get; set; }
            public dataModel data { get; set; }
            public class dataModel
            {
                public string businessCode { get; set; }
                public string businessMsg { get; set; }
                public businessDataModel businessData { get; set; }
                public class businessDataModel
                {
                    public string businessMsg { get; set; }
                    public int rewardType { get; set; }
                    public int pickStatus { get; set; }
                    public List<rewardsModel> rewards { get; set; }
                    public class rewardsModel
                    {
                        public string _rewardPrice;
                        public string rewardPrice
                        {
                            get
                            {
                                _rewardPrice = _rewardPrice.Replace("个", "");
                                int result = 0;
                                if (int.TryParse(_rewardPrice, out result))
                                    return result.ToString();
                                else
                                    return "0";
                            }
                            set
                            {
                                _rewardPrice = value;
                            }
                        }
                        //public string rewardName { get; set; }
                    }
                    public List<awardListVoModel> awardListVo { get; set; }
                    public class awardListVoModel
                    {
                        public decimal count { get; set; }
                    }
                }
            }
        }

        /// <summary>
        /// 根据JD接口返回的信息，封装，得到简化的信息
        /// </summary>
        /// <param name="actionName">接口的描述信息</param>
        /// <returns></returns>
        public CusResponseDto GetCusResponse(string actionName = "")
        {
            JDDailyTaskEnum jdDailyTaskEnum = JDDailyTaskEnum.DoSignJingRongDoll;
            JDAwardTypeEnum jdAwardTypeEnum = JDAwardTypeEnum.京豆;
            actionName = string.IsNullOrEmpty(actionName) ? jdDailyTaskEnum.GetDesc() : actionName;

            try
            {
                //失败
                if (resultCode != 0) return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, resultMsg);
                //失败
                if (resultData.code != 200) return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, resultData.msg);
                ////已挂有效奖励获取成功
                //if (resultData.data.businessCode != "200" && resultData.data.businessCode != "成功") return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, resultData.data.businessMsg);

                //成功
                decimal? beanCount = 0;

                if (resultData.data.businessData.rewards != null)
                {
                    jdAwardTypeEnum = JDAwardTypeEnum.京豆;
                    beanCount = resultData.data.businessData.rewards.Sum(s => Convert.ToInt32(s.rewardPrice));
                }
                if (actionName.Contains("京豆"))
                {
                    jdAwardTypeEnum = JDAwardTypeEnum.京豆;
                    beanCount = resultData.data.businessData.awardListVo?.Sum(s => s.count);
                }
                if (actionName.Contains("现金"))
                {
                    jdAwardTypeEnum = JDAwardTypeEnum.现金红包;
                    beanCount = resultData.data.businessData.awardListVo?.Sum(s => s.count);
                }
                if (actionName.Contains("金贴"))
                {
                    jdAwardTypeEnum = JDAwardTypeEnum.金贴;
                    beanCount = resultData.data.businessData.awardListVo?.Sum(s => s.count);
                }

                if (beanCount > 0) return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, beanCount, jdAwardTypeEnum);

                //失败
                if (resultData.data.businessData != null && !string.IsNullOrEmpty(resultData.data.businessData.businessMsg)) return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, resultData.data.businessData.businessMsg);
                if (resultData.data.businessCode == "300ssq") return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, resultData.data.businessMsg); //当天已领取
            }
            catch (Exception ex)
            {
                return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, ex.Message);
            }

            return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, null, jdAwardTypeEnum);
        }
    }
}
