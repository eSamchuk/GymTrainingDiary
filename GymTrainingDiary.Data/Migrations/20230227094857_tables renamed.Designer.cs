﻿// <auto-generated />
using System;
using GymTrainingDiary.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GymTrainingDiary.Data.Migrations
{
    [DbContext(typeof(GymTrainingDataContext))]
    [Migration("20230227094857_tables renamed")]
    partial class tablesrenamed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GymTrainingDiary.Data.Entities.Equipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Equipment");
                });

            modelBuilder.Entity("GymTrainingDiary.Data.Entities.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Excercises");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Squat"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Leg press"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Dead lift"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Bench press"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Shoulder shrug"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Bench press"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Back extension"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Chest fly"
                        });
                });

            modelBuilder.Entity("GymTrainingDiary.Data.Entities.Set", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ExcerciseId")
                        .HasColumnType("int");

                    b.Property<int>("Reps")
                        .HasColumnType("int");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.Property<int?>("WorkoutExerciseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkoutExerciseId");

                    b.ToTable("Sets");
                });

            modelBuilder.Entity("GymTrainingDiary.Data.Entities.Trainer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Trainers");
                });

            modelBuilder.Entity("GymTrainingDiary.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SignupDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TrainerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TrainerId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GymTrainingDiary.Data.Entities.Workout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Duration")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("int")
                        .HasComputedColumnSql("(datediff(minute,[WorkoutStart],[WorkoutEnd]))");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("WorkoutEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("WorkoutStart")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Workouts");
                });

            modelBuilder.Entity("GymTrainingDiary.Data.Entities.WorkoutExercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ExcerciseId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RequiredEquipmentId")
                        .HasColumnType("int");

                    b.Property<int>("WorkoutId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExcerciseId");

                    b.HasIndex("RequiredEquipmentId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("WorkoutExercise");
                });

            modelBuilder.Entity("GymTrainingDiary.Data.Entities.Set", b =>
                {
                    b.HasOne("GymTrainingDiary.Data.Entities.WorkoutExercise", "WorkoutExercise")
                        .WithMany("Sets")
                        .HasForeignKey("WorkoutExerciseId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("WorkoutExercise");
                });

            modelBuilder.Entity("GymTrainingDiary.Data.Entities.User", b =>
                {
                    b.HasOne("GymTrainingDiary.Data.Entities.Trainer", "Trainer")
                        .WithMany("ActiveAssignedUsers")
                        .HasForeignKey("TrainerId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Trainer");
                });

            modelBuilder.Entity("GymTrainingDiary.Data.Entities.Workout", b =>
                {
                    b.HasOne("GymTrainingDiary.Data.Entities.User", "User")
                        .WithMany("Workouts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("GymTrainingDiary.Data.Entities.WorkoutExercise", b =>
                {
                    b.HasOne("GymTrainingDiary.Data.Entities.Exercise", "Excercise")
                        .WithMany("WorkoutExercises")
                        .HasForeignKey("ExcerciseId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("GymTrainingDiary.Data.Entities.Equipment", "RequiredEquipment")
                        .WithMany("AvailableExercises")
                        .HasForeignKey("RequiredEquipmentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("GymTrainingDiary.Data.Entities.Workout", "Workout")
                        .WithMany("Excercises")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Excercise");

                    b.Navigation("RequiredEquipment");

                    b.Navigation("Workout");
                });

            modelBuilder.Entity("GymTrainingDiary.Data.Entities.Equipment", b =>
                {
                    b.Navigation("AvailableExercises");
                });

            modelBuilder.Entity("GymTrainingDiary.Data.Entities.Exercise", b =>
                {
                    b.Navigation("WorkoutExercises");
                });

            modelBuilder.Entity("GymTrainingDiary.Data.Entities.Trainer", b =>
                {
                    b.Navigation("ActiveAssignedUsers");
                });

            modelBuilder.Entity("GymTrainingDiary.Data.Entities.User", b =>
                {
                    b.Navigation("Workouts");
                });

            modelBuilder.Entity("GymTrainingDiary.Data.Entities.Workout", b =>
                {
                    b.Navigation("Excercises");
                });

            modelBuilder.Entity("GymTrainingDiary.Data.Entities.WorkoutExercise", b =>
                {
                    b.Navigation("Sets");
                });
#pragma warning restore 612, 618
        }
    }
}
