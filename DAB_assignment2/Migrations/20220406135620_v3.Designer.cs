﻿// <auto-generated />
using System;
using DAB_assignment2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAB_assignment2.Migrations
{
    [DbContext(typeof(MunicipalityDbContext))]
    [Migration("20220406135620_v3")]
    partial class v3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-preview.2.22153.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DAB_assignment2.Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("LocationId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("RoomId")
                        .HasColumnType("int");

                    b.Property<string>("SocietyId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TimespanId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("RoomId");

                    b.HasIndex("SocietyId");

                    b.HasIndex("TimespanId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("DAB_assignment2.Models.Chairman", b =>
                {
                    b.Property<string>("CPR")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CPR");

                    b.ToTable("Chairmen");
                });

            modelBuilder.Entity("DAB_assignment2.Models.Location", b =>
                {
                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("PeopleLimit")
                        .HasColumnType("int");

                    b.Property<string>("Properties")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Address");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("DAB_assignment2.Models.LocationTimespan", b =>
                {
                    b.Property<string>("LocationId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TimespanId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LocationId", "TimespanId");

                    b.HasIndex("TimespanId");

                    b.ToTable("LocationTimespans");
                });

            modelBuilder.Entity("DAB_assignment2.Models.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.HasKey("Id");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("DAB_assignment2.Models.MemberSociety", b =>
                {
                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<string>("SocietyId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("MemberId", "SocietyId");

                    b.HasIndex("SocietyId");

                    b.ToTable("MemberSocieties");
                });

            modelBuilder.Entity("DAB_assignment2.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("LocationId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("PeopleLimit")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("DAB_assignment2.Models.RoomTimespan", b =>
                {
                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<string>("TimespanId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RoomId", "TimespanId");

                    b.HasIndex("TimespanId");

                    b.ToTable("RoomTimespans");
                });

            modelBuilder.Entity("DAB_assignment2.Models.Society", b =>
                {
                    b.Property<string>("CVR")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Activity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChairmanId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CVR");

                    b.HasIndex("ChairmanId");

                    b.ToTable("Societies");
                });

            modelBuilder.Entity("DAB_assignment2.Models.Timespan", b =>
                {
                    b.Property<string>("Span")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Span");

                    b.ToTable("Timespans");
                });

            modelBuilder.Entity("DAB_assignment2.Models.Booking", b =>
                {
                    b.HasOne("DAB_assignment2.Models.Location", "Location")
                        .WithMany("Bookings")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAB_assignment2.Models.Room", "Room")
                        .WithMany("Bookings")
                        .HasForeignKey("RoomId");

                    b.HasOne("DAB_assignment2.Models.Society", "Society")
                        .WithMany("Bookings")
                        .HasForeignKey("SocietyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAB_assignment2.Models.Timespan", "Timespan")
                        .WithMany("Bookings")
                        .HasForeignKey("TimespanId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("Room");

                    b.Navigation("Society");

                    b.Navigation("Timespan");
                });

            modelBuilder.Entity("DAB_assignment2.Models.LocationTimespan", b =>
                {
                    b.HasOne("DAB_assignment2.Models.Location", "Location")
                        .WithMany("Availability")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAB_assignment2.Models.Timespan", "Timespan")
                        .WithMany("Locations")
                        .HasForeignKey("TimespanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("Timespan");
                });

            modelBuilder.Entity("DAB_assignment2.Models.MemberSociety", b =>
                {
                    b.HasOne("DAB_assignment2.Models.Member", "Member")
                        .WithMany("Societies")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAB_assignment2.Models.Society", "Society")
                        .WithMany("Members")
                        .HasForeignKey("SocietyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");

                    b.Navigation("Society");
                });

            modelBuilder.Entity("DAB_assignment2.Models.Room", b =>
                {
                    b.HasOne("DAB_assignment2.Models.Location", "Location")
                        .WithMany("Rooms")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("DAB_assignment2.Models.RoomTimespan", b =>
                {
                    b.HasOne("DAB_assignment2.Models.Room", "Room")
                        .WithMany("Availability")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAB_assignment2.Models.Timespan", "Timespan")
                        .WithMany("Rooms")
                        .HasForeignKey("TimespanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");

                    b.Navigation("Timespan");
                });

            modelBuilder.Entity("DAB_assignment2.Models.Society", b =>
                {
                    b.HasOne("DAB_assignment2.Models.Chairman", "Chairman")
                        .WithMany("Societies")
                        .HasForeignKey("ChairmanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chairman");
                });

            modelBuilder.Entity("DAB_assignment2.Models.Chairman", b =>
                {
                    b.Navigation("Societies");
                });

            modelBuilder.Entity("DAB_assignment2.Models.Location", b =>
                {
                    b.Navigation("Availability");

                    b.Navigation("Bookings");

                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("DAB_assignment2.Models.Member", b =>
                {
                    b.Navigation("Societies");
                });

            modelBuilder.Entity("DAB_assignment2.Models.Room", b =>
                {
                    b.Navigation("Availability");

                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("DAB_assignment2.Models.Society", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Members");
                });

            modelBuilder.Entity("DAB_assignment2.Models.Timespan", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Locations");

                    b.Navigation("Rooms");
                });
#pragma warning restore 612, 618
        }
    }
}