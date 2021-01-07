﻿// <auto-generated />
using System;
using ChessLogicEntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ChessLogicEntityFramework.Migrations
{
    [DbContext(typeof(ChessAppContext))]
    [Migration("20210107114429_Token prep")]
    partial class Tokenprep
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("ChessLogicEntityFramework.Models.Game", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("PlayerBlackID")
                        .HasColumnType("int");

                    b.Property<int?>("PlayerWhiteID")
                        .HasColumnType("int");

                    b.Property<int?>("WinnerID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("PlayerBlackID");

                    b.HasIndex("PlayerWhiteID");

                    b.HasIndex("WinnerID");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("ChessLogicEntityFramework.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ChessLogicEntityFramework.Models.Game", b =>
                {
                    b.HasOne("ChessLogicEntityFramework.Models.User", "PlayerBlack")
                        .WithMany()
                        .HasForeignKey("PlayerBlackID");

                    b.HasOne("ChessLogicEntityFramework.Models.User", "PlayerWhite")
                        .WithMany()
                        .HasForeignKey("PlayerWhiteID");

                    b.HasOne("ChessLogicEntityFramework.Models.User", "Winner")
                        .WithMany()
                        .HasForeignKey("WinnerID");

                    b.Navigation("PlayerBlack");

                    b.Navigation("PlayerWhite");

                    b.Navigation("Winner");
                });
#pragma warning restore 612, 618
        }
    }
}
