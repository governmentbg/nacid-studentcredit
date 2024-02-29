using FileStorageNetCore.Models;
using StudentCredit.Data.Common.Interfaces;

namespace StudentCredit.Data.Banks
{
    public class BankContract : IEntity
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public ContractType Type { get; set; }

        public Bank Bank { get; set; }
        public int BankId { get; set; }

        public BankContractFile File { get; set; }

        public void Update(string number, DateTime date)
        {
            this.Number = number;
            this.Date = date;
        }
    }
}
