// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SupplyApp;

#nullable disable

namespace SupplyApp.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true);

            modelBuilder.Entity("SupplyApp.Entity.Document", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("path");

                    b.Property<long>("RequestId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("request_id");

                    b.HasKey("Id");

                    b.HasIndex("RequestId");

                    b.ToTable("document", (string)null);
                });

            modelBuilder.Entity("SupplyApp.Entity.Executor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("executor", (string)null);
                });

            modelBuilder.Entity("SupplyApp.Entity.Initiator", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("initiator", (string)null);
                });

            modelBuilder.Entity("SupplyApp.Entity.Item", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("items", (string)null);
                });

            modelBuilder.Entity("SupplyApp.Entity.Provider", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("provider", (string)null);
                });

            modelBuilder.Entity("SupplyApp.Entity.Request", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<long>("Date")
                        .HasColumnType("INTEGER")
                        .HasColumnName("date");

                    b.Property<long?>("DateExecution")
                        .HasColumnType("INTEGER")
                        .HasColumnName("date_execution");

                    b.Property<string>("Description")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue(" ")
                        .HasColumnName("description");

                    b.Property<long>("ExecutorId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("executor_id");

                    b.Property<long>("InitiatorId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("initiator_id");

                    b.Property<string>("Num")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("num");

                    b.Property<long>("ProviderId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("provider_id");

                    b.Property<long>("SignificanceId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("significance_id");

                    b.Property<long>("StatusId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("status_id");

                    b.HasKey("Id");

                    b.HasIndex("ExecutorId");

                    b.HasIndex("InitiatorId");

                    b.HasIndex("ProviderId");

                    b.HasIndex("SignificanceId");

                    b.HasIndex("StatusId");

                    b.ToTable("request", null, t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("SupplyApp.Entity.RequestItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<long>("Count")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("count")
                        .HasDefaultValueSql("1");

                    b.Property<long>("ItemId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("item_id");

                    b.Property<long>("RequestId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("request_id");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("RequestId");

                    b.ToTable("request_items", (string)null);
                });

            modelBuilder.Entity("SupplyApp.Entity.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("role", (string)null);
                });

            modelBuilder.Entity("SupplyApp.Entity.Significance", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("significance", (string)null);
                });

            modelBuilder.Entity("SupplyApp.Entity.Status", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("Color")
                        .HasColumnType("TEXT")
                        .HasColumnName("color");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Name" }, "IX_status_name")
                        .IsUnique();

                    b.ToTable("status", (string)null);
                });

            modelBuilder.Entity("SupplyApp.Entity.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("password");

                    b.Property<long>("RoleId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("role_id");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("SupplyApp.Entity.Document", b =>
                {
                    b.HasOne("SupplyApp.Entity.Request", "Request")
                        .WithMany("Documents")
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Request");
                });

            modelBuilder.Entity("SupplyApp.Entity.Request", b =>
                {
                    b.HasOne("SupplyApp.Entity.Executor", "Executor")
                        .WithMany("Requests")
                        .HasForeignKey("ExecutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SupplyApp.Entity.Initiator", "Initiator")
                        .WithMany("Requests")
                        .HasForeignKey("InitiatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SupplyApp.Entity.Provider", "Provider")
                        .WithMany("Requests")
                        .HasForeignKey("ProviderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SupplyApp.Entity.Significance", "Significance")
                        .WithMany("Requests")
                        .HasForeignKey("SignificanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SupplyApp.Entity.Status", "Status")
                        .WithMany("Requests")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Executor");

                    b.Navigation("Initiator");

                    b.Navigation("Provider");

                    b.Navigation("Significance");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("SupplyApp.Entity.RequestItem", b =>
                {
                    b.HasOne("SupplyApp.Entity.Item", "Item")
                        .WithMany("RequestItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SupplyApp.Entity.Request", "Request")
                        .WithMany("RequestItems")
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Request");
                });

            modelBuilder.Entity("SupplyApp.Entity.User", b =>
                {
                    b.HasOne("SupplyApp.Entity.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("SupplyApp.Entity.Executor", b =>
                {
                    b.Navigation("Requests");
                });

            modelBuilder.Entity("SupplyApp.Entity.Initiator", b =>
                {
                    b.Navigation("Requests");
                });

            modelBuilder.Entity("SupplyApp.Entity.Item", b =>
                {
                    b.Navigation("RequestItems");
                });

            modelBuilder.Entity("SupplyApp.Entity.Provider", b =>
                {
                    b.Navigation("Requests");
                });

            modelBuilder.Entity("SupplyApp.Entity.Request", b =>
                {
                    b.Navigation("Documents");

                    b.Navigation("RequestItems");
                });

            modelBuilder.Entity("SupplyApp.Entity.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("SupplyApp.Entity.Significance", b =>
                {
                    b.Navigation("Requests");
                });

            modelBuilder.Entity("SupplyApp.Entity.Status", b =>
                {
                    b.Navigation("Requests");
                });
#pragma warning restore 612, 618
        }
    }
}
