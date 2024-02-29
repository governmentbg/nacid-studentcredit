using StudentCredit.Application.Common.Models;
using StudentCredit.Infrastructure.Interfaces.Registration;

namespace StudentCredit.Application.Common.Interfaces
{
    public interface IPdfFileService : IScopedService
    {
        Task<MemoryStream> GeneratePdfFile<T>(T payload, byte[] content, bool closeStream = true);
        Task<byte[]> GenerateSignedPdfFile<T>(T payload, byte[] content, PdfSignFieldSettings signFieldSettings);
    }
}
