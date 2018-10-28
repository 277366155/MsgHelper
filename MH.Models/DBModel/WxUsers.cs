using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MH.Models.DBModel
{
    /// <summary>
    /// wx用户信息
    /// </summary>
    public class WxUsers : ModelBase
    {
        [MaxLength(128)]
        public string Openid { get; set; }

        [MaxLength(64)]
        public string NickName { get; set; }

        public int Sex { get; set; }

        [MaxLength(500)]
        public string HeadImgUrl { get; set; }

        [MaxLength(64)]
        public string Language { get; set; }

        [MaxLength(64)]
        public string City { get; set; }

        [MaxLength(64)]
        public string Prvince { get; set; }

        [MaxLength(64)]
        public string Country { get; set; }
    }
}
