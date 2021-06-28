using JD.Utils.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace JD.Utils
{
    /// <summary>
    /// HttpClientFactory 相关方法封装
    /// </summary>
    public class HttpHelper
    {

        private IHttpClientFactory _httpClientFactory;
        public HttpHelper(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="dicHeaders"></param>
        /// <param name="timeoutSecond"></param>
        /// <returns></returns>
        //public async Task<string> GetAsync(string url, int timeoutSecond = 120)
        //{
        //    var client = _httpClientFactory.CreateClient();
        //    #region 最好不要这样绑定header，
        //    //DefaultRequestHeaders是和httpClient绑定的，当完成当前请求后，其它请求从factory中获取时，还是会有绑定的header的
        //    //会造成错误
        //    //if (dicHeaders != null)
        //    //{
        //    //    foreach (var header in dicHeaders)
        //    //    {
        //    //        client.DefaultRequestHeaders.Add(header.Key, header.Value);
        //    //    }
        //    //}
        //    #endregion
        //    client.Timeout = TimeSpan.FromSeconds(timeoutSecond);
        //    var response = await client.GetAsync(url);
        //    var result = await response.Content.ReadAsStringAsync();
        //    return result;
        //}

        /// <summary>
        /// Get异步请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="dicHeaders"></param>
        /// <param name="timeoutSecond"></param>
        /// <returns></returns>
        public async Task<string> GetAsync(string url, Dictionary<string, string> dicHeaders, int timeoutSecond = 120)
        {
            var httpWebClientResponse = await GetAndGetRespAsync(url, dicHeaders, timeoutSecond);
            return httpWebClientResponse.Result;
        }

        /// <summary>
        /// Get异步请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="dicHeaders"></param>
        /// <param name="timeoutSecond"></param>
        /// <returns></returns>
        public async Task<HttpWebClientResponse> GetAndGetRespAsync(string url, Dictionary<string, string> dicHeaders, int timeoutSecond = 120)
        {
            return await GetMethondAsync(url, dicHeaders, timeoutSecond);
        }

        private async Task<HttpWebClientResponse> GetMethondAsync(string url, Dictionary<string, string> dicHeaders, int timeoutSecond)
        {
            var client = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Content = new StringContent(string.Empty);
            if (dicHeaders != null)
            {
                foreach (var header in dicHeaders)
                {
                    if (header.Key == "Content-Type")
                        request.Content.Headers.ContentType = new MediaTypeHeaderValue(header.Value);
                    else
                        request.Headers.Add(header.Key, header.Value);
                }
            }
            client.Timeout = TimeSpan.FromSeconds(timeoutSecond);
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return new HttpWebClientResponse()
                {
                    Result = result,
                    httpResponseHeaders = response.Headers
                };
            }
            else
            {
                throw new Exception($"接口请求错误,错误代码{response.StatusCode}，错误原因{response.ReasonPhrase}");

            }
        }

        /// <summary>
        /// Post异步请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="requestString"></param>
        /// <param name="dicHeaders"></param>
        /// <param name="timeoutSecond"></param>
        /// <returns></returns>
        public async Task<string> PostAsync(string url, string requestString, Dictionary<string, string> dicHeaders, int timeoutSecond = 120)
        {
            var httpWebClientResponse = await PostAndGetRespAsync(url, requestString, dicHeaders, timeoutSecond);
            return httpWebClientResponse.Result;
        }

        /// <summary>
        /// Post异步请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="requestString"></param>
        /// <param name="dicHeaders"></param>
        /// <param name="timeoutSecond"></param>
        /// <returns></returns>
        public async Task<HttpWebClientResponse> PostAndGetRespAsync(string url, string requestString, Dictionary<string, string> dicHeaders, int timeoutSecond = 120)
        {
            return await PostMethondAsync(url, requestString, dicHeaders, timeoutSecond);
        }

        private async Task<HttpWebClientResponse> PostMethondAsync(string url, string requestString, Dictionary<string, string> dicHeaders, int timeoutSecond)
        {
            var client = _httpClientFactory.CreateClient();
            var requestContent = new StringContent(requestString);
            if (dicHeaders != null)
            {
                foreach (var head in dicHeaders)
                {
                    if (head.Key == "Content-Type")
                        requestContent.Headers.ContentType = new MediaTypeHeaderValue(head.Value);
                    else
                        client.DefaultRequestHeaders.Add(head.Key, head.Value); //requestContent.Headers.Add(head.Key, head.Value);
                }
            }
            client.Timeout = TimeSpan.FromSeconds(timeoutSecond);
            var response = await client.PostAsync(url, requestContent);
            if (response.IsSuccessStatusCode)
            {
                //var result = await response.Content.ReadAsStringAsync();
                var result = "";
                using (var sr = new StreamReader(response.Content.ReadAsStreamAsync().Result, Encoding.GetEncoding("utf-8")))
                {
                    result = sr.ReadToEnd();
                }
                return new HttpWebClientResponse()
                {
                    Result = result,
                    httpResponseHeaders = response.Headers
                };
            }
            else
            {
                throw new Exception($"接口请求错误,错误代码{response.StatusCode}，错误原因{response.ReasonPhrase}");
            }
        }

        /// <summary>
        /// post raw
        /// </summary>
        /// <param name="url"></param>
        /// <param name="requestString"></param>
        /// <param name="dicHeaders"></param>
        /// <param name="timeoutSecond"></param>
        /// <returns></returns>
        public async Task<HttpWebClientResponse> PostRawTextAsync(string url, string requestString, Dictionary<string, string> dicHeaders, int timeoutSecond = 120)
        {
            var client = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            //request.Content = new StringContent(requestString);

            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            var values = System.Web.HttpUtility.ParseQueryString(string.Join(";", requestString).Replace("; ", "&").Replace(";", "&"));
            for (int i = 0; i < values.Count; i++)
            {
                keyValues.Add(values.GetKey(i), values.GetValues(i)[0]);
            }

            request.Content = new FormUrlEncodedContent(keyValues);

            if (dicHeaders != null)
            {
                foreach (var header in dicHeaders)
                {
                    if (header.Key == "Content-Type")
                        request.Content.Headers.ContentType = new MediaTypeHeaderValue(header.Value);
                    else
                        request.Headers.Add(header.Key, header.Value);
                }
            }

            client.Timeout = TimeSpan.FromSeconds(timeoutSecond);
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return new HttpWebClientResponse()
                {
                    Result = result,
                    httpResponseHeaders = response.Headers
                };
            }
            else
            {
                throw new Exception($"接口请求错误,错误代码{response.StatusCode}，错误原因{response.ReasonPhrase}");

            }
        }

    }
}
