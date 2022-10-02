﻿// <auto-generated />
using Bookmrqr.Viewer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bookmrqr.Viewer.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20180620142507_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799");

            modelBuilder.Entity("Bookmrqr.Viewer.Data.Account", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DisplayName");

                    b.Property<string>("Email");

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Bookmrqr.Viewer.Data.Bookmark", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title");

                    b.Property<string>("Url");

                    b.Property<string>("UserName");

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.ToTable("Bookmarks");
                });
#pragma warning restore 612, 618
        }
    }
}
