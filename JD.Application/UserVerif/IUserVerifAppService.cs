using JD.Application.UserVerif;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JD.Application.UserVerif
{
    public interface IUserVerifAppService
    {
        ///// <summary>
        ///// 获取相关csrf_token
        ///// </summary>
        ///// <param name="clientFactory"></param>
        ///// <returns></returns>
        //Task<CSRFTokenDto> GetCSRFToken();


        ///// <summary>
        ///// 获取登录二维码url
        ///// </summary>
        ///// <param name="csrfTokenDto"></param>
        ///// <returns></returns>
        //Task<LoginQrcodeUrlDto> GetLoginQrcodeUrl(CSRFTokenDto csrfTokenDto);

        /// <summary>
        /// 获取相关的登录参数
        /// </summary>
        /// <returns></returns>
        Task<GetLoginParamsDto> GetLoginParamsAsync();

        /// <summary>
        /// 检查是否登录成功，并从返回值中获取cookie
        /// 注意：扫码成功后，当前接口只能调用一次。第二次后会返回："errcode": 21,"message": "Token不存在，请退出重试"
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        Task<CheckLoginDto> CheckLoginAsync(CheckLoginInputDto inputDto);
    }
}
