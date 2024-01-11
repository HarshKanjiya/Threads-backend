﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Thread.Data;

#nullable disable

namespace Thread.microservice.Migrations
{
    [DbContext(typeof(DBcontext))]
    partial class DBcontextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Thread.Constants.ThreadContent", b =>
                {
                    b.Property<Guid>("ContentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Images")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MyProperty")
                        .HasColumnType("int");

                    b.Property<Guid?>("RatingsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ContentId");

                    b.HasIndex("RatingsId");

                    b.ToTable("ThreadContent");
                });

            modelBuilder.Entity("Thread.Constants.ThreadContentOptions", b =>
                {
                    b.Property<Guid>("OptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Option")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ThreadContentContentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OptionId");

                    b.HasIndex("ThreadContentContentId");

                    b.ToTable("ThreadContentOptions");
                });

            modelBuilder.Entity("Thread.Constants.ThreadContentRatings", b =>
                {
                    b.Property<Guid>("RatingsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ResponseCounts")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Total")
                        .HasColumnType("int");

                    b.HasKey("RatingsId");

                    b.ToTable("ThreadContentRatings");
                });

            modelBuilder.Entity("Thread.Model.ThreadModel", b =>
                {
                    b.Property<Guid>("ThreadId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AuthorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ContentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ReferenceId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReplyAccess")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("ThreadId");

                    b.HasIndex("ContentId");

                    b.ToTable("Threads");
                });

            modelBuilder.Entity("Thread.Constants.ThreadContent", b =>
                {
                    b.HasOne("Thread.Constants.ThreadContentRatings", "Ratings")
                        .WithMany()
                        .HasForeignKey("RatingsId");

                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("Thread.Constants.ThreadContentOptions", b =>
                {
                    b.HasOne("Thread.Constants.ThreadContent", null)
                        .WithMany("Options")
                        .HasForeignKey("ThreadContentContentId");
                });

            modelBuilder.Entity("Thread.Model.ThreadModel", b =>
                {
                    b.HasOne("Thread.Constants.ThreadContent", "Content")
                        .WithMany()
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Content");
                });

            modelBuilder.Entity("Thread.Constants.ThreadContent", b =>
                {
                    b.Navigation("Options");
                });
#pragma warning restore 612, 618
        }
    }
}
