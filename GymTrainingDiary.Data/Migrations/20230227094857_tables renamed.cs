using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymTrainingDiary.Data.Migrations
{
    /// <inheritdoc />
    public partial class tablesrenamed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Excercises_Equipment_RequiredEquipmentId",
                table: "Excercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Excercises_ExcerciseTypes_ExcerciseId",
                table: "Excercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Excercises_Workouts_WorkoutId",
                table: "Excercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Sets_Excercises_WorkoutExerciseId",
                table: "Sets");

            migrationBuilder.DropTable(
                name: "ExcerciseTypes");

            migrationBuilder.DropIndex(
                name: "IX_Excercises_ExcerciseId",
                table: "Excercises");

            migrationBuilder.DropIndex(
                name: "IX_Excercises_RequiredEquipmentId",
                table: "Excercises");

            migrationBuilder.DropIndex(
                name: "IX_Excercises_WorkoutId",
                table: "Excercises");

            migrationBuilder.DropColumn(
                name: "ExcerciseId",
                table: "Excercises");

            migrationBuilder.DropColumn(
                name: "RequiredEquipmentId",
                table: "Excercises");

            migrationBuilder.DropColumn(
                name: "WorkoutId",
                table: "Excercises");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Excercises",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "WorkoutExercise",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExcerciseId = table.Column<int>(type: "int", nullable: false),
                    RequiredEquipmentId = table.Column<int>(type: "int", nullable: false),
                    WorkoutId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutExercise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutExercise_Equipment_RequiredEquipmentId",
                        column: x => x.RequiredEquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkoutExercise_Excercises_ExcerciseId",
                        column: x => x.ExcerciseId,
                        principalTable: "Excercises",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkoutExercise_Workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workouts",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Excercises",
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
                name: "IX_WorkoutExercise_ExcerciseId",
                table: "WorkoutExercise",
                column: "ExcerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutExercise_RequiredEquipmentId",
                table: "WorkoutExercise",
                column: "RequiredEquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutExercise_WorkoutId",
                table: "WorkoutExercise",
                column: "WorkoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_WorkoutExercise_WorkoutExerciseId",
                table: "Sets",
                column: "WorkoutExerciseId",
                principalTable: "WorkoutExercise",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sets_WorkoutExercise_WorkoutExerciseId",
                table: "Sets");

            migrationBuilder.DropTable(
                name: "WorkoutExercise");

            migrationBuilder.DeleteData(
                table: "Excercises",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Excercises",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Excercises",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Excercises",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Excercises",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Excercises",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Excercises",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Excercises",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Excercises",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExcerciseId",
                table: "Excercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RequiredEquipmentId",
                table: "Excercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorkoutId",
                table: "Excercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ExcerciseTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcerciseTypes", x => x.Id);
                });

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
                name: "IX_Excercises_ExcerciseId",
                table: "Excercises",
                column: "ExcerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_Excercises_RequiredEquipmentId",
                table: "Excercises",
                column: "RequiredEquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Excercises_WorkoutId",
                table: "Excercises",
                column: "WorkoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_Excercises_Equipment_RequiredEquipmentId",
                table: "Excercises",
                column: "RequiredEquipmentId",
                principalTable: "Equipment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Excercises_ExcerciseTypes_ExcerciseId",
                table: "Excercises",
                column: "ExcerciseId",
                principalTable: "ExcerciseTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Excercises_Workouts_WorkoutId",
                table: "Excercises",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_Excercises_WorkoutExerciseId",
                table: "Sets",
                column: "WorkoutExerciseId",
                principalTable: "Excercises",
                principalColumn: "Id");
        }
    }
}
