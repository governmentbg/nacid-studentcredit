﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using StudentCredit.Persistence;

#nullable disable

namespace StudentCredit.Persistence.Migrations
{
    [DbContext(typeof(AppLogContext))]
    partial class AppLogContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("StudentCredit.Data.Logs.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("IP")
                        .HasColumnType("text")
                        .HasColumnName("ip");

                    b.Property<DateTime>("LogDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("logdate");

                    b.Property<string>("Message")
                        .HasColumnType("text")
                        .HasColumnName("message");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.Property<string>("Url")
                        .HasColumnType("text")
                        .HasColumnName("url");

                    b.Property<string>("UserAgent")
                        .HasColumnType("text")
                        .HasColumnName("useragent");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("userid");

                    b.Property<string>("Verb")
                        .HasColumnType("text")
                        .HasColumnName("verb");

                    b.HasKey("Id");

                    b.ToTable("log");
                });
#pragma warning restore 612, 618
        }
    }
}
