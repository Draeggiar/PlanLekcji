﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Plan_lekcji.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PlanLekcjiEntities : DbContext
    {
        public PlanLekcjiEntities()
            : base("name=PlanLekcjiEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Dzien> Dzien { get; set; }
        public DbSet<Plan> Plan { get; set; }
        public DbSet<Przedmiot> Przedmiot { get; set; }
    }
}