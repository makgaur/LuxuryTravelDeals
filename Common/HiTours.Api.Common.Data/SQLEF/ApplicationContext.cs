using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.Api.Common.Data.SQLEF
{
    public class ApplicationContext : DbContext
    {
        private readonly string connectionString;
        public ApplicationContext()
        {
        }
        public ApplicationContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer(this.connectionString);

            
        }
    }
}
