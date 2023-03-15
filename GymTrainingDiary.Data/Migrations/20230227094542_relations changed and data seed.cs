using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymTrainingDiary.Data.Migrations
{
    /// <inheritdoc />
    public partial class relationschangedanddataseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Excercises_ExcerciseTypes_ExcerciseTypeId",
                table: "Excercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Sets_Excercises_ExcerciseId",
                table: "Sets");

            migrationBuilder.DropIndex(
                name: "IX_Sets_ExcerciseId",
                table: "Sets");

            migrationBuilder.RenameColumn(
                name: "ExcerciseTypeId",
                table: "Excercises",
                newName: "ExcerciseId");

            migrationBuilder.RenameIndex(
                name: "IX_Excercises_ExcerciseTypeId",
                table: "Excercises",
                newName: "IX_Excercises_ExcerciseId");

            migrationBuilder.AddColumn<int>(
                name: "WorkoutExerciseId",
                table: "Sets",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "ExcerciseTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Squat" },
                    { 2, "Leg press" },
                    { 3, "Dead lift" },
                    { 4, "Bench press" },
                    { 5, "Shoulder shrug" },
                    { 6, "Bench press" },
                    { 7, "Back extension" },
                    { 8, "Chest fly" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sets_WorkoutExerciseId",
                table: "Sets",
                column: "WorkoutExerciseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Excercises_ExcerciseTypes_ExcerciseId",
                table: "Excercises",
                column: "ExcerciseId",
                principalTable: "ExcerciseTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_Excercises_WorkoutExerciseId",
                table: "Sets",
                column: "WorkoutExerciseId",
                principalTable: "Excercises",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Excercises_ExcerciseTypes_ExcerciseId",
                table: "Excercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Sets_Excercises_WorkoutExerciseId",
                table: "Sets");

            migrationBuilder.DropIndex(
                name: "IX_Sets_WorkoutExerciseId",
                table: "Sets");

            migrationBuilder.DeleteData(
                table: "ExcerciseTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ExcerciseTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ExcerciseTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ExcerciseTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ExcerciseTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ExcerciseTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ExcerciseTypes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ExcerciseTypes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DropColumn(
                name: "WorkoutExerciseId",
                table: "Sets");

            migrationBuilder.RenameColumn(
                name: "ExcerciseId",
                table: "Excercises",
                newName: "ExcerciseTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Excercises_ExcerciseId",
                table: "Excercises",
                newName: "IX_Excercises_ExcerciseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Sets_ExcerciseId",
                table: "Sets",
                column: "ExcerciseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Excercises_ExcerciseTypes_ExcerciseTypeId",
                table: "Excercises",
                column: "ExcerciseTypeId",
                principalTable: "ExcerciseTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_Excercises_ExcerciseId",
                table: "Sets",
                column: "ExcerciseId",
                principalTable: "Excercises",
                principalColumn: "Id");
        }
    }
}
