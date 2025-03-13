﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Data.Migrations
{
    [DbContext(typeof(StoreContext))]
    [Migration("20250106031425_migration_v21")]
    partial class migration_v21
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("API.Entities.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DoctorName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("LastEditPerson")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("LastEditTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PatientName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("TheaterNumber")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("API.Entities.EventItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EditTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("UnitId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("ItemId");

                    b.HasIndex("UnitId");

                    b.ToTable("EventItems");
                });

            modelBuilder.Entity("API.Entities.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Img")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("API.Entities.OCRItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<string>("OCRKeyword")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("UnitId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("UnitId");

                    b.ToTable("OCRItems");
                });

            modelBuilder.Entity("API.Entities.Unit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Img")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("Units");
                });

            modelBuilder.Entity("API.Entities.EventItem", b =>
                {
                    b.HasOne("API.Entities.Event", "Event")
                        .WithMany("EventItems")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("API.Entities.Unit", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Event");

                    b.Navigation("Item");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("API.Entities.OCRItem", b =>
                {
                    b.HasOne("API.Entities.Item", "Item")
                        .WithMany("OCRItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.Unit", "Unit")
                        .WithMany("OCRItems")
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Item");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("API.Entities.Unit", b =>
                {
                    b.HasOne("API.Entities.Item", "Item")
                        .WithMany("Units")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("API.Entities.Event", b =>
                {
                    b.Navigation("EventItems");
                });

            modelBuilder.Entity("API.Entities.Item", b =>
                {
                    b.Navigation("OCRItems");

                    b.Navigation("Units");
                });

            modelBuilder.Entity("API.Entities.Unit", b =>
                {
                    b.Navigation("OCRItems");
                });
#pragma warning restore 612, 618
        }
    }
}
