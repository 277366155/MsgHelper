using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MH.Models.DBModel
{
    /// <summary>
    /// 用户信息表
    /// </summary>
    public class User : ModelBase
    {
        public User()
        {
            ArticlesList = new HashSet<Articles>();
            PollsList = new HashSet<Polls>();
            ReviewsList = new HashSet<Reviews>();
            PollDetailsList = new HashSet<PollDetails>();
        }
        /// <summary>
        /// 关联wxUser.openid
        /// </summary>
        [MaxLength(128)]
        public string Openid { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        [MaxLength(256)]
        public string Pwd { get; set; }
        /// <summary>
        /// 站点中的昵称，与wx昵称区分开
        /// </summary>
        [MaxLength(32)]
        public string CustomNickName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [MaxLength(32)]
        public string RealName { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        [MaxLength(32)]
        public string IDCardNo { get; set; }

        /// <summary>
        /// 登陆邮箱
        /// </summary>
        [MaxLength(64)]
        public string Email { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [MaxLength(32)]
        public string PhoneNumber{get;set;}

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }

        public virtual ICollection<Articles> ArticlesList { get; set; }

        public virtual ICollection<Polls> PollsList { get; set; }

        public virtual ICollection<Reviews> ReviewsList { get; set; }

        public virtual ICollection<PollDetails> PollDetailsList { get; set; }
    }
}
