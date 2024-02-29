using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using StudentCredit.Data.Rdpzsd.Students.Parts.PersonDoctoral;

namespace StudentCredit.Persistence.Configurations.Rdpzsd
{
	public class PersonDoctoralInfoConfiguration : IEntityTypeConfiguration<PersonDoctoralInfo>
	{
		public void Configure(EntityTypeBuilder<PersonDoctoralInfo> builder)
		{
			builder.HasOne(p => p.PersonDoctoral)
				   .WithOne(a => a.PartInfo)
				   .HasForeignKey<PersonDoctoralInfo>(p => p.Id);
		}
	}
}
