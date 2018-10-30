using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MH.Context.Migrations
{
    public partial class 初始化数据库 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "ArticleType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<int>(nullable: true),
                    IsDel = table.Column<bool>(nullable: false),
                    ModifyTime = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(maxLength: 256, nullable: true),
                    RowVersion = table.Column<DateTime>(nullable: false),
                    TypeName = table.Column<string>(maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleType_UserInfo_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "UserInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Polls",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<int>(nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Polls_UserInfo_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "UserInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Content = table.Column<string>(maxLength: 2048, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<int>(nullable: false),
                    IsDel = table.Column<bool>(nullable: false),
                    IsReview = table.Column<bool>(nullable: false),
                    IsTop = table.Column<bool>(nullable: false),
                    ModifyTime = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<DateTime>(nullable: false),
                    SimpleContent = table.Column<string>(maxLength: 256, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 32, nullable: true),
                    TypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_UserInfo_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "UserInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articles_ArticleType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "ArticleType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_PollOptions_Polls_PollId",
                        column: x => x.PollId,
                        principalTable: "Polls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Content = table.Column<string>(maxLength: 512, nullable: true),
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
                    table.ForeignKey(
                        name: "FK_Reviews_Articles_ObjId",
                        column: x => x.ObjId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Polls_ObjId",
                        column: x => x.ObjId,
                        principalTable: "Polls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_UserInfo_UserId",
                        column: x => x.UserId,
                        principalTable: "UserInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_PollDetails_Polls_PollId",
                        column: x => x.PollId,
                        principalTable: "Polls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PollDetails_PollOptions_PollOptionId",
                        column: x => x.PollOptionId,
                        principalTable: "PollOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PollDetails_UserInfo_VoterId",
                        column: x => x.VoterId,
                        principalTable: "UserInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CreatorId",
                table: "Articles",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_TypeId",
                table: "Articles",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleType_CreatorId",
                table: "ArticleType",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_PollDetails_PollId",
                table: "PollDetails",
                column: "PollId");

            migrationBuilder.CreateIndex(
                name: "IX_PollDetails_PollOptionId",
                table: "PollDetails",
                column: "PollOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_PollDetails_VoterId",
                table: "PollDetails",
                column: "VoterId");

            migrationBuilder.CreateIndex(
                name: "IX_PollOptions_PollId",
                table: "PollOptions",
                column: "PollId");

            migrationBuilder.CreateIndex(
                name: "IX_Polls_CreatorId",
                table: "Polls",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ObjId",
                table: "Reviews",
                column: "ObjId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PollDetails");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "SystemConfig");

            migrationBuilder.DropTable(
                name: "WxUserMessage");

            migrationBuilder.DropTable(
                name: "WxUsers");

            migrationBuilder.DropTable(
                name: "PollOptions");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Polls");

            migrationBuilder.DropTable(
                name: "ArticleType");

            migrationBuilder.DropTable(
                name: "UserInfo");
        }
    }
}
