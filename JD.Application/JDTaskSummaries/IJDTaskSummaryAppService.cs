using JD.Application.JDTaskSummaries.Dto;
using JD.Core.JDTaskSummaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JD.Application.JDTaskSummaries
{
    public interface IJDTaskSummaryAppService
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(CreateJDTaskSummaryDto dto);

        /// <summary>
        /// GetListAsync
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="ordering"></param>
        /// <param name="isNoTracking"></param>
        /// <returns></returns>
        Task<List<JDTaskSummaryDto>> GetListAsync(Expression<Func<JDTaskSummary, bool>> predicate = null, string ordering = "", bool isNoTracking = true);
    }
}
