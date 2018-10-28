using MH.Common;
using MH.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace MH.Context
{
    /// <summary>
    /// 基类中定义一个全局entity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ContextBase<T>
    {
        /// <summary>
        /// 各个Context中去组装Table
        /// </summary>
        protected abstract IQueryable<T> Table { get;  }

        /// <summary>
        /// 单例数据库实例
        /// </summary>
        protected static MHContext Entity;
        /// <summary>
        /// 数据库连接地址
        /// </summary>
        protected static string DbConnection ;
        private static object obj = new object();
        public ContextBase()
        {
            if (DbConnection.IsNullOrWhiteSpace())
            {
                DbConnection=BaseCore.Configuration.GetConnectionString("MySqlConnection");
            }

            if (Entity == null)
            {
                lock (obj)
                {
                    if (Entity == null)
                    {
                        Entity = new MHContext(DbConnection);
                    }
                }
            }
        }

         ~ContextBase()
        {
            if (Entity != null)
            {
                Entity.Dispose();
            }
        }
    }
}
