using MH.Models;
using MH.Models.DBModel;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore;
using System.Linq;

namespace MH.Context
{
    public class WxUsersContext
    {
        private IQueryable<WxUsers> Table;
        private MHContext Entity;
        public WxUsersContext()
        {
            Entity = new MHContext();
            Table = Entity.WxUsers.Where(a => !a.IsDel);
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
            var data = Entity.WxUsers.Add(model);
            Entity.SaveChanges();

            if (data != null)
            {
                return new Ok<WxUsers>("新增用户成功") { Data = data.Entity };
            }
            return new Error("新增用户出错");
        }
    }
}
