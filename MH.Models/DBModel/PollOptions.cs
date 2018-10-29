namespace MH.Models.DBModel
{
    public  class PollOptions:ModelBase
    {
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
    }
}
