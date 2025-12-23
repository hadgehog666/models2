using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeatExchangeCounterflow.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalculationInput",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LayerHeightRaw = table.Column<string>(type: "TEXT", nullable: false),
                    InitialMaterialTemp = table.Column<double>(type: "REAL", nullable: false),
                    InitialGasTemp = table.Column<double>(type: "REAL", nullable: false),
                    GasVelocity = table.Column<double>(type: "REAL", nullable: false),
                    GasHeatCapacity = table.Column<double>(type: "REAL", nullable: false),
                    MaterialFlow = table.Column<double>(type: "REAL", nullable: false),
                    MaterialHeatCapacity = table.Column<double>(type: "REAL", nullable: false),
                    AlphaV = table.Column<double>(type: "REAL", nullable: false),
                    ApparatusDiameter = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculationInput", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Calculations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    InputId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calculations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calculations_CalculationInput_InputId",
                        column: x => x.InputId,
                        principalTable: "CalculationInput",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LayerPoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Y = table.Column<double>(type: "REAL", nullable: false),
                    RelativeY = table.Column<double>(type: "REAL", nullable: false),
                    Theta1 = table.Column<double>(type: "REAL", nullable: false),
                    Theta2 = table.Column<double>(type: "REAL", nullable: false),
                    ThetaMaterial = table.Column<double>(type: "REAL", nullable: false),
                    ThetaGas = table.Column<double>(type: "REAL", nullable: false),
                    MaterialTemp = table.Column<double>(type: "REAL", nullable: false),
                    GasTemp = table.Column<double>(type: "REAL", nullable: false),
                    DeltaTemp = table.Column<double>(type: "REAL", nullable: false),
                    CalculationId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LayerPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LayerPoints_Calculations_CalculationId",
                        column: x => x.CalculationId,
                        principalTable: "Calculations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calculations_InputId",
                table: "Calculations",
                column: "InputId");

            migrationBuilder.CreateIndex(
                name: "IX_LayerPoints_CalculationId",
                table: "LayerPoints",
                column: "CalculationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LayerPoints");

            migrationBuilder.DropTable(
                name: "Calculations");

            migrationBuilder.DropTable(
                name: "CalculationInput");
        }
    }
}
