using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using StudentCredit.Data.Rdpzsd.Students.Parts.PersonStudent;

namespace StudentCredit.Persistence.Configurations.Rdpzsd
{
	public class PersonStudentInfoConfiguration : IEntityTypeConfiguration<PersonStudentInfo>
	{
		public void Configure(EntityTypeBuilder<PersonStudentInfo> builder)
		{
			builder.HasOne(p => p.PersonStudent)
				   .WithOne(a => a.PartInfo)
				   .HasForeignKey<PersonStudentInfo>(p => p.Id);
		}
	}
}
