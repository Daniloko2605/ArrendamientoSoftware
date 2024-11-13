using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArrendamientoSoftware.Web.Migrations
{
    /// <inheritdoc />
    public partial class MakeTipoNonNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Propiedades_Descripcion",
                table: "Propiedades");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Propiedades",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Propiedades_Descripcion",
                table: "Propiedades",
                column: "Descripcion",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Propiedades_Descripcion",
                table: "Propiedades");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Propiedades",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Propiedades_Descripcion",
                table: "Propiedades",
                column: "Descripcion",
                unique: true,
                filter: "[Descripcion] IS NOT NULL");
        }
    }
}
