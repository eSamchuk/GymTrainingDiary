using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymTrainingDiary.Data.Migrations
{
    /// <inheritdoc />
    public partial class requiredequipmentmovedandoptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutExercise_Equipment_RequiredEquipmentId",
                table: "WorkoutExercise");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutExercise_RequiredEquipmentId",
                table: "WorkoutExercise");

            migrationBuilder.DropColumn(
                name: "RequiredEquipmentId",
                table: "WorkoutExercise");

            migrationBuilder.AddColumn<int>(
                name: "RequiredEquipmentId",
                table: "Excercises",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Excercises",
                keyColumn: "Id",
                keyValue: 1,
                column: "RequiredEquipmentId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Excercises",
                keyColumn: "Id",
                keyValue: 2,
                column: "RequiredEquipmentId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Excercises",
                keyColumn: "Id",
                keyValue: 3,
                column: "RequiredEquipmentId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Excercises",
                keyColumn: "Id",
                keyValue: 4,
                column: "RequiredEquipmentId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Excercises",
                keyColumn: "Id",
                keyValue: 5,
                column: "RequiredEquipmentId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Excercises",
                keyColumn: "Id",
                keyValue: 6,
                column: "RequiredEquipmentId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Excercises",
                keyColumn: "Id",
                keyValue: 7,
                column: "RequiredEquipmentId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Excercises",
                keyColumn: "Id",
                keyValue: 8,
                column: "RequiredEquipmentId",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Excercises_RequiredEquipmentId",
                table: "Excercises",
                column: "RequiredEquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Excercises_Equipment_RequiredEquipmentId",
                table: "Excercises",
                column: "RequiredEquipmentId",
                principalTable: "Equipment",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Excercises_Equipment_RequiredEquipmentId",
                table: "Excercises");

            migrationBuilder.DropIndex(
                name: "IX_Excercises_RequiredEquipmentId",
                table: "Excercises");

            migrationBuilder.DropColumn(
                name: "RequiredEquipmentId",
                table: "Excercises");

            migrationBuilder.AddColumn<int>(
                name: "RequiredEquipmentId",
                table: "WorkoutExercise",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutExercise_RequiredEquipmentId",
                table: "WorkoutExercise",
                column: "RequiredEquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutExercise_Equipment_RequiredEquipmentId",
                table: "WorkoutExercise",
                column: "RequiredEquipmentId",
                principalTable: "Equipment",
                principalColumn: "Id");
        }
    }
}
