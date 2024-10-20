﻿// <auto-generated />
using System;
using KoishopRepositories.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KoishopRepositories.Migrations
{
    [DbContext(typeof(KoishopContext))]
    [Migration("20241020173957_move_quantity_field")]
    partial class move_quantity_field
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("KoishopBusinessObjects.Breed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("BreedName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("Personality")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("ScreeningRatio")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool?>("isDeleted")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Breeds");
                });

            modelBuilder.Entity("KoishopBusinessObjects.Consignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ConsignmentType")
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<int?>("UserID")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<bool?>("isDeleted")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("UserID");

                    b.ToTable("Consignments");
                });

            modelBuilder.Entity("KoishopBusinessObjects.ConsignmentItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("ConsignmentId")
                        .HasColumnType("integer");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("KoiFishId")
                        .HasColumnType("integer");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<bool?>("isDeleted")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("ConsignmentId");

                    b.HasIndex("KoiFishId");

                    b.ToTable("ConsignmentItems");
                });

            modelBuilder.Entity("KoishopBusinessObjects.FishCare", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CareId")
                        .HasColumnType("integer");

                    b.Property<string>("CareInstructions")
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("KoiFishId")
                        .HasColumnType("integer");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.Property<bool?>("isDeleted")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("KoiFishId");

                    b.HasIndex("UserId");

                    b.ToTable("FishCares");
                });

            modelBuilder.Entity("KoishopBusinessObjects.KoiFish", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<int?>("BreedId")
                        .HasColumnType("integer");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<decimal>("DailyFoodAmount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Gender")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<decimal>("ListPrice")
                        .HasColumnType("numeric");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Origin")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Personality")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Size")
                        .HasColumnType("numeric");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Type")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.Property<bool?>("isDeleted")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("BreedId");

                    b.HasIndex("UserId");

                    b.ToTable("KoiFishes");
                });

            modelBuilder.Entity("KoishopBusinessObjects.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("numeric");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.Property<bool?>("isDeleted")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("KoishopBusinessObjects.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("KoiFishId")
                        .HasColumnType("integer");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<int?>("OrderId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<bool?>("isDeleted")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("KoiFishId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("KoishopBusinessObjects.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("KoiFishId")
                        .HasColumnType("integer");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<int>("RatingValue")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<bool?>("isDeleted")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("KoiFishId");

                    b.HasIndex("UserId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("KoishopBusinessObjects.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("AspNetUsers", null, t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("KoishopBusinessObjects.Consignment", b =>
                {
                    b.HasOne("KoishopBusinessObjects.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("KoishopBusinessObjects.ConsignmentItem", b =>
                {
                    b.HasOne("KoishopBusinessObjects.Consignment", "Consignment")
                        .WithMany("ConsignmentItems")
                        .HasForeignKey("ConsignmentId");

                    b.HasOne("KoishopBusinessObjects.KoiFish", "KoiFish")
                        .WithMany("ConsignmentItems")
                        .HasForeignKey("KoiFishId");

                    b.Navigation("Consignment");

                    b.Navigation("KoiFish");
                });

            modelBuilder.Entity("KoishopBusinessObjects.FishCare", b =>
                {
                    b.HasOne("KoishopBusinessObjects.KoiFish", "KoiFish")
                        .WithMany("FishCare")
                        .HasForeignKey("KoiFishId");

                    b.HasOne("KoishopBusinessObjects.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("KoiFish");

                    b.Navigation("User");
                });

            modelBuilder.Entity("KoishopBusinessObjects.KoiFish", b =>
                {
                    b.HasOne("KoishopBusinessObjects.Breed", "Breed")
                        .WithMany("KoiFish")
                        .HasForeignKey("BreedId");

                    b.HasOne("KoishopBusinessObjects.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Breed");

                    b.Navigation("User");
                });

            modelBuilder.Entity("KoishopBusinessObjects.Order", b =>
                {
                    b.HasOne("KoishopBusinessObjects.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("KoishopBusinessObjects.OrderItem", b =>
                {
                    b.HasOne("KoishopBusinessObjects.KoiFish", "KoiFish")
                        .WithMany("OrderItems")
                        .HasForeignKey("KoiFishId");

                    b.HasOne("KoishopBusinessObjects.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId");

                    b.Navigation("KoiFish");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("KoishopBusinessObjects.Rating", b =>
                {
                    b.HasOne("KoishopBusinessObjects.KoiFish", "KoiFish")
                        .WithMany("Ratings")
                        .HasForeignKey("KoiFishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KoishopBusinessObjects.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KoiFish");

                    b.Navigation("User");
                });

            modelBuilder.Entity("KoishopBusinessObjects.Breed", b =>
                {
                    b.Navigation("KoiFish");
                });

            modelBuilder.Entity("KoishopBusinessObjects.Consignment", b =>
                {
                    b.Navigation("ConsignmentItems");
                });

            modelBuilder.Entity("KoishopBusinessObjects.KoiFish", b =>
                {
                    b.Navigation("ConsignmentItems");

                    b.Navigation("FishCare");

                    b.Navigation("OrderItems");

                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("KoishopBusinessObjects.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
