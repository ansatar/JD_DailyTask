using JD.Application.JDUserTaskRecords.Dto;
using JD.Utils.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Application.JDUserTaskRecords
{
    public interface IJDUserTaskRecordAppService
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(CreateJDUserTaskRecordDto dto);

        /// <summary>
        /// GetListAsync
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<PageData<JDUserTaskRecordDto>> GetListAsync(long uid, int pageIndex, int pageSize = 10);
    }
}
