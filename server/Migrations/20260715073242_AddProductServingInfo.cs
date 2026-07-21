using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class AddProductServingInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ServingDescriprion",
                table: "Products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ServingSize",
                table: "Products",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServingSizeUnit",
                table: "Products",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServingDescriprion",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ServingSize",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ServingSizeUnit",
                table: "Products");
        }
    }
}
