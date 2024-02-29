using FileStorageNetCore.Models;
using StudentCredit.Data.Banks;
using System.Linq.Expressions;

namespace StudentCredit.Application.Banks.Dtos
{
    public class BankDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Bulstat { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public TermsDto Terms { get; set; }
        public IEnumerable<BankContractDto> Contracts { get; set; }

        public static Expression<Func<Bank, BankDto>> SelectExpression
           => e => new BankDto
           {
               Id = e.Id,
               Name = e.Name,
               Bulstat = e.Bulstat,
               Address = e.Address,
               IsActive = e.IsActive,
               Description = e.Description,
               Terms = e.Terms != null ? new TermsDto
               {
                   Id = e.Terms.Id,
                   File = new AttachedFile
                   {
                       DbId = e.Terms.DbId,
                       Hash = e.Terms.Hash,
                       MimeType = e.Terms.MimeType,
                       Key = e.Terms.Key,
                       Name = e.Terms.Name,
                       Size = e.Terms.Size,
                   }
               } : new TermsDto(),
               Contracts = AddContracts(e.Contracts)
           };

        private static IEnumerable<BankContractDto> AddContracts(IEnumerable<BankContract> contracts)
        {
            var result = new List<BankContractDto>();

            foreach (var contract in contracts)
            {
                var bankContract = new BankContractDto
                {
                    Id = contract.Id,
                    Number = contract.Number,
                    Date = contract.Date,
                    Type = contract.Type,
                    File = contract.File != null ? new BankContractFile
                    {
                        DbId = contract.File.DbId,
                        Hash = contract.File.Hash,
                        Key = contract.File.Key,
                        MimeType = contract.File.MimeType,
                        Name = contract.File.Name,
                        Size = contract.File.Size,
                    } : null
                };

                result.Add(bankContract);
            }

            return result;
        }
    }
}
