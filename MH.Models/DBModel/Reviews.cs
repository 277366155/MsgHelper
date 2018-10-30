using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MH.Models.DBModel
{
    /// <summary>
    /// 评论留言表
    /// </summary>
    public class Reviews:ModelBase
    {
        /// <summary>
        /// 留言的对象id【文章id或投票id】
        /// </summary>
        public int ObjId { get; set; }

        /// <summary>
        /// 回复类别
        /// </summary>
        public ReviewTypeEnum ReviewType { get; set; }

        /// <summary>
        /// 回复内容
        /// </summary>
        [MaxLength(512)]
        public string Content { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId{get;set;}

        /// <summary>
        /// 回复留言者
        /// </summary>
        public int ReUserId { get; set; }

        [ForeignKey("UserId")]
        public virtual UserInfo UserInfo { get; set; }

        [ForeignKey("ObjId")]
        public virtual Articles Article { get; set; }

        [ForeignKey("ObjId")]
        public virtual Polls Poll { get; set; }
    }

    public enum ReviewTypeEnum
    {
        /// <summary>
        /// 文章类型
        /// </summary>
        Article=0,

        /// <summary>
        /// 投票类型
        /// </summary>
        Vote=1,
    }
}
