using StudentCredit.Infrastructure.Interfaces.Registration;

namespace StudentCredit.Application.Common.Interfaces
{
    public interface ITemplateService : ITransientService
    {
        Task<byte[]> GetTemplateAsync(string alias);
    }
}
