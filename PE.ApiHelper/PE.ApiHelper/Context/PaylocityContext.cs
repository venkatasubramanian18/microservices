using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PE.ApiHelper.Entities;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace PE.ApiHelper.Context
{
    public partial class PaylocityContext : DbContext
    {
        public PaylocityContext()
        {
        }

        public PaylocityContext(DbContextOptions<PaylocityContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DependentTypes> DependentTypes { get; set; }
        public virtual DbSet<Dependents> Dependents { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<PaycheckTypes> PaycheckTypes { get; set; }
        public virtual DbSet<Salaries> Salaries { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-ULED8V1;Initial Catalog=Paylocity;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DependentTypes>(entity =>
            {
                entity.HasKey(e => e.DependentTypeId);

                entity.HasIndex(e => e.DependentType)
                    .HasName("UK_DependentTypes")
                    .IsUnique();

                entity.Property(e => e.DependentTypeId).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.DependentType)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<Dependents>(entity =>
            {
                entity.HasKey(e => e.DependentId);

                entity.Property(e => e.DependentId).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.DependentType)
                    .WithMany(p => p.Dependents)
                    .HasForeignKey(d => d.DependentTypeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Dependents_DependentTypes");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Dependents)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Dependents_Employees");
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.Property(e => e.EmployeeId).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<PaycheckTypes>(entity =>
            {
                entity.HasKey(e => e.PaycheckTypeId);

                entity.HasIndex(e => e.PaycheckType)
                    .HasName("UK_PaycheckTypes")
                    .IsUnique();

                entity.Property(e => e.PaycheckTypeId).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.PaycheckType)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<Salaries>(entity =>
            {
                entity.HasKey(e => e.SalaryId);

                entity.Property(e => e.SalaryId).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Salary)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Salaries)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Salaries_Employees");

                entity.HasOne(d => d.PaycheckType)
                    .WithMany(p => p.Salaries)
                    .HasForeignKey(d => d.PaycheckTypeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Salaries_PaycheckTypes");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
