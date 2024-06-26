﻿// <auto-generated />
using System;
using MeasurementInfrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MeasurementInfrastructure.Migrations
{
    [DbContext(typeof(MeasurementDbContext))]
    [Migration("20240429151955_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Measurement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Diastolic")
                        .HasColumnType("int");

                    b.Property<bool>("IsSeen")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("PatientSSN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Systolic")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Measurements");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Date = new DateTime(2024, 4, 29, 17, 19, 55, 178, DateTimeKind.Local).AddTicks(7049),
                            Diastolic = 60,
                            IsSeen = false,
                            PatientSSN = "123",
                            Systolic = 60
                        },
                        new
                        {
                            Id = 2,
                            Date = new DateTime(2024, 4, 29, 17, 19, 55, 178, DateTimeKind.Local).AddTicks(7104),
                            Diastolic = 30,
                            IsSeen = false,
                            PatientSSN = "1234",
                            Systolic = 20
                        },
                        new
                        {
                            Id = 3,
                            Date = new DateTime(2024, 4, 29, 17, 19, 55, 178, DateTimeKind.Local).AddTicks(7107),
                            Diastolic = 40,
                            IsSeen = false,
                            PatientSSN = "12345",
                            Systolic = 30
                        },
                        new
                        {
                            Id = 4,
                            Date = new DateTime(2024, 4, 29, 17, 19, 55, 178, DateTimeKind.Local).AddTicks(7110),
                            Diastolic = 55,
                            IsSeen = false,
                            PatientSSN = "123456",
                            Systolic = 70
                        },
                        new
                        {
                            Id = 5,
                            Date = new DateTime(2024, 4, 29, 17, 19, 55, 178, DateTimeKind.Local).AddTicks(7112),
                            Diastolic = 60,
                            IsSeen = false,
                            PatientSSN = "1234567",
                            Systolic = 80
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
