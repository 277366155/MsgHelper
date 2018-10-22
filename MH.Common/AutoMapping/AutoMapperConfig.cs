using System;
using AutoMapper;
using MH.Models.DBModel;

namespace MH.Common.AutoMapping
{
    public   class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                ////CreateMap<TestEntity.Models.Person, Person>().AfterMap((source, destination) =>
                ////{
                ////    destination.Brithday = source.Brithday.ToString();
                ////    destination.Balance = Convert.ToDouble(source.Balance);
                ////}).ForAllMembers(p => {
                ////    p.Condition((s, d, sMember) => {
                ////        return sMember != null;
                ////    });
                ////});
                
                //// CreateMap<PagerResponse, TestEntity.Models.Pager<TestEntity.Models.Person>>();

                cfg.CreateMap<WXMsgBase, WxUserMessage>().AfterMap((source, destination) =>
                {
                    destination.MsgType = (MsgTypeEnum)Enum.Parse<MsgTypeEnum>(source.MsgType.ToUpper());
                    destination.MsgContent = source.ObjToJson();
                    destination.CreateTimeSpan = source.CreateTime;
                    destination.CreateTime = DateTime.Now;
                });


            });
        }
    }
}
