using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentCredit.Data.SummaryReport;

namespace StudentCredit.Persistence.Configurations
{
	public class SheetConfiguration : IEntityTypeConfiguration<Sheet>
	{
		public void Configure(EntityTypeBuilder<Sheet> builder)
		{
			builder
				.HasMany(e => e.SheetList)
				.WithOne(c => c.Sheet)
				.HasForeignKey(c => c.SheetId);
		}
	}
}
