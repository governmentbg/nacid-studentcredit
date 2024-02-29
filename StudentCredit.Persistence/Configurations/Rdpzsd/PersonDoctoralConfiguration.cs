using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using StudentCredit.Data.Rdpzsd.Students.Parts.PersonDoctoral;

namespace StudentCredit.Persistence.Configurations.Rdpzsd
{
	public class PersonDoctoralConfiguration : IEntityTypeConfiguration<PersonDoctoral>
	{
		public void Configure(EntityTypeBuilder<PersonDoctoral> builder)
		{
			builder.HasMany(e => e.Semesters)
				.WithOne()
				.HasForeignKey(e => e.PartId);
		}
	}
}
