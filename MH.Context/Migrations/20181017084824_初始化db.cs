using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MH.Context.Migrations
{
    public partial class 初始化db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    RowVersion = table.Column<DateTime>(nullable: true),
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
                name: "WxUsers");
        }
    }
}
