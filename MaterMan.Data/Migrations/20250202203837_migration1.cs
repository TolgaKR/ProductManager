using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaterMan.Data.Migrations
{
    /// <inheritdoc />
    public partial class migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ReceteKalemler_ReceteBaslikId",
                table: "ReceteKalemler");

            migrationBuilder.CreateIndex(
                name: "IX_ReceteKalemler_ReceteBaslikId",
                table: "ReceteKalemler",
                column: "ReceteBaslikId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ReceteKalemler_ReceteBaslikId",
                table: "ReceteKalemler");

            migrationBuilder.CreateIndex(
                name: "IX_ReceteKalemler_ReceteBaslikId",
                table: "ReceteKalemler",
                column: "ReceteBaslikId",
                unique: true);
        }
    }
}
