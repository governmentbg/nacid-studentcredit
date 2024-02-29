using FileStorageNetCore.Models;

namespace StudentCredit.Application.Banks.Dtos
{
    public class TermsDto
    {
        public int Id { get; set; }
        public AttachedFile File { get; set; }
    }
}
