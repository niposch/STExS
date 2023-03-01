﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repositories;

#nullable disable

namespace Repositories.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Common.Models.Authentication.ApplicationRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Common.Models.Authentication.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("MatrikelNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Common.Models.ExerciseSystem.BaseExercise", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AchievablePoints")
                        .HasColumnType("int");

                    b.Property<Guid>("ChapterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExerciseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExerciseType")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("RunningNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChapterId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Exercises");

                    b.HasDiscriminator<int>("ExerciseType");
                });

            modelBuilder.Entity("Common.Models.ExerciseSystem.Chapter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ChapterDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChapterName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ModuleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("RunningNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ModuleId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Chapters");
                });

            modelBuilder.Entity("Common.Models.ExerciseSystem.Module", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ArchivedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MaxParticipants")
                        .HasColumnType("int");

                    b.Property<string>("ModuleDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModuleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Modules");
                });

            modelBuilder.Entity("Common.Models.ExerciseSystem.ModuleParticipation", b =>
                {
                    b.Property<Guid>("ModuleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsFavorite")
                        .HasColumnType("bit");

                    b.Property<bool>("ParticipationConfirmed")
                        .HasColumnType("bit");

                    b.HasKey("ModuleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("ModuleParticipations");
                });

            modelBuilder.Entity("Common.Models.ExerciseSystem.Parson.ParsonElement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Indentation")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ParsonSolutionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RelatedSolutionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("RunningNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("ParsonSolutionId");

                    b.HasIndex("RelatedSolutionId");

                    b.ToTable("ParsonElements");
                });

            modelBuilder.Entity("Common.Models.ExerciseSystem.Parson.ParsonSolution", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IndentationIsRelevant")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RelatedExerciseId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("RelatedExerciseId")
                        .IsUnique();

                    b.ToTable("ParsonSolutions");
                });

            modelBuilder.Entity("Common.Models.Grading.BaseSubmission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ExerciseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("GradingResultId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SubmissionType")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GradingResultId")
                        .IsUnique()
                        .HasFilter("[GradingResultId] IS NOT NULL");

                    b.HasIndex("UserId", "ExerciseId");

                    b.ToTable("Submissions");

                    b.HasDiscriminator<int>("SubmissionType");
                });

            modelBuilder.Entity("Common.Models.Grading.GradingResult", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("AppealDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FinalAppealDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("GradedSubmissionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("GradingState")
                        .HasColumnType("int");

                    b.Property<bool>("IsAutomaticallyGraded")
                        .HasColumnType("bit");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<Guid?>("UserSubmissionExerciseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserSubmissionUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserSubmissionUserId", "UserSubmissionExerciseId");

                    b.ToTable("GradingResults");
                });

            modelBuilder.Entity("Common.Models.Grading.TimeTrack", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CloseDateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ExerciseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId", "ExerciseId");

                    b.ToTable("TimeTracks");
                });

            modelBuilder.Entity("Common.Models.Grading.UserSubmission", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ExerciseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("FinalSubmissionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "ExerciseId");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("FinalSubmissionId");

                    b.ToTable("UserSubmissions");
                });

            modelBuilder.Entity("Common.Models.WeatherForecast", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ArchivedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TemperatureC")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("WeatherForecasts");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Common.Models.ExerciseSystem.Cloze.ClozeTextExercise", b =>
                {
                    b.HasBaseType("Common.Models.ExerciseSystem.BaseExercise");

                    b.Property<string>("TextWithAnswers")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue(40);
                });

            modelBuilder.Entity("Common.Models.ExerciseSystem.CodeOutput.CodeOutputExercise", b =>
                {
                    b.HasBaseType("Common.Models.ExerciseSystem.BaseExercise");

                    b.Property<string>("ExpectedAnswer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsMultiLineResponse")
                        .HasColumnType("bit");

                    b.HasDiscriminator().HasValue(60);
                });

            modelBuilder.Entity("Common.Models.ExerciseSystem.CodeOutput.CodeOutputSubmission", b =>
                {
                    b.HasBaseType("Common.Models.Grading.BaseSubmission");

                    b.Property<string>("SubmittedAnswer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue(60);
                });

            modelBuilder.Entity("Common.Models.ExerciseSystem.Parson.ParsonExercise", b =>
                {
                    b.HasBaseType("Common.Models.ExerciseSystem.BaseExercise");

                    b.HasDiscriminator().HasValue(20);
                });

            modelBuilder.Entity("Common.Models.ExerciseSystem.BaseExercise", b =>
                {
                    b.HasOne("Common.Models.ExerciseSystem.Chapter", "Chapter")
                        .WithMany("Exercises")
                        .HasForeignKey("ChapterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Common.Models.Authentication.ApplicationUser", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Chapter");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Common.Models.ExerciseSystem.Chapter", b =>
                {
                    b.HasOne("Common.Models.ExerciseSystem.Module", "Module")
                        .WithMany("Chapters")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Common.Models.Authentication.ApplicationUser", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Module");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Common.Models.ExerciseSystem.Module", b =>
                {
                    b.HasOne("Common.Models.Authentication.ApplicationUser", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Common.Models.ExerciseSystem.ModuleParticipation", b =>
                {
                    b.HasOne("Common.Models.ExerciseSystem.Module", "Module")
                        .WithMany("ModuleParticipations")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Common.Models.Authentication.ApplicationUser", "User")
                        .WithMany("ModuleParticipations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Module");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Common.Models.ExerciseSystem.Parson.ParsonElement", b =>
                {
                    b.HasOne("Common.Models.Authentication.ApplicationUser", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Common.Models.ExerciseSystem.Parson.ParsonSolution", null)
                        .WithMany("CodeElements")
                        .HasForeignKey("ParsonSolutionId");

                    b.HasOne("Common.Models.ExerciseSystem.Parson.ParsonSolution", "RelatedSolution")
                        .WithMany()
                        .HasForeignKey("RelatedSolutionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");

                    b.Navigation("RelatedSolution");
                });

            modelBuilder.Entity("Common.Models.ExerciseSystem.Parson.ParsonSolution", b =>
                {
                    b.HasOne("Common.Models.Authentication.ApplicationUser", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Common.Models.ExerciseSystem.Parson.ParsonExercise", "RelatedExercise")
                        .WithOne("ExpectedSolution")
                        .HasForeignKey("Common.Models.ExerciseSystem.Parson.ParsonSolution", "RelatedExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");

                    b.Navigation("RelatedExercise");
                });

            modelBuilder.Entity("Common.Models.Grading.BaseSubmission", b =>
                {
                    b.HasOne("Common.Models.Grading.GradingResult", "GradingResult")
                        .WithOne("GradedSubmission")
                        .HasForeignKey("Common.Models.Grading.BaseSubmission", "GradingResultId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Common.Models.Grading.UserSubmission", "UserSubmission")
                        .WithMany("Submissions")
                        .HasForeignKey("UserId", "ExerciseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("GradingResult");

                    b.Navigation("UserSubmission");
                });

            modelBuilder.Entity("Common.Models.Grading.GradingResult", b =>
                {
                    b.HasOne("Common.Models.Grading.UserSubmission", null)
                        .WithMany("GradingResults")
                        .HasForeignKey("UserSubmissionUserId", "UserSubmissionExerciseId");
                });

            modelBuilder.Entity("Common.Models.Grading.TimeTrack", b =>
                {
                    b.HasOne("Common.Models.Authentication.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Common.Models.Grading.UserSubmission", "UserSubmission")
                        .WithMany("TimeTracks")
                        .HasForeignKey("UserId", "ExerciseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("UserSubmission");
                });

            modelBuilder.Entity("Common.Models.Grading.UserSubmission", b =>
                {
                    b.HasOne("Common.Models.ExerciseSystem.BaseExercise", "Exercise")
                        .WithMany("UserSubmissions")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Common.Models.Grading.BaseSubmission", "FinalSubmission")
                        .WithMany()
                        .HasForeignKey("FinalSubmissionId");

                    b.HasOne("Common.Models.Authentication.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");

                    b.Navigation("FinalSubmission");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Common.Models.WeatherForecast", b =>
                {
                    b.HasOne("Common.Models.Authentication.ApplicationUser", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Common.Models.Authentication.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Common.Models.Authentication.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Common.Models.Authentication.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Common.Models.Authentication.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Common.Models.Authentication.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Common.Models.Authentication.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Common.Models.Authentication.ApplicationUser", b =>
                {
                    b.Navigation("ModuleParticipations");
                });

            modelBuilder.Entity("Common.Models.ExerciseSystem.BaseExercise", b =>
                {
                    b.Navigation("UserSubmissions");
                });

            modelBuilder.Entity("Common.Models.ExerciseSystem.Chapter", b =>
                {
                    b.Navigation("Exercises");
                });

            modelBuilder.Entity("Common.Models.ExerciseSystem.Module", b =>
                {
                    b.Navigation("Chapters");

                    b.Navigation("ModuleParticipations");
                });

            modelBuilder.Entity("Common.Models.ExerciseSystem.Parson.ParsonSolution", b =>
                {
                    b.Navigation("CodeElements");
                });

            modelBuilder.Entity("Common.Models.Grading.GradingResult", b =>
                {
                    b.Navigation("GradedSubmission");
                });

            modelBuilder.Entity("Common.Models.Grading.UserSubmission", b =>
                {
                    b.Navigation("GradingResults");

                    b.Navigation("Submissions");

                    b.Navigation("TimeTracks");
                });

            modelBuilder.Entity("Common.Models.ExerciseSystem.Parson.ParsonExercise", b =>
                {
                    b.Navigation("ExpectedSolution")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
