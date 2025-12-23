using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeatExchangeCounterflow.Migrations
{
    /// <inheritdoc />
    public partial class AddCalculationRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LayerPoints_Calculations_CalculationId",
                table: "LayerPoints");

            migrationBuilder.AlterColumn<int>(
                name: "CalculationId",
                table: "LayerPoints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LayerPoints_Calculations_CalculationId",
                table: "LayerPoints",
                column: "CalculationId",
                principalTable: "Calculations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LayerPoints_Calculations_CalculationId",
                table: "LayerPoints");

            migrationBuilder.AlterColumn<int>(
                name: "CalculationId",
                table: "LayerPoints",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_LayerPoints_Calculations_CalculationId",
                table: "LayerPoints",
                column: "CalculationId",
                principalTable: "Calculations",
                principalColumn: "Id");
        }
    }
}
