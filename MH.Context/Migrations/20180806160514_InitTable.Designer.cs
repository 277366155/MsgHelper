﻿// <auto-generated />
using MH.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Storage.Internal;
using System;

namespace MH.Context.Migrations
{
    [DbContext(typeof(MHContext))]
    [Migration("20180806160514_InitTable")]
    partial class InitTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026");

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

                    b.Property<DateTime?>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("Sex");

                    b.HasKey("Id");

                    b.ToTable("WxUsers");
                });
#pragma warning restore 612, 618
        }
    }
}