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
        public UserDTO GetWxUserInfoAndInsertToDb(string userOpenid)
        {
            var userInfoStr = WxApi.GetUserInfo(userOpenid);
            var data = userInfoStr.JsonToObj<WxUsers>();
           return Create(data);
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public UserDTO Create(WxUsers model)
        {
            if (Table.Any(a => a.Openid == model.Openid))
            {
              throw new SystemException("用户已存在");
            }
            using (var entity = new MHContext())
            {
                //using (var tran= entity.Database.BeginTransaction())
                //{
                    /*
                 1,插入wxUsers
                 2,插入UserInfo
                 */
                    var wxUser = entity.WxUsers.Add(model);
                    var userInfo = entity.User.Add(new User()
                    {
                        Openid = model.Openid,
                        CustomNickName = model.NickName
                    });
                    entity.SaveChanges();
                if (wxUser != null&& userInfo!=null)
                {
                    return Mapper.Map<Tuple<WxUsers,User>,UserDTO>(new Tuple<WxUsers, User>( wxUser.Entity,userInfo.Entity)) ;
                }
                return null;
            }
        }

        public bool GetUserListAndUpdateDb()
        {
            var nextOpenid = "";

            //用户详情列表对象，用于最后批量插入数据库
            var userInfoList = new UserInfoList() { User_info_list = new List<UserInfo>() };
            var lockObj = new object();

            //用于标识异步操作是否全部完成
            var isFinished = false;
            //nextOpenid不为空则继续获取下一页
            do
            {
                var openidList = WxApi.GetUserList(nextOpenid).JsonToObj<OpenidListModel>();
                if (openidList == null || openidList.Data.Openid == null || openidList.Data.Openid.Count <= 0)
                {
                    //nextOpenid = "";
                    //continue;
                    break;//跳出循环体
                }

                //构造批量获取用户详情参数
                var openidListParam = new OpenidListParam() { user_list = new List<GetUserInfoParam>() };
                openidList.Data.Openid.ForEach(a =>
                {
                    openidListParam.user_list.Add(new GetUserInfoParam() { lang = "zh-CN", openid = a });
                });
                nextOpenid = openidList.Next_Openid;

                //每500个openid启动一个线程请求，每个请求最多100个openid
                Task.Run(() =>
                {
                    var requestDataCount = 100;
                    var threadDataCount = requestDataCount * 5;
                    var times = openidListParam.user_list.Count % threadDataCount > 0 ? openidListParam.user_list.Count / threadDataCount + 1 : openidListParam.user_list.Count / threadDataCount;
                    for (var i = 1; i <= times; i++)
                    {
                        Task.Run(() =>
                        {
                            //最多请求100条用户详情，存入变量中。
                            var userInfoListJson = WxApi.GetBatchUserInfos(new OpenidListParam() { user_list = openidListParam.user_list.Skip((i - 1) * requestDataCount).Take(requestDataCount).ToList() });
                            lock (lockObj)
                            {
                                userInfoList.User_info_list.AddRange(userInfoListJson.JsonToObj<List<UserInfo>>());
                            }
                            if (i == times && nextOpenid.IsNullOrWhiteSpace())
                            {
                                isFinished = true;
                            }
                        });
                    }
                });

            } while (!nextOpenid.IsNullOrWhiteSpace());

            //如果线程未全部完成，则等待2秒钟
            while (!isFinished)
            {
                Thread.Sleep(2000);
            }

            //无已关注用户，正常情况
            if (userInfoList.User_info_list.Count <= 0)
            {
                return true;
            }

            //todo:批量插入userInfoList.User_info_list到DB;

            return true;
        }
    }
}
