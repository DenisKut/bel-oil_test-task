﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using backend.Contexts;

#nullable disable

namespace backend.Migrations
{
    [DbContext(typeof(KinderContext))]
    partial class KinderContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("backend.Models.Child", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<long>("ContractId")
                        .HasColumnType("bigint");

                    b.Property<long>("GroupId")
                        .HasColumnType("bigint");

                    b.Property<double>("Growth")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("ParentInfoId")
                        .HasColumnType("bigint");

                    b.Property<string>("Passport")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("StateOfHealth")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Weight")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("ContractId");

                    b.HasIndex("GroupId");

                    b.HasIndex("ParentInfoId");

                    b.ToTable("Children");
                });

            modelBuilder.Entity("backend.Models.Contract", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("HeadId")
                        .HasColumnType("bigint");

                    b.Property<long>("HeadOfKindergartenId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("HeadOfKindergartenId");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("backend.Models.Educator", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Educators");
                });

            modelBuilder.Entity("backend.Models.Group", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("EducatorId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EducatorId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("backend.Models.HeadOfKindergarten", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Heads");
                });

            modelBuilder.Entity("backend.Models.ParentInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Father")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("FatherAge")
                        .HasColumnType("integer");

                    b.Property<string>("FatherProfession")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Mother")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MotherAge")
                        .HasColumnType("integer");

                    b.Property<string>("MotherProfession")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ParentInfos");
                });

            modelBuilder.Entity("backend.Models.Payment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("ChildId")
                        .HasColumnType("integer");

                    b.Property<long>("ChildId1")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateOfPayment")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("PaymentAmount")
                        .HasColumnType("double precision");

                    b.Property<string>("TypeOfPayment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ChildId1");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("backend.Models.Child", b =>
                {
                    b.HasOne("backend.Models.Contract", "Contract")
                        .WithMany("Children")
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.Models.Group", "Group")
                        .WithMany("Children")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.Models.ParentInfo", "ParentInfo")
                        .WithMany("Children")
                        .HasForeignKey("ParentInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contract");

                    b.Navigation("Group");

                    b.Navigation("ParentInfo");
                });

            modelBuilder.Entity("backend.Models.Contract", b =>
                {
                    b.HasOne("backend.Models.HeadOfKindergarten", "HeadOfKindergarten")
                        .WithMany("Contracts")
                        .HasForeignKey("HeadOfKindergartenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HeadOfKindergarten");
                });

            modelBuilder.Entity("backend.Models.Group", b =>
                {
                    b.HasOne("backend.Models.Educator", "Educator")
                        .WithMany("Groups")
                        .HasForeignKey("EducatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Educator");
                });

            modelBuilder.Entity("backend.Models.Payment", b =>
                {
                    b.HasOne("backend.Models.Child", "Child")
                        .WithMany("Payments")
                        .HasForeignKey("ChildId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Child");
                });

            modelBuilder.Entity("backend.Models.Child", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("backend.Models.Contract", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("backend.Models.Educator", b =>
                {
                    b.Navigation("Groups");
                });

            modelBuilder.Entity("backend.Models.Group", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("backend.Models.HeadOfKindergarten", b =>
                {
                    b.Navigation("Contracts");
                });

            modelBuilder.Entity("backend.Models.ParentInfo", b =>
                {
                    b.Navigation("Children");
                });
#pragma warning restore 612, 618
        }
    }
}