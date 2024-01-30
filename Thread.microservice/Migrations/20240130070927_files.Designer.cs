﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Thread.Data;

#nullable disable

namespace Thread.microservice.Migrations
{
    [DbContext(typeof(DBcontext))]
    [Migration("20240130070927_files")]
    partial class files
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Thread.Model.ThreadContent", b =>
                {
                    b.Property<Guid>("ContentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePublicIDs")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Files")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("RatingsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ContentId");

                    b.HasIndex("RatingsId");

                    b.ToTable("Contents");
                });

            modelBuilder.Entity("Thread.Model.ThreadContentOptions", b =>
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

            modelBuilder.Entity("Thread.Model.ThreadContentRatings", b =>
                {
                    b.Property<Guid>("RatingsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Responses")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalResponse")
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

                    b.Property<string>("ReplyAccess")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ThreadId");

                    b.HasIndex("ContentId");

                    b.ToTable("Threads");
                });

            modelBuilder.Entity("Thread.microservice.Model.Hashtags", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TagId"));

                    b.Property<string>("Tags")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TagId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Thread.microservice.Model.PollResponseModel", b =>
                {
                    b.Property<Guid>("PollResponseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Selection")
                        .HasColumnType("int");

                    b.Property<string>("ThreadId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PollResponseId");

                    b.ToTable("PollResponses");
                });

            modelBuilder.Entity("Thread.Model.ThreadContent", b =>
                {
                    b.HasOne("Thread.Model.ThreadContentRatings", "Ratings")
                        .WithMany()
                        .HasForeignKey("RatingsId");

                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("Thread.Model.ThreadContentOptions", b =>
                {
                    b.HasOne("Thread.Model.ThreadContent", null)
                        .WithMany("Options")
                        .HasForeignKey("ThreadContentContentId");
                });

            modelBuilder.Entity("Thread.Model.ThreadModel", b =>
                {
                    b.HasOne("Thread.Model.ThreadContent", "Content")
                        .WithMany()
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Content");
                });

            modelBuilder.Entity("Thread.Model.ThreadContent", b =>
                {
                    b.Navigation("Options");
                });
#pragma warning restore 612, 618
        }
    }
}
