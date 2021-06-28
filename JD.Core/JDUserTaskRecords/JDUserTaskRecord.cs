using JD.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Core.JDUserTaskRecords
{
    /// <summary>
    /// 京东用户自动任务记录表
    /// </summary>
    [Table("jd_user_taskrecord")]
    public class JDUserTaskRecord : CreationTimeEntity
    {
        /// <summary>
        /// 执行任务的批次id
        /// </summary>
        [Required]
        public Guid TaskBatchId { get; set; }
        /// <summary>
        /// 任务执行时间
        /// </summary>
        [Required]
        public DateTime TaskTime { get; set; }
        /// <summary>
        /// 任务执行时间（秒）
        /// </summary>
        [Required]
        public int TaskDuration { get; set; }
        /// <summary>
        /// jd_user表id
        /// </summary>
        [Required]
        public long Uid { get; set; }
        /// <summary>
        /// 京豆
        /// </summary>
        public int? JdBeans { get; set; }
        /// <summary>
        /// 钢镚
        /// </summary>
        [DecimalPrecision(8, 2)]
        public decimal? JdSteels { get; set; }
        /// <summary>
        /// 金贴
        /// </summary>
        [DecimalPrecision(8, 2)]
        public decimal? JdGolds { get; set; }
        /// <summary>
        /// 现金红包
        /// </summary>
        [DecimalPrecision(8, 2)]
        public decimal? JdCashes { get; set; }
    }
}
