﻿// <auto-generated />
using System;
using Aquifer.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Aquifer.API.Data.Migrations
{
    [DbContext(typeof(AquiferDbContext))]
    [Migration("20230720192310_Update5")]
    partial class Update5
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Aquifer.API.Data.Entities.LanguageEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("EnglishDisplay")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ISO6393Code")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("Aquifer.API.Data.Entities.PassageEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<int>("EndBnVerse")
                        .HasColumnType("int");

                    b.Property<int>("StartBnVerse")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("Id");

                    b.ToTable("Passages");
                });

            modelBuilder.Entity("Aquifer.API.Data.Entities.PassageResourceEntity", b =>
                {
                    b.Property<int>("PassageId")
                        .HasColumnType("int");

                    b.Property<int>("ResourceId")
                        .HasColumnType("int");

                    b.HasKey("PassageId", "ResourceId");

                    b.HasIndex("ResourceId")
                        .IsUnique();

                    b.ToTable("PassageResources");
                });

            modelBuilder.Entity("Aquifer.API.Data.Entities.ResourceContentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Completed")
                        .HasColumnType("bit");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LanguageId")
                        .HasColumnType("int");

                    b.Property<int>("ResourceId")
                        .HasColumnType("int");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Trusted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.HasIndex("ResourceId")
                        .IsUnique();

                    b.ToTable("ResourceContents");
                });

            modelBuilder.Entity("Aquifer.API.Data.Entities.ResourceEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("EnglishLabel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tag")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("Aquifer.API.Data.Entities.SupportingResourceEntity", b =>
                {
                    b.Property<int>("ParentResourceId")
                        .HasColumnType("int");

                    b.Property<int>("SupportingResourceId")
                        .HasColumnType("int");

                    b.HasKey("ParentResourceId", "SupportingResourceId");

                    b.HasIndex("SupportingResourceId")
                        .IsUnique();

                    b.ToTable("SupportingResources");
                });

            modelBuilder.Entity("Aquifer.API.Data.Entities.PassageResourceEntity", b =>
                {
                    b.HasOne("Aquifer.API.Data.Entities.PassageEntity", "Passage")
                        .WithMany("PassageResources")
                        .HasForeignKey("PassageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aquifer.API.Data.Entities.ResourceEntity", "Resource")
                        .WithOne("PassageResource")
                        .HasForeignKey("Aquifer.API.Data.Entities.PassageResourceEntity", "ResourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Passage");

                    b.Navigation("Resource");
                });

            modelBuilder.Entity("Aquifer.API.Data.Entities.ResourceContentEntity", b =>
                {
                    b.HasOne("Aquifer.API.Data.Entities.LanguageEntity", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aquifer.API.Data.Entities.ResourceEntity", "Resource")
                        .WithOne("ResourceContent")
                        .HasForeignKey("Aquifer.API.Data.Entities.ResourceContentEntity", "ResourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");

                    b.Navigation("Resource");
                });

            modelBuilder.Entity("Aquifer.API.Data.Entities.SupportingResourceEntity", b =>
                {
                    b.HasOne("Aquifer.API.Data.Entities.ResourceEntity", "ParentResource")
                        .WithMany("SupportingResources")
                        .HasForeignKey("ParentResourceId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Aquifer.API.Data.Entities.ResourceEntity", "SupportingResource")
                        .WithOne("SupportingResource")
                        .HasForeignKey("Aquifer.API.Data.Entities.SupportingResourceEntity", "SupportingResourceId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ParentResource");

                    b.Navigation("SupportingResource");
                });

            modelBuilder.Entity("Aquifer.API.Data.Entities.PassageEntity", b =>
                {
                    b.Navigation("PassageResources");
                });

            modelBuilder.Entity("Aquifer.API.Data.Entities.ResourceEntity", b =>
                {
                    b.Navigation("PassageResource");

                    b.Navigation("ResourceContent")
                        .IsRequired();

                    b.Navigation("SupportingResource");

                    b.Navigation("SupportingResources");
                });
#pragma warning restore 612, 618
        }
    }
}
