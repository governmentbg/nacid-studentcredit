using FileStorageNetCore.Models;

namespace StudentCredit.Application.Banks.Dtos
{
    public class BankContractDto
    {
        public int? Id { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public ContractType Type { get; set; }
        public AttachedFile File { get; set; }
    }
}
