using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MH.Models.DBModel
{
    /// <summary>
    /// 文章
    /// </summary>
    public class Articles:ModelBase
    {
        public Articles()
        {
            ReviewsList = new HashSet<Reviews>();
        }
        /// <summary>
        /// 标题
        /// </summary>
        [MaxLength(32)]
        public string Title { get; set; }

        /// <summary>
        ///关联userinfo中的id
        /// </summary>
        public int CreatorId { get; set; }

        /// <summary>
        /// 文章类型id
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// 文章正文，富文本
        /// </summary>
        [MaxLength(2048)]
        public string Content { get; set; }

        /// <summary>
        /// 文章摘要
        /// </summary>
        [MaxLength(256)]
        public string SimpleContent { get; set; }

        /// <summary>
        /// 文章状态
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

        [ForeignKey("TypeId")]
        public virtual ArticleType ArticleType { get; set; }

        [ForeignKey("CreatorId")]
        public virtual User Creator { get; set; }

        public virtual ICollection<Reviews> ReviewsList { get; set; }
    }

    public enum AuthorityStatusEnum
    {
        /// <summary>
        /// 私密
        /// </summary>
        Private = 0,
        /// <summary>
        /// 好友可见
        /// </summary>
        Protected = 1,
        /// <summary>
        /// 公开
        /// </summary>
        Public = 2
    }
}
