﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication4
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Intern_prjEntities : DbContext
    {
        public Intern_prjEntities()
            : base("name=Intern_prjEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Cauhinh> Cauhinhs { get; set; }
        public DbSet<Don_vi> Don_vi { get; set; }
        public DbSet<Goithau> Goithaus { get; set; }
        public DbSet<Phong_ban> Phong_ban { get; set; }
        public DbSet<Pro> Pros { get; set; }
        public DbSet<Serial> Serials { get; set; }
        public DbSet<Thietbi> Thietbis { get; set; }
    }
}