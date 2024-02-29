using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using StudentCredit.Data.Rdpzsd.Students.Parts.PersonBasic;

namespace StudentCredit.Persistence.Configurations.Rdpzsd
{
	public class PersonBasicInfoConfiguration : IEntityTypeConfiguration<PersonBasicInfo>
	{
		public void Configure(EntityTypeBuilder<PersonBasicInfo> builder)
		{
			builder.HasOne(p => p.PersonBasic)
				   .WithOne(a => a.PartInfo)
				   .HasForeignKey<PersonBasicInfo>(p => p.Id);
		}
	}
}
