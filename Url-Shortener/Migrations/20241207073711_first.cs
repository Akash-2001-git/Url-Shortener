using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Url_Shortener.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShortenUrl",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LongUrl = table.Column<string>(type: "varchar(120)", unicode: false, maxLength: 120, nullable: false),
                    UniqueCode = table.Column<string>(type: "varchar(120)", unicode: false, maxLength: 120, nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(120)", unicode: false, maxLength: 120, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShortenUrl", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShortenUrl_UniqueCode",
                table: "ShortenUrl",
                column: "UniqueCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShortenUrl");
        }
    }
}
