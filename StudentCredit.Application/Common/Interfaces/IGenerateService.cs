using StudentCredit.Infrastructure.Interfaces.Registration;

namespace StudentCredit.Application.Common.Interfaces
{
    public interface IGeneratePdfService : IScopedService
    {
        Task<byte[]> GeneratePdf(object items, string templateAlias, bool isOneItem);
    }
}
