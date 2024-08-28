using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WPFExampleEF.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFieldLitera : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Litera",
                table: "Bonds");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Litera",
                table: "Bonds",
                type: "TEXT",
                nullable: true);
        }
    }
}
