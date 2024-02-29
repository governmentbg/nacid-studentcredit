using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using StudentCredit.Data.Rdpzsd.Students.Parts.PersonBasic;
using StudentCredit.Data.Rdpzsd.Students;

namespace StudentCredit.Persistence.Configurations.Rdpzsd
{
	public class PersonLotConfiguration : IEntityTypeConfiguration<PersonLot>
	{
		public void Configure(EntityTypeBuilder<PersonLot> builder)
		{
			builder.HasIndex(e => e.Uan)
				.IsUnique();

			builder.HasOne(e => e.PersonBasic)
				.WithOne(c => c.Lot)
				.HasForeignKey<PersonBasic>();

			builder.HasMany(e => e.PersonStudents)
				.WithOne(c => c.Lot)
				.HasForeignKey(e => e.LotId);

			builder.HasMany(e => e.PersonDoctorals)
				.WithOne(c => c.Lot)
				.HasForeignKey(e => e.LotId);
		}
	}
}
