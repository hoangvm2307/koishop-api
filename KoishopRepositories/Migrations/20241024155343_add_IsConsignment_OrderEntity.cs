using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KoishopRepositories.Migrations
{
    /// <inheritdoc />
    public partial class add_IsConsignment_OrderEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsConsignment",
                table: "Orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConsignment",
                table: "Orders");
        }
    }
}
