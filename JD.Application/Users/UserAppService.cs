using JD.Application.Users;
using JD.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using JD.EntityFrameworkCore.Repositories;
using JD.Core.JDUsers;
using JD.EntityFrameworkCore.EntityFrameworkCore;
using AutoMapper;
using System.Linq.Expressions;

namespace JD.Application.Users
{
    public class UserAppService : BaseRepository<JDUser, long>, IUserAppService
    {
        private readonly ILogger<UserAppService> _logger;
        private readonly HttpHelper _httpHelper;
        private readonly IMapper _mapper;

        public UserAppService(JDDailyTaskDbContext dbContext, IHttpClientFactory httpClientFactory, ILogger<UserAppService> logger, IMapper mapper) : base(dbContext)
        {
            _logger = logger;
            _httpHelper = new HttpHelper(httpClientFactory);
            _mapper = mapper;
        }

        /// <summary>
        /// 查询JD用户基本信息
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public async Task<QueryJDUserInfoDto> QueryJDUserInfoAsync(string cookies)
        {
            var url = $"https://wq.jd.com/user/info/QueryJDUserInfo?sceneval=2";
            var requestString = string.Empty;

            Dictionary<string, string> dicHeaders = JDHelper.GetDefaultDicHeaders(cookies);
            dicHeaders.Add("Referer", $"https://wqs.jd.com/my/jingdou/my.shtml?sceneval=2");

            var httpResponse = await _httpHelper.PostAndGetRespAsync(url, requestString, dicHeaders);
            _logger.LogDebug($"查询JD用户基本信息：{httpResponse.Result}");
            var res = JsonConvert.DeserializeObject<QueryJDUserInfoDto>(httpResponse.Result);

            return res;
        }

        ///// <summary>
        ///// 新增
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //public async Task<bool> CreateAsync(CreateUserDto input)
        //{
        //    var model = _mapper.Map<JDUser>(input);
        //    model.CreationTime = DateTime.Now;

        //    return await InsertAsync(model);
        //}

        ///// <summary>
        ///// 更新
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //public async Task<UserDto> UpdateAsync(UpdateUserDto input)
        //{
        //    var entity = await GetAsync(input.Id);
        //    if (entity == null) throw new Exception($"未能找到 id={input.Id} 的数据");

        //    _mapper.Map(input, entity);
        //    entity.LastModificationTime = DateTime.Now;

        //    await UpdateAsync(entity);

        //    return _mapper.Map<UserDto>(entity);
        //}

        /// <summary>
        /// 新增或更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<UserDto> CreateOrUpdateUserAsync(CreateOrUpdateUserDto input)
        {
            var entity = await GetAsync(w => w.CurPin == input.CurPin);
            //if (entity == null) throw new Exception($"未能找到 id={input.Id} 的数据");
            if (entity == null)
            {
                entity = _mapper.Map<JDUser>(input);
                entity.CreationTime = DateTime.Now;

                await InsertAsync(entity);
            }
            else
            {
                _mapper.Map(input, entity);
                entity.LastModificationTime = DateTime.Now;

                await UpdateAsync(entity);
            }

            return _mapper.Map<UserDto>(entity);
        }

        /// <summary>
        /// GetAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async new Task<UserDto> GetAsync(long id)
        {
            var entity = await base.GetAsync(id);

            return _mapper.Map<UserDto>(entity);
        }

        /// <summary>
        /// GetAsync
        /// </summary>
        /// <param name="curPin"></param>
        /// <returns></returns>
        public async Task<UserDto> GetByCurPinAsync(string curPin)
        {            
            var entity = await base.GetAsync(w => w.CurPin == curPin);

            return _mapper.Map<UserDto>(entity);
        }

        /// <summary>
        /// GetListAsync
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="ordering"></param>
        /// <param name="isNoTracking"></param>
        /// <returns></returns>
        public async new Task<List<UserDto>> GetListAsync(Expression<Func<JDUser, bool>> predicate = null, string ordering = "", bool isNoTracking = true)
        {
            var list = await base.GetListAsync(predicate, ordering, isNoTracking);

            return _mapper.Map<List<UserDto>>(list);
        }

    }
}
