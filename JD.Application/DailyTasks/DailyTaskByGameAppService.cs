using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JD.Application.DailyTasks
{
    public partial class DailyTaskAppService : IDailyTaskAppService
    {
        #region 京东游戏-天天加速
        /// <summary>
        /// 京东游戏-天天加速
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public async Task<List<CusResponseDto>> DoGameBySpeedUpAsync(string cookies)
        {
            var listRes = new List<CusResponseDto>();

            try
            {
                var responseDto = await _dailyTaskHttpHelper.DoGameBySpeedUpAsync(cookies);

                if (responseDto.success && responseDto.data != null)
                {
                    //step1: 本次任务是否有奖励
                    if (responseDto.data.task_status == 2)
                    {
                        listRes.Add(responseDto.GetCusResponse());

                        Thread.Sleep(1000);

                        //本次任务有奖励，再次尝试领取
                        await DoGameBySpeedUpLoopAsync(cookies, listRes);
                    }

                    //step2: 开启下轮任务
                    if (responseDto.data.task_status == 0)
                    {
                        listRes.Add(responseDto.GetCusResponse());

                        Thread.Sleep(1000);

                        responseDto = await _dailyTaskHttpHelper.DoGameBySpeedUpAsync(cookies, responseDto.data.source_id);
                    }

                    //step3: 处理太空事件、处理太空任务、查询道具ID、使用道具、如果使用了道具, 则再次检查任务
                    if (responseDto.data.task_status == 1)
                    {
                        //step3.1: 获取太空事件
                        var dic = new Dictionary<int, string>(); //太空事件的问题id和答案

                        Thread.Sleep(1000);

                        var spaceEventDto = await _dailyTaskHttpHelper.DoGameBySpeedUpSpaceEventAsync(cookies);
                        if (spaceEventDto.success && spaceEventDto.data.Count > 0)
                        {
                            var _options = spaceEventDto.data.Where(w => w.status == 1).ToList();
                            foreach (var item in _options)
                            {
                                foreach (var son in item.options.Where(w => w.type == 1))
                                {
                                    dic.Add(item.id, son.value);
                                }
                            }
                        }

                        //step3.2: 处理太空任务
                        foreach (var item in dic)
                        {
                            Thread.Sleep(1000 * 2);

                            await _dailyTaskHttpHelper.DoGameBySpeedUpSpaceEventHandleAsync(cookies, item.Key, item.Value);
                        }

                        //step3.3: 查询道具ID
                        var ids = new List<string>();

                        Thread.Sleep(1000);

                        var propDto = await _dailyTaskHttpHelper.DoGameBySpeedUpPropAsync(cookies);
                        if (propDto.success && propDto.data.Count > 0)
                        {
                            ids = propDto.data.Select(s => s.id).ToList();
                        }

                        //step3.4: 使用道具
                        foreach (var id in ids)
                        {
                            Thread.Sleep(1000);

                            await _dailyTaskHttpHelper.DoGameBySpeedUpPropHandleAsync(cookies, id);
                        }

                        //step3.5: 如果使用了道具, 则再次检查任务
                        if (ids.Count > 0)
                        {
                            Thread.Sleep(1000);

                            await DoGameBySpeedUpLoopAsync(cookies, listRes);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return listRes;
        }
        private async Task<List<CusResponseDto>> DoGameBySpeedUpLoopAsync(string cookies, List<CusResponseDto> listRes = null)
        {
            //var listRes = new List<CusResponseDto>();
            if (listRes == null) listRes = new List<CusResponseDto>();

            var responseDto = await _dailyTaskHttpHelper.DoGameBySpeedUpAsync(cookies);
            listRes.Add(responseDto.GetCusResponse());
            if (responseDto.success && responseDto.data != null)
            {
                //step1: 本次任务是否有奖励
                if (responseDto.data.task_status == 2)
                {
                    listRes.Add(responseDto.GetCusResponse());

                    Thread.Sleep(1000);

                    //本次任务有奖励，再次尝试领取
                    await DoGameBySpeedUpLoopAsync(cookies, listRes);
                }
            }
            return listRes;
        }
        #endregion

    }
}
