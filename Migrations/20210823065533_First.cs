using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Valkimia.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Valkimia");

            migrationBuilder.CreateTable(
                name: "Ciudades",
                schema: "Valkimia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciudades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                schema: "Valkimia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Domicilio = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CiudadId = table.Column<int>(type: "int", nullable: false),
                    Habilitado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clientes_Ciudades_CiudadId",
                        column: x => x.CiudadId,
                        principalSchema: "Valkimia",
                        principalTable: "Ciudades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Facturas",
                schema: "Valkimia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Detalle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Importe = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Facturas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalSchema: "Valkimia",
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Valkimia",
                table: "Ciudades",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "San Miguel de Tucumán" },
                    { 2, "Salta" },
                    { 3, "Rosario" },
                    { 4, "La Plata" },
                    { 5, "La Pampa" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_CiudadId",
                schema: "Valkimia",
                table: "Clientes",
                column: "CiudadId");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_ClienteId",
                schema: "Valkimia",
                table: "Facturas",
                column: "ClienteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Facturas",
                schema: "Valkimia");

            migrationBuilder.DropTable(
                name: "Clientes",
                schema: "Valkimia");

            migrationBuilder.DropTable(
                name: "Ciudades",
                schema: "Valkimia");
        }
    }
}
