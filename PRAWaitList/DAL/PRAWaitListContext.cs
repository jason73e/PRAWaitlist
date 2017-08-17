using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PRAWaitList.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Configuration;

namespace PRAWaitList.DAL
{
    public class PRAWaitListContext : DbContext
    {
        public PRAWaitListContext() : base(ConfigurationManager.ConnectionStrings["PRAWaitlistConnection"].ConnectionString)
        {
            
        }
        public DbSet<FamilyModel> Families { get; set; }
        public DbSet<StudentModel> Students { get; set; }
        public DbSet<ParentModel> Parents { get; set; }
        public DbSet<SiblingModel> Siblings { get; set; }
        public DbSet<StateModel> States { get; set; }
        public DbSet<SchoolModel> Schools { get; set; }
        public DbSet<SchoolYearModel> SchoolYears { get; set; }
        public DbSet<HearAboutPRAModel> HearAboutPRAs { get; set; }
        public DbSet<LotteryBatchModel> LotteryBatches { get; set; }
        public DbSet<LotteryModel> Lotteries { get; set; }
        public DbSet<ConfigurationSettingsModel> ConfigurationSettings { get; set; }

        public DbSet<EmailQueueModel> EmailQueues { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<PRAWaitListContext>(null);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public System.Data.Entity.DbSet<PRAWaitList.Models.StatusModel> StatusModels { get; set; }
    }
}