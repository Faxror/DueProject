﻿// <auto-generated />
using System;
using DaireYonetimAPI.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DaireYonetimAPI.DataAccess.Migrations
{
    [DbContext(typeof(DaireDbContext))]
    [Migration("20230911113846_mig_update_configs")]
    partial class mig_update_configs
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DaireYönetimAPI.Entity.Bakiye", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<int?>("ApartmentId")
                        .HasColumnType("integer");

                    b.Property<int>("ApartmentNo")
                        .HasColumnType("integer");

                    b.Property<decimal>("Paid")
                        .HasColumnType("numeric");

                    b.HasKey("id");

                    b.ToTable("Bakiyes");
                });

            modelBuilder.Entity("DaireYönetimAPI.Entity.Config", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SmptEmailAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SmptEmailPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SmptSenderServers")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SmptSenderUsers")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SmptUsersMailTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Configs");
                });

            modelBuilder.Entity("DaireYönetimAPI.Entity.Daire", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<int?>("BakiyeId")
                        .HasColumnType("integer");

                    b.Property<string>("apartmentemail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("apartmentno")
                        .HasColumnType("integer");

                    b.Property<string>("familyname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("BakiyeId")
                        .IsUnique();

                    b.ToTable("Daires");
                });

            modelBuilder.Entity("DaireYönetimAPI.Entity.Daire", b =>
                {
                    b.HasOne("DaireYönetimAPI.Entity.Bakiye", "Bakiye")
                        .WithOne("Daire")
                        .HasForeignKey("DaireYönetimAPI.Entity.Daire", "BakiyeId");

                    b.Navigation("Bakiye");
                });

            modelBuilder.Entity("DaireYönetimAPI.Entity.Bakiye", b =>
                {
                    b.Navigation("Daire");
                });
#pragma warning restore 612, 618
        }
    }
}
