using JD.Application.DailyTasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Application.DailyTasks
{
    public interface IDailyTaskAppService
    {
        /// <summary>
        /// 执行所有任务
        /// </summary>
        /// <param name="cookies"></param>
        /// <param name="autoTaskList"></param>
        /// <returns></returns>
        Task<List<CusResponseDto>> DoAllDailyTaskAsync(string cookies, List<string> autoTaskList);
        
        ///// <summary>
        ///// 执行所有任务
        ///// </summary>
        ///// <param name="cookies"></param>
        ///// <param name="autoTaskList"></param>
        ///// <returns></returns>
        //Task<List<CusResponseDto>> DoAllDailyTaskTestAsync(string cookies, List<string> autoTaskList);

        /// <summary>
        /// 京东商城-京豆签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        Task<CusResponseDto> DoSignBeanAsync(string cookies);

        /// <summary>
        /// 京东商城-超市签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        Task<CusResponseDto> DoSignSupermarketAsync(string cookies);

        /// <summary>
        /// 京东商城-直播签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        Task<CusResponseDto> DoSignWebcastsAsync(string cookies);

        /// <summary>
        /// 京东金融-钢镚签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        Task<CusResponseDto> DoSignRongSteelAsync(string cookies);

        /// <summary>
        /// 京东金融-赚钱频道-开屏奖励
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        Task<CusResponseDto> DoOpenScreenRewardAsync(string cookies);

        ///// <summary>
        ///// 京东商城-转盘查询
        ///// </summary>
        ///// <param name="cookies"></param>
        ///// <returns></returns>
        //Task<QueryLotteryDrawDto> QueryLotteryDrawAsync(string cookies);

        /// <summary>
        /// 京东商城-转盘抽奖
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        Task<List<CusResponseDto>> DoSignLotteryDrawAsync(string cookies);

        /// <summary>
        /// 京东商城-摇一摇
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        Task<List<CusResponseDto>> DoSignShakeAsync(string cookies);

        /// <summary>
        /// 京东商城-闪购签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        Task<CusResponseDto> DoSignFlashSaleAsync(string cookies);

        /// <summary>
        /// 京东现金-红包签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        Task<CusResponseDto> DoSignCashAsync(string cookies);

        /// <summary>
        /// 京东商城-现金签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        Task<CusResponseDto> DoSignGetCashAsync(string cookies);

        /// <summary>
        /// 京东商城-金贴签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        Task<CusResponseDto> DoSignSubsidyAsync(string cookies);

        /// <summary>
        /// 京东秒杀-红包签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        Task<CusResponseDto> DoSignSecKillingAsync(string cookies);

        /// <summary>
        /// 京东金融-抽奖签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        Task<CusResponseDto> DoSignLuckyLotteryAsync(string cookies);

        /// <summary>
        /// 京东金融-签壹、签贰、签叁、签肆、签伍
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        Task<List<CusResponseDto>> DoSignJingRongDollAsync(string cookies);

        /// <summary>
        /// 京东商城-店铺签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        Task<List<CusResponseDto>> DoSignShopAsync(string cookies);
    }
}
