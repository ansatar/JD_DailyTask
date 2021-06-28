using System;
using System.Collections.Generic;
using System.Text;

namespace JD.Application.UserVerif
{
    public class LoginQrcodeUrlDto
    {
        public int checklogin { get; set; }
        public int errcode { get; set; }
        public string message { get; set; }
        public int need_poll { get; set; }
        public string onekeylog_switch { get; set; }
        public string onekeylog_url { get; set; }
        public int ou_state { get; set; }
        public string token { get; set; }


        public ExtFieldModel ExtFields { get; set; }

        public class ExtFieldModel
        {
            public string okl_token { get; set; }
            public string QrcodeUrl { get; set; }
        }
    }
}
