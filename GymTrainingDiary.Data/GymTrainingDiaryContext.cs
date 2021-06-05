using GymTrainingDiary.Data.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymTrainingDiary.Data
{
    public class GymTrainingDiaryContext : DbContext
    {
        public GymTrainingDiaryContext() : base("GymTrainingDiaryContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer(new DbInitializer());
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasPrecision(18, 0);


            modelBuilder.Entity<PhotoContainer>().HasKey(x => x.Id)
               .Property(x => x.Id)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
               .HasPrecision(18, 0);

            modelBuilder.Entity<Excercise>().Property(x => x.Id)
                .HasPrecision(18, 0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<ExcerciseType>().HasKey(x => x.Id).Property(x => x.Id)
                .HasPrecision(18, 0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Training>().HasKey(x => x.Id).Property(x => x.Id)
                .HasPrecision(18, 0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<CommonType>().HasKey(x => x.Id).Property(x => x.Id)
                .HasPrecision(18, 0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            modelBuilder.Entity<User>().HasOptional(x => x.PhotoContainer).WithRequired(x => x.User);


            modelBuilder.Entity<User>().HasMany(x => x.UserTrainings).WithRequired(x => x.User);
            modelBuilder.Entity<User>().HasRequired(x => x.AccountType);

            modelBuilder.Entity<Training>().HasMany(x => x.TrainingExcercises).WithRequired(x => x.Training);
            modelBuilder.Entity<Excercise>().HasRequired(x => x.ExcerciseType);
            modelBuilder.Entity<ExcerciseType>().HasMany(x => x.Excercises).WithRequired(x => x.ExcerciseType);



        }

        public DbSet<User> Users { get; set; }

        public DbSet<PhotoContainer> PhotoContainers { get; set; }
        public DbSet<Training> Trainings { get; set; }

        public DbSet<CommonType> CommonTypes { get; set; }

        public DbSet<Excercise> Excercises { get; set; }
        public DbSet<ExcerciseType> ExcerciseTypes { get; set; }



    }
}
