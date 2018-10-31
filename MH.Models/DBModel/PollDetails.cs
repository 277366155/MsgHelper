using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MH.Models.DBModel
{
    /// <summary>
    /// 投票明细
    /// </summary>
  public   class PollDetails:ModelBase
    {

        /// <summary>
        /// 调查投票id
        /// </summary>
        public int PollId { get; set; }

        /// <summary>
        /// 投票选项id
        /// </summary>
        public int PollOptionId { get; set; }

        /// <summary>
        /// 投票人id
        /// </summary>
        public int VoterId { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        [MaxLength(128)]
        public string IpAddress { get; set; }

        /// <summary>
        /// 客户端类型
        /// </summary>
        public ClientTypeEnum ClientType { get; set; }

        [ForeignKey("PollId")]
        public virtual Polls Poll { get; set; }

        [ForeignKey("PollOptionId")]
        public virtual PollOptions PollOption { get; set; }

        [ForeignKey("VoterId")]
        public virtual User Voter { get; set; }
    }

    public enum ClientTypeEnum
    {
        Mobile=0,
        PC=1
    }
}
