﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StackOverflow25.Data;

namespace StackOverflow25.Data.Migrations
{
    [DbContext(typeof(QuestionsDataContext))]
    [Migration("20220425021845_UpdatedAnswers")]
    partial class UpdatedAnswers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.16")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StackOverflow25.Data.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatePosted")
                        .HasColumnType("datetime2");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("StackOverflow25.Data.Like", b =>
                {
                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("QuestionId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("StackOverflow25.Data.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatePosted")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("StackOverflow25.Data.QuestionsTags", b =>
                {
                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("QuestionId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("QuestionsTags");
                });

            modelBuilder.Entity("StackOverflow25.Data.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("StackOverflow25.Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("StackOverflow25.Data.Answer", b =>
                {
                    b.HasOne("StackOverflow25.Data.Question", null)
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("StackOverflow25.Data.Like", b =>
                {
                    b.HasOne("StackOverflow25.Data.Question", "Question")
                        .WithMany("Likes")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("StackOverflow25.Data.User", "User")
                        .WithMany("Likes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StackOverflow25.Data.Question", b =>
                {
                    b.HasOne("StackOverflow25.Data.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("StackOverflow25.Data.QuestionsTags", b =>
                {
                    b.HasOne("StackOverflow25.Data.Question", "Question")
                        .WithMany("QuestionsTags")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("StackOverflow25.Data.Tag", "Tag")
                        .WithMany("QuestionsTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("StackOverflow25.Data.Question", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("Likes");

                    b.Navigation("QuestionsTags");
                });

            modelBuilder.Entity("StackOverflow25.Data.Tag", b =>
                {
                    b.Navigation("QuestionsTags");
                });

            modelBuilder.Entity("StackOverflow25.Data.User", b =>
                {
                    b.Navigation("Likes");
                });
#pragma warning restore 612, 618
        }
    }
}
