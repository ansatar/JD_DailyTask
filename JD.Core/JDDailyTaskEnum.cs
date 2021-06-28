using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Core
{
    /// <summary>
    /// 京东任务枚举
    /// </summary>
    public enum JDDailyTaskEnum
    {
        /// <summary>
        /// 京东商城-京豆签到
        /// </summary>
        [Description("京东商城-京豆签到")]
        DoSignBean,
        /// <summary>
        /// 京东商城-超市签到
        /// </summary>
        [Description("京东商城-超市签到")]
        DoSignSupermarket,
        /// <summary>
        /// 京东商城-直播签到
        /// </summary>
        [Description("京东商城-直播签到")]
        DoSignWebcasts,
        /// <summary>
        /// 京东金融-钢镚签到
        /// </summary>
        [Description("京东金融-钢镚签到")]
        DoSignRongSteel,
        /// <summary>
        /// 京东金融-赚钱频道-开屏奖励
        /// </summary>
        [Description("京东金融-赚钱频道-开屏奖励")]
        DoOpenScreenReward,
        /// <summary>
        /// 京东商城-转盘抽奖
        /// </summary>
        [Description("京东商城-转盘抽奖")]
        DoSignLotteryDraw,
        /// <summary>
        /// 京东商城-摇一摇
        /// </summary>
        [Description("京东商城-摇一摇")]
        DoSignShake,
        /// <summary>
        /// 京东商城-闪购签到
        /// </summary>
        [Description("京东商城-闪购签到")]
        DoSignFlashSale,
        /// <summary>
        /// 京东现金-红包签到
        /// </summary>
        [Description("京东现金-红包签到")]
        DoSignCash,
        /// <summary>
        /// 京东商城-现金签到
        /// </summary>
        [Description("京东商城-现金签到")]
        DoSignGetCash,
        /// <summary>
        /// 京东商城-金贴签到
        /// </summary>
        [Description("京东商城-金贴签到")]
        DoSignSubsidy,
        /// <summary>
        /// 京东秒杀-红包签到
        /// </summary>
        [Description("京东秒杀-红包签到")]
        DoSignSecKilling,
        /// <summary>
        /// 京东金融-抽奖签到
        /// </summary>
        [Description("京东金融-抽奖签到")]
        DoSignLuckyLottery,
        /// <summary>
        /// 京东金融-签壹、签贰、签叁、签肆、签伍
        /// </summary>
        [Description("京东金融-签壹、签贰、签叁、签肆、签伍")]
        DoSignJingRongDoll,
        /// <summary>
        /// 京东商城-店铺签到
        /// </summary>
        [Description("京东商城-店铺签到")]
        DoSignShop,
        /// <summary>
        /// 京东游戏-天天加速
        /// </summary>
        [Description("京东游戏-天天加速")]
        DoGameBySpeedUp,
    }

    public static class JDDailyTaskEnumHelper
    {
        static System.Reflection.FieldInfo[] _fi;

        /// <summary>
        /// 获取指定枚举的 Description 
        /// </summary>
        /// <param name="jdDailyTaskEnum"></param>
        /// <returns></returns>
        private static string GetDescription(JDDailyTaskEnum jdDailyTaskEnum)
        {
            if (_fi == null) _fi = typeof(JDDailyTaskEnum).GetFields();

            if (_fi == null)
            {
                return jdDailyTaskEnum.ToString();
            }
            //object[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            object[] attributes = _fi.FirstOrDefault(w => w.Name == jdDailyTaskEnum.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return ((DescriptionAttribute)attributes[0]).Description;
            }
            else
            {
                return jdDailyTaskEnum.ToString();
            }

            //return ((DescriptionAttribute)jdDailyTaskEnum.GetType().GetField(jdDailyTaskEnum.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false)[0]).Description;
        }

        /// <summary>
        /// 获取指定枚举的 Description 
        /// </summary>
        /// <param name="jdDailyTaskEnum"></param>
        /// <returns></returns>
        public static string GetDesc(this JDDailyTaskEnum jdDailyTaskEnum)
        {
            return GetDescription(jdDailyTaskEnum);
        }
    }
}
