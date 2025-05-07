using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMSTest.Migrations
{
    /// <inheritdoc />
    public partial class AddPaidToAngsuranTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Paid",
                table: "Angsuran",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Paid",
                table: "Angsuran");
        }
    }
}
