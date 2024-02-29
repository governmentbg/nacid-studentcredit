using FileStorageNetCore.Models;
using StudentCredit.Data.Applications.ApplicationFour.Enums;

namespace StudentCredit.Application.Applications.ApplicationsFour.Dtos
{
    public class ApplicationFourAttachedFileDto
    {
        public ApplicationFourAttachedFileType Type { get; set; }

        public AttachedFile File { get; set; }
    }
}
