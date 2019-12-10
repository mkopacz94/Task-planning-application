using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPlanningApp.Model;

namespace TaskPlanningApp.Database
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ToDoTask> Tasks { get; set; }

        public DataContext() : base(GetConnection(), false)
        {

        }

        public static DbConnection GetConnection()
        {
            var connection = ConfigurationManager.ConnectionStrings["SQLiteConnection"];
            var factory = DbProviderFactories.GetFactory(connection.ProviderName);
            var dbCon = factory.CreateConnection();
            dbCon.ConnectionString = connection.ConnectionString;
            return dbCon;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new UsersMapping()).Add(new ToDoTaksMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}
