using AutoMapper;
using JD.Application.JDUserTaskRecords.Dto;
using JD.Core.JDUserTaskRecords;
using JD.EntityFrameworkCore.EntityFrameworkCore;
using JD.EntityFrameworkCore.Repositories;
using JD.Utils;
using JD.Utils.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JD.Application.JDUserTaskRecords
{
    /// <summary>
    /// 京东用户自动任务记录
    /// </summary>
    public class JDUserTaskRecordAppService : BaseRepository<JDUserTaskRecord, long>, IJDUserTaskRecordAppService
    {
        private readonly ILogger<JDUserTaskRecordAppService> _logger;
        private readonly HttpHelper _httpHelper;
        private readonly IMapper _mapper;

        public JDUserTaskRecordAppService(JDDailyTaskDbContext dbContext, IHttpClientFactory httpClientFactory, ILogger<JDUserTaskRecordAppService> logger, IMapper mapper) : base(dbContext)
        {
            _logger = logger;
            _httpHelper = new HttpHelper(httpClientFactory);
            _mapper = mapper;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(CreateJDUserTaskRecordDto dto)
        {
            var model = _mapper.Map<JDUserTaskRecord>(dto);
            model.CreationTime = DateTime.Now;

            return await InsertAsync(model);
        }

        /// <summary>
        /// GetListAsync
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PageData<JDUserTaskRecordDto>> GetListAsync(long uid, int pageIndex, int pageSize = 10)
        {
            var listEntity = await base.GetPageAsync(w => w.Uid == uid, o => o.CreationTime, pageIndex, pageSize, false);

            return new PageData<JDUserTaskRecordDto>() { Rows = _mapper.Map<List<JDUserTaskRecordDto>>(listEntity.Rows), Totals = listEntity.Totals };
        }
    }
}
