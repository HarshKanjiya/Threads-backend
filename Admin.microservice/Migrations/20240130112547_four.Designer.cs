﻿// <auto-generated />
using System;
using Admin.microservice.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Admin.microservice.Migrations
{
    [DbContext(typeof(DBcontext))]
    [Migration("20240130112547_four")]
    partial class four
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Admin.microservice.Model.BugReportModel", b =>
                {
                    b.Property<Guid>("ReportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReportId");

                    b.ToTable("BugReports");
                });

            modelBuilder.Entity("Admin.microservice.Model.CustomReportModel", b =>
                {
                    b.Property<Guid>("ReportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReportId");

                    b.ToTable("CustomReports");
                });

            modelBuilder.Entity("Admin.microservice.Model.FilesModel", b =>
                {
                    b.Property<string>("FileId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid?>("BugReportModelReportId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("filePublicId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fileURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FileId");

                    b.HasIndex("BugReportModelReportId");

                    b.ToTable("FilesModel");
                });

            modelBuilder.Entity("Admin.microservice.Model.ReportCategoryModel", b =>
                {
                    b.Property<Guid>("ReportCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReportCategoryId");

                    b.ToTable("ReportCategories");
                });

            modelBuilder.Entity("Admin.microservice.Model.ReportModel", b =>
                {
                    b.Property<Guid>("ReportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ReportCategoryModelReportCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReportId");

                    b.HasIndex("ReportCategoryModelReportCategoryId");

                    b.ToTable("AvailableReports");
                });

            modelBuilder.Entity("Admin.microservice.Model.UserReportModel", b =>
                {
                    b.Property<Guid>("UserReportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ReportId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserReportId");

                    b.ToTable("UserReports");
                });

            modelBuilder.Entity("Admin.microservice.Model.FilesModel", b =>
                {
                    b.HasOne("Admin.microservice.Model.BugReportModel", null)
                        .WithMany("Files")
                        .HasForeignKey("BugReportModelReportId");
                });

            modelBuilder.Entity("Admin.microservice.Model.ReportModel", b =>
                {
                    b.HasOne("Admin.microservice.Model.ReportCategoryModel", null)
                        .WithMany("Reports")
                        .HasForeignKey("ReportCategoryModelReportCategoryId");
                });

            modelBuilder.Entity("Admin.microservice.Model.BugReportModel", b =>
                {
                    b.Navigation("Files");
                });

            modelBuilder.Entity("Admin.microservice.Model.ReportCategoryModel", b =>
                {
                    b.Navigation("Reports");
                });
#pragma warning restore 612, 618
        }
    }
}
