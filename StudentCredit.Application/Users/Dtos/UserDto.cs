using StudentCredit.Application.Banks.Dtos;
using StudentCredit.Data.Users.Enums;
using StudentCredit.Data.Users;
using System.Linq.Expressions;

namespace StudentCredit.Application.Users.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }

        public UserStatus Status { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public int BankId { get; set; }
        public BankDto Bank { get; set; }

        public static Expression<Func<User, UserDto>> SelectExpression => e => new UserDto
        {
            Id = e.Id,
            FirstName = e.FirstName,
            MiddleName = e.MiddleName,
            LastName = e.LastName,
            Email = e.Email,
            Phone = e.Phone,
            Status = e.Status,
            Role = new Role
            {
                Id = e.RoleId,
                Alias = e.Role.Alias,
                Name = e.Role.Name
            },
            Bank = e.Bank != null
            ? new BankDto
            {
                Id = e.Bank.Id,
                Name = e.Bank.Name,
            }
            : null
        };
    }
}
