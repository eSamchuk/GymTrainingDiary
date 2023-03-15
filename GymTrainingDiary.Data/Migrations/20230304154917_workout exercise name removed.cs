using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymTrainingDiary.Data.Migrations
{
    /// <inheritdoc />
    public partial class workoutexercisenameremoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "WorkoutExercise");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "WorkoutExercise",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
