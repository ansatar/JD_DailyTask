using System;
using System.Collections.Generic;
using System.Text;

namespace JD.Application.UserVerif
{
    public class CSRFTokenDto
    {
        public bool country_code_switch { get; set; }
        public bool enable_smslogin { get; set; }
        public bool enable_usernamelogin { get; set; }
        public int err_code { get; set; }
        public string err_msg { get; set; }
        public string jcap_domain { get; set; }
        public bool kepler { get; set; }
        public bool kpkey_switch { get; set; }
        public string logoUrl { get; set; }
        public bool need_auth { get; set; }
        public bool onekeylog_switch { get; set; }
        public bool only_login { get; set; }
        public bool qblog_switch { get; set; }
        public string rsa_modulus { get; set; }
        public string s_token { get; set; }
        public bool show_applelogin { get; set; }
        public bool show_jdpaylogin { get; set; }
        public bool show_otherlogin { get; set; }
        public bool show_title { get; set; }
        public bool show_wxlogin { get; set; }
        public string tpl { get; set; }
        public string unmodified_name { get; set; }


        public ExtFieldModel ExtFields { get; set; }

        public class ExtFieldModel
        {
            /// <summary>
            /// 用于后面的 tmauthchecktoken 接口
            /// </summary>
            public string cookies { get; set; }
        }
    }
}
