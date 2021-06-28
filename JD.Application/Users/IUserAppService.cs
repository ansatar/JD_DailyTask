using JD.Application.Users;
using JD.Core.JDUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JD.Application.Users
{
    public interface IUserAppService
    {
        /// <summary>
        /// 查询JD用户基本信息
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        Task<QueryJDUserInfoDto> QueryJDUserInfoAsync(string cookies);

        ///// <summary>
        ///// 新增
        ///// </summary>
        ///// <param name="dto"></param>
        ///// <returns></returns>
        //Task<bool> CreateAsync(CreateUserDto dto);

        ///// <summary>
        ///// 更新
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //Task<UserDto> UpdateAsync(UpdateUserDto input);

        /// <summary>
        /// 新增或更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<UserDto> CreateOrUpdateUserAsync(CreateOrUpdateUserDto input);

        /// <summary>
        /// GetAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserDto> GetAsync(long id);

        /// <summary>
        /// GetAsync
        /// </summary>
        /// <param name="curPin"></param>
        /// <returns></returns>
        Task<UserDto> GetByCurPinAsync(string curPin);

        /// <summary>
        /// GetListAsync
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="ordering"></param>
        /// <param name="isNoTracking"></param>
        /// <returns></returns>
        Task<List<UserDto>> GetListAsync(Expression<Func<JDUser, bool>> predicate = null, string ordering = "", bool isNoTracking = true);
    }
}
