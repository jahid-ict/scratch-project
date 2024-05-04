﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScratchProject.Api.DatabaseContext;

#nullable disable

namespace ScratchProject.Api.Migrations
{
    [DbContext(typeof(ApplicationDatabaseContext))]
    [Migration("20240407182050_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ScratchProject.Api.Models.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CityName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d4e62ad2-30b6-4fc0-96ce-d43720101b4e"),
                            CityName = "Cumilla"
                        },
                        new
                        {
                            Id = new Guid("5a662592-e02e-4220-8deb-5909c95c6498"),
                            CityName = "Dhaka"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}