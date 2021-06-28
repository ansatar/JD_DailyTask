using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace JD.Application
{
    public static class JDHelper
    {
        /// <summary>
        /// 获取默认的请求头信息
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDefaultDicHeaders(string cookies)
        {
            if (!string.IsNullOrEmpty(cookies))
            {
                //正则获取pt_pin的值（并进行UrlEncode编码）
                Regex MyRegex = new Regex(
                                    "pt_pin=(?<value>.*?);",
                                  RegexOptions.IgnoreCase
                                  | RegexOptions.CultureInvariant
                                  | RegexOptions.IgnorePatternWhitespace
                                  | RegexOptions.Compiled
                                  );
                Match m = MyRegex.Match(cookies);
                string pt_pin = m.Groups.GetValueOrDefault("value").Value.Trim();

                cookies = cookies.Replace($"pt_pin={pt_pin}", $"pt_pin={HttpUtility.UrlEncode(pt_pin)}");
            }

            var dicHeaders = new Dictionary<string, string>();
            dicHeaders.Add("Connection", "Keep-Alive");
            dicHeaders.Add("Content-Type", "application/x-www-form-urlencoded");
            dicHeaders.Add("Accept", "application/json, text/plain, */*");
            dicHeaders.Add("Accept-Language", "zh-cn");
            dicHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36");
            if (!string.IsNullOrEmpty(cookies)) dicHeaders.Add("Cookie", cookies);
            return dicHeaders;
        }
    }
}
