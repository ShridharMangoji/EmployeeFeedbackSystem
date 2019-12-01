using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DAL.Models
{
    public partial class Entities : DbContext
    {
        public Entities()
        {
        }

        public Entities(DbContextOptions<Entities> options)
            : base(options)
        {
        }

        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Feedback> Feedback { get; set; }
        public virtual DbSet<FeedbackCategory> FeedbackCategory { get; set; }
        public virtual DbSet<FeedbackEscalationMapping> FeedbackEscalationMapping { get; set; }
        public virtual DbSet<FeedbackStatus> FeedbackStatus { get; set; }
        public virtual DbSet<RegisteredDevice> RegisteredDevice { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<UserRoleMapping> UserRoleMapping { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                 optionsBuilder.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Shridhar\\source\\repos\\EmpFeedbackSystem\\EmpFeedbackSystem\\Database\\DB.mdf;Trusted_Connection=True;Integrated Security=True;Connect Timeout=30;");

               // optionsBuilder.UseSqlServer("Server=db4free.net;UID=shridharmangoji; password=shridharmangoji; database=empsystem;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("company");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("feedback");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedFor).HasColumnName("created_for");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.FeedbackCategoryId).HasColumnName("feedback_category_id");

                entity.Property(e => e.LastUpdate)
                    .HasColumnName("last_update")
                    .HasColumnType("datetime");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasColumnName("message");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.Subject)
                    .HasColumnName("subject");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.FeedbackCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_feedback_To_users_by");

                entity.HasOne(d => d.CreatedForNavigation)
                    .WithMany(p => p.FeedbackCreatedForNavigation)
                    .HasForeignKey(d => d.CreatedFor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_feedback_To_users_for");

                entity.HasOne(d => d.FeedbackCategory)
                    .WithMany(p => p.Feedback)
                    .HasForeignKey(d => d.FeedbackCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_feedback_To_category_id");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Feedback)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_feedback_To_feedback_status");
            });

            modelBuilder.Entity<FeedbackCategory>(entity =>
            {
                entity.ToTable("feedback_category");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastUpdate)
                    .HasColumnName("last_update")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");
            });

            modelBuilder.Entity<FeedbackEscalationMapping>(entity =>
            {
                entity.ToTable("feedback_escalation_mapping");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EscalatedUserId).HasColumnName("escalated_user_id");

                entity.Property(e => e.FeedbackId).HasColumnName("feedback_id");

                entity.Property(e => e.LastUpdate)
                    .HasColumnName("last_update")
                    .HasColumnType("datetime");

                entity.Property(e => e.Message).HasColumnName("message");

                entity.Property(e => e.Subject).HasColumnName("subject");

                entity.HasOne(d => d.EscalatedUser)
                    .WithMany(p => p.FeedbackEscalationMapping)
                    .HasForeignKey(d => d.EscalatedUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_feedback_escalation_mapping_To_users");

                entity.HasOne(d => d.Feedback)
                    .WithMany(p => p.FeedbackEscalationMapping)
                    .HasForeignKey(d => d.FeedbackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_feedback_escalation_mapping_To_feedback");
            });

            modelBuilder.Entity<FeedbackStatus>(entity =>
            {
                entity.ToTable("feedback_status");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<RegisteredDevice>(entity =>
            {
                entity.ToTable("registered_device");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DeviceId)
                    .IsRequired()
                    .HasColumnName("device_id");

                entity.Property(e => e.LastUpdate)
                    .HasColumnName("last_update")
                    .HasColumnType("datetime");

                entity.Property(e => e.OsType)
                    .IsRequired()
                    .HasColumnName("os_type")
                    .HasMaxLength(50);

                entity.Property(e => e.Otp)
                    .HasColumnName("otp")
                    .HasMaxLength(50);

                entity.Property(e => e.RegisteredOn)
                    .HasColumnName("registered_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RegisteredDevice)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_registered_device_To_users");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("user_role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasColumnName("role_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Scale).HasColumnName("scale");
            });

            modelBuilder.Entity<UserRoleMapping>(entity =>
            {
                entity.ToTable("user_role_mapping");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LastUpdate)
                    .HasColumnName("last_update")
                    .HasColumnType("datetime");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoleMapping)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_role_mapping_To_user_role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoleMapping)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_role_mapping_To_user");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.UserId)
                    .HasName("UQ__tmp_ms_x__B9BE370EE80A977C")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.LastUpdate)
                    .HasColumnName("last_update")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.RegisteredOn)
                    .HasColumnName("registered_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("user_id")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.InverseCompany)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_users_To_company");
            });
        }
    }
}
