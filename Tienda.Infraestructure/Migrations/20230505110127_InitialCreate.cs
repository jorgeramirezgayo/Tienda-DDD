using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tienda.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "tienda");

            migrationBuilder.CreateTable(
                name: "ClienteTypes",
                schema: "tienda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                schema: "tienda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    precio = table.Column<double>(type: "float", nullable: false),
                    nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                schema: "tienda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    telefono = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    Direccion_Calle = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Direccion_Ciudad = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Direccion_Provincia = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Direccion_Pais = table.Column<string>(type: "varchar(35)", maxLength: 35, nullable: false),
                    Direccion_CodigoPostal = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false),
                    clienteType = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clientes_ClienteTypes_clienteType",
                        column: x => x.clienteType,
                        principalSchema: "tienda",
                        principalTable: "ClienteTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                schema: "tienda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    total = table.Column<double>(type: "float", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedidos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalSchema: "tienda",
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LineaPedidos",
                schema: "tienda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PedidoId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineaPedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LineaPedidos_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalSchema: "tienda",
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineaPedidos_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalSchema: "tienda",
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "tienda",
                table: "ClienteTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "premium" },
                    { 2, "standard" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_clienteType",
                schema: "tienda",
                table: "Clientes",
                column: "clienteType");

            migrationBuilder.CreateIndex(
                name: "IX_LineaPedidos_PedidoId",
                schema: "tienda",
                table: "LineaPedidos",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_LineaPedidos_ProductoId",
                schema: "tienda",
                table: "LineaPedidos",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ClienteId",
                schema: "tienda",
                table: "Pedidos",
                column: "ClienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LineaPedidos",
                schema: "tienda");

            migrationBuilder.DropTable(
                name: "Pedidos",
                schema: "tienda");

            migrationBuilder.DropTable(
                name: "Productos",
                schema: "tienda");

            migrationBuilder.DropTable(
                name: "Clientes",
                schema: "tienda");

            migrationBuilder.DropTable(
                name: "ClienteTypes",
                schema: "tienda");
        }
    }
}
