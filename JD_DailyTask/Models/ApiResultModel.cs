using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JD_DailyTask.Models
{
    public class ApiResultModel
    {
        /// <summary>
        /// 返回结果编码：小于0-失败，大于0-成功
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 返回结果内容
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 返回结果 成功：返回T类型数据 失败：默认null
        /// </summary>
        public object Data { get; set; }

        public static ApiResultModel IsOk()
        {
            return IsOk(null);
        }

        public static ApiResultModel IsOk(object t)
        {
            return Instance(0, string.Empty, t);
        }

        public static ApiResultModel IsError(string message)
        {
            return IsError(-1, message);
        }

        public static ApiResultModel IsError(int code, string message)
        {
            return Instance(code, message, null);
        }

        public static ApiResultModel Instance(int code, string message, object t)
        {
            var model = new ApiResultModel();

            model.Code = code;
            model.Message = message;
            model.Data = t;

            return model;
        }
    }
}
