using Domain;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Data
{
    public class DatabaseContext : DbContext, IDisposable
    {
        public DatabaseContext()
    : this("MainDatabase")
        {
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 300;
        }

        public DatabaseContext(string connectionString)
            : base(connectionString)
        {
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 300;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<DatabaseContext>(null);
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Employee> Employees { get; set; }
    }
}

