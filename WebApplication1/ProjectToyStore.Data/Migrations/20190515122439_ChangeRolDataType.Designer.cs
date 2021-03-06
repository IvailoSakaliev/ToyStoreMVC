﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectToyStore.Data;

namespace ProjectToyStore.Data.Migrations
{
    [DbContext(typeof(ToyContext))]
    [Migration("20190515122439_ChangeRolDataType")]
    partial class ChangeRolDataType
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProjectToyStore.Data.Models.Images", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Path");

                    b.Property<int>("Subject_id");

                    b.HasKey("ID");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("ProjectToyStore.Data.Models.Login", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("Password");

                    b.Property<int>("Role");

                    b.Property<int>("UserID");

                    b.Property<bool>("isRegisted");

                    b.HasKey("ID");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("Logins");
                });

            modelBuilder.Entity("ProjectToyStore.Data.Models.Order", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Status");

                    b.Property<int>("SubjectID");

                    b.Property<int>("UserID");

                    b.HasKey("ID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ProjectToyStore.Data.Models.Subject", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<string>("Description");

                    b.Property<double>("Price");

                    b.Property<string>("Title");

                    b.HasKey("ID");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("ProjectToyStore.Data.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adress");

                    b.Property<string>("City");

                    b.Property<string>("FirstName");

                    b.Property<string>("SecondName");

                    b.Property<string>("Telephone");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ProjectToyStore.Data.Models.Login", b =>
                {
                    b.HasOne("ProjectToyStore.Data.Models.User")
                        .WithOne("LoginID")
                        .HasForeignKey("ProjectToyStore.Data.Models.Login", "UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
