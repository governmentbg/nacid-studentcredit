using FileStorageNetCore.Models;
using StudentCredit.Data.Common.Interfaces;

namespace StudentCredit.Data.Banks
{
    public class BankContractFile : AttachedFile, IEntity
    {
        public int Id { get; set; }

        public BankContract Contract { get; set; }
        public int ContractId { get; set; }

        public void Update(AttachedFile file)
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
