using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MH.Context.Migrations
{
    public partial class 新增用户消息表结构 : Migration
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WxUserMessage");
        }
    }
}
