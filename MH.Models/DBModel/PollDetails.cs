using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    }

    public enum ClientTypeEnum
    {
        Mobile=0,
        PC=1
    }
}
