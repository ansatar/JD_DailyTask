using AutoMapper;
using JD.Application.JDTaskSummaries;
using JD.Application.JDTaskSummaries.Dto;
using JD.Core.JDTaskSummaries;
using JD.EntityFrameworkCore.EntityFrameworkCore;
using JD.EntityFrameworkCore.Repositories;
using JD.Utils;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JD.Application.JDTaskSummaries
{
    /// <summary>
    /// 京东用户自动任务汇总
    /// </summary>
    public class JDTaskSummaryAppService : BaseRepository<JDTaskSummary, long>, IJDTaskSummaryAppService
    {
        private readonly IMapper _mapper;

        public JDTaskSummaryAppService(JDDailyTaskDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(CreateJDTaskSummaryDto dto)
        {
            var model = _mapper.Map<JDTaskSummary>(dto);
            model.CreationTime = DateTime.Now;

            return await InsertAsync(model);
        }

        /// <summary>
        /// GetListAsync
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="ordering"></param>
        /// <param name="isNoTracking"></param>
        /// <returns></returns>
        public async new Task<List<JDTaskSummaryDto>> GetListAsync(Expression<Func<JDTaskSummary, bool>> predicate = null, string ordering = "", bool isNoTracking = true)
        {
            var list = await base.GetListAsync(predicate, ordering, isNoTracking);

            return _mapper.Map<List<JDTaskSummaryDto>>(list);
        }
    }
}
