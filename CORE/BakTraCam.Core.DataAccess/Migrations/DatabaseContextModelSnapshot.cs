﻿// <auto-generated />
using System;
using BakTraCam.Core.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BakTraCam.Core.DataAccess.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7");

            modelBuilder.Entity("BakTraCam.Core.Entity.BakimEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Aciklama")
                        .HasColumnType("TEXT");

                    b.Property<string>("Ad")
                        .HasColumnType("TEXT");

                    b.Property<string>("Gerceklestiren1")
                        .HasColumnType("TEXT");

                    b.Property<string>("Gerceklestiren2")
                        .HasColumnType("TEXT");

                    b.Property<string>("Gerceklestiren3")
                        .HasColumnType("TEXT");

                    b.Property<string>("Gerceklestiren4")
                        .HasColumnType("TEXT");

                    b.Property<int>("Oncelik")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Sorumlu1")
                        .HasColumnType("TEXT");

                    b.Property<string>("Sorumlu2")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Tarihi")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("tBakim");
                });

            modelBuilder.Entity("BakTraCam.Core.Entity.KullaniciEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Ad")
                        .HasColumnType("TEXT");

                    b.Property<int>("UnvanId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("tKullanici");
                });
#pragma warning restore 612, 618
        }
    }
}
