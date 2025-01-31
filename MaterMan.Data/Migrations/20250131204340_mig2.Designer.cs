﻿// <auto-generated />
using System;
using MaterMan.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MaterMan.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250131204340_mig2")]
    partial class mig2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Fiyat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("EskiFiyat")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("EskiFiyatTarih")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("GuncelFiyat")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("MalzemeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("YeniFiyatTarih")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("MalzemeId");

                    b.ToTable("Fiyatlar");
                });

            modelBuilder.Entity("MaterMan.Entity.AppRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("MaterMan.Entity.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Department")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("IdentityCard")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("MaterMan.Entity.Concrete.Malzeme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("MalzemeAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MalzemeBirimId")
                        .HasColumnType("int");

                    b.Property<int>("MalzemeGrupId")
                        .HasColumnType("int");

                    b.Property<decimal>("StokMiktari")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("MalzemeBirimId");

                    b.HasIndex("MalzemeGrupId");

                    b.ToTable("Malzemeler");
                });

            modelBuilder.Entity("MaterMan.Entity.Concrete.MalzemeBirim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BirimAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MalzemeBirimleri");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BirimAdi = "Kg"
                        },
                        new
                        {
                            Id = 2,
                            BirimAdi = "Metre"
                        },
                        new
                        {
                            Id = 3,
                            BirimAdi = "Adet"
                        },
                        new
                        {
                            Id = 4,
                            BirimAdi = "Litre"
                        });
                });

            modelBuilder.Entity("MaterMan.Entity.Concrete.MalzemeGrup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("GrupAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MalzemeGruplari");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            GrupAdi = "Hammadde"
                        },
                        new
                        {
                            Id = 2,
                            GrupAdi = "Yan Ürün"
                        },
                        new
                        {
                            Id = 3,
                            GrupAdi = "Nihai Ürün"
                        });
                });

            modelBuilder.Entity("MaterMan.Entity.Concrete.ReceteBaslik", b =>
                {
                    b.Property<int>("ReceteBaslikId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReceteBaslikId"));

                    b.Property<string>("Aciklama")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EklemeTarihi")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("MalzemeId")
                        .HasColumnType("int");

                    b.Property<string>("ReceteIsmi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VersiyonNo")
                        .HasColumnType("int");

                    b.HasKey("ReceteBaslikId");

                    b.ToTable("ReceteBasliklar");
                });

            modelBuilder.Entity("MaterMan.Entity.Concrete.ReceteKalem", b =>
                {
                    b.Property<int>("ReceteKalemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReceteKalemId"));

                    b.Property<int>("MalzemeId")
                        .HasColumnType("int");

                    b.Property<decimal>("Miktar")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ReceteBaslikId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ReceteKalemId");

                    b.HasIndex("MalzemeId");

                    b.HasIndex("ReceteBaslikId")
                        .IsUnique();

                    b.ToTable("ReceteKalemler");
                });

            modelBuilder.Entity("MaterMan.Entity.Concrete.Stok", b =>
                {
                    b.Property<int>("StokId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StokId"));

                    b.Property<DateTime>("IslemTarihi")
                        .HasColumnType("datetime2");

                    b.Property<string>("IslemTipi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MalzemeId")
                        .HasColumnType("int");

                    b.Property<int>("StokAdet")
                        .HasColumnType("int");

                    b.HasKey("StokId");

                    b.HasIndex("MalzemeId");

                    b.ToTable("Stoklar");
                });

            modelBuilder.Entity("MaterMan.Entity.Concrete.UrunTipi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("UrunAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UrunTips");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Fiyat", b =>
                {
                    b.HasOne("MaterMan.Entity.Concrete.Malzeme", "Malzeme")
                        .WithMany("MalzemeFiyatlar")
                        .HasForeignKey("MalzemeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Malzeme");
                });

            modelBuilder.Entity("MaterMan.Entity.Concrete.Malzeme", b =>
                {
                    b.HasOne("MaterMan.Entity.Concrete.MalzemeBirim", "MalzemeBirim")
                        .WithMany("Malzemeler")
                        .HasForeignKey("MalzemeBirimId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MaterMan.Entity.Concrete.MalzemeGrup", "MalzemeGrup")
                        .WithMany("Malzemeler")
                        .HasForeignKey("MalzemeGrupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MalzemeBirim");

                    b.Navigation("MalzemeGrup");
                });

            modelBuilder.Entity("MaterMan.Entity.Concrete.ReceteKalem", b =>
                {
                    b.HasOne("MaterMan.Entity.Concrete.Malzeme", "Malzeme")
                        .WithMany("ReceteKalems")
                        .HasForeignKey("MalzemeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MaterMan.Entity.Concrete.ReceteBaslik", "ReceteBaslik")
                        .WithOne("ReceteKalem")
                        .HasForeignKey("MaterMan.Entity.Concrete.ReceteKalem", "ReceteBaslikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Malzeme");

                    b.Navigation("ReceteBaslik");
                });

            modelBuilder.Entity("MaterMan.Entity.Concrete.Stok", b =>
                {
                    b.HasOne("MaterMan.Entity.Concrete.Malzeme", "Malzeme")
                        .WithMany("Stoklar")
                        .HasForeignKey("MalzemeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Malzeme");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("MaterMan.Entity.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("MaterMan.Entity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MaterMan.Entity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("MaterMan.Entity.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MaterMan.Entity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("MaterMan.Entity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MaterMan.Entity.Concrete.Malzeme", b =>
                {
                    b.Navigation("MalzemeFiyatlar");

                    b.Navigation("ReceteKalems");

                    b.Navigation("Stoklar");
                });

            modelBuilder.Entity("MaterMan.Entity.Concrete.MalzemeBirim", b =>
                {
                    b.Navigation("Malzemeler");
                });

            modelBuilder.Entity("MaterMan.Entity.Concrete.MalzemeGrup", b =>
                {
                    b.Navigation("Malzemeler");
                });

            modelBuilder.Entity("MaterMan.Entity.Concrete.ReceteBaslik", b =>
                {
                    b.Navigation("ReceteKalem")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
