using JD.Core;
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
        #region 京东商城-京豆签到
        /// <summary>
        /// 京东商城-京豆签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public async Task<CusResponseDto> DoSignBeanAsync(string cookies)
        {
            var responseDto = await _dailyTaskHttpHelper.DoSignBeanAsync(cookies);
            return responseDto.GetCusResponse();
        }
        #endregion

        #region 京东商城-超市签到
        /// <summary>
        /// 京东商城-超市签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public async Task<CusResponseDto> DoSignSupermarketAsync(string cookies)
        {
            var responseDto = await _dailyTaskHttpHelper.DoSignSupermarketAsync(cookies);
            return responseDto.GetCusResponse();
        }
        #endregion

        #region 京东商城-直播签到
        /// <summary>
        /// 京东商城-直播签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public async Task<CusResponseDto> DoSignWebcastsAsync(string cookies)
        {
            var responseDto = await _dailyTaskHttpHelper.DoSignWebcastsAsync(cookies);
            return responseDto.GetCusResponse();
        }
        #endregion

        #region 京东金融-钢镚签到
        /// <summary>
        /// 京东金融-钢镚签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public async Task<CusResponseDto> DoSignRongSteelAsync(string cookies)
        {
            var responseDto = await _dailyTaskHttpHelper.DoSignRongSteelAsync(cookies);
            return responseDto.GetCusResponse();
        }
        #endregion

        #region 京东金融-赚钱频道-开屏奖励
        /// <summary>
        /// 京东金融-赚钱频道-开屏奖励
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public async Task<CusResponseDto> DoOpenScreenRewardAsync(string cookies)
        {
            var responseDto = await _dailyTaskHttpHelper.DoOpenScreenRewardAsync(cookies);
            return responseDto.GetCusResponse();
        }
        #endregion

        #region 京东商城-转盘抽奖
        /// <summary>
        /// 京东商城-转盘抽奖
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public async Task<List<CusResponseDto>> DoSignLotteryDrawAsync(string cookies)
        {
            JDDailyTaskEnum jdDailyTaskIndex = JDDailyTaskEnum.DoSignLotteryDraw;
            var actionName = jdDailyTaskIndex.GetDesc();
            var times = 0;
            var listRes = new List<CusResponseDto>();

            try
            {
                string errQueryLotteryDraw = string.Empty;

                //需要先查询获取转盘抽奖机会次数和lotteryCode
                var queryLotteryDraw = await _dailyTaskHttpHelper.QueryLotteryDrawAsync(cookies);
                var lotteryCount = queryLotteryDraw.data.lotteryCount;
                if (queryLotteryDraw.code != 0) errQueryLotteryDraw = "转盘查询失败";
                else if (lotteryCount == 0) errQueryLotteryDraw = "无剩余转盘机会";

                if (string.IsNullOrEmpty(errQueryLotteryDraw))
                {
                    var lotteryCode = queryLotteryDraw.data.lotteryCode;
                    for (var i = 0; i < lotteryCount; i++) //有多少次数就转多少次
                    {
                        Thread.Sleep(1000 * 2); //不能调用太频繁
                        times++;
                        var responseDto = await _dailyTaskHttpHelper.DoSignLotteryDrawAsync(cookies, lotteryCode);
                        var cusResponse = responseDto.GetCusResponse();
                        cusResponse.ActionName = $"{actionName}[{times}]";
                        listRes.Add(cusResponse);

                        if (responseDto.code != 0 && responseDto.errorCode == "T211" && responseDto.errorMessage.Contains("操作太频繁"))
                        {
                            if (times < 10)
                            {
                                i -= 1; //相当于失败重试
                                Thread.Sleep(1000);
                            }
                        }
                    }
                }
                else
                {
                    listRes.Add(CusResponseDto.IsFail(jdDailyTaskIndex, $"{actionName}[1]", errQueryLotteryDraw));
                }
            }
            catch (Exception ex)
            {
                listRes.Add(CusResponseDto.IsFail(jdDailyTaskIndex, $"{actionName}[{times}]", ex.Message));
            }

            return listRes;
        }
        #endregion

        #region 京东商城-摇一摇
        /// <summary>
        /// 京东商城-摇一摇
        /// </summary>
        /// <param name="doActionIntput"></param>
        /// <param name="listResDetail"></param>
        /// <returns></returns>
        public async Task<List<CusResponseDto>> DoSignShakeAsync(string cookies)
        {
            JDDailyTaskEnum jdDailyTaskIndex = JDDailyTaskEnum.DoSignShake;
            var actionName = jdDailyTaskIndex.GetDesc();
            var times = 0;
            var listRes = new List<CusResponseDto>();

            try
            {
                times++;
                var responseDto = await DoSignShakeLoopAsync(cookies, $"{actionName}[{times}]", listRes);
                var freeTimes = (responseDto.success && responseDto.data != null && responseDto.data.luckyBox != null) ? responseDto.data.luckyBox.freeTimes : 0;
                for (var i = 0; i < freeTimes; i++)
                {
                    Thread.Sleep(1000 * 2); //不能调用太频繁
                    times++;
                    await DoSignShakeLoopAsync(cookies, $"{actionName}[{times}]", listRes);
                }
            }
            catch (Exception ex)
            {
                listRes.Add(CusResponseDto.IsFail(jdDailyTaskIndex, $"{actionName}[{times}]", ex.Message));
            }

            return listRes;
        }

        /// <summary>
        /// 京东商城-摇一摇
        /// </summary>
        /// <param name="doActionIntput"></param>
        /// <param name="listResDetail"></param>
        /// <param name="actionName"></param>
        /// <param name="jdDailyTaskIndex"></param>
        /// <returns></returns>
        private async Task<DoSignShakeDto> DoSignShakeLoopAsync(string cookies, string actionName, List<CusResponseDto> listRes)
        {
            var responseDto = await _dailyTaskHttpHelper.DoSignShake(cookies);

            var cusResponse = responseDto.GetCusResponse();
            cusResponse.ActionName = actionName;
            listRes.Add(cusResponse);

            return responseDto;
        }
        #endregion

        #region 京东商城-闪购签到
        /// <summary>
        /// 京东商城-闪购签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public async Task<CusResponseDto> DoSignFlashSaleAsync(string cookies)
        {
            var responseDto = await _dailyTaskHttpHelper.DoSignFlashSaleAsync(cookies);
            return responseDto.GetCusResponse();
        }
        #endregion

        #region 京东现金-红包签到
        /// <summary>
        /// 京东现金-红包签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public async Task<CusResponseDto> DoSignCashAsync(string cookies)
        {
            var responseDto = await _dailyTaskHttpHelper.DoSignCashAsync(cookies);
            return responseDto.GetCusResponse();
        }
        #endregion

        #region 京东商城-现金签到
        /// <summary>
        /// 京东商城-现金签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public async Task<CusResponseDto> DoSignGetCashAsync(string cookies)
        {
            var responseDto = await _dailyTaskHttpHelper.DoSignGetCashAsync(cookies);
            return responseDto.GetCusResponse();
        }
        #endregion

        #region 京东商城-金贴签到
        /// <summary>
        /// 京东商城-金贴签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public async Task<CusResponseDto> DoSignSubsidyAsync(string cookies)
        {
            var responseDto = await _dailyTaskHttpHelper.DoSignSubsidyAsync(cookies);
            return responseDto.GetCusResponse();
        }
        #endregion

        #region 京东秒杀-红包签到
        /// <summary>
        /// 京东秒杀-红包签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public async Task<CusResponseDto> DoSignSecKillingAsync(string cookies)
        {
            var responseDto = await _dailyTaskHttpHelper.DoSignSecKillingAsync(cookies);
            return responseDto.GetCusResponse();
        }
        #endregion

        #region 京东金融-抽奖签到
        /// <summary>
        /// 京东金融-抽奖签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public async Task<CusResponseDto> DoSignLuckyLotteryAsync(string cookies)
        {
            var responseDto = await _dailyTaskHttpHelper.DoSignLuckyLotteryAsync(cookies);
            return responseDto.GetCusResponse();
        }
        #endregion

        #region 京东金融-签壹、签贰、签叁、签肆、签伍
        /// <summary>
        /// 京东金融-签壹、签贰、签叁、签肆、签伍
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public async Task<List<CusResponseDto>> DoSignJingRongDollAsync(string cookies)
        {
            JDDailyTaskEnum jdDailyTaskIndex = JDDailyTaskEnum.DoSignJingRongDoll;
            var actionName = jdDailyTaskIndex.GetDesc();
            var listRes = new List<CusResponseDto>();

            var actCodes = new List<string>() { "4D25A6F482", "3A3E839252", "69F5EC743C", "30C4F86264", "1D06AA3B0F" };
            var actionNames = new List<string>() { "京东金融-签壹", "京东金融-签贰", "京东金融-签叁", "京东金融-签肆", "京东金融-签伍" };
            for (var i = 0; i < actCodes.Count; i++)
            {
                try
                {
                    Thread.Sleep(1000 * 2); //不能调用太频繁
                    var actCode = actCodes[i];
                    actionName = actionNames[i];

                    var responseDto = await _dailyTaskHttpHelper.DoSignJingRongDollAsync(cookies, actCode, 3);
                    //需要重复调用才能正常领取到京豆
                    await _dailyTaskHttpHelper.DoSignJingRongDollAsync(cookies, actCode, 4);
                    listRes.Add(responseDto.GetCusResponse(actionName));
                }
                catch (Exception ex)
                {
                    listRes.Add(CusResponseDto.IsFail(jdDailyTaskIndex, actionName, ex.Message));
                }
            }

            return listRes;
        }
        #endregion

        #region 金融现金-双签、京东金贴-双签
        /// <summary>
        /// 金融现金-双签、京东金贴-双签
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public async Task<List<CusResponseDto>> DoSignJingRongDoll2Async(string cookies)
        {
            JDDailyTaskEnum jdDailyTaskIndex = JDDailyTaskEnum.DoSignJingRongDoll;
            var actionName = jdDailyTaskIndex.GetDesc();
            var listRes = new List<CusResponseDto>();

            var actCodes = new List<string>() { "F68B2C3E71", "F68B2C3E71", "1DF13833F7" };
            var actionNames = new List<string>() { "金融京豆-双签", "金融现金-双签", "京东金贴-双签" };
            var frontParams = new List<string>() { "{\"belong\":\"jingdou\"}", "{\"belong\":\"xianjin\"}", "{\"channel\":\"JR\",\"belong\":4}" };
            for (var i = 0; i < actCodes.Count; i++)
            {
                try
                {
                    Thread.Sleep(1000 * 2); //不能调用太频繁
                    var actCode = actCodes[i];
                    actionName = actionNames[i];
                    var frontParam = frontParams[i];

                    var responseDto = await _dailyTaskHttpHelper.DoSignJingRongDollAsync(cookies, actCode, 3, frontParam);
                    ////需要重复调用才能正常领取到京豆
                    //await _dailyTaskHttpHelper.DoSignJingRongDollAsync(cookies, actCode, 4);
                    listRes.Add(responseDto.GetCusResponse(actionName));
                }
                catch (Exception ex)
                {
                    listRes.Add(CusResponseDto.IsFail(jdDailyTaskIndex, actionName, ex.Message));
                }
            }

            return listRes;
        }
        #endregion

        #region 京东商城-店铺签到
        /// <summary>
        /// 京东商城-店铺签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public async Task<List<CusResponseDto>> DoSignShopAsync(string cookies)
        {
            JDDailyTaskEnum jdDailyTaskIndex = JDDailyTaskEnum.DoSignShop;
            var actionName = jdDailyTaskIndex.GetDesc();
            var listRes = new List<CusResponseDto>();
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            ////keyValues.Add("xK148m4kWj5hBcTPuJUNNXH3AkJ", "京东商城-签到(晚市补贴金)");
            ////keyValues.Add("3tqTG5sF1xCUyC6vgEF5CLCxGn7w", "京东商城-签到(医药馆)");
            ////keyValues.Add("zGwAUzL3pVGjptBBGeYfpKjYdtX", "京东商城-签到(酒饮馆)");
            ////keyValues.Add("37ta5sh5ocrMZF3Fz5UMJbTsL42", "京东商城-签到(宠物)");

            keyValues.Add("4RXyb1W4Y986LJW8ToqMK14BdTD", "京东商城-签到(鞋靴)");
            keyValues.Add("36V2Qw59VPNsuLxY84vCFtxFzrFs", "京东商城-签到(日历翻牌)");
            keyValues.Add("3Af6mZNcf5m795T8dtDVfDwWVNhJ", "京东商城-签到(童装馆)");
            keyValues.Add("3BbAVGQPDd6vTyHYjmAutXrKAos6", "京东商城-签到(母婴馆)");
            keyValues.Add("4SWjnZSCTHPYjE5T7j35rxxuMTb6", "京东商城-签到(数码电器馆)");
            keyValues.Add("DpSh7ma8JV7QAxSE2gJNro8Q2h9", "京东商城-签到(女装馆)");
            keyValues.Add("3SC6rw5iBg66qrXPGmZMqFDwcyXi", "京东商城-签到(图书)");
            keyValues.Add("4RBT3H9jmgYg1k2kBnHF8NAHm7m8", "京东商城-签到(服饰)");
            keyValues.Add("4PgpL1xqPSW1sVXCJ3xopDbB1f69", "京东商城-签到(内衣)");
            keyValues.Add("ZrH7gGAcEkY2gH8wXqyAPoQgk6t", "京东商城-签到(箱包)");
            keyValues.Add("2QUxWHx5BSCNtnBDjtt5gZTq7zdZ", "京东商城-签到(校园)");
            keyValues.Add("w2oeK5yLdHqHvwef7SMMy4PL8LF", "京东商城-签到(健康)");
            keyValues.Add("3S28janPLYmtFxypu37AYAGgivfp", "京东商城-签到(二手)");
            keyValues.Add("2Tjm6ay1ZbZ3v7UbriTj6kHy9dn6", "京东商城-签到(清洁)");
            keyValues.Add("2tZssTgnQsiUqhmg5ooLSHY9XSeN", "京东商城-签到(个护)");
            keyValues.Add("zHUHpTHNTaztSRfNBFNVZscyFZU", "京东商城-签到(珠宝)");
            keyValues.Add("2BcJPCVVzMEtMUynXkPscCSsx68W", "京东商城-签到(钟表)");
            keyValues.Add("2smCxzLNuam5L14zNJHYu43ovbAP", "京东商城-签到(美妆)");
            keyValues.Add("Wcu2LVCFMkBP3HraRvb7pgSpt64", "京东商城-签到(菜场)");

            foreach (var dic in keyValues)
            {
                try
                {
                    var responseDto = await DoSignShopAsync(cookies, dic.Key, dic.Value);
                    listRes.Add(responseDto.GetCusResponse(dic.Value));
                    Thread.Sleep(StepMilliSeconds); //每次调用的时间间隔
                }
                catch (Exception ex)
                {
                    listRes.Add(CusResponseDto.IsFail(jdDailyTaskIndex, actionName, ex.Message));
                }
            }

            return listRes;
        }
        /// <summary>
        /// 京东商城-店铺签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <param name="activityId"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        private async Task<DoSignShopDto> DoSignShopAsync(string cookies, string activityId, string title, bool isFirstAction = true, string paginationFlrs = "")
        {
            string _params = string.Empty;
            var responseDto = new DoSignShopDto();

            //查询
            var queryDto = await _dailyTaskHttpHelper.QuerySignShopAsync(cookies, activityId, title, paginationFlrs);

            if (queryDto.code != 0) throw new Exception(queryDto.msg);
            if (queryDto.floatLayerList != null && queryDto.floatLayerList.Exists(w => !string.IsNullOrEmpty(w.Params)))
            {
                //签到数据1
                _params = queryDto.floatLayerList.FirstOrDefault(w => !string.IsNullOrEmpty(w.Params))?.Params;
            }
            else
            {
                if (queryDto.floorList != null)
                {
                    if (queryDto.floorList.Exists(w => w.signInfos != null))
                    {
                        var signInfos = queryDto.floorList.FirstOrDefault(w => w.signInfos != null).signInfos;
                        if (signInfos.signStat == 1)
                        {
                            responseDto.code = -1;
                            responseDto.msg = "重复签到";
                            return responseDto;
                        };
                        //签到数据2
                        _params = signInfos?.Params;
                    }
                    if (string.IsNullOrEmpty(_params))
                    {
                        var turnTableId = queryDto.floorList.Where(w => w.boardParams != null && w.boardParams.turnTableId != null).FirstOrDefault().boardParams?.turnTableId;

                        // 无签到数据, 但含有关注店铺签到
                        if (!string.IsNullOrEmpty(turnTableId))
                        {
                            Thread.Sleep(1000);

                            //“进入详情页面”
                            await _dailyTaskHttpHelper.QuerySignChannelAsync(cookies, turnTableId, title);

                            Thread.Sleep(1000);

                            //签到
                            var doSignChannelDto = await _dailyTaskHttpHelper.DoSignChannelAsync(cookies, turnTableId, title);
                            var doSignChannelResponseDto = doSignChannelDto.GetCusResponse();
                            if (doSignChannelResponseDto.Success)
                            {
                                responseDto.code = 0;
                                responseDto.jdBean = doSignChannelResponseDto.AwardCount;
                            }
                            else
                            {
                                responseDto.code = -1;
                                responseDto.msg = doSignChannelResponseDto.ErrMsg;
                            }
                            return responseDto;
                        }
                        // 无签到数据, 尝试带参查询
                        if (!string.IsNullOrEmpty(queryDto.paginationFlrs) && isFirstAction)
                        {
                            //二次查询
                            return await DoSignShopAsync(cookies, activityId, $"{title}二次查询", false, queryDto.paginationFlrs);
                        }
                    }
                }
            }
            //if (queryDto.floatLayerList == null || queryDto.floatLayerList.Count == 0) throw new Exception(queryDto.msg ?? "京东商城-店铺签到（查询）异常");

            if (string.IsNullOrEmpty(_params)) throw new Exception($"{(isFirstAction ? "" : "二次查询")}不含活动数据");

            Thread.Sleep(1000);

            //签到
            responseDto = await _dailyTaskHttpHelper.DoSignShopAsync(cookies, title, _params);
            return responseDto;
        }
        #endregion

    }
}
