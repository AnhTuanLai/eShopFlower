using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopFlower.Data.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppConfigs",
                columns: new[] { "Key", "Value" },
                values: new object[,]
                {
                    { "HomeDescription", "This is description of eShopFlower" },
                    { "HomeKeyword", "This is keyword of eShopFlower" },
                    { "HomeTitle", "This is home page of eShopFlower" }
                });

            migrationBuilder.InsertData(
                table: "Categoríes",
                columns: new[] { "Id", "IsShowOnHome", "ParentId", "SortOrder", "Status" },
                values: new object[,]
                {
                    { 1, true, null, 1, 1 },
                    { 2, true, null, 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "IsDefault", "Name" },
                values: new object[,]
                {
                    { "en", false, "English" },
                    { "vi", true, "Tiếng Việt" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DateCreated", "IsFeatured", "OriginalPrice", "Price" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 25, 16, 13, 17, 258, DateTimeKind.Local).AddTicks(738), null, 100000m, 200000m },
                    { 2, new DateTime(2024, 1, 25, 16, 13, 17, 258, DateTimeKind.Local).AddTicks(749), null, 100000m, 200000m }
                });

            migrationBuilder.InsertData(
                table: "CategoryTranslations",
                columns: new[] { "Id", "CategoryId", "LanguageId", "Name", "SeoAlias", "SeoDescription", "SeoTitle" },
                values: new object[,]
                {
                    { 1, 1, "vi", "Hoa Sinh Nhật", "hoa-sinh-nhat", "Sản phẩm hoa sinh nhật", "Sản phẩm hoa sinh nhật" },
                    { 2, 1, "en", "Birthday Flowers", "birthday-flowers", "The birthday flower products ", "The birthday flower products " },
                    { 3, 2, "vi", "Hoa Khai Trương", "hoa-khai-truong", "Sản phẩm hoa khai trương", "Sản phẩm hoa khai trương" },
                    { 4, 2, "en", "Grand Opening Flower", "grand-opening-flower", "The grand opening flower products", "The grand opening flower products" }
                });

            migrationBuilder.InsertData(
                table: "ProductInCategories",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "ProductTranslations",
                columns: new[] { "Id", "Description", "Details", "LanguageId", "Name", "ProductId", "SeoAlias", "SeoDescription", "SeoTitle" },
                values: new object[,]
                {
                    { 1, "Bó hoa tươi - Nàng tiên", "Bó hoa tươi - Nàng tiên", "vi", "Bó hoa tươi - Nàng tiên", 1, "bó-hoa-tuoi-nang-tien", "Bó hoa tươi - Nàng tiên", "Bó hoa tươi - Nàng tiên" },
                    { 2, "Fresh flower bouquet - Fairy", "Fresh flower bouquet - Fairy", "en", "Fresh flower bouquet - Fairy", 1, "fresh-flower-bouquet-fairy", "Fresh flower bouquet - Fairy", "Fresh flower bouquet - Fairy" },
                    { 3, "Bó hoa tươi - Tinh tú", "Bó hoa tươi - Tinh tú", "vi", "Bó hoa tươi - Tinh tú", 2, "bó-hoa-tuoi-tinh-tu", "Bó hoa tươi - Tinh tú", "Bó hoa tươi - Tinh tú" },
                    { 4, "Fresh flower bouquet - Stars", "Fresh flower bouquet - Stars", "en", "Fresh flower bouquet - Fairy", 2, "fresh-flower-bouquet-stars", "Fresh flower bouquet - Stars", "Fresh flower bouquet - Stars" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppConfigs",
                keyColumn: "Key",
                keyValue: "HomeDescription");

            migrationBuilder.DeleteData(
                table: "AppConfigs",
                keyColumn: "Key",
                keyValue: "HomeKeyword");

            migrationBuilder.DeleteData(
                table: "AppConfigs",
                keyColumn: "Key",
                keyValue: "HomeTitle");

            migrationBuilder.DeleteData(
                table: "CategoryTranslations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CategoryTranslations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CategoryTranslations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CategoryTranslations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductInCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ProductInCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categoríes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categoríes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: "en");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: "vi");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
