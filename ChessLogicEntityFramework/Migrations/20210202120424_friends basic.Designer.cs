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
    [Migration("20210202120424_friends basic")]
    partial class friendsbasic
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("ChessLogicEntityFramework.Models.Friendship", b =>
                {
                    b.Property<int>("User1")
                        .HasColumnType("int");

                    b.Property<int>("User2")
                        .HasColumnType("int");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.Property<int?>("UserID1")
                        .HasColumnType("int");

                    b.HasKey("User1", "User2");

                    b.HasIndex("UserID");

                    b.HasIndex("UserID1");

                    b.ToTable("Friendships");
                });

            modelBuilder.Entity("ChessLogicEntityFramework.Models.Game", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("GameTime")
                        .HasColumnType("int");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("bit");

                    b.Property<string>("MovesList")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PlayerBlackID")
                        .HasColumnType("int");

                    b.Property<int?>("PlayerWhiteID")
                        .HasColumnType("int");

                    b.Property<int>("TimeGain")
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

            modelBuilder.Entity("ChessLogicEntityFramework.Models.UserGames", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("GameID")
                        .HasColumnType("int");

                    b.HasKey("UserID", "GameID");

                    b.HasIndex("GameID");

                    b.ToTable("UserGames");
                });

            modelBuilder.Entity("ChessLogicEntityFramework.Models.Friendship", b =>
                {
                    b.HasOne("ChessLogicEntityFramework.Models.User", null)
                        .WithMany("Users1Friendships")
                        .HasForeignKey("UserID");

                    b.HasOne("ChessLogicEntityFramework.Models.User", null)
                        .WithMany("Users2Friendships")
                        .HasForeignKey("UserID1");
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

            modelBuilder.Entity("ChessLogicEntityFramework.Models.UserGames", b =>
                {
                    b.HasOne("ChessLogicEntityFramework.Models.Game", "Game")
                        .WithMany("UserGames")
                        .HasForeignKey("GameID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ChessLogicEntityFramework.Models.User", "User")
                        .WithMany("UserGames")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ChessLogicEntityFramework.Models.Game", b =>
                {
                    b.Navigation("UserGames");
                });

            modelBuilder.Entity("ChessLogicEntityFramework.Models.User", b =>
                {
                    b.Navigation("UserGames");

                    b.Navigation("Users1Friendships");

                    b.Navigation("Users2Friendships");
                });
#pragma warning restore 612, 618
        }
    }
}