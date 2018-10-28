using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MH.Context.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WxUserMessage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateTimeSpan = table.Column<long>(nullable: false),
                    FromUserName = table.Column<string>(maxLength: 128, nullable: true),
                    IsDel = table.Column<bool>(nullable: false),
                    ModifyTime = table.Column<DateTime>(nullable: false),
                    MsgContent = table.Column<string>(maxLength: 2000, nullable: true),
                    MsgType = table.Column<int>(nullable: false),
                    RowVersion = table.Column<DateTime>(nullable: false),
                    ToUserName = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WxUserMessage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WxUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    City = table.Column<string>(maxLength: 64, nullable: true),
                    Country = table.Column<string>(maxLength: 64, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    HeadImgUrl = table.Column<string>(maxLength: 500, nullable: true),
                    IsDel = table.Column<bool>(nullable: false),
                    Language = table.Column<string>(maxLength: 64, nullable: true),
                    ModifyTime = table.Column<DateTime>(nullable: false),
                    NickName = table.Column<string>(maxLength: 64, nullable: true),
                    Openid = table.Column<string>(maxLength: 128, nullable: true),
                    Prvince = table.Column<string>(maxLength: 64, nullable: true),
                    RowVersion = table.Column<DateTime>(nullable: false),
                    Sex = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WxUsers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WxUserMessage");

            migrationBuilder.DropTable(
                name: "WxUsers");
        }
    }
}
