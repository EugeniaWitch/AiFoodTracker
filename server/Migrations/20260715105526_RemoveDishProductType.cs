using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDishProductType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000007"));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000008"),
                columns: new[] { "Icon", "Name" },
                values: new object[] { "soup", "Суп" });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "Icon", "Name", "Type" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000009"), "salad", "Салат", 0 },
                    { new Guid("10000000-0000-0000-0000-000000000010"), "main-dish", "Основное блюдо", 0 },
                    { new Guid("10000000-0000-0000-0000-000000000011"), "side-dish", "Гарнир", 0 },
                    { new Guid("10000000-0000-0000-0000-000000000012"), "dessert", "Десерт", 0 },
                    { new Guid("10000000-0000-0000-0000-000000000013"), "bakery", "Выпечка", 0 },
                    { new Guid("10000000-0000-0000-0000-000000000014"), "other-food", "Другое", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000014"));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000008"),
                columns: new[] { "Icon", "Name" },
                values: new object[] { "other-food", "Другое" });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "Icon", "Name", "Type" },
                values: new object[,]
                {
                    { new Guid("30000000-0000-0000-0000-000000000001"), "soup", "Суп", 2 },
                    { new Guid("30000000-0000-0000-0000-000000000002"), "salad", "Салат", 2 },
                    { new Guid("30000000-0000-0000-0000-000000000003"), "main-dish", "Основное блюдо", 2 },
                    { new Guid("30000000-0000-0000-0000-000000000004"), "side-dish", "Гарнир", 2 },
                    { new Guid("30000000-0000-0000-0000-000000000005"), "dessert", "Десерт", 2 },
                    { new Guid("30000000-0000-0000-0000-000000000006"), "bakery", "Выпечка", 2 },
                    { new Guid("30000000-0000-0000-0000-000000000007"), "other-dish", "Другое", 2 }
                });
        }
    }
}
