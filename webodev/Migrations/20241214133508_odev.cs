using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webodev.Migrations
{
    /// <inheritdoc />
    public partial class odev : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HizmetKategorisiLinks");

            migrationBuilder.DropTable(
                name: "HizmetKategorileris");

            migrationBuilder.DropColumn(
                name: "Telefon",
                table: "Salonlars");

            migrationBuilder.DropColumn(
                name: "Telefon",
                table: "Calisanlars");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Telefon",
                table: "Salonlars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telefon",
                table: "Calisanlars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "HizmetKategorileris",
                columns: table => new
                {
                    HizmetKtgID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HizmetKategorileris", x => x.HizmetKtgID);
                });

            migrationBuilder.CreateTable(
                name: "HizmetKategorisiLinks",
                columns: table => new
                {
                    HizmetLinkID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HizmetKategorisileriHizmetKtgID = table.Column<int>(type: "int", nullable: false),
                    HizmetlerID = table.Column<int>(type: "int", nullable: false),
                    HizmetKtgID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HizmetKategorisiLinks", x => x.HizmetLinkID);
                    table.ForeignKey(
                        name: "FK_HizmetKategorisiLinks_HizmetKategorileris_HizmetKategorisileriHizmetKtgID",
                        column: x => x.HizmetKategorisileriHizmetKtgID,
                        principalTable: "HizmetKategorileris",
                        principalColumn: "HizmetKtgID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HizmetKategorisiLinks_Hizmetlers_HizmetlerID",
                        column: x => x.HizmetlerID,
                        principalTable: "Hizmetlers",
                        principalColumn: "HizmetlerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HizmetKategorisiLinks_HizmetKategorisileriHizmetKtgID",
                table: "HizmetKategorisiLinks",
                column: "HizmetKategorisileriHizmetKtgID");

            migrationBuilder.CreateIndex(
                name: "IX_HizmetKategorisiLinks_HizmetlerID",
                table: "HizmetKategorisiLinks",
                column: "HizmetlerID");
        }
    }
}
