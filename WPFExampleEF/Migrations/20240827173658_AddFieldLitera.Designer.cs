﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WPFExampleEF.Classes;

#nullable disable

namespace WPFExampleEF.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240827173658_AddFieldLitera")]
    partial class AddFieldLitera
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("WPFExampleEF.Models.BondModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("AccumulatedCouponIncome")
                        .HasColumnType("REAL");

                    b.Property<int>("Count")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Litera")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<string>("SecId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Bonds");
                });
#pragma warning restore 612, 618
        }
    }
}
