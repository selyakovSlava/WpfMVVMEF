using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WPFExampleEF.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldLitera : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Litera",
                table: "Bonds",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Litera",
                table: "Bonds");
        }
    }
}
