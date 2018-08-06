using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MH.Models.DBModel
{
    public class BaseModel
    {
        public BaseModel()
        {
            var now = DateTime.Now;
            this.CreateTime = now;
            this.ModifyTime = now;
            this.IsDel = false;
        }

        /// <summary>
        /// ID
        /// </summary>
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 最近一次修改时间
        /// <para>第一次创建时==CreateTime</para>
        /// </summary>
        public DateTime ModifyTime { get; set; }
        /// <summary>
        /// 本条记录是否被删除
        /// </summary>
        public bool IsDel { get; set; }

        /// <summary>
        /// RowVersion标识
        /// </summary> 
        [Timestamp]
        [ConcurrencyCheck]
        public DateTime? RowVersion { get;  set; }

        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            Type T = this.GetType();
            System.Reflection.PropertyInfo[] properties = T.GetProperties();
            foreach (System.Reflection.PropertyInfo property in properties)
            {
                object o = property.GetValue(this, null) ?? string.Empty;
                string value = Convert.ToString(o);
                sb.AppendFormat("{0}={1}&", property.Name, value);
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

    }
}
