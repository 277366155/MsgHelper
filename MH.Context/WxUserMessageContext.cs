using MH.Common;
using MH.Models;
using MH.Models.DBModel;
using System.Linq;
using AutoMapper;
using System;

namespace MH.Context
{
    public   class WxUserMessageContext : ContextBase<WxUserMessage>
    {
        protected override IQueryable<WxUserMessage> Table => Entity.WxUserMessage.Where(a=>!a.IsDel);


        /// <summary>
        /// 新增用户消息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="entity">如果为null，则在内部声明，结束时释放</param>
        /// <returns></returns>
        public WxUserMessage Create(WxUserMessage model, MHContext entity)
        {
            MHContext context = entity==null?new MHContext():entity;

            try
            {
                var table = context.WxUserMessage.Where(a => !a.IsDel);
                if (table.Any(a => a.FromUserName == model.FromUserName && a.CreateTimeSpan == model.CreateTimeSpan))
                {
                    throw new  SystemException("数据已存在");
                }
                var data = context.WxUserMessage.Add(model);

              var result=  context.SaveChanges();

                return result>0 ? data.Entity:null;
            }
            finally
            {
                if(entity == null)                    
                    context.Dispose();
            }
        }

        /// <summary>
        ///接收wx消息并保持到db
        /// </summary>
        /// <returns></returns>
        public WxUserMessage GetXmlDataAndInsert()
        {
            var data =WxApi.GetUserMsg();

            //autoMapper转换model
            var msgData = Mapper.Map<WXMsgBase, WxUserMessage>(data);
            WxUserMessage msgAddResult = null;
            var userAddReulst = 1;
            using (var entity = new MHContext())
            {
                using (var tran = entity.Database.BeginTransaction())
                {
                    try
                    {
                        msgAddResult = Create(msgData, entity);
                        //如果db不存在该用户信息，则插入
                        if (!entity.WxUsers.Any(a => !a.IsDel && a.Openid == msgData.FromUserName))
                        {
                            var userInfoJson = WxApi.GetUserInfo(msgData.FromUserName);
                            entity.WxUsers.Add(userInfoJson.JsonToObj<WxUsers>());
                            userAddReulst = entity.SaveChanges();
                        }
                        //两者都成功才commit
                        if (msgAddResult == null || userAddReulst <= 0)
                        {
                            throw new SystemException("操作失败");
                        }
                        tran.Commit();
                        return msgAddResult;
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw ex;
                    }
                }
            }
        }
    }
}
