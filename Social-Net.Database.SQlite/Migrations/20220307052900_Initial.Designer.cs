﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SocialNet1.Database.SQlite.Context;

namespace SocialNet1.Database.SQlite.Migrations
{
    [DbContext(typeof(SocialNetDBSQlite))]
    [Migration("20220307052900_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Social_Net.Domain.Friends.FriendStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FriendName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("HiddenStatus")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserDTOId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserDTOId");

                    b.ToTable("FriendStatus");
                });

            modelBuilder.Entity("Social_Net.Domain.Group.GroupDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ShortGroupName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("SocNetItemsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SocNetItemsId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Social_Net.Domain.Group.GroupImages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("GroupDTOId")
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<int>("ImageNumber")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("GroupDTOId");

                    b.ToTable("GroupImages");
                });

            modelBuilder.Entity("Social_Net.Domain.Group.SocNetEntityGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CurrentImage")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("X")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Y")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("SocNetEntityGroup");
                });

            modelBuilder.Entity("Social_Net.Domain.Group.UserGroupStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("GroupDTOId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserDTOId")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GroupDTOId");

                    b.HasIndex("UserDTOId");

                    b.ToTable("UserGroupStatuses");
                });

            modelBuilder.Entity("Social_Net.Domain.Identity.RoleDTO", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Social_Net.Domain.Identity.SocNetEntityUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CurrentImage")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("X")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Y")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("SocNetEntityUser");
                });

            modelBuilder.Entity("Social_Net.Domain.Identity.UserDTO", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<int?>("SocNetItemsId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.HasIndex("SocNetItemsId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Social_Net.Domain.Identity.UserImages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<int>("ImageNumber")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserDTOId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserDTOId");

                    b.ToTable("UserImages");
                });

            modelBuilder.Entity("Social_Net.Domain.Message.ChatDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastTimeMess")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName1")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName2")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("Social_Net.Domain.Message.MessageDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ChatDTOId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SenderName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TimeMess")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ChatDTOId");

                    b.ToTable("MessageDTO");
                });

            modelBuilder.Entity("Social_Net.Domain.Message.MessageImages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<int>("ImageNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MessageDTOId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("MessageDTOId");

                    b.ToTable("MessageImages");
                });

            modelBuilder.Entity("Social_Net.Domain.PostCom.CommentDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CommentatorName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("CommentatorStatus")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<int?>("PostDTOId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PostDTOId");

                    b.ToTable("CommentDTO");
                });

            modelBuilder.Entity("Social_Net.Domain.PostCom.CommentImages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CommentDTOId")
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<int>("ImageNumber")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CommentDTOId");

                    b.ToTable("CommentImages");
                });

            modelBuilder.Entity("Social_Net.Domain.PostCom.Like", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CommentDTOId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Emoji")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Likers")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CommentDTOId");

                    b.ToTable("Like");
                });

            modelBuilder.Entity("Social_Net.Domain.PostCom.PostDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("SocNetEntityGroupId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("SocNetEntityUserId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ThisPostId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SocNetEntityGroupId");

                    b.HasIndex("SocNetEntityUserId");

                    b.HasIndex("ThisPostId");

                    b.ToTable("PostDTOs");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Social_Net.Domain.Identity.RoleDTO", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Social_Net.Domain.Identity.UserDTO", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Social_Net.Domain.Identity.UserDTO", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Social_Net.Domain.Identity.RoleDTO", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Social_Net.Domain.Identity.UserDTO", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Social_Net.Domain.Identity.UserDTO", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Social_Net.Domain.Friends.FriendStatus", b =>
                {
                    b.HasOne("Social_Net.Domain.Identity.UserDTO", null)
                        .WithMany("Friends")
                        .HasForeignKey("UserDTOId");
                });

            modelBuilder.Entity("Social_Net.Domain.Group.GroupDTO", b =>
                {
                    b.HasOne("Social_Net.Domain.Group.SocNetEntityGroup", "SocNetItems")
                        .WithMany()
                        .HasForeignKey("SocNetItemsId");

                    b.Navigation("SocNetItems");
                });

            modelBuilder.Entity("Social_Net.Domain.Group.GroupImages", b =>
                {
                    b.HasOne("Social_Net.Domain.Group.GroupDTO", null)
                        .WithMany("Images")
                        .HasForeignKey("GroupDTOId");
                });

            modelBuilder.Entity("Social_Net.Domain.Group.UserGroupStatus", b =>
                {
                    b.HasOne("Social_Net.Domain.Group.GroupDTO", null)
                        .WithMany("Users")
                        .HasForeignKey("GroupDTOId");

                    b.HasOne("Social_Net.Domain.Identity.UserDTO", null)
                        .WithMany("Groups")
                        .HasForeignKey("UserDTOId");
                });

            modelBuilder.Entity("Social_Net.Domain.Identity.UserDTO", b =>
                {
                    b.HasOne("Social_Net.Domain.Identity.SocNetEntityUser", "SocNetItems")
                        .WithMany()
                        .HasForeignKey("SocNetItemsId");

                    b.Navigation("SocNetItems");
                });

            modelBuilder.Entity("Social_Net.Domain.Identity.UserImages", b =>
                {
                    b.HasOne("Social_Net.Domain.Identity.UserDTO", null)
                        .WithMany("Images")
                        .HasForeignKey("UserDTOId");
                });

            modelBuilder.Entity("Social_Net.Domain.Message.MessageDTO", b =>
                {
                    b.HasOne("Social_Net.Domain.Message.ChatDTO", null)
                        .WithMany("Messages")
                        .HasForeignKey("ChatDTOId");
                });

            modelBuilder.Entity("Social_Net.Domain.Message.MessageImages", b =>
                {
                    b.HasOne("Social_Net.Domain.Message.MessageDTO", null)
                        .WithMany("Images")
                        .HasForeignKey("MessageDTOId");
                });

            modelBuilder.Entity("Social_Net.Domain.PostCom.CommentDTO", b =>
                {
                    b.HasOne("Social_Net.Domain.PostCom.PostDTO", null)
                        .WithMany("Comments")
                        .HasForeignKey("PostDTOId");
                });

            modelBuilder.Entity("Social_Net.Domain.PostCom.CommentImages", b =>
                {
                    b.HasOne("Social_Net.Domain.PostCom.CommentDTO", null)
                        .WithMany("Images")
                        .HasForeignKey("CommentDTOId");
                });

            modelBuilder.Entity("Social_Net.Domain.PostCom.Like", b =>
                {
                    b.HasOne("Social_Net.Domain.PostCom.CommentDTO", null)
                        .WithMany("Likes")
                        .HasForeignKey("CommentDTOId");
                });

            modelBuilder.Entity("Social_Net.Domain.PostCom.PostDTO", b =>
                {
                    b.HasOne("Social_Net.Domain.Group.SocNetEntityGroup", null)
                        .WithMany("Posts")
                        .HasForeignKey("SocNetEntityGroupId");

                    b.HasOne("Social_Net.Domain.Identity.SocNetEntityUser", null)
                        .WithMany("Posts")
                        .HasForeignKey("SocNetEntityUserId");

                    b.HasOne("Social_Net.Domain.PostCom.CommentDTO", "ThisPost")
                        .WithMany()
                        .HasForeignKey("ThisPostId");

                    b.Navigation("ThisPost");
                });

            modelBuilder.Entity("Social_Net.Domain.Group.GroupDTO", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Social_Net.Domain.Group.SocNetEntityGroup", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("Social_Net.Domain.Identity.SocNetEntityUser", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("Social_Net.Domain.Identity.UserDTO", b =>
                {
                    b.Navigation("Friends");

                    b.Navigation("Groups");

                    b.Navigation("Images");
                });

            modelBuilder.Entity("Social_Net.Domain.Message.ChatDTO", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("Social_Net.Domain.Message.MessageDTO", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("Social_Net.Domain.PostCom.CommentDTO", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Likes");
                });

            modelBuilder.Entity("Social_Net.Domain.PostCom.PostDTO", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
