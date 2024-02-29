using FileStorageNetCore.Models;
using StudentCredit.Data.Common.Interfaces;

namespace StudentCredit.Data.Banks
{
    public class Terms : AttachedFile, IEntity
    {
        public int Id { get; set; }

        public Bank Bank { get; set; }
        public int BankId { get; set; }

        public void AddFile(AttachedFile file)
        {
            this.DbId = file.DbId;
            this.Hash = file.Hash;
            this.Key = file.Key;
            this.Size = file.Size;
            this.MimeType = file.MimeType;
            this.Name = file.Name;
        }
    }
}
