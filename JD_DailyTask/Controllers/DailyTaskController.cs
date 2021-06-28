using JD.Core;
using JD.Application.DailyTasks;
using JD_DailyTask.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JD.Application.Users;
using Microsoft.Extensions.Configuration;
using JD.Application.JDUserTaskRecords;
using JD.Application.JDTaskSummaries;
using System.Text;

namespace JD_DailyTask.Controllers
{
    public class DailyTaskController : JDControllerBase
    {
        readonly IConfiguration _configuration;

        readonly IDailyTaskAppService _dailyTaskAppService;
        readonly IUserAppService _userAppService;
        readonly IJDUserTaskRecordAppService _jdUserTaskRecordAppService;
        readonly IJDTaskSummaryAppService _jdTaskSummaryAppService;

        public DailyTaskController(IConfiguration configuration, IUserAppService userAppService,
            IDailyTaskAppService dailyTaskAppService, IJDUserTaskRecordAppService jdUserTaskRecordAppService, IJDTaskSummaryAppService jdTaskSummaryAppService)
        {
            _configuration = configuration;

            _userAppService = userAppService;
            _dailyTaskAppService = dailyTaskAppService;
            _jdUserTaskRecordAppService = jdUserTaskRecordAppService;
            _jdTaskSummaryAppService = jdTaskSummaryAppService;
        }


        /// <summary>
        /// 领京豆方法
        /// </summary>
        /// <param name="doActionIntput"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResultModel> DoActionAsync(DoActionIntput doActionIntput)
        {
            var taskBatchId = Guid.Empty;
            var beginTime = DateTime.Now;

            //查找用户
            var user = await _userAppService.GetByCurPinAsync(doActionIntput.pt_pin);
            if (user == null) return ApiResultModel.IsError($"未能找到 pt_pin={doActionIntput.pt_pin} 的用户，请重新登录");

            //执行任务
            var listRes = await _dailyTaskAppService.DoAllDailyTaskAsync(user.Cookie, doActionIntput.ActionList);

            //新增：用户自动任务记录
            await _jdUserTaskRecordAppService.CreateAsync(new CreateJDUserTaskRecordDto()
            {
                TaskBatchId = taskBatchId,
                TaskTime = beginTime,
                TaskDuration = Convert.ToInt32((DateTime.Now - beginTime).TotalSeconds),
                Uid = user.Id,
                JdBeans = (int)listRes.Where(w => w.AwardType == JDAwardTypeEnum.京豆).Sum(s => s.AwardCount),
                JdSteels = listRes.Where(w => w.AwardType == JDAwardTypeEnum.钢镚).Sum(s => s.AwardCount),
                JdGolds = listRes.Where(w => w.AwardType == JDAwardTypeEnum.金贴).Sum(s => s.AwardCount),
                JdCashes = listRes.Where(w => w.AwardType == JDAwardTypeEnum.现金红包).Sum(s => s.AwardCount)
            });

            return ApiResultModel.IsOk(listRes);
        }

        /// <summary>
        /// 执行自动任务
        /// </summary>
        /// <param name="userId">用户表id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> AutoTaskAsync(long userId)
        {
            var taskBatchId = Guid.NewGuid();
            var beginTime = DateTime.Now;
            var today = Convert.ToDateTime(DateTime.Now.ToShortDateString());

            //step1: 检查今天是否已经执行自动任务
            //var listTaskSummary = await _jdTaskSummaryAppService.GetListAsync(w => w.TaskTime >= today);
            //if (listTaskSummary.Any()) return "今天已执行过自动任务";


            //step2: 查询所有需要执行自动任务的用户
            var jdUserExpiresDay = _configuration.GetSection("JdUserExpiresDay").Value;
            var expiresDay = DateTime.Now.AddDays(Convert.ToInt32(jdUserExpiresDay) * -1);
            List<UserDto> listUser;
            if (userId > 0)
                listUser = await _userAppService.GetListAsync(w => w.Id == userId && w.IsEnabledAutoTask && w.LastLoginTime >= expiresDay, "Id asc");
            else
                listUser = await _userAppService.GetListAsync(w => w.IsEnabledAutoTask && w.LastLoginTime >= expiresDay, "Id asc");


            //step3: 开始执行自动任务清单
            var taskList = _configuration.GetSection("AutoTaskList").Get<string[]>().ToList();
            foreach (var user in listUser)
            {
                var listRes = new List<CusResponseDto>();
                var taskTime = DateTime.Now;

                listRes = await _dailyTaskAppService.DoAllDailyTaskAsync(user.Cookie, taskList);

                //新增：用户自动任务记录
                await _jdUserTaskRecordAppService.CreateAsync(new CreateJDUserTaskRecordDto()
                {
                    TaskBatchId = taskBatchId,
                    TaskTime = taskTime,
                    TaskDuration = Convert.ToInt32((DateTime.Now - taskTime).TotalSeconds),
                    Uid = user.Id,
                    JdBeans = (int)listRes.Where(w => w.AwardType == JDAwardTypeEnum.京豆).Sum(s => s.AwardCount),
                    JdSteels = listRes.Where(w => w.AwardType == JDAwardTypeEnum.钢镚).Sum(s => s.AwardCount),
                    JdGolds = listRes.Where(w => w.AwardType == JDAwardTypeEnum.金贴).Sum(s => s.AwardCount),
                    JdCashes = listRes.Where(w => w.AwardType == JDAwardTypeEnum.现金红包).Sum(s => s.AwardCount)
                });
            }


            //step4: 新增：用户自动任务汇总
            await _jdTaskSummaryAppService.CreateAsync(new CreateJDTaskSummaryDto()
            {
                TaskBatchId = taskBatchId,
                TaskList = string.Join(",", taskList),
                TaskTime = beginTime,
                TaskDuration = Convert.ToInt32((DateTime.Now - beginTime).TotalSeconds),
                Users = listUser.Count
            });


            return $"执行时间：{beginTime.ToString("HH:mm:ss")} - {DateTime.Now.ToString("HH:mm:ss")}，用户数：{listUser.Count}人，任务数：{taskList.Count}个";
        }
    }
}
