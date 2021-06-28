using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Application.Users
{
    public class CreateOrUpdateUserDto
    {
        /// <summary>
        /// CurPin
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string CurPin { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [MaxLength(250)]
        public string HeadImageUrl { get; set; }
        /// <summary>
        /// 京豆
        /// </summary>
        [Required]
        public int JdNum { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        [MaxLength(20)]
        public string LevelName { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        [MaxLength(20)]
        public string Mobile { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [MaxLength(20)]
        public string Nickname { get; set; }
        /// <summary>
        /// Cookie
        /// </summary>
        [MaxLength(500)]
        public string Cookie { get; set; }
        /// <summary>
        /// 最近一次登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }
        /// <summary>
        /// 是否开启自动任务
        /// </summary>
        public bool IsEnabledAutoTask { get; set; }
    }
}
