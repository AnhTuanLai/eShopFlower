using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopFlower.Data.Migrations
{
    public partial class SeedIdentityUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("f8f0b9b6-4f9b-4af6-b02c-15096528dc89"), "14d8c982-e37a-48ca-8781-67e8743a7234", "Administrator role", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("f8f0b9b6-4f9b-4af6-b02c-15096528dc89"), new Guid("dc5fbc3b-e067-4d0c-80e3-00348a067d79") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("dc5fbc3b-e067-4d0c-80e3-00348a067d79"), 0, "2ad9ec9f-b810-4c9d-afda-731bb230a4e1", new DateTime(2020, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "tuananhlai0920@gmail.com", true, "Hung", "Nguyen", false, null, "tuananhlai0920@gmail.com", "admin", "AQAAAAEAACcQAAAAEJS3BIMYExog3uDXJgdsj7f3C6FKTeXPMFBG40G4Dnc7E65CGYl1Ui23hH1aGJvyyg==", null, false, "", false, "admin" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 1, 25, 21, 56, 24, 548, DateTimeKind.Local).AddTicks(3324));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 1, 25, 21, 56, 24, 548, DateTimeKind.Local).AddTicks(3331));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("f8f0b9b6-4f9b-4af6-b02c-15096528dc89"));

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("f8f0b9b6-4f9b-4af6-b02c-15096528dc89"), new Guid("dc5fbc3b-e067-4d0c-80e3-00348a067d79") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("dc5fbc3b-e067-4d0c-80e3-00348a067d79"));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 1, 25, 21, 42, 12, 172, DateTimeKind.Local).AddTicks(29));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 1, 25, 21, 42, 12, 172, DateTimeKind.Local).AddTicks(37));
        }
    }
}
