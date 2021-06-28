using JD.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Application.DailyTasks
{
    public class DoSignBeanDto : IDoSignBaseDto
    {
        public int code { get; set; }
        public string echo { get; set; }
        public TempModel data { get; set; }
        public class TempModel
        {
            public string signedRan { get; set; }
            /// <summary>
            /// 1-正常签到，2-连续签到
            /// </summary>
            public int status { get; set; }
            public int beanUserType { get; set; }
            public int awardType { get; set; }
            public dailyAwardModel dailyAward { get; set; }
            public class dailyAwardModel
            {
                public int type { get; set; }
                public string title { get; set; }
                public string subTitle { get; set; }
                public beanAwardModel beanAward { get; set; }
                public class beanAwardModel
                {
                    public int beanCount { get; set; }
                    public string beanImgUrl { get; set; }
                }
                public couponAwardModel couponAward { get; set; }
                public class couponAwardModel
                {
                    public string couponName { get; set; }
                    public string couponImgUrl { get; set; }
                    public string couponLinkUrl { get; set; }
                    public int awardType { get; set; }
                    public int couponType { get; set; }
                    public int couponStyle { get; set; }
                }
            }
            public continuityAwardModel continuityAward { get; set; }
            public class continuityAwardModel
            {
                public string title { get; set; }
                public beanAwardModel beanAward { get; set; }
                public class beanAwardModel
                {
                    public int beanCount { get; set; }
                }

            }
            //public conductionBtnModel conductionBtn { get; set; }
            //public class conductionBtnModel
            //{
            //    public string btnText { get; set; }
            //    public string linkUrl { get; set; }
            //}
            //public signRemindModel signRemind { get; set; }
            //public class signRemindModel
            //{
            //    public string title { get; set; }
            //    public string content { get; set; }
            //    public string popImgUrl { get; set; }
            //    public string beanHomeLink { get; set; }
            //}
            //public signCalendarModel signCalendar { get; set; }
            //public class signCalendarModel
            //{
            //    public string currentDate { get; set; }

            //}
            //public recommendModel recommend { get; set; }
            public int msgGuideSwitch { get; set; }
            public string sourceTips { get; set; }
        }

        /// <summary>
        /// 根据JD接口返回的信息，封装，得到简化的信息
        /// </summary>
        /// <param name="actionName">接口的描述信息</param>
        /// <returns></returns>
        public CusResponseDto GetCusResponse(string actionName = "")
        {
            JDDailyTaskEnum jdDailyTaskEnum = JDDailyTaskEnum.DoSignBean;
            JDAwardTypeEnum jdAwardTypeEnum = JDAwardTypeEnum.京豆;
            actionName = string.IsNullOrEmpty(actionName) ? jdDailyTaskEnum.GetDesc() : actionName;

            try
            {
                //失败
                if (code != 0) return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, echo);
                //重复签到
                if (data != null && data.status == 2) return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, "你今天已经签到过了");
                ////正常签到
                //if (data.awardType == 2) return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, data.dailyAward.beanAward.beanCount, jdAwardTypeEnum);
                ////连续签到
                //if (data.awardType == 1) return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, data.continuityAward.beanAward.beanCount, jdAwardTypeEnum);

                if(data.continuityAward != null) return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, data.continuityAward.beanAward.beanCount, jdAwardTypeEnum);
                if(data.dailyAward != null) return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, data.dailyAward.beanAward.beanCount, jdAwardTypeEnum);
            }
            catch (Exception ex)
            {
                return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, ex.Message);
            }

            return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, null, jdAwardTypeEnum);
        }

    }
}
