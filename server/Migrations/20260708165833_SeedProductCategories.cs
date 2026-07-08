using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class SeedProductCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "Icon", "Name", "Type" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), "milk", "Молочные продукты", 0 },
                    { new Guid("10000000-0000-0000-0000-000000000002"), "meat", "Мясо", 0 },
                    { new Guid("10000000-0000-0000-0000-000000000003"), "fish", "Рыба", 0 },
                    { new Guid("10000000-0000-0000-0000-000000000004"), "fruit", "Фрукты", 0 },
                    { new Guid("10000000-0000-0000-0000-000000000005"), "vegetable", "Овощи", 0 },
                    { new Guid("10000000-0000-0000-0000-000000000006"), "grain", "Крупы", 0 },
                    { new Guid("10000000-0000-0000-0000-000000000007"), "sweet", "Сладкое", 0 },
                    { new Guid("10000000-0000-0000-0000-000000000008"), "other-food", "Другое", 0 },
                    { new Guid("20000000-0000-0000-0000-000000000001"), "water", "Вода", 1 },
                    { new Guid("20000000-0000-0000-0000-000000000002"), "tea", "Чай", 1 },
                    { new Guid("20000000-0000-0000-0000-000000000003"), "coffee", "Кофе", 1 },
                    { new Guid("20000000-0000-0000-0000-000000000004"), "juice", "Сок", 1 },
                    { new Guid("20000000-0000-0000-0000-000000000005"), "milk-drink", "Молоко", 1 },
                    { new Guid("20000000-0000-0000-0000-000000000006"), "soda", "Газировка", 1 },
                    { new Guid("20000000-0000-0000-0000-000000000007"), "other-drink", "Другое", 1 },
                    { new Guid("30000000-0000-0000-0000-000000000001"), "soup", "Суп", 2 },
                    { new Guid("30000000-0000-0000-0000-000000000002"), "salad", "Салат", 2 },
                    { new Guid("30000000-0000-0000-0000-000000000003"), "main-dish", "Основное блюдо", 2 },
                    { new Guid("30000000-0000-0000-0000-000000000004"), "side-dish", "Гарнир", 2 },
                    { new Guid("30000000-0000-0000-0000-000000000005"), "dessert", "Десерт", 2 },
                    { new Guid("30000000-0000-0000-0000-000000000006"), "bakery", "Выпечка", 2 },
                    { new Guid("30000000-0000-0000-0000-000000000007"), "other-dish", "Другое", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000007"));

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
        }
    }
}
