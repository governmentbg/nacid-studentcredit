using StudentCredit.Data.Common.Interfaces;

namespace StudentCredit.Data.Common.Models
{
    public class FileTemplate : AttachedFile, IEntity
    {
        public string Alias { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
