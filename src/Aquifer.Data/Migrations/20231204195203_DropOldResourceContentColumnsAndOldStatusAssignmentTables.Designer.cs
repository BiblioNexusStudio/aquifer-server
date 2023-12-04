﻿// <auto-generated />
using System;
using Aquifer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Aquifer.Data.Migrations
{
    [DbContext(typeof(AquiferDbContext))]
    [Migration("20231204195203_DropOldResourceContentColumnsAndOldStatusAssignmentTables")]
    partial class DropOldResourceContentColumnsAndOldStatusAssignmentTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Aquifer.Data.Entities.BibleBookContentEntity", b =>
                {
                    b.Property<int>("BibleId")
                        .HasColumnType("int");

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("AudioSize")
                        .HasColumnType("int");

                    b.Property<string>("AudioUrls")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ChapterCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TextSize")
                        .HasColumnType("int");

                    b.Property<string>("TextUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("BibleId", "BookId");

                    b.ToTable("BibleBookContents");
                });

            modelBuilder.Entity("Aquifer.Data.Entities.BibleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<int>("LanguageId")
                        .HasColumnType("int");

                    b.Property<string>("LicenseInfo")
                        .HasColumnType("nvarchar(max)");

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

                    b.HasIndex("ISO6393Code")
                        .IsUnique();

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("Aquifer.Data.Entities.ParentResourceEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ComplexityLevel")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LicenseInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ResourceType")
                        .HasColumnType("int");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("Id");

                    b.ToTable("ParentResources");
                });

            modelBuilder.Entity("Aquifer.Data.Entities.PassageEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<int>("EndVerseId")
                        .HasColumnType("int");

                    b.Property<int>("StartVerseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("Id");

                    b.HasIndex("EndVerseId");

                    b.HasIndex("StartVerseId", "EndVerseId")
                        .IsUnique();

                    b.ToTable("Passages");
                });

            modelBuilder.Entity("Aquifer.Data.Entities.PassageResourceEntity", b =>
                {
                    b.Property<int>("PassageId")
                        .HasColumnType("int");

                    b.Property<int>("ResourceId")
                        .HasColumnType("int");

                    b.HasKey("PassageId", "ResourceId");

                    b.HasIndex("ResourceId");

                    b.ToTable("PassageResources");
                });

            modelBuilder.Entity("Aquifer.Data.Entities.ResourceContentEntity", b =>
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

                    b.Property<int>("MediaType")
                        .HasColumnType("int");

                    b.Property<int>("ResourceId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<bool>("Trusted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.HasIndex("ResourceId", "LanguageId", "MediaType")
                        .IsUnique();

                    b.ToTable("ResourceContents");
                });

            modelBuilder.Entity("Aquifer.Data.Entities.ResourceContentVersionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AssignedUserId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ContentSize")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDraft")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("bit");

                    b.Property<int>("ResourceContentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AssignedUserId");

                    b.HasIndex("ResourceContentId")
                        .IsUnique()
                        .HasDatabaseName("IX_ResourceContentVersions_ResourceContentId_IsDraft")
                        .HasFilter("IsDraft = 1");

                    b.ToTable("ResourceContentVersions", t =>
                        {
                            t.HasCheckConstraint("CK_ResourceContentVersions_IsPublishedOrIsDraftNotBoth", "IsPublished = 0 OR IsDraft = 0");
                        });
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

                    b.Property<string>("ExternalId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ParentResourceId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("Id");

                    b.HasIndex("ParentResourceId", "ExternalId")
                        .IsUnique()
                        .HasFilter("[ExternalId] IS NOT NULL");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("Aquifer.Data.Entities.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("ProviderId")
                        .IsUnique();

                    b.ToTable("Users");
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

            modelBuilder.Entity("AssociatedResources", b =>
                {
                    b.Property<int>("AssociatedResourceId")
                        .HasColumnType("int");

                    b.Property<int>("ResourceId")
                        .HasColumnType("int");

                    b.HasKey("AssociatedResourceId", "ResourceId");

                    b.HasIndex("ResourceId");

                    b.ToTable("AssociatedResources");
                });

            modelBuilder.Entity("Aquifer.Data.Entities.BibleBookContentEntity", b =>
                {
                    b.HasOne("Aquifer.Data.Entities.BibleEntity", "Bible")
                        .WithMany("BibleBookContents")
                        .HasForeignKey("BibleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bible");
                });

            modelBuilder.Entity("Aquifer.Data.Entities.PassageEntity", b =>
                {
                    b.HasOne("Aquifer.Data.Entities.VerseEntity", "EndVerse")
                        .WithMany()
                        .HasForeignKey("EndVerseId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Aquifer.Data.Entities.VerseEntity", "StartVerse")
                        .WithMany()
                        .HasForeignKey("StartVerseId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("EndVerse");

                    b.Navigation("StartVerse");
                });

            modelBuilder.Entity("Aquifer.Data.Entities.PassageResourceEntity", b =>
                {
                    b.HasOne("Aquifer.Data.Entities.PassageEntity", "Passage")
                        .WithMany("PassageResources")
                        .HasForeignKey("PassageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aquifer.Data.Entities.ResourceEntity", "Resource")
                        .WithMany("PassageResources")
                        .HasForeignKey("ResourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Passage");

                    b.Navigation("Resource");
                });

            modelBuilder.Entity("Aquifer.Data.Entities.ResourceContentEntity", b =>
                {
                    b.HasOne("Aquifer.Data.Entities.LanguageEntity", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aquifer.Data.Entities.ResourceEntity", "Resource")
                        .WithMany("ResourceContents")
                        .HasForeignKey("ResourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");

                    b.Navigation("Resource");
                });

            modelBuilder.Entity("Aquifer.Data.Entities.ResourceContentVersionEntity", b =>
                {
                    b.HasOne("Aquifer.Data.Entities.UserEntity", "AssignedUser")
                        .WithMany()
                        .HasForeignKey("AssignedUserId");

                    b.HasOne("Aquifer.Data.Entities.ResourceContentEntity", "ResourceContent")
                        .WithMany("Versions")
                        .HasForeignKey("ResourceContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssignedUser");

                    b.Navigation("ResourceContent");
                });

            modelBuilder.Entity("Aquifer.Data.Entities.ResourceEntity", b =>
                {
                    b.HasOne("Aquifer.Data.Entities.ParentResourceEntity", "ParentResource")
                        .WithMany()
                        .HasForeignKey("ParentResourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParentResource");
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

            modelBuilder.Entity("AssociatedResources", b =>
                {
                    b.HasOne("Aquifer.Data.Entities.ResourceEntity", null)
                        .WithMany()
                        .HasForeignKey("AssociatedResourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aquifer.Data.Entities.ResourceEntity", null)
                        .WithMany()
                        .HasForeignKey("ResourceId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Aquifer.Data.Entities.BibleEntity", b =>
                {
                    b.Navigation("BibleBookContents");
                });

            modelBuilder.Entity("Aquifer.Data.Entities.PassageEntity", b =>
                {
                    b.Navigation("PassageResources");
                });

            modelBuilder.Entity("Aquifer.Data.Entities.ResourceContentEntity", b =>
                {
                    b.Navigation("Versions");
                });

            modelBuilder.Entity("Aquifer.Data.Entities.ResourceEntity", b =>
                {
                    b.Navigation("PassageResources");

                    b.Navigation("ResourceContents");

                    b.Navigation("VerseResources");
                });

            modelBuilder.Entity("Aquifer.Data.Entities.VerseEntity", b =>
                {
                    b.Navigation("VerseResources");
                });
#pragma warning restore 612, 618
        }
    }
}
