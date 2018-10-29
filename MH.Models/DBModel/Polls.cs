using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MH.Models.DBModel
{
    /// <summary>
    /// 投票表
    /// </summary>
    public class Polls: ModelBase
    {
        /// <summary>
        /// 投票标题
        /// </summary>
        [MaxLength(32)]
        public string Title { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        [MaxLength(256)]
        public string Remarks { get; set; }

        /// <summary>
        /// 权限状态
        /// </summary>
        public AuthorityStatusEnum Status { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsTop { get; set; }

        /// <summary>
        /// 是否可评论
        /// </summary>
        public bool IsReview { get; set; }

        /// <summary>
        /// 创建者id
        /// </summary>
        public int CreateUserId { get; set; }

        /// <summary>
        /// 可选择选项的数量：1-单选，>1多选。。。
        /// </summary>
        public int MaxOptionsNum { get; set; }

        /// <summary>
        /// 截止日期
        /// </summary>
        public DateTime? Deadline { get; set; }
    }
}
