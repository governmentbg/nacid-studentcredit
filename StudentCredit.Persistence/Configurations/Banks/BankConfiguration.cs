using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentCredit.Data.Banks;

namespace StudentCredit.Persistence.Configurations.Banks
{
    public class BankConfiguration : IEntityTypeConfiguration<Bank>
    {
        public void Configure(EntityTypeBuilder<Bank> builder)
        {
            builder
                .HasMany(b => b.Contracts)
                .WithOne(c => c.Bank)
                .HasForeignKey(c => c.BankId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
