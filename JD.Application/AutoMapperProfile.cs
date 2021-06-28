using AutoMapper;
using JD.Application.JDTaskSummaries;
using JD.Application.JDTaskSummaries.Dto;
using JD.Application.JDUserTaskRecords;
using JD.Application.JDUserTaskRecords.Dto;
using JD.Application.Users;
using JD.Core.JDTaskSummaries;
using JD.Core.JDUsers;
using JD.Core.JDUserTaskRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Application
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //js_user
            CreateMap<UserDto, JDUser>();
            CreateMap<CreateUserDto, JDUser>();
            CreateMap<UpdateUserDto, JDUser>();
            CreateMap<JDUser, UserDto>();
            CreateMap<CreateOrUpdateUserDto, JDUser>();

            //jd_task_summary
            CreateMap<CreateJDTaskSummaryDto, JDTaskSummary>();

            //jd_user_taskrecord
            CreateMap<CreateJDUserTaskRecordDto, JDUserTaskRecord>();
            CreateMap<JDUserTaskRecord, JDUserTaskRecordDto>();

            //jd_task_summary
            CreateMap<JDTaskSummary, JDTaskSummaryDto>();


            //...
        }
    }
}
