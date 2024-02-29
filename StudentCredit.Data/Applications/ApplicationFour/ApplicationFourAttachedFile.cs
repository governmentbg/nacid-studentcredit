using FileStorageNetCore.Models;
using StudentCredit.Data.Applications.ApplicationFour.Enums;
using StudentCredit.Data.Common.Interfaces;

namespace StudentCredit.Data.Applications.ApplicationFour
{
    public class ApplicationFourAttachedFile : AttachedFile, IEntity
    {
        public int Id { get; set; }
        public ApplicationFourAttachedFileType Type { get; set; }

        public int ApplicationFourId { get; set; }
        public ApplicationFour ApplicationFour { get; set; }
    }
}
