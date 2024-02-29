using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentCredit.Data.SummaryReport;

namespace StudentCredit.Persistence.Configurations
{
	public class SheetYearConfiguration : IEntityTypeConfiguration<SheetYear>
	{
		public void Configure(EntityTypeBuilder<SheetYear> builder)
		{
			builder
				.HasMany(e => e.SheetList)
				.WithOne(c => c.SheetYear)
				.HasForeignKey(c => c.SheetYearId);
		}
	}
}
