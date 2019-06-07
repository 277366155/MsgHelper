using System;
using AutoMapper;
using MH.Models.DBModel;
using MH.Models.DTO;
using MH.WxApiModels;

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
                    destination.MsgType = (MsgTypeEnum)Enum.Parse(typeof(MsgTypeEnum),source.MsgType.ToUpper());
                    destination.MsgContent = source.ToJson();
                    destination.CreateTimeSpan = source.CreateTime;
                    destination.CreateTime = DateTime.Now;
                });

                cfg.CreateMap<Tuple<WxUsers, User>, UserDTO>()
                .AfterMap((s,d)=> {
                    d.UserId = s.Item2.Id;
                    d.HeadImgUrl = s.Item1.HeadImgUrl;
                    d.IDCardNo = s.Item2.IDCardNo;
                    d.NickName = string.IsNullOrEmpty(s.Item2.CustomNickName)? s.Item1.NickName : s.Item2.CustomNickName;
                    d.Openid = s.Item1.Openid;
                    d.PhoneNumber = s.Item2.PhoneNumber;
                    d.RealName = s.Item2.RealName;
                    d.Sex = s.Item1.Sex;
                    d.Email = s.Item2.Email;
                });

                cfg.CreateMap<MassUserInfo, WxUsers>();
                cfg.CreateMap<WxUsers, User>().AfterMap((source, destination) =>
                {
                    //WxUsers映射到User时，id会被映射过来，出现问题
                    destination.Id = 0;
                    destination.CustomNickName = source.NickName;
                });
            });
        }
    }
}
