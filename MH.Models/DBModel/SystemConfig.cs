using System.ComponentModel.DataAnnotations;

namespace MH.Models.DBModel
{
    public class SystemConfig:ModelBase
    {
        /// <summary>
        /// 配置项key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 配置项值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 配置项说明
        /// </summary>
        [MaxLength(256)]
        public string Remark { get; set; }
    }
}
