using MongoDB.Driver;
using System.Collections.Generic;
using MH.Core;
using Microsoft.Extensions.Configuration;

namespace MH.MongoDB
{
    public class MongoDBHelper
    {
        private static IConfiguration MongoDbConfig = BaseCore.Configuration.GetSection("MongoDb");

        private static MongoClient client;
        private static string MongoDbStr = MongoDbConfig.GetSection("ConnStr").Value; 
        private static string MongoDbName = MongoDbConfig.GetSection("DbName").Value;


        private  static  object LockObj=new object();
        public static MongoClient Client
        {
            get
            {
                if (client == null)
                {
                    lock (LockObj)
                    {
                        if (client == null)
                        {
                            client = new MongoClient(MongoDbStr);
                        }
                    }
                }

                return client;
            }
        }

        /// <summary>
        /// 从mongo获取数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="collectionName"></param>
        /// <param name="mongoDbName"></param>
        /// <returns></returns>
        public static IMongoCollection<T> GetCollection<T>(string collectionName = "", string mongoDbName = "")
        {
            mongoDbName = GetDbName(mongoDbName);
            var database = Client.GetDatabase(mongoDbName);
            return database.GetCollection<T>(string.IsNullOrWhiteSpace(collectionName) ? typeof(T).Name : collectionName);
        }

        private static string GetDbName(string mongoDbName)
        {
            if (string.IsNullOrWhiteSpace(mongoDbName))
            {
                mongoDbName = MongoDbName;
            }

            return  mongoDbName;
        }


        /// <summary>
        /// 设置mongo数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="collectionName"></param>
        /// <param name="obj">插入的数据</param>
        /// <param name="mongoDbName"></param>
        public static void SetCollection<T>(T obj, string collectionName = "", string mongoDbName = "")
        {
            //var dbname = GetDbName(mongoDbName);
            var collection = GetCollection<T>(collectionName, mongoDbName);
            collection.InsertOne(obj);
        }

        //public static async Task ErrorLogAsync<T>(T obj)
        //{
        //    Task.Run(() =>
        //    {
        //        var collectionName = "Log_" + DateTime.Now.ToString("yyyyMMdd");
        //        var mongoDbName = "ErrorLog";

        //        SetCollection(obj, collectionName, mongoDbName);
        //    });
        //}

        /// <summary>
        /// 设置mongo数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="collectionName"></param>
        /// <param name="obj">插入的数据</param>
        /// <param name="mongoDbName"></param>
        public static void SetInsertManyCollection<T>(List<T> obj, string collectionName = "", string mongoDbName = "")
        {

            var collection = GetCollection<T>(collectionName, mongoDbName);
            collection.InsertMany(obj);
        }

        /// <summary>
        /// 清空数据
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="mongoDbName"></param>
        public static void DropCollection(string collectionName, string mongoDbName)
        {
            if (!string.IsNullOrWhiteSpace(mongoDbName))
            {
                var dbname = GetDbName(mongoDbName);
                Client.GetDatabase(dbname).DropCollection(collectionName);
            }

        }

        /// <summary>
        /// 清空数据
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="mongoDbName"></param>
        public static void DropDatabase(string mongoDbName)
        {
            if (!string.IsNullOrWhiteSpace(mongoDbName))
            {
                var dbname = GetDbName(mongoDbName);
                Client.DropDatabase(dbname);
            }

        }
    }
}
