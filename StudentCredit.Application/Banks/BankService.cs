using Microsoft.EntityFrameworkCore;
using StudentCredit.Application.Banks.Dtos;
using StudentCredit.Application.Banks.Interfaces;
using StudentCredit.Application.Common.Dtos;
using StudentCredit.Data.Banks;
using StudentCredit.Infrastructure.Interfaces.Contexts;

namespace StudentCredit.Application.Banks
{
    public class BankService : IBankService
    {
        private readonly IAppDbContext context;

        public BankService(
            IAppDbContext context
            )
        {
            this.context = context;
        }

        public async Task Create(BankDto model)
        {
            var bank = new Bank
            {
                Name = model.Name,
                Bulstat = model.Bulstat,
                Address = model.Address,
                Description = model.Description,
                IsActive = true
            };

            if (model.Terms.File != null)
            {
                bank.Terms = new Terms
                {
                    DbId = model.Terms.File.DbId,
                    Hash = model.Terms.File.Hash,
                    Key = model.Terms.File.Key,
                    Size = model.Terms.File.Size,
                    MimeType = model.Terms.File.MimeType,
                    Name = model.Terms.File.Name
                };
            }

            foreach (var contract in model.Contracts)
            {
                var bankContract = new BankContract
                {
                    Number = contract.Number,
                    Type = contract.Type,
                    Date = contract.Date
                };

                if (contract.File != null)
                {
                    bankContract.File = new BankContractFile
                    {
                        DbId = contract.File.DbId,
                        Hash = contract.File.Hash,
                        Key = contract.File.Key,
                        Size = contract.File.Size,
                        MimeType = contract.File.MimeType,
                        Name = contract.File.Name
                    };
                }

                bank.Contracts.Add(bankContract);
            }

            this.context.Set<Bank>().Add(bank);
            await this.context.SaveChangesAsync();
        }

        public async Task<BankDto> Get(int id)
        {
            var result = await this.context.Set<Bank>()
                .Include(b => b.Contracts)
                    .ThenInclude(c => c.File)
                .Include(b => b.Terms)
                .Select(BankDto.SelectExpression)
                .Where(b => b.Id == id)
                .SingleOrDefaultAsync();

            return result;
        }

        public async Task<SearchResultItemDto<BankDto>> GetAll(BankSearchFilter filter)
        {
            var query = this.context.Set<Bank>()
                .Include(b => b.Contracts)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                query = query.Where(b => b.Name.ToLower().Trim().Contains(filter.Name.ToLower().Trim()));
            }

            if (!string.IsNullOrWhiteSpace(filter.Bulstat))
            {
                query = query.Where(b => b.Bulstat.ToLower().Trim().Contains(filter.Bulstat.ToLower().Trim()));
            }

            var result = await query
                .Select(BankDto.SelectExpression)
                .OrderBy(b => b.Name)
                .ToListAsync();

            return new SearchResultItemDto<BankDto>
            {
                Items = result,
                TotalCount = result.Count()
            };
        }

        public async Task<bool> ChangeStatus(int id)
        {
            var bank = await this.context.Set<Bank>()
                .SingleOrDefaultAsync(b => b.Id == id);

			bank.IsActive = !bank.IsActive;
            await this.context.SaveChangesAsync();

            return bank.IsActive;
        }

        public async Task Edit(BankDto model)
        {
            var bank = await this.context.Set<Bank>()
                .Include(b => b.Terms)
                .Include(b => b.Contracts)
                    .ThenInclude(c => c.File)
                .SingleOrDefaultAsync(b => b.Id == model.Id);

            bank.Update(model.Name, model.Address, model.Bulstat, model.Description);

            if (model.Terms.File != null)
            {
                if (bank.Terms != null)
                {
                    bank.Terms.AddFile(model.Terms.File);
                }
                else
                {
                    bank.Terms = new Terms
                    {
                        DbId = model.Terms.File.DbId,
                        Hash = model.Terms.File.Hash,
                        Key = model.Terms.File.Key,
                        Size = model.Terms.File.Size,
                        MimeType = model.Terms.File.MimeType,
                        Name = model.Terms.File.Name
                    };
                }
            }
            else
            {
                if (bank.Terms != null)
                {
                    this.context.Entry(bank.Terms).State = EntityState.Deleted;
                }
            }

            var existingCC = bank.Contracts.FirstOrDefault(c => c.Type == ContractType.Concluded);
            var newCC = model.Contracts.FirstOrDefault(c => c.Type == ContractType.Concluded);

            existingCC.Update(newCC.Number, newCC.Date);

            if (newCC.File != null)
            {
                if (existingCC.File != null)
                {
                    existingCC.File.Update(newCC.File);
                }
                else
                {
                    existingCC.File = new BankContractFile
                    {
                        DbId = newCC.File.DbId,
                        Hash = newCC.File.Hash,
                        Key = newCC.File.Key,
                        Size = newCC.File.Size,
                        MimeType = newCC.File.MimeType,
                        Name = newCC.File.Name
                    };
                }
            }
            else
            {
                if (existingCC.File != null)
                {
                    this.context.Entry(existingCC.File).State = EntityState.Deleted;
                }
            }

            var contractChange = model.Contracts.FirstOrDefault(c => c.Id == null);
            if (contractChange != null)
            {
                var bankContractChange = new BankContract
                {
                    Number = contractChange.Number,
                    Type = contractChange.Type,
                    Date = contractChange.Date
                };

                if (contractChange.File != null)
                {
                    bankContractChange.File = new BankContractFile
                    {
                        DbId = contractChange.File.DbId,
                        Hash = contractChange.File.Hash,
                        Key = contractChange.File.Key,
                        Size = contractChange.File.Size,
                        MimeType = contractChange.File.MimeType,
                        Name = contractChange.File.Name
                    };
                }

                bank.Contracts.Add(bankContractChange);
            }

            await this.context.SaveChangesAsync();
        }
    }
}
