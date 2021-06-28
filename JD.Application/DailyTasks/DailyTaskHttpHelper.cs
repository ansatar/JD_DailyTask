using JD.Utils;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JD.Application.DailyTasks
{
    public class DailyTaskHttpHelper
    {
        private readonly ILogger _logger;
        private readonly HttpHelper _httpHelper;

        public DailyTaskHttpHelper(IHttpClientFactory httpClientFactory, ILogger logger)
        {
            _logger = logger;
            _httpHelper = new HttpHelper(httpClientFactory);
        }

        /// <summary>
        /// 京东商城-京豆签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public async Task<DoSignBeanDto> DoSignBeanAsync(string cookies)
        {
            var url = $"https://api.m.jd.com/client.action";
            var requestString = $"functionId=signBeanIndex&appid=ld";

            var dicHeaders = JDHelper.GetDefaultDicHeaders(cookies);
            dicHeaders.Add("Referer", $"https://wqs.jd.com/my/jingdou/my.shtml?sceneval=2");

            var httpResponse = await _httpHelper.PostRawTextAsync(url, requestString, dicHeaders);
            _logger.LogDebug($"京东商城-京豆签到：{httpResponse.Result}");
            var res = JsonConvert.DeserializeObject<DoSignBeanDto>(httpResponse.Result);

            return res;
        }

        /// <summary>
        /// 京东商城-超市签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public async Task<DoSignSupermarketDto> DoSignSupermarketAsync(string cookies)
        {
            var url = $"https://api.m.jd.com/api";
            var requestString = $"appid=jdsupermarket&functionId=smtg_sign&clientVersion=8.0.0&client=m&body=%7B%7D";

            var dicHeaders = JDHelper.GetDefaultDicHeaders(cookies);
            dicHeaders.Add("Origin", $"https://jdsupermarket.jd.com");

            var httpResponse = await _httpHelper.PostRawTextAsync(url, requestString, dicHeaders);
            _logger.LogDebug($"京东商城-超市签到：{httpResponse.Result}");
            var res = JsonConvert.DeserializeObject<DoSignSupermarketDto>(httpResponse.Result);

            return res;
        }

        /// <summary>
        /// 京东商城-直播签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public async Task<DoSignWebcastsDto> DoSignWebcastsAsync(string cookies)
        {
            var url = $"https://api.m.jd.com/api";
            var requestString = $"functionId=getChannelTaskRewardToM&appid=h5-live&body=%7B%22type%22%3A%22signTask%22%2C%22itemId%22%3A%221%22%7D";

            var dicHeaders = JDHelper.GetDefaultDicHeaders(cookies);
            dicHeaders.Add("Origin", $"https://h.m.jd.com");

            var httpResponse = await _httpHelper.PostRawTextAsync(url, requestString, dicHeaders);
            _logger.LogDebug($"京东商城-直播签到：{httpResponse.Result}");
            var res = JsonConvert.DeserializeObject<DoSignWebcastsDto>(httpResponse.Result);

            return res;
        }

        /// <summary>
        /// 京东金融-钢镚签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public async Task<DoSignRongSteelDto> DoSignRongSteelAsync(string cookies)
        {
            var url = $"https://ms.jr.jd.com/gw/generic/hy/h5/m/signIn1";
            var requestString = $"reqData=%7B%22channelSource%22%3A%22JRAPP6.0%22%2C%22riskDeviceParam%22%3A%22%7B%7D%22%7D";

            var dicHeaders = JDHelper.GetDefaultDicHeaders(cookies);

            var httpResponse = await _httpHelper.PostRawTextAsync(url, requestString, dicHeaders);
            _logger.LogDebug($"京东金融-钢镚签到：{httpResponse.Result}");
            var res = JsonConvert.DeserializeObject<DoSignRongSteelDto>(httpResponse.Result);

            return res;
        }

        /// <summary>
        /// 京东金融-赚钱频道-开屏奖励
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public async Task<DoOpenScreenRewardDto> DoOpenScreenRewardAsync(string cookies)
        {
            var url = $"https://ms.jr.jd.com/gw/generic/zc/h5/m/openScreenReward";
            var requestString = $"reqData=%7B%22channelCode%22%3A%22ZHUANQIAN%22%2C%22clientType%22%3A%22sms%22%2C%22clientVersion%22%3A%2211.0%22%7D";

            var dicHeaders = JDHelper.GetDefaultDicHeaders(cookies);

            var httpResponse = await _httpHelper.PostRawTextAsync(url, requestString, dicHeaders);
            _logger.LogDebug($"京东金融-赚钱频道-开屏奖励：{httpResponse.Result}");
            var res = JsonConvert.DeserializeObject<DoOpenScreenRewardDto>(httpResponse.Result);

            return res;
        }

        /// <summary>
        /// 京东商城-转盘查询
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public async Task<QueryLotteryDrawDto> QueryLotteryDrawAsync(string cookies)
        {
            var url = $"https://api.m.jd.com/client.action";
            var requestString = $"functionId=wheelSurfIndex&body=%7B%22actId%22%3A%22jgpqtzjhvaoym%22%2C%22appSource%22%3A%22jdhome%22%7D&appid=ld";

            var dicHeaders = JDHelper.GetDefaultDicHeaders(cookies);

            var httpResponse = await _httpHelper.PostRawTextAsync(url, requestString, dicHeaders);
            _logger.LogDebug($"京东商城-转盘查询：{httpResponse.Result}");
            var res = JsonConvert.DeserializeObject<QueryLotteryDrawDto>(httpResponse.Result);

            return res;
        }

        /// <summary>
        /// 京东商城-转盘抽奖
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public async Task<DoSignLotteryDrawDto> DoSignLotteryDrawAsync(string cookies, string lotteryCode)
        {
            var url = $"https://api.m.jd.com/client.action";
            var requestString = $"functionId=lotteryDraw&body=%7B%22actId%22%3A%22jgpqtzjhvaoym%22%2C%22appSource%22%3A%22jdhome%22%2C%22lotteryCode%22%3A%22{lotteryCode}%22%7D&appid=ld";

            var dicHeaders = JDHelper.GetDefaultDicHeaders(cookies);

            var httpResponse = await _httpHelper.PostRawTextAsync(url, requestString, dicHeaders);
            _logger.LogDebug($"京东商城-转盘抽奖：{httpResponse.Result}");
            var res = JsonConvert.DeserializeObject<DoSignLotteryDrawDto>(httpResponse.Result);

            return res;
        }

        /// <summary>
        /// 京东商城-摇一摇
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public async Task<DoSignShakeDto> DoSignShake(string cookies)
        {
            var url = $"https://api.m.jd.com/client.action?appid=vip_h5&functionId=vvipclub_shaking";

            var dicHeaders = JDHelper.GetDefaultDicHeaders(cookies);

            var httpResponse = await _httpHelper.GetAndGetRespAsync(url, dicHeaders);
            _logger.LogDebug($"京东商城-摇一摇：{httpResponse.Result}");
            var res = JsonConvert.DeserializeObject<DoSignShakeDto>(httpResponse.Result);

            return res;
        }

        /// <summary>
        /// 京东商城-闪购签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public async Task<DoSignFlashSaleDto> DoSignFlashSaleAsync(string cookies)
        {
            var url = $"https://api.m.jd.com/client.action?functionId=partitionJdSgin";
            var requestString = $"body=%7B%22version%22%3A%22v2%22%7D&client=apple&clientVersion=9.0.8&openudid=1fce88cd05c42fe2b054e846f11bdf33f016d676&sign=6768e2cf625427615dd89649dd367d41&st=1597248593305&sv=121";

            var dicHeaders = JDHelper.GetDefaultDicHeaders(cookies);

            var httpResponse = await _httpHelper.PostAndGetRespAsync(url, requestString, dicHeaders);
            _logger.LogDebug($"京东商城-闪购签到：{httpResponse.Result}");
            var res = JsonConvert.DeserializeObject<DoSignFlashSaleDto>(httpResponse.Result);

            return res;
        }

        /// <summary>
        /// 京东现金-红包签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public async Task<DoSignCashDto> DoSignCashAsync(string cookies)
        {
            var url = $"https://api.m.jd.com/client.action?functionId=ccSignInNew";
            var requestString = $"body=%7B%22pageClickKey%22%3A%22CouponCenter%22%2C%22eid%22%3A%22O5X6JYMZTXIEX4VBCBWEM5PTIZV6HXH7M3AI75EABM5GBZYVQKRGQJ5A2PPO5PSELSRMI72SYF4KTCB4NIU6AZQ3O6C3J7ZVEP3RVDFEBKVN2RER2GTQ%22%2C%22shshshfpb%22%3A%22v1%5C%2FzMYRjEWKgYe%2BUiNwEvaVlrHBQGVwqLx4CsS9PH1s0s0Vs9AWk%2B7vr9KSHh3BQd5NTukznDTZnd75xHzonHnw%3D%3D%22%2C%22childActivityUrl%22%3A%22openapp.jdmobile%253a%252f%252fvirtual%253fparams%253d%257b%255c%2522category%255c%2522%253a%255c%2522jump%255c%2522%252c%255c%2522des%255c%2522%253a%255c%2522couponCenter%255c%2522%257d%22%2C%22monitorSource%22%3A%22cc_sign_ios_index_config%22%7D&client=apple&clientVersion=8.5.0&d_brand=apple&d_model=iPhone8%2C2&openudid=1fce88cd05c42fe2b054e846f11bdf33f016d676&scope=11&screen=1242%2A2208&sign=1cce8f76d53fc6093b45a466e93044da&st=1581084035269&sv=102";

            var dicHeaders = JDHelper.GetDefaultDicHeaders(cookies);

            var httpResponse = await _httpHelper.PostAndGetRespAsync(url, requestString, dicHeaders);
            _logger.LogDebug($"京东现金-红包签到：{httpResponse.Result}");
            var res = JsonConvert.DeserializeObject<DoSignCashDto>(httpResponse.Result);

            return res;
        }

        /// <summary>
        /// 京东商城-现金签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public async Task<DoSignGetCashDto> DoSignGetCashAsync(string cookies)
        {
            var url = $"https://api.m.jd.com/client.action?functionId=cash_sign&body=%7B%22remind%22%3A0%2C%22inviteCode%22%3A%22%22%2C%22type%22%3A0%2C%22breakReward%22%3A0%7D&client=apple&clientVersion=9.0.8&openudid=1fce88cd05c42fe2b054e846f11bdf33f016d676&sign=7e2f8bcec13978a691567257af4fdce9&st=1596954745073&sv=111";

            var dicHeaders = JDHelper.GetDefaultDicHeaders(cookies);

            var httpResponse = await _httpHelper.GetAndGetRespAsync(url, dicHeaders);
            _logger.LogDebug($"京东商城-现金签到：{httpResponse.Result}");
            var res = JsonConvert.DeserializeObject<DoSignGetCashDto>(httpResponse.Result);

            return res;
        }

        /// <summary>
        /// 京东商城-金贴签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public async Task<DoSignSubsidyDto> DoSignSubsidyAsync(string cookies)
        {
            var url = $"https://ms.jr.jd.com/gw/generic/uc/h5/m/signIn7";

            var dicHeaders = JDHelper.GetDefaultDicHeaders(cookies);
            dicHeaders.Add("Referer", "https://active.jd.com/forever/cashback/index");

            var httpResponse = await _httpHelper.GetAndGetRespAsync(url, dicHeaders);
            _logger.LogDebug($"京东商城-金贴签到：{httpResponse.Result}");
            var res = JsonConvert.DeserializeObject<DoSignSubsidyDto>(httpResponse.Result);

            return res;
        }

        /// <summary>
        /// 京东秒杀-红包签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        private async Task<QuerySecKillingDto> QuerySecKilling(string cookies)
        {
            var url = $"https://api.m.jd.com/client.action";
            var requestString = $"functionId=freshManHomePage&body=%7B%7D&client=wh5&appid=SecKill2020";

            var dicHeaders = JDHelper.GetDefaultDicHeaders(cookies);
            dicHeaders.Add("Origin", "https://h5.m.jd.com");

            var httpResponse = await _httpHelper.PostAndGetRespAsync(url, requestString, dicHeaders);
            _logger.LogDebug($"京东秒杀-红包签到：{httpResponse.Result}");
            var res = JsonConvert.DeserializeObject<QuerySecKillingDto>(httpResponse.Result);

            return res;
        }

        /// <summary>
        /// 京东秒杀-红包签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public async Task<DoSignSecKillingDto> DoSignSecKillingAsync(string cookies)
        {
            var querySecKillingDto = await QuerySecKilling(cookies);

            if (querySecKillingDto.code != 200)
            {
                return new DoSignSecKillingDto() { code = -1, msg = querySecKillingDto.result == null ? "查询红包异常" : querySecKillingDto.result.button };
            }

            var url = $"https://api.m.jd.com/client.action";
            var requestString = $"functionId=doInteractiveAssignment&body=%7B%22encryptProjectId%22%3A%22{querySecKillingDto.result.projectId}%22%2C%22encryptAssignmentId%22%3A%22{querySecKillingDto.result.taskId}%22%2C%22completionFlag%22%3Atrue%7D&client=wh5&appid=SecKill2020";

            var dicHeaders = JDHelper.GetDefaultDicHeaders(cookies);
            dicHeaders.Add("Origin", "https://h5.m.jd.com");

            var httpResponse = await _httpHelper.PostAndGetRespAsync(url, requestString, dicHeaders);
            _logger.LogDebug($"京东秒杀-红包签到：{httpResponse.Result}");
            var res = JsonConvert.DeserializeObject<DoSignSecKillingDto>(httpResponse.Result);

            return res;

            //return JsonConvert.DeserializeObject<DoSignSecKillingDto>("{\"msg\":\"success\",\"code\":\"0\",\"subCode\":\"0\",\"rewardsInfo\":{\"successRewards\":{\"4\":[{\"rewardImg\":\"\",\"hbId\":\"8556388759\",\"poolId\":1,\"discount\":0.06,\"beginTime\":1611824292982,\"endTime\":1611935999982}]},\"failRewards\":[]},\"assignmentInfo\":{\"completionCnt\":1,\"maxTimes\":1}}");
        }

        /// <summary>
        /// 京东金融-抽奖签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public async Task<DoSignLuckyLotteryDto> DoSignLuckyLotteryAsync(string cookies)
        {
            var url = $"https://ms.jr.jd.com/gw/generic/syh_yxmx/h5/m/handleSign";
            var requestString = $"reqData=%7B%22activityNo%22%3A%22e2d1b240d5674def8178be6b4faac5b6%22%2C%22signType%22%3A%221%22%2C%22encryptSign%22%3A%22%22%7D";

            var dicHeaders = JDHelper.GetDefaultDicHeaders(cookies);

            var httpResponse = await _httpHelper.PostAndGetRespAsync(url, requestString, dicHeaders);
            _logger.LogDebug($"京东金融-抽奖签到：{httpResponse.Result}");
            var res = JsonConvert.DeserializeObject<DoSignLuckyLotteryDto>(httpResponse.Result);

            return res;
        }

        /// <summary>
        /// 京东金融-签壹、签贰、签叁、签肆、签伍，金融现金-双签，京东金贴-双签
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public async Task<DoSignJingRongDollDto> DoSignJingRongDollAsync(string cookies, string actCode, int type, string frontParam = "")
        {
            var url = $"https://nu.jr.jd.com/gw/generic/jrm/h5/m/process";
            var requestString = string.Empty;

            if (string.IsNullOrEmpty(frontParam))
                requestString = $"reqData={{\"actCode\":\"{actCode}\",\"type\":{type}}}";
            else
                requestString = $"reqData={{\"actCode\":\"{actCode}\",\"type\":{type},\"frontParam\":{frontParam}}}";

            var dicHeaders = JDHelper.GetDefaultDicHeaders(cookies);

            var httpResponse = await _httpHelper.PostAndGetRespAsync(url, requestString, dicHeaders);
            _logger.LogDebug($"京东金融-签壹：{httpResponse.Result}");
            var res = JsonConvert.DeserializeObject<DoSignJingRongDollDto>(httpResponse.Result);

            return res;
        }

        /// <summary>
        /// 京东商城-店铺签到（查询）
        /// </summary>
        /// <param name="cookies"></param>
        /// <param name="activityId"></param>
        /// <param name="title"></param>
        /// <param name="paginationFlrs"></param>
        /// <returns></returns>
        public async Task<QuerySignShopDto> QuerySignShopAsync(string cookies, string activityId, string title, string paginationFlrs = "")
        {
            var url = $"https://api.m.jd.com/?client=wh5&functionId=qryH5BabelFloors";
            var requestString = "";
            if (string.IsNullOrEmpty(paginationFlrs))
                requestString = $"body={{\"activityId\":\"{activityId}\"}}";
            else
                requestString = $"body={{\"activityId\":\"{activityId}\",\"paginationParam\":\"2\",\"paginationFlrs\":\"{paginationFlrs}\"}}";

            var dicHeaders = JDHelper.GetDefaultDicHeaders(cookies);

            var httpResponse = await _httpHelper.PostAndGetRespAsync(url, requestString, dicHeaders);
            _logger.LogDebug($"{title}（查询）：{httpResponse.Result}");
            var res = JsonConvert.DeserializeObject<QuerySignShopDto>(httpResponse.Result);

            return res;
        }

        /// <summary>
        /// 京东商城-店铺签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <param name="title"></param>
        /// <param name="_params"></param>
        /// <returns></returns>
        public async Task<DoSignShopDto> DoSignShopAsync(string cookies, string title, string _params)
        {
            var url = $"https://api.m.jd.com/client.action?functionId=userSign";
            _params = _params.Replace("\\", "\\\\");
            _params = _params.Replace("\"", "\\\"");
            //var requestString = $"body={{\"params\":\"{_params }\"}}&client=wh5";
            var encode = System.Web.HttpUtility.UrlEncode($"{{\"params\":\"{_params }\"}}");
            var requestString = $"body={encode}&client=wh5";

            var dicHeaders = JDHelper.GetDefaultDicHeaders(cookies);

            var httpResponse = await _httpHelper.PostAndGetRespAsync(url, requestString, dicHeaders);
            _logger.LogDebug($"{title}：{httpResponse.Result}");
            var res = JsonConvert.DeserializeObject<DoSignShopDto>(httpResponse.Result);

            return res;
        }

        /// <summary>
        /// 京东商城-店铺（频道）查询
        /// </summary>
        /// <param name="cookies"></param>
        /// <param name="turnTableId"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public async Task QuerySignChannelAsync(string cookies, string turnTableId, string title)
        {
            var url = $"https://jdjoy.jd.com/api/turncard/channel/detail?turnTableId={turnTableId}&invokeKey=yPsq1PHN";

            var dicHeaders = JDHelper.GetDefaultDicHeaders(cookies);

            var httpResponse = await _httpHelper.GetAndGetRespAsync(url, dicHeaders);
            _logger.LogDebug($"{title}（频道）查询：{httpResponse.Result}");
            //var res = JsonConvert.DeserializeObject<QuerySignShopDto>(httpResponse.Result);

            //return res;
        }

        /// <summary>
        /// 京东商城-店铺（频道）签到
        /// </summary>
        /// <param name="cookies"></param>
        /// <param name="turnTableId"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public async Task<DoSignChannelDto> DoSignChannelAsync(string cookies, string turnTableId, string title)
        {
            var url = $"https://jdjoy.jd.com/api/turncard/channel/sign?invokeKey=yPsq1PHN";
            var requestString = $"turnTableId={turnTableId}";

            var dicHeaders = JDHelper.GetDefaultDicHeaders(cookies);

            var httpResponse = await _httpHelper.PostAndGetRespAsync(url, requestString, dicHeaders);
            _logger.LogDebug($"{title}（频道）签到：{httpResponse.Result}");
            var res = JsonConvert.DeserializeObject<DoSignChannelDto>(httpResponse.Result);

            return res;
        }

        /// <summary>
        /// 京东游戏-天天加速
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public async Task<DoGameBySpeedUpDto> DoGameBySpeedUpAsync(string cookies, int id = 0)
        {
            var functionId = id > 0 ? $"start&body=%7B%22source%22%3A%22game%22%2C%22source_id%22%3A{id}%7D" : $"state&body=%7B%22source%22%3A%22game%22%7D";
            var url = $"https://api.m.jd.com/?appid=memberTaskCenter&functionId=flyTask_{functionId}";

            var dicHeaders = JDHelper.GetDefaultDicHeaders(cookies);
            dicHeaders.Add("Referer", $"https://h5.m.jd.com/babelDiy/Zeus/6yCQo2eDJPbyPXrC3eMCtMWZ9ey/index.html");

            var httpResponse = await _httpHelper.GetAndGetRespAsync(url, dicHeaders);
            _logger.LogDebug($"京东游戏-天天加速：{httpResponse.Result}");
            var res = JsonConvert.DeserializeObject<DoGameBySpeedUpDto>(httpResponse.Result);

            return res;
        }

        /// <summary>
        /// 京东游戏-天天加速（查询太空事件）
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public async Task<DoGameBySpeedUpSpaceEventDto> DoGameBySpeedUpSpaceEventAsync(string cookies)
        {
            var url = $"https://api.m.jd.com/?appid=memberTaskCenter&functionId=spaceEvent_list&body=%7B%22source%22%3A%22game%22%7D";

            var dicHeaders = JDHelper.GetDefaultDicHeaders(cookies);
            dicHeaders.Add("Referer", $"https://h5.m.jd.com/babelDiy/Zeus/6yCQo2eDJPbyPXrC3eMCtMWZ9ey/index.html");

            var httpResponse = await _httpHelper.GetAndGetRespAsync(url, dicHeaders);
            _logger.LogDebug($"京东游戏-天天加速（查询太空事件）：{httpResponse.Result}");
            var res = JsonConvert.DeserializeObject<DoGameBySpeedUpSpaceEventDto>(httpResponse.Result);

            return res;
        }

        /// <summary>
        /// 京东游戏-天天加速（处理太空事件）
        /// </summary>
        /// <param name="cookies"></param>
        /// <param name="eventId"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task DoGameBySpeedUpSpaceEventHandleAsync(string cookies, int eventId, string value)
        {
            var url = $"https://api.m.jd.com/?appid=memberTaskCenter&functionId=spaceEvent_handleEvent&body={{\"source\":\"game\",\"eventId\":{eventId},\"option\":\"{value}\"}}";

            var dicHeaders = JDHelper.GetDefaultDicHeaders(cookies);
            dicHeaders.Add("Referer", $"https://h5.m.jd.com/babelDiy/Zeus/6yCQo2eDJPbyPXrC3eMCtMWZ9ey/index.html");

            var httpResponse = await _httpHelper.GetAndGetRespAsync(url, dicHeaders);
            _logger.LogDebug($"京东游戏-天天加速（处理太空事件）：{httpResponse.Result}");
            //var res = JsonConvert.DeserializeObject<DoGameBySpeedUpSpaceEventDto>(httpResponse.Result);

            //return res;
        }

        /// <summary>
        /// 京东游戏-天天加速（查询道具）
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public async Task<DoGameBySpeedUpPropDto> DoGameBySpeedUpPropAsync(string cookies)
        {
            var url = $"https://api.m.jd.com/?appid=memberTaskCenter&functionId=energyProp_usalbeList&body={{\"source\":\"game\"}}";

            var dicHeaders = JDHelper.GetDefaultDicHeaders(cookies);
            dicHeaders.Add("Referer", $"https://h5.m.jd.com/babelDiy/Zeus/6yCQo2eDJPbyPXrC3eMCtMWZ9ey/index.html");

            var httpResponse = await _httpHelper.GetAndGetRespAsync(url, dicHeaders);
            _logger.LogDebug($"京东游戏-天天加速（查询道具）：{httpResponse.Result}");
            var res = JsonConvert.DeserializeObject<DoGameBySpeedUpPropDto>(httpResponse.Result);

            return res;
        }

        /// <summary>
        /// 京东游戏-天天加速（使用道具）
        /// </summary>
        /// <param name="cookies"></param>
        /// <param name="propID"></param>
        /// <returns></returns>
        public async Task DoGameBySpeedUpPropHandleAsync(string cookies, string propID)
        {
            var url = $"https://api.m.jd.com/?appid=memberTaskCenter&functionId=energyProp_use&body={{\"source\":\"game\",\"energy_id\":\"{propID}\"}}";

            var dicHeaders = JDHelper.GetDefaultDicHeaders(cookies);
            dicHeaders.Add("Referer", $"https://h5.m.jd.com/babelDiy/Zeus/6yCQo2eDJPbyPXrC3eMCtMWZ9ey/index.html");

            var httpResponse = await _httpHelper.GetAndGetRespAsync(url, dicHeaders);
            _logger.LogDebug($"京东游戏-天天加速（使用道具）：{httpResponse.Result}");
            //var res = JsonConvert.DeserializeObject<DoGameBySpeedUpSpaceEventDto>(httpResponse.Result);

            //return res;
        }
    }
}
