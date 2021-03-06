﻿// <auto-generated />
using System;
using Kaberdin_PostItNotes.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Kaberdin_PostItNotes.Migrations
{
    [DbContext(typeof(DefaultDbContext))]
    [Migration("20201216191502_Height")]
    partial class Height
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Kaberdin_PostItNotes.Data.Models.StickerModel", b =>
                {
                    b.Property<int>("StickerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("Color")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Height")
                        .HasColumnType("int");

                    b.Property<int?>("PositionX")
                        .HasColumnType("int");

                    b.Property<int?>("PositionY")
                        .HasColumnType("int");

                    b.Property<int?>("Width")
                        .HasColumnType("int");

                    b.HasKey("StickerId");

                    b.ToTable("Stickers");
                });
#pragma warning restore 612, 618
        }
    }
}
