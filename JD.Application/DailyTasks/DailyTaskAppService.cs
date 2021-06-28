using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Threading;
using JD.Core;
using System.Linq;
using System.Diagnostics;

namespace JD.Application.DailyTasks
{
    /// <summary>
    /// 京东每日任务服务接口
    /// </summary>
    public partial class DailyTaskAppService : IDailyTaskAppService
    {
        private readonly DailyTaskHttpHelper _dailyTaskHttpHelper;
        private readonly ILogger _logger;


        /// <summary>
        /// 执行任务的间隔时间ms（为了不频繁调用京东接口）
        /// </summary>
        private int StepMilliSeconds = JDDailyTaskConst.StepMilliSeconds;

        /// <summary>
        /// 京东每日任务服务接口
        /// </summary>
        /// <param name="httpClientFactory"></param>
        /// <param name="logger"></param>
        public DailyTaskAppService(IHttpClientFactory httpClientFactory, ILogger<DailyTaskAppService> logger)
        {
            _dailyTaskHttpHelper = new DailyTaskHttpHelper(httpClientFactory, logger);
            _logger = logger;
        }


        /// <summary>
        /// 执行所有任务
        /// </summary>
        /// <param name="cookies"></param>
        /// <param name="autoTaskList"></param>
        /// <returns></returns>
        public async Task<List<CusResponseDto>> DoAllDailyTaskAsync(string cookies, List<string> autoTaskList)
        {
            List<CusResponseDto> listRes = new List<CusResponseDto>();
            List<JDDailyTaskEnum> jdDailyTaskEnum = autoTaskList.Select(s => Enum.Parse<JDDailyTaskEnum>(s)).ToList();

            // 京东商城-京豆签到
            //if (jdDailyTaskEnum.Contains(JDDailyTaskEnum.DoSignBean)) listRes.Add(DoSignBeanAsync(cookies);
            //Thread.Sleep(StepMilliSeconds);

            // 京东商城-京豆签到
            if (jdDailyTaskEnum.Contains(JDDailyTaskEnum.DoSignBean)) listRes.Add(await DoAllDailyTaskDelegateAsync(DoSignBeanAsync, cookies));

            // 京东商城-超市签到
            if (jdDailyTaskEnum.Contains(JDDailyTaskEnum.DoSignSupermarket)) listRes.Add(await DoAllDailyTaskDelegateAsync(DoSignSupermarketAsync, cookies));

            // 京东商城-直播签到
            if (jdDailyTaskEnum.Contains(JDDailyTaskEnum.DoSignWebcasts)) listRes.Add(await DoAllDailyTaskDelegateAsync(DoSignWebcastsAsync, cookies));

            // 京东金融-钢镚签到
            if (jdDailyTaskEnum.Contains(JDDailyTaskEnum.DoSignRongSteel)) listRes.Add(await DoAllDailyTaskDelegateAsync(DoSignRongSteelAsync, cookies));

            // 京东金融-赚钱频道-开屏奖励
            if (jdDailyTaskEnum.Contains(JDDailyTaskEnum.DoOpenScreenReward)) listRes.Add(await DoAllDailyTaskDelegateAsync(DoOpenScreenRewardAsync, cookies));

            // 京东商城-转盘抽奖
            if (jdDailyTaskEnum.Contains(JDDailyTaskEnum.DoSignLotteryDraw)) listRes.AddRange(await DoAllDailyTaskDelegateAsync(DoSignLotteryDrawAsync, cookies));

            // 京东商城-摇一摇
            if (jdDailyTaskEnum.Contains(JDDailyTaskEnum.DoSignShake)) listRes.AddRange(await DoAllDailyTaskDelegateAsync(DoSignShakeAsync, cookies));

            // 京东商城-闪购签到
            if (jdDailyTaskEnum.Contains(JDDailyTaskEnum.DoSignFlashSale)) listRes.Add(await DoAllDailyTaskDelegateAsync(DoSignFlashSaleAsync, cookies));

            // 京东现金-红包签到
            if (jdDailyTaskEnum.Contains(JDDailyTaskEnum.DoSignCash)) listRes.Add(await DoAllDailyTaskDelegateAsync(DoSignCashAsync, cookies));

            // 京东商城-现金签到
            if (jdDailyTaskEnum.Contains(JDDailyTaskEnum.DoSignGetCash)) listRes.Add(await DoAllDailyTaskDelegateAsync(DoSignGetCashAsync, cookies));

            // 京东商城-金贴签到
            if (jdDailyTaskEnum.Contains(JDDailyTaskEnum.DoSignSubsidy)) listRes.Add(await DoAllDailyTaskDelegateAsync(DoSignSubsidyAsync, cookies));

            // 京东秒杀-红包签到
            if (jdDailyTaskEnum.Contains(JDDailyTaskEnum.DoSignSecKilling)) listRes.Add(await DoAllDailyTaskDelegateAsync(DoSignSecKillingAsync, cookies));

            // 京东金融-抽奖签到
            if (jdDailyTaskEnum.Contains(JDDailyTaskEnum.DoSignLuckyLottery)) listRes.Add(await DoAllDailyTaskDelegateAsync(DoSignLuckyLotteryAsync, cookies));

            // 京东金融-签壹、签贰、签叁、签肆、签伍；金融现金-双签、京东金贴-双签
            if (jdDailyTaskEnum.Contains(JDDailyTaskEnum.DoSignJingRongDoll))
            {
                listRes.AddRange(await DoAllDailyTaskDelegateAsync(DoSignJingRongDollAsync, cookies));
                listRes.AddRange(await DoAllDailyTaskDelegateAsync(DoSignJingRongDoll2Async, cookies));
            }

            //京东商城-店铺签到
            if (jdDailyTaskEnum.Contains(JDDailyTaskEnum.DoSignShop)) listRes.AddRange(await DoAllDailyTaskDelegateAsync(DoSignShopAsync, cookies));

            //京东游戏-天天加速
            if (jdDailyTaskEnum.Contains(JDDailyTaskEnum.DoGameBySpeedUp)) listRes.AddRange(await DoAllDailyTaskDelegateAsync(DoGameBySpeedUpAsync, cookies));


            return listRes;
        }

        #region 为了限定方法的执行时间
        /// <summary>
        /// 为了限定方法的执行时间
        /// </summary>
        /// <param name="taskDelegate"></param>
        /// <param name="cookies"></param>
        /// <returns></returns>
        private async Task<CusResponseDto> DoAllDailyTaskDelegateAsync(Func<string, Task<CusResponseDto>> taskDelegate, string cookies)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var cusResponseDto = await taskDelegate(cookies);

            stopwatch.Stop();
            try
            {
                var diffSecond = StepMilliSeconds - stopwatch.ElapsedMilliseconds;
                if (diffSecond > 0) Thread.Sleep((int)diffSecond);
            }
            catch (Exception ex)
            {
                throw new Exception($"StepMilliSeconds: {StepMilliSeconds}, stopwatch.ElapsedMilliseconds: {stopwatch.ElapsedMilliseconds}, ex: {ex.Message}");
            }
            return cusResponseDto;
        }
        /// <summary>
        /// 为了限定方法的执行时间
        /// </summary>
        /// <param name="taskDelegate"></param>
        /// <param name="cookies"></param>
        /// <returns></returns>
        private async Task<List<CusResponseDto>> DoAllDailyTaskDelegateAsync(Func<string, Task<List<CusResponseDto>>> taskDelegate, string cookies)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var listCusResponseDto = await taskDelegate(cookies);

            stopwatch.Stop();
            try
            {
                var diffSecond = StepMilliSeconds - stopwatch.ElapsedMilliseconds;
                if (diffSecond > 0) Thread.Sleep((int)diffSecond);
            }
            catch (Exception ex)
            {
                throw new Exception($"StepMilliSeconds: {StepMilliSeconds}, stopwatch.ElapsedMilliseconds: {stopwatch.ElapsedMilliseconds}, ex: {ex.Message}");
            }
            return listCusResponseDto;
        }
        #endregion

    }
}
