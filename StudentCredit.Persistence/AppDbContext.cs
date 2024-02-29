using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using StudentCredit.Data.Applications.ApplicationFive;
using StudentCredit.Data.Applications.ApplicationFour;
using StudentCredit.Data.Applications.ApplicationOne;
using StudentCredit.Data.Applications.ApplicationTwo;
using StudentCredit.Data.Applications.Common.Models;
using StudentCredit.Data.Banks;
using StudentCredit.Data.Common.Interfaces;
using StudentCredit.Data.Common.Models;
using StudentCredit.Data.Emails;
using StudentCredit.Data.Nomenclatures;
using StudentCredit.Data.SummaryReport;
using StudentCredit.Data.Users;
using StudentCredit.Infrastructure.Interfaces;
using StudentCredit.Infrastructure.Interfaces.Contexts;
using System.Reflection;

namespace StudentCredit.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        private readonly IUserContext userContext;

        public AppDbContext(
            IUserContext userContext,
            DbContextOptions<AppDbContext> options)
            : base(options)
        {
            this.userContext = userContext;
        }

        #region Nomenclatures
        public DbSet<ApplicationOneType> ApplicationOneTypes { get; set; }
        public DbSet<ApplicationFourType> ApplicationFourTypes { get; set; }
        public DbSet<AdjournType> AdjournTypes { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<InstitutionSpeciality> InstitutionSpecialities { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<ResearchArea> ResearchAreas { get; set; }
        public DbSet<EducationalQualification> EducationalQualifications { get; set; }
        public DbSet<EducationFormType> EducationFormTypes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<EmailType> EmailTypes { get; set; }
        public DbSet<ExtensionOfGracePeriod> ExtensionOfGracePeriods { get; set; }
        public DbSet<FileTemplate> FileTemplates { get; set; }
        public DbSet<Year> Years { get; set; }
		#endregion

		#region Applications
		public DbSet<ApplicationHistory> ApplicationHistories { get; set; }

		// ApplicationOne
		public DbSet<ApplicationOne> ApplicationOnes { get; set; }

        // ApplicationFour
        public DbSet<ApplicationFour> ApplicationFours { get; set; }
        public DbSet<ApplicationFourAttachedFile> ApplicationFourAttachedFiles { get; set; }

        // ApplicationTwo
        public DbSet<ApplicationTwo> ApplicationTwos { get; set; }
        public DbSet<RecordEntry> RecordEntries { get; set; }

        // ApplicationFive
        public DbSet<ApplicationFive> ApplicationFives { get; set; }
        public DbSet<ApplicationFiveAttachedFile> ApplicationFiveAttachedFiles { get; set; }
        #endregion

        #region Users
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<PasswordToken> PasswordTokens { get; set; }
        #endregion

        #region Emails
        public DbSet<Email> Emails { get; set; }
        public DbSet<EmailAddressee> EmailAddressees { get; set; }
        #endregion

        #region Banks
        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankContract> BankContracts { get; set; }
        public DbSet<BankContractFile> BankContractFiles { get; set; }
        public DbSet<Terms> Terms { get; set; }
        #endregion

        #region SummaryReport
        public DbSet<Sheet> Sheets { get; set; }
        public DbSet<SheetYear> SheetYears { get; set; }
        public DbSet<MonthData> MonthDatas { get; set; }
        public DbSet<YearLimit> YearLimits { get; set; }
		#endregion

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Configure name mappings
                entity.SetTableName(entity.ClrType.Name.ToLower());

                if (typeof(IEntity).IsAssignableFrom(entity.ClrType))
                {
                    modelBuilder.Entity(entity.ClrType)
                        .HasKey(nameof(IEntity.Id));
                }

                if (typeof(IConcurrency).IsAssignableFrom(entity.ClrType))
                {
                    modelBuilder.Entity(entity.ClrType)
                        .Property(nameof(IConcurrency.Version))
                        .IsConcurrencyToken()
                        .HasDefaultValue(0);
                }

                entity.GetProperties()
                    .ToList()
                    .ForEach(e => e.SetColumnName(e.Name.ToLower()));

                entity.GetForeignKeys()
                    .Where(e => !e.IsOwnership && e.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(e => e.DeleteBehavior = DeleteBehavior.Restrict);
            }

            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                   .Where(t => t.GetInterfaces().Any(gi =>
                       gi.IsGenericType
                       && gi.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)) && t.Namespace == "StudentCredit.Persistence.Configurations")
                   .ToList();
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
        }

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return Database.BeginTransactionAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (typeof(IAuditable).IsAssignableFrom(entry.Entity.GetType()) && entry.State == EntityState.Added)
                {
                    var entity = entry.Entity as IAuditable;
                    entity.CreateDate = DateTime.Now;
                    entity.CreatorUserId = this.userContext.UserId;
                }

                if (typeof(IConcurrency).IsAssignableFrom(entry.Entity.GetType()) && entry.State == EntityState.Modified)
                {
                    var entity = entry.Entity as IConcurrency;
                    entity.Version++;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
