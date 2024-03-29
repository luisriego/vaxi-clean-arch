﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    apellido = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    email = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuarios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vehiculos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    modelo = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    vin = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    direccion_calle = table.Column<string>(type: "text", nullable: true),
                    direccion_ciudad = table.Column<string>(type: "text", nullable: true),
                    direccion_departamento = table.Column<string>(type: "text", nullable: true),
                    direccion_pais = table.Column<string>(type: "text", nullable: true),
                    direccion_provincia = table.Column<string>(type: "text", nullable: true),
                    precio_monto = table.Column<decimal>(type: "numeric", nullable: true),
                    precio_tipo_moneda = table.Column<string>(type: "text", nullable: true),
                    mantenimiento_monto = table.Column<decimal>(type: "numeric", nullable: true),
                    mantenimiento_tipo_moneda = table.Column<string>(type: "text", nullable: true),
                    fecha_ultimo_alquiler = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    accesorios = table.Column<int[]>(type: "integer[]", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vehiculos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "alquileres",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    vehiculo_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    precio_por_periodo_monto = table.Column<decimal>(type: "numeric", nullable: true),
                    precio_por_periodo_tipo_moneda = table.Column<string>(type: "text", nullable: true),
                    mantenimiento_monto = table.Column<decimal>(type: "numeric", nullable: true),
                    mantenimiento_tipo_moneda = table.Column<string>(type: "text", nullable: true),
                    accesorios_monto = table.Column<decimal>(type: "numeric", nullable: true),
                    accesorios_tipo_moneda = table.Column<string>(type: "text", nullable: true),
                    precio_total_monto = table.Column<decimal>(type: "numeric", nullable: true),
                    precio_total_tipo_moneda = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    duracion_inicio = table.Column<DateOnly>(type: "date", nullable: true),
                    duracion_fin = table.Column<DateOnly>(type: "date", nullable: true),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_confirmacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_denegacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_completado = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_cancelacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_alquileres", x => x.id);
                    table.ForeignKey(
                        name: "fk_alquileres_usuario_usuario_id",
                        column: x => x.user_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_alquileres_vehiculo_vehiculo_id",
                        column: x => x.vehiculo_id,
                        principalTable: "vehiculos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comentarios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    vehiculo_id = table.Column<Guid>(type: "uuid", nullable: false),
                    alquiler_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    clasificacion = table.Column<int>(type: "integer", nullable: false),
                    comentario_texto = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comentarios", x => x.id);
                    table.ForeignKey(
                        name: "fk_comentarios_alquileres_alquiler_id",
                        column: x => x.alquiler_id,
                        principalTable: "alquileres",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_comentarios_usuario_usuario_id",
                        column: x => x.user_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_comentarios_vehiculo_vehiculo_id",
                        column: x => x.vehiculo_id,
                        principalTable: "vehiculos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_alquileres_user_id",
                table: "alquileres",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_alquileres_vehiculo_id",
                table: "alquileres",
                column: "vehiculo_id");

            migrationBuilder.CreateIndex(
                name: "ix_comentarios_alquiler_id",
                table: "comentarios",
                column: "alquiler_id");

            migrationBuilder.CreateIndex(
                name: "ix_comentarios_user_id",
                table: "comentarios",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_comentarios_vehiculo_id",
                table: "comentarios",
                column: "vehiculo_id");

            migrationBuilder.CreateIndex(
                name: "ix_usuarios_email",
                table: "usuarios",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comentarios");

            migrationBuilder.DropTable(
                name: "alquileres");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "vehiculos");
        }
    }
}
