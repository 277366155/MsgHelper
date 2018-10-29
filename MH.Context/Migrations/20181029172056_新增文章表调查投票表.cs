using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MH.Context.Migrations
{
    public partial class 新增文章表调查投票表 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Content = table.Column<string>(maxLength: 2048, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsDel = table.Column<bool>(nullable: false),
                    IsReview = table.Column<bool>(nullable: false),
                    IsTop = table.Column<bool>(nullable: false),
                    ModifyTime = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<DateTime>(nullable: false),
                    SimpleContent = table.Column<string>(maxLength: 256, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 32, nullable: true),
                    TypeId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticleType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsDel = table.Column<bool>(nullable: false),
                    ModifyTime = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(maxLength: 256, nullable: true),
                    RowVersion = table.Column<DateTime>(nullable: false),
                    TypeName = table.Column<string>(maxLength: 16, nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PollDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    ClientType = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IpAddress = table.Column<string>(maxLength: 128, nullable: true),
                    IsDel = table.Column<bool>(nullable: false),
                    ModifyTime = table.Column<DateTime>(nullable: false),
                    PollId = table.Column<int>(nullable: false),
                    PollOptionId = table.Column<int>(nullable: false),
                    RowVersion = table.Column<DateTime>(nullable: false),
                    VoterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PollOptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsDel = table.Column<bool>(nullable: false),
                    ModifyTime = table.Column<DateTime>(nullable: false),
                    OptionContent = table.Column<string>(nullable: true),
                    OrderNo = table.Column<int>(nullable: false),
                    PollId = table.Column<int>(nullable: false),
                    RowVersion = table.Column<DateTime>(nullable: false),
                    SelectCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Polls",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUserId = table.Column<int>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: true),
                    IsDel = table.Column<bool>(nullable: false),
                    IsReview = table.Column<bool>(nullable: false),
                    IsTop = table.Column<bool>(nullable: false),
                    MaxOptionsNum = table.Column<int>(nullable: false),
                    ModifyTime = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(maxLength: 256, nullable: true),
                    RowVersion = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Content = table.Column<string>(maxLength: 500, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsDel = table.Column<bool>(nullable: false),
                    ModifyTime = table.Column<DateTime>(nullable: false),
                    ObjId = table.Column<int>(nullable: false),
                    ReUserId = table.Column<int>(nullable: false),
                    ReviewType = table.Column<int>(nullable: false),
                    RowVersion = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemConfig",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsDel = table.Column<bool>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    ModifyTime = table.Column<DateTime>(nullable: false),
                    Remark = table.Column<string>(maxLength: 256, nullable: true),
                    RowVersion = table.Column<DateTime>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CustomNickName = table.Column<string>(maxLength: 32, nullable: true),
                    Email = table.Column<string>(maxLength: 64, nullable: true),
                    IDCardNo = table.Column<string>(maxLength: 32, nullable: true),
                    IsDel = table.Column<bool>(nullable: false),
                    LastLoginTime = table.Column<DateTime>(nullable: false),
                    ModifyTime = table.Column<DateTime>(nullable: false),
                    Openid = table.Column<string>(maxLength: 128, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 32, nullable: true),
                    RealName = table.Column<string>(maxLength: 32, nullable: true),
                    RowVersion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "ArticleType");

            migrationBuilder.DropTable(
                name: "PollDetails");

            migrationBuilder.DropTable(
                name: "PollOptions");

            migrationBuilder.DropTable(
                name: "Polls");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "SystemConfig");

            migrationBuilder.DropTable(
                name: "UserInfo");
        }
    }
}
