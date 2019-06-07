using AutoMapper;
using MH.Common;
using MH.Models;
using MH.Models.DBModel;
using MH.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MH.WxApiModels;

namespace MH.Context
{
    public class WxUsersContext : ContextBase<WxUsers>
    {
        protected override IQueryable<WxUsers> Table
        {
            get
            {
                return Entity.WxUsers.Where(a => !a.IsDel);
            }
        }

        /// <summary>
        /// 根据openid从wx端获取用户的详细信息
        /// </summary>
        /// <param name="userOpenid"></param>
        /// <returns></returns>
        public bool GetWxUserInfoAndInsertToDb(string userOpenid)
        {
            var userInfoStr = WxApi.WxApi.GetUserInfo(userOpenid);
            var data = userInfoStr.ToObj<WxUsers>();
            return Create(data);
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns>是否成功</returns>
        public bool Create(WxUsers model)
        {
            using (var entity = new MHContext())
            {
                if (Table.Any(a => a.Openid == model.Openid))
                {
                    var user = entity.User.FirstOrDefault(a => a.Openid == model.Openid);
                    user.LastLoginTime = DateTime.Now;
                    entity.SaveChanges();
                    return true;
                }

                var wxUser = entity.WxUsers.Add(model);
                var userInfo = entity.User.Add(new User()
                {
                    Openid = model.Openid,
                    CustomNickName = model.NickName
                });
                entity.SaveChanges();
                if (wxUser != null && userInfo != null)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 批量获取关注用户信息
        /// </summary>
        /// <returns></returns>
        public bool GetUserListAndUpdateDb()
        {
            var nextOpenid = "";

            //用户详情列表对象，用于最后批量插入数据库
            var userInfoList = new UserInfoList() { User_info_list = new List<MassUserInfo>() };
            var lockObj = new object();

            //用于标识异步操作是否全部完成
            var isFinished = false;
            //是否有待获取用户详情的任务
            var hasTask = false;
            //nextOpenid不为空则继续获取下一页
            #region 获取已关注用户列表
            do
            {
                var openidList = WxApi.WxApi.GetUserList(nextOpenid).ToObj<OpenidListModel>();
                if (openidList == null || openidList.Data == null || openidList.Data.Openid.Count <= 0)
                {
                    break;//跳出循环体
                }

                //构造批量获取用户详情参数
                var openidListParam = new OpenidListParam() { user_list = new List<GetUserInfoParam>() };
                openidList.Data.Openid.ForEach(a =>
                {
                    //db中不存在的用户，才加入待获取列表
                    if (!Table.Any(m => m.Openid == a))
                        openidListParam.user_list.Add(new GetUserInfoParam() { lang = "zh-CN", openid = a });
                });
                nextOpenid = openidList.Next_Openid;

                //待获取列表为空时，跳出循环体
                if (openidListParam.user_list.Count <= 0)
                {
                    //如果无任务，直接完成
                    if (!hasTask)
                        isFinished = true;
                    break;
                }
                else
                {
                    hasTask = true;
                }
                #region 每500用户启动一个线程获取详细信息
                //每500个openid启动一个线程请求，每个请求最多100个openid
                Task.Run(() =>
                {
                    var requestDataCount = 100;
                    var threadDataCount = requestDataCount * 5;
                    var times = openidListParam.user_list.Count % threadDataCount > 0 ? openidListParam.user_list.Count / threadDataCount + 1 : openidListParam.user_list.Count / threadDataCount;

                        //并行for循环，for()循环参数小于第二个参数值
                        Parallel.For(1, times + 1, i =>
                            {
                            //最多请求100条用户详情，存入变量中。
                            var userInfoListJson = WxApi.WxApi.GetBatchUserInfos(new OpenidListParam() { user_list = openidListParam.user_list.Skip((i - 1) * requestDataCount).Take(requestDataCount).ToList() });
                        lock (lockObj)
                        {
                            userInfoList.User_info_list.AddRange(userInfoListJson.ToObj<UserInfoList>()?.User_info_list);
                        }
                        if (i == times)
                        {
                            isFinished = true;
                        }
                    });
                });
                #endregion 每500用户启动一个线程获取细信息

            } while (true);
            #endregion 获取已关注用户列表

            //如果线程未全部完成，则等待1秒钟
            while (!isFinished)//=="false")
            {
                Thread.Sleep(1000);
            }

            //无已关注用户，正常情况
            if (userInfoList.User_info_list.Count <= 0)
            {
                return true;
            }

            //todo:批量插入userInfoList.User_info_list到DB;
            using (var entity = new MHContext())
            {
                var wxUsersList = Mapper.Map<List<MassUserInfo>, List<WxUsers>>(userInfoList.User_info_list);
                entity.WxUsers.AddRange(wxUsersList.Where(a => !Table.Any(m => m.Openid == a.Openid)));
                //todo:批量插入user表。。
                var usersList = Mapper.Map<List<WxUsers>, List<User>>(wxUsersList);
                entity.User.AddRange(usersList.Where(a=>!entity.User.Any(m=>m.Openid==a.Openid)));
                entity.SaveChanges();
            }
            return true;
        }
    }
}
