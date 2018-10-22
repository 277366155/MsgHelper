using MH.Common;
using MH.Models;
using MH.Models.DBModel;
using System.Linq;
using AutoMapper;

namespace MH.Context
{
    public   class WxUserMessageContext : BaseContext<WxUserMessage>
    {
        protected override IQueryable<WxUserMessage> Table => Entity.WxUserMessage.Where(a=>!a.IsDel);


        /// <summary>
        /// 新增用户消息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResultBase Create(WxUserMessage model)
        {
            if (Table.Any(a => a.FromUserName == model.FromUserName && a.CreateTimeSpan == model.CreateTimeSpan))
            {
                return new Error("数据已存在");
            }
            var data = Entity.WxUserMessage.Add(model);

            Entity.SaveChanges();
            if (data != null)
            {
                return new Ok<WxUserMessage>("消息保存成功") { Data=data.Entity};
            }
            return new Error("消息保存出错");
        }


        public ResultBase GetXmlDataAndInsert()
        {
            var data =WxApi.GetUserMsg();

            //autoMapper转换model
            var dbData = Mapper.Map<WXMsgBase, WxUserMessage>(data);

            return Create(dbData);
        }
    }
}
