using System;
using AutoMapper;
using MH.Models.DBModel;
using MH.Models.DTO;

namespace MH.Common.AutoMapping
{
    public static class AutoMapperConfig
    {
        public static TDestination Map<TSource, TDestination>(this TDestination destination, TSource source)
        {
            return Mapper.Map(source, destination);
        }

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

                cfg.CreateMap<Tuple<WxUsers, User>, UserDTO>()
                .ForMember(
                    d => d.NickName,
                    opt => opt.MapFrom(s =>
                    string.IsNullOrEmpty(s.Item2.CustomNickName)
                    ? s.Item1.NickName
                    : s.Item2.CustomNickName
                ));

                cfg.CreateMap<MassUserInfo, WxUsers>();
                cfg.CreateMap<WxUsers, User>().AfterMap((source, destination)=> {
                    destination.CustomNickName = source.NickName;                    
                });
            });
        }
    }
}
