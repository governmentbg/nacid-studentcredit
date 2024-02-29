using Microsoft.EntityFrameworkCore;
using StudentCredit.Data.Rdpzsd.Nomenclatures;
using StudentCredit.Data.Rdpzsd.Nomenclatures.Institution;
using StudentCredit.Data.Rdpzsd.Nomenclatures.Settlement;
using StudentCredit.Data.Rdpzsd.Students;
using StudentCredit.Data.Rdpzsd.Students.Parts.PersonBasic;
using StudentCredit.Data.Rdpzsd.Students.Parts.PersonDoctoral;
using StudentCredit.Data.Rdpzsd.Students.Parts.PersonStudent;
using StudentCredit.Infrastructure.Interfaces.Contexts;
using System.Reflection;

namespace StudentCredit.Persistence
{
	public class RdpzsdDbContext : DbContext, IRdpzsdDbContext
	{
		public RdpzsdDbContext(DbContextOptions<RdpzsdDbContext> options)
			: base(options)
		{
		}

		#region Rdpzsd
		public DbSet<PersonLot> PersonLots { get; set; }
		public DbSet<PersonBasic> PersonBasics { get; set; }
		public DbSet<PersonBasicInfo> PersonBasicInfos { get; set; }

		public DbSet<PersonStudent> PersonStudents { get; set; }
		public DbSet<PersonStudentSemester> PersonStudentSemesters { get; set; }
		public DbSet<PersonStudentInfo> PersonStudentInfos { get; set; }

        public DbSet<PersonDoctoral> PersonDoctorals { get; set; }
		public DbSet<PersonDoctoralSemester> PersonDoctoralSemesters { get; set; }
		public DbSet<PersonDoctoralInfo> PersonDoctoralInfos { get; set; }
		#endregion

		#region Nomenclatures
		public DbSet<Country> Countries { get; set; }

		public DbSet<Institution> Institutions { get; set; }
		public DbSet<InstitutionSpeciality> InstitutionSpecialities { get; set; }
		public DbSet<Speciality> Specialities { get; set; }
		public DbSet<ResearchArea> ResearchAreas { get; set; }
		public DbSet<EducationalForm> EducationalForms { get; set; }
		public DbSet<EducationalQualification> EducationalQualifications { get; set; }

		public DbSet<StudentStatus> StudentStatuses { get; set; }
        public DbSet<StudentEvent> StudentEvents { get; set; }
        public DbSet<Period> Periods { get; set; }
		#endregion

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
		{
			return base.SaveChangesAsync(cancellationToken);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			ApplyConfigurations(modelBuilder);
			DisableCascadeDelete(modelBuilder);
			ConfigurePgSqlNameMappings(modelBuilder);
		}

		protected void ApplyConfigurations(ModelBuilder modelBuilder)
		{
			var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
				   .Where(t => t.GetInterfaces().Any(gi =>
					   gi.IsGenericType
					   && gi.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)) && t.Namespace == "StudentCredit.Persistence.Configurations.Rdpzsd")
				   .ToList();

			foreach (var type in typesToRegister)
			{
				dynamic configurationInstance = Activator.CreateInstance(type);
				modelBuilder.ApplyConfiguration(configurationInstance);
			}
		}

		protected void DisableCascadeDelete(ModelBuilder modelBuilder)
		{
			modelBuilder.Model.GetEntityTypes()
				.SelectMany(t => t.GetForeignKeys())
				.Where(fk => !fk.IsOwnership
					&& fk.DeleteBehavior == DeleteBehavior.Cascade)
				.ToList()
				.ForEach(e => e.DeleteBehavior = DeleteBehavior.Restrict);
		}

		protected void ConfigurePgSqlNameMappings(ModelBuilder modelBuilder)
		{
			foreach (var entity in modelBuilder.Model.GetEntityTypes())
			{
				// Configure pgsql table names convention.
				entity.SetTableName(entity.ClrType.Name.ToLower());

				// Configure pgsql column names convention.
				foreach (var property in entity.GetProperties())
					property.SetColumnName(property.Name.ToLower());
			}
		}
	}
}
