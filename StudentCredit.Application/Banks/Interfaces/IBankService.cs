using StudentCredit.Application.Banks.Dtos;
using StudentCredit.Application.Common.Dtos;
using StudentCredit.Infrastructure.Interfaces.Registration;

namespace StudentCredit.Application.Banks.Interfaces
{
    public interface IBankService : ITransientService
	{
        public Task<SearchResultItemDto<BankDto>> GetAll(BankSearchFilter filter);
        public Task<BankDto> Get(int id);
        public Task Create(BankDto model);
        public Task Edit(BankDto model);
        public Task<bool> ChangeStatus(int id);
    }
}
