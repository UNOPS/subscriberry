using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Subscriberry.EntityFramework.DataAccess;

namespace Subscriberry.EntityFramework.Migrations
{
    [DbContext(typeof(SubscriberryDbContext))]
    partial class SubscriberryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Subscriberry.core.Model.Subscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("GroupId");

                    b.Property<string>("Name")
                        .HasColumnName("Name")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Subscription","dbo");
                });

            modelBuilder.Entity("Subscriberry.core.Model.SubscriptionGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnName("Name")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("Group","dbo");
                });

            modelBuilder.Entity("Subscriberry.core.Model.SubscriptionRole", b =>
                {
                    b.Property<int>("RoleId");

                    b.Property<int>("SubscriptionId");

                    b.HasKey("RoleId", "SubscriptionId");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("SubscriptionRole","dbo");
                });

            modelBuilder.Entity("Subscriberry.core.Model.UserSubscription", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<int>("SubscriptionId");

                    b.HasKey("UserId", "SubscriptionId");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("UserSubscription","dbo");
                });

            modelBuilder.Entity("Subscriberry.core.Model.Subscription", b =>
                {
                    b.HasOne("Subscriberry.core.Model.SubscriptionGroup", "Group")
                        .WithMany("Subscriptions")
                        .HasForeignKey("GroupId");
                });

            modelBuilder.Entity("Subscriberry.core.Model.SubscriptionRole", b =>
                {
                    b.HasOne("Subscriberry.core.Model.Subscription", "Subscription")
                        .WithMany()
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Subscriberry.core.Model.UserSubscription", b =>
                {
                    b.HasOne("Subscriberry.core.Model.Subscription", "Subscription")
                        .WithMany()
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
