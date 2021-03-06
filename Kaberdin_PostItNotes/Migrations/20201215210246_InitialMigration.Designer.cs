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
    [Migration("20201215210246_InitialMigration")]
    partial class InitialMigration
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

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DueTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<int?>("WorkColumnModelWorkColumnId")
                        .HasColumnType("int");

                    b.HasKey("StickerId");

                    b.HasIndex("WorkColumnModelWorkColumnId");

                    b.ToTable("Stickers");
                });

            modelBuilder.Entity("Kaberdin_PostItNotes.Data.Models.WorkColumnModel", b =>
                {
                    b.Property<int>("WorkColumnId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.HasKey("WorkColumnId");

                    b.ToTable("WorkColumns");
                });

            modelBuilder.Entity("Kaberdin_PostItNotes.Data.Models.StickerModel", b =>
                {
                    b.HasOne("Kaberdin_PostItNotes.Data.Models.WorkColumnModel", null)
                        .WithMany("Stickers")
                        .HasForeignKey("WorkColumnModelWorkColumnId");
                });

            modelBuilder.Entity("Kaberdin_PostItNotes.Data.Models.WorkColumnModel", b =>
                {
                    b.Navigation("Stickers");
                });
#pragma warning restore 612, 618
        }
    }
}
