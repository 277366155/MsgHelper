using System.Linq;

namespace MH.Context
{
    /// <summary>
    /// 基类中定义一个全局entity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseContext<T>
    {
        /// <summary>
        /// 各个Context中去组装Table
        /// </summary>
        protected abstract IQueryable<T> Table { get;  }

        protected static MHContext Entity;
        private static object obj = new object();
        public BaseContext()
        {
            if (Entity == null)
            {
                lock (obj)
                {
                    if (Entity == null)
                    {
                        Entity = new MHContext();
                    }
                }
            }
        }

         ~BaseContext()
        {
            if (Entity != null)
            {
                Entity.Dispose();
            }
        }
    }
}
