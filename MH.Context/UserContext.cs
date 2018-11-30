using MH.Models.DBModel;
using MH.Models.DTO;
using System;
using System.Linq;

namespace MH.Context
{
    public class UserContext : ContextBase<User>
    {
        protected override IQueryable<User> Table => Entity.User.Where(a => !a.IsDel);

        /// <summary>
        /// 根据openid获取用户基本信息
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public UserDTO GetUserDTOByOpenid(string openid)
        {
            var userInfo = Table.FirstOrDefault(a => a.Openid.Equals(openid));
            var wxUserInfo=Entity.WxUsers.FirstOrDefault(a=>a.Openid.Equals(openid));
            var result = AutoMapper.Mapper.Map<Tuple<WxUsers, User>, UserDTO>(new Tuple<WxUsers, User>(wxUserInfo, userInfo));
            return result;
         }

    }
}
