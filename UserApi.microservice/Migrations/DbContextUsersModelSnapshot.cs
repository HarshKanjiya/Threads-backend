﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserApi.microservice.Data;

#nullable disable

namespace UserApi.microservice.Migrations
{
    [DbContext(typeof(DbContextUsers))]
    partial class DbContextUsersModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("UserApi.microservice.Models.DeviceModel", b =>
                {
                    b.Property<Guid>("DeviceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeviceType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserModelUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("DeviceId");

                    b.HasIndex("UserModelUserId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("UserApi.microservice.Models.OtpModel", b =>
                {
                    b.Property<Guid>("OtpId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("otp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OtpId");

                    b.ToTable("Otps");
                });

            modelBuilder.Entity("UserApi.microservice.Models.PasswordTokenModel", b =>
                {
                    b.Property<Guid>("TokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Token")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TokenId");

                    b.ToTable("PasswordTokens");
                });

            modelBuilder.Entity("UserApi.microservice.Models.UserModel", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AvatarPublicID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AvatarURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BanDuration")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("BanStartTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("BanStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Birthdate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FollowersCount")
                        .HasColumnType("int");

                    b.Property<int>("FollowingCount")
                        .HasColumnType("int");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostsCount")
                        .HasColumnType("int");

                    b.Property<int>("RepliesCount")
                        .HasColumnType("int");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Verified")
                        .HasColumnType("bit");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("UserApi.microservice.Models.DeviceModel", b =>
                {
                    b.HasOne("UserApi.microservice.Models.UserModel", null)
                        .WithMany("Devices")
                        .HasForeignKey("UserModelUserId");
                });

            modelBuilder.Entity("UserApi.microservice.Models.UserModel", b =>
                {
                    b.Navigation("Devices");
                });
#pragma warning restore 612, 618
        }
    }
}
