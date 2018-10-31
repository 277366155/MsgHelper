using AutoMapper;
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
    }
}
