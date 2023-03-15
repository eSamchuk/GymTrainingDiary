using GymTrainingDiary.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymTrainingDiary.Data
{
    public class GymTrainingDataContext : DbContext
    {
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkoutExercise> WorkoutExercise { get; set; }
        public DbSet<Exercise> Excercises { get; set; }
        public DbSet<Set> Sets { get; set; }
        public DbSet<Equipment> Equipment { get; set; }

        public GymTrainingDataContext(DbContextOptions<GymTrainingDataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging(false);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trainer>()
                .HasMany(x => x.ActiveAssignedUsers)
                .WithOne(x => x.Trainer)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Workouts)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Workout>()
               .HasMany(x => x.Excercises)
               .WithOne(x => x.Workout)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<WorkoutExercise>()
                .HasMany(x => x.Sets)
                .WithOne(x => x.WorkoutExercise)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Exercise>()
                .HasMany(x => x.WorkoutExercises)
                .WithOne(x => x.Excercise)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Exercise>()
               .HasOne(x => x.RequiredEquipment)
               .WithMany(x => x.AvailableExercises)
               .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<Equipment>()
            //    .HasMany(x => x.AvailableExercises)
            //    .WithOne(x => x.RequiredEquipment);

            modelBuilder.Entity<Workout>()
                .Property(x => x.Duration)
                .HasComputedColumnSql("(datediff(minute,[WorkoutStart],[WorkoutEnd]))");

            this.SeedInitData(modelBuilder);
        }

        private void SeedInitData(ModelBuilder builder)
        {
            builder.Entity<Exercise>().HasData(
                new Exercise() { Id = 1, Name = "Squat", RequiredEquipmentId = 2 },
                new Exercise() { Id = 2, Name = "Leg press", RequiredEquipmentId = 2 },
                new Exercise() { Id = 3, Name = "Dead lift", RequiredEquipmentId = 2 },
                new Exercise() { Id = 4, Name = "Bench press", RequiredEquipmentId = 2 },
                new Exercise() { Id = 5, Name = "Shoulder shrug", RequiredEquipmentId = 2 },
                new Exercise() { Id = 6, Name = "Bench press", RequiredEquipmentId = 2 },
                new Exercise() { Id = 7, Name = "Back extension", RequiredEquipmentId = 2 },
                new Exercise() { Id = 8, Name = "Chest fly", RequiredEquipmentId = 2 }
                );
        }
    }
}
