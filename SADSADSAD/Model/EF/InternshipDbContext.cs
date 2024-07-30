using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Model.EF
{
    public partial class InternshipDbContext : DbContext
    {
        public InternshipDbContext()
            : base("name=InternshipDbContext")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Cauhinh> Cauhinhs { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<Donvi> Donvis { get; set; }
        public virtual DbSet<Phongban> Phongbans { get; set; }
        public virtual DbSet<Pro> Pros { get; set; }
        public virtual DbSet<Storage> Storages { get; set; }
        public virtual DbSet<SubProject> SubProjects { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cauhinh>()
                .Property(e => e.Chip)
                .IsUnicode(false);

            modelBuilder.Entity<Cauhinh>()
                .Property(e => e.RAM)
                .IsUnicode(false);

            modelBuilder.Entity<Cauhinh>()
                .Property(e => e.HDD)
                .IsUnicode(false);

            modelBuilder.Entity<Cauhinh>()
                .Property(e => e.SSD)
                .IsUnicode(false);

            modelBuilder.Entity<Cauhinh>()
                .Property(e => e.Main)
                .IsUnicode(false);

            modelBuilder.Entity<Cauhinh>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Unit)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Department)
                .IsUnicode(false);

            modelBuilder.Entity<Donvi>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Donvi>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Phongban>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Phongban>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Storage>()
                .Property(e => e.Device_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Storage>()
                .Property(e => e.Serial_No)
                .IsUnicode(false);

            modelBuilder.Entity<Storage>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.name)
                .IsUnicode(false);
        }
    }
}
