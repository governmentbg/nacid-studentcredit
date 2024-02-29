using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentCredit.Data.Applications.ApplicationTwo;

namespace StudentCredit.Persistence.Configurations
{
	public class ApplicationTwoConfiguration : IEntityTypeConfiguration<ApplicationTwo>
	{
		public void Configure(EntityTypeBuilder<ApplicationTwo> builder)
		{
			builder
				.HasMany(e => e.RecordEntries)
				.WithOne(c => c.ApplicationTwo)
				.HasForeignKey(e => e.ApplicationTwoId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
