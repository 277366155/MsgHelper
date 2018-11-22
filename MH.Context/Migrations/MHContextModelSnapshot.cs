﻿// <auto-generated />
using MH.Context;
using MH.Models.DBModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Storage.Internal;
using System;

namespace MH.Context.Migrations
{
    [DbContext(typeof(MHContext))]
    partial class MHContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026");

            modelBuilder.Entity("MH.Models.DBModel.Articles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .HasMaxLength(2048);

                    b.Property<string>("CoverImg")
                        .HasMaxLength(256);

                    b.Property<DateTime>("CreateTime");

                    b.Property<int>("CreatorId");

                    b.Property<bool>("IsDel");

                    b.Property<bool>("IsReview");

                    b.Property<bool>("IsTop");

                    b.Property<DateTime>("ModifyTime");

                    b.Property<DateTime>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("SimpleContent")
                        .HasMaxLength(256);

                    b.Property<int>("Status");

                    b.Property<string>("Title")
                        .HasMaxLength(32);

                    b.Property<int>("TypeId");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("TypeId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("MH.Models.DBModel.ArticleType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<int?>("CreatorId");

                    b.Property<bool>("IsDel");

                    b.Property<DateTime>("ModifyTime");

                    b.Property<string>("Remarks")
                        .HasMaxLength(256);

                    b.Property<DateTime>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("TypeName")
                        .HasMaxLength(16);

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("ArticleType");
                });

            modelBuilder.Entity("MH.Models.DBModel.PollDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClientType");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("IpAddress")
                        .HasMaxLength(128);

                    b.Property<bool>("IsDel");

                    b.Property<DateTime>("ModifyTime");

                    b.Property<int>("PollId");

                    b.Property<int>("PollOptionId");

                    b.Property<DateTime>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("VoterId");

                    b.HasKey("Id");

                    b.HasIndex("PollId");

                    b.HasIndex("PollOptionId");

                    b.HasIndex("VoterId");

                    b.ToTable("PollDetails");
                });

            modelBuilder.Entity("MH.Models.DBModel.PollOptions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<bool>("IsDel");

                    b.Property<DateTime>("ModifyTime");

                    b.Property<string>("OptionContent")
                        .HasMaxLength(512);

                    b.Property<int>("OrderNo");

                    b.Property<int>("PollId");

                    b.Property<DateTime>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("SelectCount");

                    b.HasKey("Id");

                    b.HasIndex("PollId");

                    b.ToTable("PollOptions");
                });

            modelBuilder.Entity("MH.Models.DBModel.Polls", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .HasMaxLength(512);

                    b.Property<string>("CoverImg")
                        .HasMaxLength(256);

                    b.Property<DateTime>("CreateTime");

                    b.Property<int>("CreatorId");

                    b.Property<DateTime?>("Deadline");

                    b.Property<bool>("IsDel");

                    b.Property<bool>("IsReview");

                    b.Property<bool>("IsTop");

                    b.Property<int>("MaxOptionsNum");

                    b.Property<DateTime>("ModifyTime");

                    b.Property<string>("Remarks")
                        .HasMaxLength(256);

                    b.Property<DateTime>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("Status");

                    b.Property<string>("Title")
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Polls");
                });

            modelBuilder.Entity("MH.Models.DBModel.Reviews", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .HasMaxLength(512);

                    b.Property<DateTime>("CreateTime");

                    b.Property<bool>("IsDel");

                    b.Property<DateTime>("ModifyTime");

                    b.Property<int>("ObjId");

                    b.Property<int>("ReUserId");

                    b.Property<int>("ReviewType");

                    b.Property<DateTime>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ObjId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("MH.Models.DBModel.SystemConfig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<bool>("IsDel");

                    b.Property<string>("Key");

                    b.Property<DateTime>("ModifyTime");

                    b.Property<string>("Remark")
                        .HasMaxLength(256);

                    b.Property<DateTime>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("SystemConfig");
                });

            modelBuilder.Entity("MH.Models.DBModel.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("CustomNickName")
                        .HasMaxLength(32);

                    b.Property<string>("Email")
                        .HasMaxLength(64);

                    b.Property<string>("IDCardNo")
                        .HasMaxLength(32);

                    b.Property<bool>("IsDel");

                    b.Property<DateTime>("LastLoginTime");

                    b.Property<DateTime>("ModifyTime");

                    b.Property<string>("Openid")
                        .HasMaxLength(128);

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(32);

                    b.Property<string>("Pwd")
                        .HasMaxLength(256);

                    b.Property<string>("RealName")
                        .HasMaxLength(32);

                    b.Property<DateTime>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("MH.Models.DBModel.WxUserMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<long>("CreateTimeSpan");

                    b.Property<string>("FromUserName")
                        .HasMaxLength(128);

                    b.Property<bool>("IsDel");

                    b.Property<DateTime>("ModifyTime");

                    b.Property<string>("MsgContent")
                        .HasMaxLength(2000);

                    b.Property<int>("MsgType");

                    b.Property<DateTime>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("ToUserName")
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.ToTable("WxUserMessage");
                });

            modelBuilder.Entity("MH.Models.DBModel.WxUsers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City")
                        .HasMaxLength(64);

                    b.Property<string>("Country")
                        .HasMaxLength(64);

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("HeadImgUrl")
                        .HasMaxLength(500);

                    b.Property<bool>("IsDel");

                    b.Property<string>("Language")
                        .HasMaxLength(64);

                    b.Property<DateTime>("ModifyTime");

                    b.Property<string>("NickName")
                        .HasMaxLength(64);

                    b.Property<string>("Openid")
                        .HasMaxLength(128);

                    b.Property<string>("Prvince")
                        .HasMaxLength(64);

                    b.Property<DateTime>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("Sex");

                    b.HasKey("Id");

                    b.ToTable("WxUsers");
                });

            modelBuilder.Entity("MH.Models.DBModel.Articles", b =>
                {
                    b.HasOne("MH.Models.DBModel.User", "Creator")
                        .WithMany("ArticlesList")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MH.Models.DBModel.ArticleType", "ArticleType")
                        .WithMany("ArticlesList")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MH.Models.DBModel.ArticleType", b =>
                {
                    b.HasOne("MH.Models.DBModel.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");
                });

            modelBuilder.Entity("MH.Models.DBModel.PollDetails", b =>
                {
                    b.HasOne("MH.Models.DBModel.Polls", "Poll")
                        .WithMany("PollDetailsList")
                        .HasForeignKey("PollId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MH.Models.DBModel.PollOptions", "PollOption")
                        .WithMany("PollDetailsList")
                        .HasForeignKey("PollOptionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MH.Models.DBModel.User", "Voter")
                        .WithMany("PollDetailsList")
                        .HasForeignKey("VoterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MH.Models.DBModel.PollOptions", b =>
                {
                    b.HasOne("MH.Models.DBModel.Polls", "Poll")
                        .WithMany("PollOptionsList")
                        .HasForeignKey("PollId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MH.Models.DBModel.Polls", b =>
                {
                    b.HasOne("MH.Models.DBModel.User", "Creator")
                        .WithMany("PollsList")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MH.Models.DBModel.Reviews", b =>
                {
                    b.HasOne("MH.Models.DBModel.Articles", "Article")
                        .WithMany("ReviewsList")
                        .HasForeignKey("ObjId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MH.Models.DBModel.Polls", "Poll")
                        .WithMany("ReviewsList")
                        .HasForeignKey("ObjId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MH.Models.DBModel.User", "UserInfo")
                        .WithMany("ReviewsList")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
