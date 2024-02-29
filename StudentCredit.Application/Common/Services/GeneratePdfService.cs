using StudentCredit.Application.Common.Interfaces;

namespace StudentCredit.Application.Common.Services
{
    public class GeneratePdfService : IGeneratePdfService
    {
        private readonly ITemplateService templateService;
        private readonly IPdfFileService pdfFileService;
        public GeneratePdfService(ITemplateService templateService, IPdfFileService pdfFileService)
        {
            this.templateService = templateService;
            this.pdfFileService = pdfFileService;
        }

        public async Task<byte[]> GeneratePdf(object items, string tempalteAlias, bool isOneItem)
        {
            var template = await this.templateService.GetTemplateAsync(tempalteAlias);
            var stream = await this.pdfFileService.GeneratePdfFile(isOneItem ? items : new { Items = items }, template);
            return stream.ToArray();
        }
    }
}
