using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MH.Models.DBModel
{
    /// <summary>
    /// 投票表
    /// </summary>
    public class Polls: ModelBase
    {

        public Polls()
        {
            PollOptionsList = new HashSet<PollOptions>();
            PollDetailsList = new HashSet<PollDetails>();
            ReviewsList = new HashSet<Reviews>();
        }
        /// <summary>
        /// 投票标题
        /// </summary>
        [MaxLength(32)]
        public string Title { get; set; }

        /// <summary>
        /// 投票封面
        /// </summary>
        [MaxLength(256)]
        public string CoverImg { get; set; }

        /// <summary>
        /// 投票正文
        /// </summary>
        [MaxLength(512)]
        public string Content { get; set; }

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
        public int CreatorId { get; set; }

        /// <summary>
        /// 可选择选项的数量：1-单选，>1多选。。。
        /// </summary>
        public int MaxOptionsNum { get; set; }

        /// <summary>
        /// 截止日期
        /// </summary>
        public DateTime? Deadline { get; set; }

        [ForeignKey("CreatorId")]
        public virtual User Creator { get; set; }

        public virtual ICollection<PollOptions> PollOptionsList { get; set; }

        public virtual ICollection<PollDetails> PollDetailsList { get; set; }

        public virtual ICollection<Reviews> ReviewsList { get; set; }
    }
}
