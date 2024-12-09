using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webodev.Migrations
{
    public partial class deneme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Kullanicilars",
                columns: table => new
                {
                    KullanicilarID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanicilars", x => x.KullanicilarID);
                });

            migrationBuilder.CreateTable(
                name: "Salonlars",
                columns: table => new
                {
                    SalonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CalismaBaslangic = table.Column<TimeSpan>(type: "time", nullable: false),
                    CalismaBitis = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salonlars", x => x.SalonID);
                });

            migrationBuilder.CreateTable(
                name: "Calisanlars",
                columns: table => new
                {
                    CalisanlarID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalonID = table.Column<int>(type: "int", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calisanlars", x => x.CalisanlarID);
                    table.ForeignKey(
                        name: "FK_Calisanlars_Salonlars_SalonID",
                        column: x => x.SalonID,
                        principalTable: "Salonlars",
                        principalColumn: "SalonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hizmetlers",
                columns: table => new
                {
                    HizmetlerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sure = table.Column<int>(type: "int", nullable: false),
                    Ucret = table.Column<float>(type: "real", nullable: false),
                    SalonID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hizmetlers", x => x.HizmetlerID);
                    table.ForeignKey(
                        name: "FK_Hizmetlers_Salonlars_SalonID",
                        column: x => x.SalonID,
                        principalTable: "Salonlars",
                        principalColumn: "SalonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CalismaSaatleris",
                columns: table => new
                {
                    CalismaSaatleriID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CalisanlarID = table.Column<int>(type: "int", nullable: false),
                    Gun = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BaslangicSaati = table.Column<TimeSpan>(type: "time", nullable: false),
                    BitisSaati = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalismaSaatleris", x => x.CalismaSaatleriID);
                    table.ForeignKey(
                        name: "FK_CalismaSaatleris_Calisanlars_CalisanlarID",
                        column: x => x.CalisanlarID,
                        principalTable: "Calisanlars",
                        principalColumn: "CalisanlarID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CalisanUzmanliks",
    columns: table => new
    {
        CalisanUzmanlikID = table.Column<int>(type: "int", nullable: false)
            .Annotation("SqlServer:Identity", "1, 1"),
        CalisanlarID = table.Column<int>(type: "int", nullable: false),
        HizmetlerID = table.Column<int>(type: "int", nullable: false)
    },
    constraints: table =>
    {
        table.PrimaryKey("PK_CalisanUzmanliks", x => x.CalisanUzmanlikID);
        table.ForeignKey(
            name: "FK_CalisanUzmanliks_Calisanlars_CalisanlarID",
            column: x => x.CalisanlarID,
            principalTable: "Calisanlars",
            principalColumn: "CalisanlarID",
            onDelete: ReferentialAction.Cascade); // Birincil ilişki CASCADE bırakıldı.
        table.ForeignKey(
            name: "FK_CalisanUzmanliks_Hizmetlers_HizmetlerID",
            column: x => x.HizmetlerID,
            principalTable: "Hizmetlers",
            principalColumn: "HizmetlerID",
            onDelete: ReferentialAction.Restrict); // Diğer ilişki RESTRICT yapıldı.
    });

            migrationBuilder.CreateTable(
                name: "HizmetKategorisiLinks",
                columns: table => new
                {
                    HizmetLinkID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HizmetlerID = table.Column<int>(type: "int", nullable: false),
                    HizmetKtgID = table.Column<int>(type: "int", nullable: false),
                    HizmetKategorisileriHizmetKtgID = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
    name: "Randevulars",
    columns: table => new
    {
        RandevularID = table.Column<int>(type: "int", nullable: false)
            .Annotation("SqlServer:Identity", "1, 1"),
        KullanicilarID = table.Column<int>(type: "int", nullable: false),
        CalisanlarID = table.Column<int>(type: "int", nullable: false),
        HizmetlerID = table.Column<int>(type: "int", nullable: false),
        SalonID = table.Column<int>(type: "int", nullable: false),
        Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
        Saat = table.Column<TimeSpan>(type: "time", nullable: false),
        ToplamUcret = table.Column<float>(type: "real", nullable: false)
    },
    constraints: table =>
    {
        table.PrimaryKey("PK_Randevulars", x => x.RandevularID);

        // CalisanlarID ilişkisi
        table.ForeignKey(
            name: "FK_Randevulars_Calisanlars_CalisanlarID",
            column: x => x.CalisanlarID,
            principalTable: "Calisanlars",
            principalColumn: "CalisanlarID",
            onDelete: ReferentialAction.Cascade);

        // HizmetlerID ilişkisi
        table.ForeignKey(
            name: "FK_Randevulars_Hizmetlers_HizmetlerID",
            column: x => x.HizmetlerID,
            principalTable: "Hizmetlers",
            principalColumn: "HizmetlerID",
            onDelete: ReferentialAction.Restrict); // Çakışmayı önlemek için 'Restrict'

        // KullanicilarID ilişkisi
        table.ForeignKey(
            name: "FK_Randevulars_Kullanicilars_KullanicilarID",
            column: x => x.KullanicilarID,
            principalTable: "Kullanicilars",
            principalColumn: "KullanicilarID",
            onDelete: ReferentialAction.Cascade);

        // SalonID ilişkisi
        table.ForeignKey(
            name: "FK_Randevulars_Salonlars_SalonID",
            column: x => x.SalonID,
            principalTable: "Salonlars",
            principalColumn: "SalonID",
            onDelete: ReferentialAction.Restrict); // Çakışmayı önlemek için 'Restrict'
    });


            migrationBuilder.CreateIndex(
                name: "IX_Calisanlars_SalonID",
                table: "Calisanlars",
                column: "SalonID");

            migrationBuilder.CreateIndex(
                name: "IX_CalisanUzmanliks_CalisanlarID",
                table: "CalisanUzmanliks",
                column: "CalisanlarID");

            migrationBuilder.CreateIndex(
                name: "IX_CalisanUzmanliks_HizmetlerID",
                table: "CalisanUzmanliks",
                column: "HizmetlerID");

            migrationBuilder.CreateIndex(
                name: "IX_CalismaSaatleris_CalisanlarID",
                table: "CalismaSaatleris",
                column: "CalisanlarID");

            migrationBuilder.CreateIndex(
                name: "IX_HizmetKategorisiLinks_HizmetKategorisileriHizmetKtgID",
                table: "HizmetKategorisiLinks",
                column: "HizmetKategorisileriHizmetKtgID");

            migrationBuilder.CreateIndex(
                name: "IX_HizmetKategorisiLinks_HizmetlerID",
                table: "HizmetKategorisiLinks",
                column: "HizmetlerID");

            migrationBuilder.CreateIndex(
                name: "IX_Hizmetlers_SalonID",
                table: "Hizmetlers",
                column: "SalonID");

            migrationBuilder.CreateIndex(
                name: "IX_Randevulars_CalisanlarID",
                table: "Randevulars",
                column: "CalisanlarID");

            migrationBuilder.CreateIndex(
                name: "IX_Randevulars_HizmetlerID",
                table: "Randevulars",
                column: "HizmetlerID");

            migrationBuilder.CreateIndex(
                name: "IX_Randevulars_KullanicilarID",
                table: "Randevulars",
                column: "KullanicilarID");

            migrationBuilder.CreateIndex(
                name: "IX_Randevulars_SalonID",
                table: "Randevulars",
                column: "SalonID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalisanUzmanliks");

            migrationBuilder.DropTable(
                name: "CalismaSaatleris");

            migrationBuilder.DropTable(
                name: "HizmetKategorisiLinks");

            migrationBuilder.DropTable(
                name: "Randevulars");

            migrationBuilder.DropTable(
                name: "HizmetKategorileris");

            migrationBuilder.DropTable(
                name: "Calisanlars");

            migrationBuilder.DropTable(
                name: "Hizmetlers");

            migrationBuilder.DropTable(
                name: "Kullanicilars");

            migrationBuilder.DropTable(
                name: "Salonlars");
        }
    }
}