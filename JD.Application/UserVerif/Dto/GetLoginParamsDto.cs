using System;
using System.Collections.Generic;
using System.Text;

namespace JD.Application.UserVerif
{
    public class GetLoginParamsDto
    {
        public string QrcodeUrl { get; set; }

        public string Token { get; set; }

        public string Okl_Token { get; set; }

        public string Cookies { get; set; }
    }
}
