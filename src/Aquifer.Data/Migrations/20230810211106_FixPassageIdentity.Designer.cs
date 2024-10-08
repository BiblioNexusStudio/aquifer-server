﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Aquifer.Data.Migrations
{
    [DbContext(typeof(AquiferDbContext))]
    [Migration("20230810211106_FixPassageIdentity")]
    partial class FixPassageIdentity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Aquifer.Data.Entities.BibleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<int>("LanguageId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("Id");

                    b.ToTable("Bibles");
                });

            modelBuilder.Entity("Aquifer.Data.Entities.LanguageEntity", b =>
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

            modelBuilder.Entity("Aquifer.Data.Entities.ResourceContentEntity", b =>
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
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

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
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.HasIndex("ResourceId")
                        .IsUnique();

                    b.ToTable("ResourceContents");
                });

            modelBuilder.Entity("Aquifer.Data.Entities.ResourceEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("EnglishLabel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tag")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("Id");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("Aquifer.Data.Entities.SupportingResourceEntity", b =>
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

            modelBuilder.Entity("Aquifer.Data.Entities.VerseContentEntity", b =>
                {
                    b.Property<int>("VerseId")
                        .HasColumnType("int");

                    b.Property<int>("BibleId")
                        .HasColumnType("int");

                    b.Property<float?>("AudioEndTime")
                        .HasColumnType("real");

                    b.Property<float?>("AudioStartTime")
                        .HasColumnType("real");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<DateTime>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("VerseId", "BibleId");

                    b.HasIndex("BibleId");

                    b.ToTable("VerseContents");
                });

            modelBuilder.Entity("Aquifer.Data.Entities.VerseEntity", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Verses");
                });

            modelBuilder.Entity("Aquifer.Data.Entities.VerseResourceEntity", b =>
                {
                    b.Property<int>("VerseId")
                        .HasColumnType("int");

                    b.Property<int>("ResourceId")
                        .HasColumnType("int");

                    b.HasKey("VerseId", "ResourceId");

                    b.HasIndex("ResourceId");

                    b.ToTable("VerseResources");
                });

            modelBuilder.Entity("Aquifer.Data.Entities.ResourceContentEntity", b =>
                {
                    b.HasOne("Aquifer.Data.Entities.LanguageEntity", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aquifer.Data.Entities.ResourceEntity", "Resource")
                        .WithOne("ResourceContent")
                        .HasForeignKey("Aquifer.Data.Entities.ResourceContentEntity", "ResourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");

                    b.Navigation("Resource");
                });

            modelBuilder.Entity("Aquifer.Data.Entities.SupportingResourceEntity", b =>
                {
                    b.HasOne("Aquifer.Data.Entities.ResourceEntity", "ParentResource")
                        .WithMany("SupportingResources")
                        .HasForeignKey("ParentResourceId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Aquifer.Data.Entities.ResourceEntity", "SupportingResource")
                        .WithOne("SupportingResource")
                        .HasForeignKey("Aquifer.Data.Entities.SupportingResourceEntity", "SupportingResourceId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ParentResource");

                    b.Navigation("SupportingResource");
                });

            modelBuilder.Entity("Aquifer.Data.Entities.VerseContentEntity", b =>
                {
                    b.HasOne("Aquifer.Data.Entities.BibleEntity", "Bible")
                        .WithMany("VerseContents")
                        .HasForeignKey("BibleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aquifer.Data.Entities.VerseEntity", "Verse")
                        .WithMany("VerseContents")
                        .HasForeignKey("VerseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bible");

                    b.Navigation("Verse");
                });

            modelBuilder.Entity("Aquifer.Data.Entities.VerseResourceEntity", b =>
                {
                    b.HasOne("Aquifer.Data.Entities.ResourceEntity", "Resource")
                        .WithMany("VerseResources")
                        .HasForeignKey("ResourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aquifer.Data.Entities.VerseEntity", "Verse")
                        .WithMany("VerseResources")
                        .HasForeignKey("VerseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Resource");

                    b.Navigation("Verse");
                });

            modelBuilder.Entity("Aquifer.Data.Entities.BibleEntity", b =>
                {
                    b.Navigation("VerseContents");
                });

            modelBuilder.Entity("Aquifer.Data.Entities.ResourceEntity", b =>
                {
                    b.Navigation("ResourceContent")
                        .IsRequired();

                    b.Navigation("SupportingResource");

                    b.Navigation("SupportingResources");

                    b.Navigation("VerseResources");
                });

            modelBuilder.Entity("Aquifer.Data.Entities.VerseEntity", b =>
                {
                    b.Navigation("VerseContents");

                    b.Navigation("VerseResources");
                });
#pragma warning restore 612, 618
        }
    }
}
