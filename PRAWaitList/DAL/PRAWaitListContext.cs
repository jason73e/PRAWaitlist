using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PRAWaitList.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Configuration;
using System.Data.Entity.Validation;

namespace PRAWaitList.DAL
{
    public class PRAWaitListContext : DbContext
    {
        public PRAWaitListContext() : base(ConfigurationManager.ConnectionStrings["PRAWaitlistConnection"].ConnectionString)
        {
            Configuration.ValidateOnSaveEnabled = false;
            
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
        public DbSet<Results> Results { get; set; }

        public DbSet<EmailQueueModel> EmailQueues { get; set; }

        public DbSet<EmailControlModel> EmailControls { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<PRAWaitListContext>(null);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public System.Data.Entity.DbSet<PRAWaitList.Models.StatusModel> StatusModels { get; set; }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat("The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        public System.Data.Entity.DbSet<PRAWaitList.Models.PRAMenuModel> PRAMenuModels { get; set; }

        public System.Data.Entity.DbSet<PRAWaitList.Models.PortalMenusModel> PortalMenusModels { get; set; }
    }
}