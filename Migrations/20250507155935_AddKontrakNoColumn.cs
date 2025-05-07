using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMSTest.Migrations
{
    /// <inheritdoc />
    public partial class AddKontrakNoColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KontrakNo",
                table: "Kontrak",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KontrakNo",
                table: "Kontrak");
        }
    }
}
