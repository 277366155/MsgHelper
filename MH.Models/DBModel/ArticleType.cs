using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MH.Models.DBModel
{
    /// <summary>
    /// 文章类别
    /// </summary>
    public class ArticleType : ModelBase
    {
        public ArticleType()
        {
            ArticlesList = new HashSet<Articles>();
        }
        /// <summary>
        /// 关联userinfo中的id
        /// </summary>        
        public int? CreatorId { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        [MaxLength(16)]
        public string TypeName { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        [MaxLength(256)]
        public string Remarks { get; set; }

        [ForeignKey("CreatorId")]
        public virtual User Creator { get; set; }

        public virtual ICollection<Articles> ArticlesList { get; set; }
    }
}
