﻿// <auto-generated />
using System;
using DFP.CORE;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DFP.CORE.DBMigrations
{
    [DbContext(typeof(FileContext))]
    [Migration("20231202200720_ChangesName")]
    partial class ChangesName
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DFP.CORE.FileEntity", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("FileData")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("PTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RTime")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("ResultData")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<DateTime>("UTime")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("ID")
                        .IsUnique();

                    b.HasIndex("State");

                    b.HasIndex("UTime");

                    b.ToTable("Files");
                });
#pragma warning restore 612, 618
        }
    }
}
