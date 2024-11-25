using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestmentOrders.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstadoId",
                table: "Ordenes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EstadoOrdenId",
                table: "Ordenes",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_EstadoOrdenId",
                table: "Ordenes",
                column: "EstadoOrdenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ordenes_EstadosOrden_EstadoOrdenId",
                table: "Ordenes",
                column: "EstadoOrdenId",
                principalTable: "EstadosOrden",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ordenes_EstadosOrden_EstadoOrdenId",
                table: "Ordenes");

            migrationBuilder.DropIndex(
                name: "IX_Ordenes_EstadoOrdenId",
                table: "Ordenes");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "Ordenes");

            migrationBuilder.DropColumn(
                name: "EstadoOrdenId",
                table: "Ordenes");
        }
    }
}
