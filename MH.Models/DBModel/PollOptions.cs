using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MH.Models.DBModel
{
    public  class PollOptions:ModelBase
    {
        public PollOptions()
        {
            PollDetailsList = new HashSet<PollDetails>();
        }
        /// <summary>
        /// 调查投票id
        /// </summary>
        public int PollId { get; set; }

        /// <summary>
        /// 选项内容
        /// </summary>
        public string OptionContent { get; set; }

        /// <summary>
        /// 排序编号
        /// </summary>
        public int OrderNo { get; set; }

        /// <summary>
        /// 投票选中次数
        /// </summary>
        public int SelectCount { get; set; }

        [ForeignKey("PollId")]
        public virtual Polls Poll { get; set; }

        public virtual ICollection<PollDetails> PollDetailsList { get; set; }
    }
}
