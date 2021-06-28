using JD.Application.UserVerif;
using JD.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace JD.Application.UserVerif
{
    public class UserVerifAppService : IUserVerifAppService
    {
        private readonly ILogger<UserVerifAppService> _logger;
        private readonly HttpHelper _httpHelper;
        private readonly string Appid = "10086";

        public UserVerifAppService(IHttpClientFactory httpClientFactory, ILogger<UserVerifAppService> logger)
        {
            _logger = logger;
            _httpHelper = new HttpHelper(httpClientFactory);
        }

        /// <summary>
        /// 获取相关csrf_token
        /// </summary>
        /// <returns></returns>
        private async Task<CSRFTokenDto> GetCSRFTokenAsync()
        {
            var timeMillis = DateHelper.currentTimeMillis();

            var url = $"https://plogin.m.jd.com/cgi-bin/mm/new_login_entrance?lang=chs&appid={Appid}&returnurl=https://wq.jd.com/passport/LoginRedirect?state={timeMillis}&returnurl=https://home.m.jd.com/myJd/newhome.action?sceneval=2&ufc=&/myJd/home.action&source=wq_passport";

            var dicHeaders = JDHelper.GetDefaultDicHeaders(string.Empty);
            dicHeaders.Add("Host", "plogin.m.jd.com");
            dicHeaders.Add("Referer", $"https://plogin.m.jd.com/login/login?appid={Appid}&returnurl=https://wq.jd.com/passport/LoginRedirect?state={timeMillis}&returnurl=https://home.m.jd.com/myJd/newhome.action?sceneval=2&ufc=&/myJd/home.action&source=wq_passport");

            var httpResponse = await _httpHelper.GetAndGetRespAsync(url, dicHeaders);
            _logger.LogDebug($"获取相关csrf_token：{httpResponse.Result}");
            var res = JsonConvert.DeserializeObject<CSRFTokenDto>(httpResponse.Result);

            //设置cookies的值，用于后面的 tmauthchecktoken 接口
            var set_cookies = httpResponse.httpResponseHeaders.GetValues("Set-Cookie");
            var values = System.Web.HttpUtility.ParseQueryString(string.Join(";", set_cookies).Replace("; ", "&").Replace(";", "&"));
            var guid = values.GetValues("guid")[0];
            var lsid = values.GetValues("lsid")[0];
            var lstoken = values.GetValues("lstoken")[0];
            res.ExtFields = new CSRFTokenDto.ExtFieldModel()
            {
                cookies = $"guid={guid}; lang=chs; lsid={lsid}; lstoken={lstoken};"
            };


            return res;
        }

        /// <summary>
        /// 获取登录二维码url
        /// </summary>
        /// <param name="csrfTokenDto"></param>
        /// <returns></returns>
        private async Task<LoginQrcodeUrlDto> GetLoginQrcodeUrlAsync(CSRFTokenDto csrfTokenDto)
        {
            var timeMillis = DateHelper.currentTimeMillis();

            var url = $"https://plogin.m.jd.com/cgi-bin/m/tmauthreflogurl?s_token={csrfTokenDto.s_token}&v={timeMillis}&remember=true";
            var requestString = $"lang=chs&appid={Appid}&source=wq_passport&returnurl=https://wqlogin2.jd.com/passport/LoginRedirect?state={timeMillis}&returnurl=//home.m.jd.com/myJd/newhome.action?sceneval=2&ufc=&/myJd/home.action";

            var dicHeaders = JDHelper.GetDefaultDicHeaders(string.Empty);
            dicHeaders.Add("Host", "plogin.m.jd.com");
            dicHeaders.Add("Referer", $"https://plogin.m.jd.com/login/login?appid={Appid}&returnurl=https://wq.jd.com/passport/LoginRedirect?state={timeMillis}&returnurl=https://home.m.jd.com/myJd/newhome.action?sceneval=2&ufc=&/myJd/home.action&source=wq_passport");

            var httpResponse = await _httpHelper.PostAndGetRespAsync(url, requestString, dicHeaders);
            _logger.LogDebug($"获取登录二维码url：{httpResponse.Result}");
            var res = JsonConvert.DeserializeObject<LoginQrcodeUrlDto>(httpResponse.Result);

            var set_cookies = httpResponse.httpResponseHeaders.GetValues("Set-Cookie");
            var values = System.Web.HttpUtility.ParseQueryString(string.Join(";", set_cookies).Replace("; ", "&").Replace(";", "&"));
            var okl_token = values.GetValues("okl_token")[0];

            res.ExtFields = new LoginQrcodeUrlDto.ExtFieldModel()
            {
                okl_token = okl_token,
                QrcodeUrl = $"https://plogin.m.jd.com/cgi-bin/m/tmauth?appid={Appid}&client_type=m&token={res.token}"
            };

            return res;
        }

        /// <summary>
        /// 获取相关的登录参数
        /// </summary>
        /// <returns></returns>
        public async Task<GetLoginParamsDto> GetLoginParamsAsync()
        {
            var csrfTokenModel = await GetCSRFTokenAsync();
            if (csrfTokenModel.err_code != 0) throw new Exception(csrfTokenModel.err_msg);

            var loginQrcodeUrlModel = await GetLoginQrcodeUrlAsync(csrfTokenModel);
            if (loginQrcodeUrlModel.errcode != 0) throw new Exception(loginQrcodeUrlModel.message);

            return new GetLoginParamsDto()
            {
                QrcodeUrl = loginQrcodeUrlModel.ExtFields.QrcodeUrl,
                Token = loginQrcodeUrlModel.token,
                Okl_Token = loginQrcodeUrlModel.ExtFields.okl_token,
                Cookies = csrfTokenModel.ExtFields.cookies
            };
        }

        /// <summary>
        /// 检查是否登录成功，并从返回值中获取cookie
        /// 注意：扫码成功后，当前接口只能调用一次。第二次后会返回："errcode": 21,"message": "Token不存在，请退出重试"
        /// </summary>
        /// <param name="clientFactory"></param>
        /// <param name="token"></param>
        /// <param name="okl_token"></param>
        /// <returns></returns>
        public async Task<CheckLoginDto> CheckLoginAsync(CheckLoginInputDto inputDto)
        {
            var timeMillis = DateHelper.currentTimeMillis();

            var url = $"https://plogin.m.jd.com/cgi-bin/m/tmauthchecktoken?&token={inputDto.token}&ou_state=0&okl_token={inputDto.okl_token}";
            var requestString = $"lang=chs&appid={Appid}&source=wq_passport&returnurl=https://wqlogin2.jd.com/passport/LoginRedirect?state={timeMillis}&returnurl=//home.m.jd.com/myJd/newhome.action?sceneval=2&ufc=&/myJd/home.action";

            var dicHeaders = JDHelper.GetDefaultDicHeaders(string.Empty);
            dicHeaders.Add("Host", "plogin.m.jd.com");
            dicHeaders.Add("Referer", $"https://plogin.m.jd.com/login/login?appid={Appid}&returnurl=https://wqlogin2.jd.com/passport/LoginRedirect?state={timeMillis}&returnurl=//home.m.jd.com/myJd/newhome.action?sceneval=2&ufc=&/myJd/home.action&source=wq_passport");
            dicHeaders.Add("Cookie", inputDto.cookies);

            var httpResponse = await _httpHelper.PostAndGetRespAsync(url, requestString, dicHeaders);
            _logger.LogDebug($"检查是否登录成功，并从返回值中获取cookie：{httpResponse.Result}");
            var res = JsonConvert.DeserializeObject<CheckLoginDto>(httpResponse.Result);

            switch (res.errcode)
            {
                case 0:
                    //设置cookies的值，用于后面的 签到 等接口
                    var set_cookies = httpResponse.httpResponseHeaders.GetValues("Set-Cookie");
                    var values = System.Web.HttpUtility.ParseQueryString(string.Join(";", set_cookies).Replace("; ", "&").Replace(";", "&"));
                    var pt_key = values.GetValues("pt_key")[0];
                    var pt_pin = values.GetValues("pt_pin")[0];
                    res.cookie = $"pt_key={pt_key};pt_pin={pt_pin};";

                    //获取cookies过期时间
                    var temps = httpResponse.httpResponseHeaders.GetValues("strict-transport-security");
                    var temps_values = System.Web.HttpUtility.ParseQueryString(string.Join(";", temps).Replace("; ", "&").Replace(";", "&"));
                    var max_age = temps_values.GetValues("max-age")[0];
                    res.expires = Convert.ToInt32(max_age);
                    break;
                //授权登录未确认
                case 176:
                    break;
                //Token不存在，请退出重试（当前接口只能调用一次。第二次后会返回："errcode": 21,"message": "Token不存在，请退出重试"）
                case 21:
                    break;
                //其他情况
                default:
                    _logger.LogError($"检查是否登录成功时异常：{httpResponse.Result}");
                    break;
            }

            return res;
        }
    }
}
