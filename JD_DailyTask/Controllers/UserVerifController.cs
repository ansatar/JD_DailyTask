using JD.Application;
using JD.Application.UserVerif;
using JD_DailyTask.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace JD_DailyTask.Controllers
{
    /// <summary>
    /// 京东授权相关
    /// </summary>
    public class UserVerifController : JDControllerBase
    {
        readonly IUserVerifAppService _userVerifAppService;

        public UserVerifController(IUserVerifAppService userVerifAppService)
        {
            _userVerifAppService = userVerifAppService;
        }

        /// <summary>
        /// 获取相关的登录参数
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResultModel> GetLoginParamsAsync()
        {
            var res = await _userVerifAppService.GetLoginParamsAsync();

            return ApiResultModel.IsOk(res);
        }

        /// <summary>
        /// 检查是否登录成功，并从返回值中获取cookie
        /// 注意：扫码成功后，当前接口只能调用一次。第二次后会返回："errcode": 21,"message": "Token不存在，请退出重试"
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<object> CheckLoginAsync(CheckLoginInputDto inputDto)
        {
            var res = await _userVerifAppService.CheckLoginAsync(inputDto);

            if (res.errcode != 0) return ApiResultModel.IsError(res.message);

            return ApiResultModel.IsOk(res);
        }

    }
}
