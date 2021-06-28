using JD.Application.JDUserTaskRecords;
using JD.Application.Users;
using JD_DailyTask.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace JD_DailyTask.Controllers
{
    /// <summary>
    /// 京东用户相关信息
    /// </summary>
    public class UserController : JDControllerBase
    {
        readonly IUserAppService _userAppService;
        readonly IJDUserTaskRecordAppService _jdUserTaskRecordAppService;

        public UserController(IUserAppService userAppService, IJDUserTaskRecordAppService jdUserTaskRecordAppService)
        {
            _userAppService = userAppService;
            _jdUserTaskRecordAppService = jdUserTaskRecordAppService;
        }

        /// <summary>
        /// 京东用户基本信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResultModel> QueryJDUserInfoAsync([Required] string cookies)
        {
            var res = await _userAppService.QueryJDUserInfoAsync(cookies);

            return ApiResultModel.IsOk(res);
        }

        /// <summary>
        /// 新增或更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResultModel> CreateOrUpdateUserAsync(CreateOrUpdateUserDto input)
        {
            var res = await _userAppService.CreateOrUpdateUserAsync(input);

            return ApiResultModel.IsOk(res);
        }

        /// <summary>
        /// 获取京东用户自动任务记录
        /// </summary>
        /// <param name="pt_pin"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResultModel> GetUserTaskRecords(string pt_pin, int pageIndex, int pageSize = 10)
        {
            //用户信息
            var user = await _userAppService.GetByCurPinAsync(pt_pin);
            if (user == null) return ApiResultModel.IsError($"未能找到 pt_pin={pt_pin} 的用户");

            //任务记录
            var records = await _jdUserTaskRecordAppService.GetListAsync(user.Id, pageIndex, pageSize);

            return ApiResultModel.IsOk(records);
        }
    }
}
