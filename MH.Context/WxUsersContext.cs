using AutoMapper;
using MH.Common;
using MH.Models;
using MH.Models.DBModel;
using MH.Models.DTO;
using System;
using System.Linq;

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
        public ResultBase GetWxUserInfoAndInsertToDb(string userOpenid)
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
        public ResultBase Create(WxUsers model)
        {
            if (Table.Any(a => a.Openid == model.Openid))
            {
                return new Error("用户已存在");
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
                    return new Ok<UserDTO>("新增用户成功") { Data = Mapper.Map<Tuple<WxUsers,User>,UserDTO>(new Tuple<WxUsers, User>( wxUser.Entity,userInfo.Entity)) };
                }
                return new Error("新增用户出错");
                //}
            }
        }

        public bool GetUserListAndUpdateDb()
        {
            var nextOpenid = "";
           var userListJson= WxApi.GetUserList(nextOpenid);
            
            //获取已关注用户列表
            var userList = userListJson.JsonToObj<UserListModel>();
            if (userList == null)
            {
                return true;
            }

            nextOpenid = userList.Next_Openid;
            var openidList = userList.Data.Openid;

            //nextOpenid不为空则继续获取下一页
            while (!nextOpenid.IsNullOrWhiteSpace())
            {
                var newUserList=WxApi.GetUserList(nextOpenid).JsonToObj<UserListModel>();
                if (newUserList == null)
                {
                    nextOpenid = "";
                    continue;
                }
                openidList.AddRange(newUserList.Data.Openid);
                nextOpenid = newUserList.Next_Openid;
            }

            if (openidList.Count <= 0)
            {
                return true;
            }
            /*todo:
            1，调用wx接口获取用户详情
            2，获取到用户详细信息之后，批量插入db
             */

            return true;
        }
    }
}
