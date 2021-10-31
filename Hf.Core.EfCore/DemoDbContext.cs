﻿using System;
using Hf.Core.EfCore.Models;
using Microsoft.EntityFrameworkCore;

namespace Hf.Core.EfCore
{
    public class DemoDbContext : DbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().HasKey(e => e.Id);
            modelBuilder.Entity<Customer>().HasMany<Address>();
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
