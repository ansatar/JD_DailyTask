using System;
using System.Collections.Generic;
using System.Text;

namespace JD.Application.UserVerif
{
    public class CheckLoginDto
    {
        public int errcode { get; set; }
        public string message { get; set; }

        /// <summary>
        /// 扫码登录成功后返回
        /// </summary>
        public string cookie { get; set; }

        /// <summary>
        /// 过期时间（秒）
        /// </summary>
        public int expires { get; set; }
    }
}
