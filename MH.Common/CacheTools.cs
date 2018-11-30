using System;
using MH.Core;
using MH.Models.DTO;
using Microsoft.Extensions.Caching.Memory;

namespace MH.Common
{
    public class CacheTools
    {
        /// <summary>
        /// 程序内存缓存对象
        /// </summary>
        private static MemoryCache Cache = new MemoryCache(new MemoryCacheOptions());
        private static string  MemoryCacheKey="MemoryCacheKey_";
        /// <summary>
        /// 在内存缓存中 根据key获取缓存值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetData<T>(string key)
        {
            key = MemoryCacheKey + key;
            var data = default(T);
            Cache.TryGetValue(key, out data);

            return data;
        }


        /// <summary>
        ///在内存缓存中 保存缓存数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="timeOffset">缓存时长，单位：秒</param>
        /// <returns>是否成功</returns>
        public static bool SetData<T>(string key,T value,double timeOffset=60*60)
        {
            try
            {
                key = MemoryCacheKey + key;

                Cache.Set(key, value,new MemoryCacheEntryOptions() { SlidingExpiration=TimeSpan.FromSeconds(timeOffset) });
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 根据当前用户cookie，判断是否登录
        /// </summary>
        /// <returns></returns>
        public static UserDTO GetUserInfo()
        {
            var openid = BaseCore.CurrentContext.GetCookie();
            if (openid.IsNullOrWhiteSpace())
            {
                return null;
            }
            var ip = BaseCore.CurrentContext.Connection.RemoteIpAddress;
            var port = BaseCore.CurrentContext.Connection.RemotePort;

          return GetData<UserDTO>(openid+ip+port);
            
        }
    }
}
