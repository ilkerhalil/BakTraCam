using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BakTraCam.Core.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bakims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Aciklama = table.Column<string>(type: "TEXT", nullable: true),
                    Ad = table.Column<string>(type: "TEXT", nullable: true),
                    BaslangicTarihi = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BitisTarihi = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Tarihi = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Gerceklestiren1 = table.Column<string>(type: "TEXT", nullable: true),
                    Gerceklestiren2 = table.Column<string>(type: "TEXT", nullable: true),
                    Gerceklestiren3 = table.Column<string>(type: "TEXT", nullable: true),
                    Gerceklestiren4 = table.Column<string>(type: "TEXT", nullable: true),
                    Sorumlu1 = table.Column<string>(type: "TEXT", nullable: true),
                    Sorumlu2 = table.Column<string>(type: "TEXT", nullable: true),
                    Period = table.Column<int>(type: "INTEGER", nullable: false),
                    Durum = table.Column<int>(type: "INTEGER", nullable: false),
                    Tip = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bakims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Maintenance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Gerceklestiren1 = table.Column<int>(type: "INTEGER", nullable: false),
                    Gerceklestiren2 = table.Column<int>(type: "INTEGER", nullable: false),
                    Gerceklestiren3 = table.Column<int>(type: "INTEGER", nullable: false),
                    Gerceklestiren4 = table.Column<int>(type: "INTEGER", nullable: false),
                    Sorumlu1 = table.Column<int>(type: "INTEGER", nullable: false),
                    Sorumlu2 = table.Column<int>(type: "INTEGER", nullable: false),
                    Period = table.Column<int>(type: "INTEGER", nullable: false),
                    Durum = table.Column<int>(type: "INTEGER", nullable: false),
                    Tip = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintenance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Aciklama = table.Column<string>(type: "TEXT", nullable: true),
                    Tarihi = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    UnvanId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bakims");

            migrationBuilder.DropTable(
                name: "Maintenance");

            migrationBuilder.DropTable(
                name: "Notice");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
