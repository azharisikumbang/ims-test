using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMSTest.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kontrak",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClientName = table.Column<string>(type: "TEXT", nullable: false),
                    OTR = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kontrak", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Angsuran",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AngsuranKe = table.Column<int>(type: "INTEGER", nullable: false),
                    AngsuranPerBulan = table.Column<decimal>(type: "TEXT", nullable: false),
                    TanggalJatuhTempo = table.Column<DateTime>(type: "TEXT", nullable: false),
                    KontrakId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Angsuran", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Angsuran_Kontrak_KontrakId",
                        column: x => x.KontrakId,
                        principalTable: "Kontrak",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Angsuran_KontrakId",
                table: "Angsuran",
                column: "KontrakId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Angsuran");

            migrationBuilder.DropTable(
                name: "Kontrak");
        }
    }
}
