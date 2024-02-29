using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using StudentCredit.Data.Rdpzsd.Students.Parts.PersonStudent;

namespace StudentCredit.Persistence.Configurations.Rdpzsd
{
	public class PersonStudentConfiguration : IEntityTypeConfiguration<PersonStudent>
	{
		public void Configure(EntityTypeBuilder<PersonStudent> builder)
		{
			builder.HasMany(e => e.Semesters)
				.WithOne()
				.HasForeignKey(e => e.PartId);
		}
	}
}
