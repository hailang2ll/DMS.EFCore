using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DMS.EFCore.Repository.Mysql.Models
{
    public partial class trydou_sysMysqlContext : DbContext
    {
        public trydou_sysMysqlContext()
        {
        }

        public trydou_sysMysqlContext(DbContextOptions<trydou_sysMysqlContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SysAddress> SysAddresses { get; set; }
        public virtual DbSet<SysJobLog> SysJobLogs { get; set; }
        public virtual DbSet<SysLog> SysLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<SysAddress>(entity =>
            {
                entity.ToTable("Sys_Address");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CityCode).HasMaxLength(50);

                entity.Property(e => e.MergerShortName).HasMaxLength(200);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(50);

                entity.Property(e => e.ShortName).HasMaxLength(50);

                entity.Property(e => e.ZipCode).HasMaxLength(50);
            });

            modelBuilder.Entity<SysJobLog>(entity =>
            {
                entity.HasKey(e => e.JobLogId)
                    .HasName("PRIMARY");

                entity.ToTable("Sys_JobLog");

                entity.Property(e => e.JobLogId).HasColumnName("JobLogID");

                entity.Property(e => e.CreateTime).HasColumnType("timestamp");

                entity.Property(e => e.Message).HasMaxLength(4000);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.ServerIp)
                    .HasMaxLength(32)
                    .HasColumnName("ServerIP");
            });

            modelBuilder.Entity<SysLog>(entity =>
            {
                entity.HasKey(e => e.LogId)
                    .HasName("PRIMARY");

                entity.ToTable("Sys_Log");

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.CreateTime).HasColumnType("timestamp");

                entity.Property(e => e.Exception).HasMaxLength(2048);

                entity.Property(e => e.Ip)
                    .HasMaxLength(32)
                    .HasColumnName("IP");

                entity.Property(e => e.Level).HasMaxLength(128);

                entity.Property(e => e.Logger).HasMaxLength(512);

                entity.Property(e => e.MemberName)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Message).HasMaxLength(2048);

                entity.Property(e => e.SubSysId).HasColumnName("SubSysID");

                entity.Property(e => e.SubSysName).HasMaxLength(256);

                entity.Property(e => e.Thread).HasMaxLength(128);

                entity.Property(e => e.Url).HasMaxLength(256);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
