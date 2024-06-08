﻿// <auto-generated />
using System;
using Blog_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Blog_Management_System.Migrations
{
    [DbContext(typeof(BlogManagementSystemDbContext))]
    partial class BlogManagementSystemDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Blog_Management_System.Models.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentId"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("ForumId")
                        .HasColumnType("int");

                    b.Property<int>("Like")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CommentId");

                    b.HasIndex("ForumId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Blog_Management_System.Models.Forum", b =>
                {
                    b.Property<int>("ForumId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ForumId"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("CategoriesId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created_at")
                        .HasColumnType("datetime2");

                    b.Property<int>("Like")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ForumId");

                    b.HasIndex("UserId");

                    b.ToTable("Forums");
                });

            modelBuilder.Entity("Blog_Management_System.Models.Tags.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Alien"
                        },
                        new
                        {
                            Id = 2,
                            Name = "UFO"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Dog"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Cat"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Nasa"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Zombie"
                        });
                });

            modelBuilder.Entity("Blog_Management_System.Models.Tags.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("Blog_Management_System.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CategoryForum", b =>
                {
                    b.Property<int>("CategoriesId")
                        .HasColumnType("int");

                    b.Property<int>("ForumsForumId")
                        .HasColumnType("int");

                    b.HasKey("CategoriesId", "ForumsForumId");

                    b.HasIndex("ForumsForumId");

                    b.ToTable("CategoryForum");
                });

            modelBuilder.Entity("ForumStatus", b =>
                {
                    b.Property<int>("ForumsForumId")
                        .HasColumnType("int");

                    b.Property<int>("StatusesId")
                        .HasColumnType("int");

                    b.HasKey("ForumsForumId", "StatusesId");

                    b.HasIndex("StatusesId");

                    b.ToTable("ForumStatus");
                });

            modelBuilder.Entity("Blog_Management_System.Models.Comment", b =>
                {
                    b.HasOne("Blog_Management_System.Models.Forum", "Forum")
                        .WithMany("Comments")
                        .HasForeignKey("ForumId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Blog_Management_System.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Forum");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Blog_Management_System.Models.Forum", b =>
                {
                    b.HasOne("Blog_Management_System.Models.User", "User")
                        .WithMany("Forums")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CategoryForum", b =>
                {
                    b.HasOne("Blog_Management_System.Models.Tags.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Blog_Management_System.Models.Forum", null)
                        .WithMany()
                        .HasForeignKey("ForumsForumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ForumStatus", b =>
                {
                    b.HasOne("Blog_Management_System.Models.Forum", null)
                        .WithMany()
                        .HasForeignKey("ForumsForumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Blog_Management_System.Models.Tags.Status", null)
                        .WithMany()
                        .HasForeignKey("StatusesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Blog_Management_System.Models.Forum", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("Blog_Management_System.Models.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Forums");
                });
#pragma warning restore 612, 618
        }
    }
}
