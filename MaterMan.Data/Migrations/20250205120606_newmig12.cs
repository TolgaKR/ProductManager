using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaterMan.Data.Migrations
{
    /// <inheritdoc />
    public partial class newmig12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ReceteBasliklar_MalzemeId",
                table: "ReceteBasliklar",
                column: "MalzemeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceteBasliklar_Malzemeler_MalzemeId",
                table: "ReceteBasliklar",
                column: "MalzemeId",
                principalTable: "Malzemeler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceteBasliklar_Malzemeler_MalzemeId",
                table: "ReceteBasliklar");

            migrationBuilder.DropIndex(
                name: "IX_ReceteBasliklar_MalzemeId",
                table: "ReceteBasliklar");
        }
    }
}
