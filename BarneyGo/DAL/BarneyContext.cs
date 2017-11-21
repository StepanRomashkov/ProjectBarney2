using System;
using System.Collections.Generic;
using BarneyGo.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BarneyGo.DAL
{
    public class BarneyContext: DbContext
    {

        //public BarneyContext(): base("BarneyContext")
        //{
        //}

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Syllabus> Syllabuses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Day> Days { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}