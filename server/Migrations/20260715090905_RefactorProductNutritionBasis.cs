using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class RefactorProductNutritionBasis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServingSizeUnit",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "SugarPer100",
                table: "Products",
                newName: "Sugar");

            migrationBuilder.RenameColumn(
                name: "ServingDescriprion",
                table: "Products",
                newName: "ServingDescription");

            migrationBuilder.RenameColumn(
                name: "ProteinPer100",
                table: "Products",
                newName: "Protein");

            migrationBuilder.RenameColumn(
                name: "IronMgPer100",
                table: "Products",
                newName: "IronMg");

            migrationBuilder.RenameColumn(
                name: "FiberPer100",
                table: "Products",
                newName: "Fiber");

            migrationBuilder.RenameColumn(
                name: "FatPer100",
                table: "Products",
                newName: "NutritionAmount");

            migrationBuilder.RenameColumn(
                name: "CarbsPer100",
                table: "Products",
                newName: "Fat");

            migrationBuilder.RenameColumn(
                name: "CaloriesPer100",
                table: "Products",
                newName: "Carbs");

            migrationBuilder.AddColumn<double>(
                name: "Calories",
                table: "Products",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "NutritionUnit",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calories",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "NutritionUnit",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Sugar",
                table: "Products",
                newName: "SugarPer100");

            migrationBuilder.RenameColumn(
                name: "ServingDescription",
                table: "Products",
                newName: "ServingDescriprion");

            migrationBuilder.RenameColumn(
                name: "Protein",
                table: "Products",
                newName: "ProteinPer100");

            migrationBuilder.RenameColumn(
                name: "NutritionAmount",
                table: "Products",
                newName: "FatPer100");

            migrationBuilder.RenameColumn(
                name: "IronMg",
                table: "Products",
                newName: "IronMgPer100");

            migrationBuilder.RenameColumn(
                name: "Fiber",
                table: "Products",
                newName: "FiberPer100");

            migrationBuilder.RenameColumn(
                name: "Fat",
                table: "Products",
                newName: "CarbsPer100");

            migrationBuilder.RenameColumn(
                name: "Carbs",
                table: "Products",
                newName: "CaloriesPer100");

            migrationBuilder.AddColumn<int>(
                name: "ServingSizeUnit",
                table: "Products",
                type: "integer",
                nullable: true);
        }
    }
}
