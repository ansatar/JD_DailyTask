using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Core.JDTaskSummaries
{
    /// <summary>
    /// 京东用户自动任务汇总表
    /// </summary>
    [Table("jd_task_summary")]
    public class JDTaskSummary : CreationTimeEntity
    {
        /// <summary>
        /// 执行任务的批次id
        /// </summary>
        [Required]
        public Guid TaskBatchId { get; set; }
        /// <summary>
        /// 执行的任务清单（多个用英文逗号分隔）
        /// </summary>
        [Required]
        [MaxLength(1000)]
        public string TaskList { get; set; }
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
        /// 用户数
        /// </summary>
        [Required]
        public int Users { get; set; }
    }
}
