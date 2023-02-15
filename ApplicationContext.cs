using System.IO;
using Microsoft.EntityFrameworkCore;
using SupplyApp.Entity;
using SupplyApp.Utils;

namespace SupplyApp;

public partial class ApplicationContext : DbContext
{
    public ApplicationContext()
    {
        ConnectionString = "";
        if (DefaultDataLoad.GetInstance.Connections is { Active: { } })
            ConnectionString = DefaultDataLoad.GetInstance.Connections.Active.Path;
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        ConnectionString = "";
        if (DefaultDataLoad.GetInstance.Connections is { Active: { } })
            ConnectionString = DefaultDataLoad.GetInstance.Connections.Active.Path;
    }

    public string ConnectionString { get; }

    public virtual DbSet<Executor> Executors { get; set; }
    public virtual DbSet<Initiator> Initiators { get; set; }
    public virtual DbSet<Item> Items { get; set; }
    public virtual DbSet<Provider> Providers { get; set; }
    public virtual DbSet<Request> Requests { get; set; }
    public virtual DbSet<RequestItem> RequestItems { get; set; }
    public virtual DbSet<Document> Documents { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<Significance> Significances { get; set; }
    public virtual DbSet<Status> Statuses { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlite("Data Source=" + (string.IsNullOrEmpty(ConnectionString) && !File.Exists(ConnectionString)
                ? "admin_db.sqlite"
                : ConnectionString))
            .UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Executor>(entity =>
        {
            entity.ToTable("executor");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Initiator>(entity =>
        {
            entity.ToTable("initiator");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.ToTable("items");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Provider>(entity =>
        {
            entity.ToTable("provider");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.ToTable("request", t => t.ExcludeFromMigrations());

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.DateExecution).HasColumnName("date_execution");
            entity.Property(e => e.ExecutorId).HasColumnName("executor_id");
            entity.Property(e => e.InitiatorId).HasColumnName("initiator_id");
            entity.Property(e => e.Num).HasColumnName("num");
            entity.Property(e => e.ProviderId).HasColumnName("provider_id");
            entity.Property(e => e.SignificanceId).HasColumnName("significance_id");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.Description)
                .HasColumnName("description")
                .IsRequired(false)
                .HasDefaultValue(" ");

            entity.HasOne(d => d.Executor).WithMany(p => p.Requests).HasForeignKey(d => d.ExecutorId);

            entity.HasOne(d => d.Initiator).WithMany(p => p.Requests).HasForeignKey(d => d.InitiatorId);

            entity.HasOne(d => d.Provider).WithMany(p => p.Requests).HasForeignKey(d => d.ProviderId);

            entity.HasOne(d => d.Significance).WithMany(p => p.Requests).HasForeignKey(d => d.SignificanceId);

            entity.HasOne(d => d.Status).WithMany(p => p.Requests).HasForeignKey(d => d.StatusId);
        });

        modelBuilder.Entity<RequestItem>(entity =>
        {
            entity.ToTable("request_items");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Count)
                .HasDefaultValueSql("1")
                .HasColumnName("count");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.RequestId).HasColumnName("request_id");

            entity.HasOne(d => d.Item).WithMany(p => p.RequestItems).HasForeignKey(d => d.ItemId);

            entity.HasOne(d => d.Request).WithMany(p => p.RequestItems).HasForeignKey(d => d.RequestId);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("role");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Significance>(entity =>
        {
            entity.ToTable("significance");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("status");

            entity.HasIndex(e => e.Name, "IX_status_name").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Color).HasColumnName("color");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Username).HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.ToTable("document");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Path).HasColumnName("path");
            entity.Property(e => e.RequestId).HasColumnName("request_id");

            entity.HasOne(d => d.Request).WithMany(p => p.Documents).HasForeignKey(d => d.RequestId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
