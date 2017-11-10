using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Configuration;

namespace WaitListService
{
    public class dbContext : DbContext
    {
        public dbContext() :base(ConfigurationManager.ConnectionStrings["PRAWaitlistConnection"].ConnectionString)
        {

        }
        public DbSet<AppTask> AppTasks { get; set; }
        public DbSet<AppTaskHistory> AppTaskHistories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<PRAWaitListContext>(null);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}


