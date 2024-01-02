﻿// <auto-generated />
using System;
using LaunchMyHub.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LaunchMyHub.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LaunchMyHub.Models.HubMaster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BriefDescription")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ContactName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<byte>("FeeType")
                        .HasColumnType("tinyint");

                    b.Property<int?>("HubTypeId")
                        .HasColumnType("int");

                    b.Property<string>("HubTypeOther")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("NonProfit")
                        .HasColumnType("bit");

                    b.Property<string>("OrganizationName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PreferredSubdomain")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("ReferralSourceId")
                        .HasColumnType("int");

                    b.Property<string>("ReferralSourceOther")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Website")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("HubTypeId");

                    b.HasIndex("ReferralSourceId");

                    b.ToTable("HubMasters");
                });

            modelBuilder.Entity("LaunchMyHub.Models.HubType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("HubTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Description for Type A",
                            Name = "Type A"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Description for Type B",
                            Name = "Type B"
                        });
                });

            modelBuilder.Entity("LaunchMyHub.Models.ReferralSource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("ReferralSources");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Description for Source A",
                            Name = "Source A"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Description for Source B",
                            Name = "Source B"
                        });
                });

            modelBuilder.Entity("LaunchMyHub.Models.HubMaster", b =>
                {
                    b.HasOne("LaunchMyHub.Models.HubType", "HubType")
                        .WithMany()
                        .HasForeignKey("HubTypeId");

                    b.HasOne("LaunchMyHub.Models.ReferralSource", "ReferralSource")
                        .WithMany()
                        .HasForeignKey("ReferralSourceId");

                    b.Navigation("HubType");

                    b.Navigation("ReferralSource");
                });
#pragma warning restore 612, 618
        }
    }
}
