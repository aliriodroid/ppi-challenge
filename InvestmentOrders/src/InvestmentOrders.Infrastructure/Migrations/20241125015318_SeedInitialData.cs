using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestmentOrders.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TiposActivo",
                columns: new[] { "Id", "Descripcion" },
                values: new object[,]
                {
                    { 1, "Acción" },
                    { 2, "Bono" },
                    { 3, "FCI" }
                });
            
            migrationBuilder.InsertData(
                table: "EstadosOrden",
                columns: new[] { "Id", "DescripcionEstado" },
                values: new object[,]
                {
                    { 0, "En proceso" },
                    { 1, "Ejecutada" },
                    { 3, "Cancelada" }
                });
            
            migrationBuilder.InsertData(
                table: "Activos",
                columns: new[] { "Id", "Ticker", "Nombre", "TipoActivoId", "PrecioUnitario" },
                values: new object[,]
                {
                    { 1, "AAPL", "Apple", 1, 177.97m },
                    { 2, "GOOGL", "Alphabet Inc", 1, 138.21m },
                    { 3, "MSFT", "Microsoft", 1, 329.04m },
                    { 4, "KO", "Coca Cola", 1, 58.3m },
                    { 5, "WMT", "Walmart", 1, 163.42m },
                    { 6, "AL30", "BONOS ARGENTINA USD 2030 L.A", 2, 307.4m },
                    { 7, "GD30", "Bonos Globales Argentina USD Step Up 2030", 2, 336.1m },
                    { 8, "Delta.Pesos", "Delta Pesos Clase A", 3, 0.0181m },
                    { 9, "Fima.Premium", "Fima Premium Clase A", 3, 0.0317m }
                });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Activos",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });

            migrationBuilder.DeleteData(
                table: "TiposActivo",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3 });

            migrationBuilder.DeleteData(
                table: "EstadosOrden",
                keyColumn: "Id",
                keyValues: new object[] { 0, 1, 3 });

        }
    }
}
