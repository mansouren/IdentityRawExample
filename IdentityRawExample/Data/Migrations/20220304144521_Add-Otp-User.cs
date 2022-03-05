using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityRawExample.Data.Migrations
{
    public partial class AddOtpUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OTPCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OtpExpiry",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OTPCode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "OtpExpiry",
                table: "AspNetUsers");
        }
    }
}
