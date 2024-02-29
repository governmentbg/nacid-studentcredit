using StudentCredit.Data.Banks;
using StudentCredit.Data.Users.Enums;
using System.Text.Json.Serialization;

namespace StudentCredit.Data.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        [JsonIgnore]
        public string PasswordSalt { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public UserStatus Status { get; set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public int? BankId { get; set; }
        public Bank Bank { get; set; }

        public User(string firstName, string middleName, string lastName, string email, string phone, int roleId)
        {
            this.Username = email?.Trim().ToLower(); ;
            this.FirstName = firstName;
            this.MiddleName = middleName;
            this.LastName = lastName;
            this.Email = email?.Trim().ToLower();
            this.Phone = phone;
            this.RoleId = roleId;

            this.CreateDate = DateTime.UtcNow;
            this.Status = UserStatus.Inactive;
        }

        public void Activate(string passwordHash, string passwordSalt)
        {
            this.Password = passwordHash;
            this.PasswordSalt = passwordSalt;
            this.UpdateDate = DateTime.UtcNow;
            this.Status = UserStatus.Active;
        }

        public void ChangePassword(string passwordHash, string passwordSalt)
        {
            this.Password = passwordHash;
            this.PasswordSalt = passwordSalt;
            this.UpdateDate = DateTime.UtcNow;
        }

        public void Update(string email, string phone, string firstName, string middleName, string lastName, int roleId)
        {
            this.Username = email?.Trim().ToLower();
            this.Email = email?.Trim().ToLower();
            this.Phone = phone;
            this.FirstName = firstName;
            this.MiddleName = middleName;
            this.LastName = lastName;
            this.RoleId = roleId;
        }

        public void UpdateBank(int? bankId)
        {
            this.BankId = bankId;
        }

        public void ClearBank()
        {
            this.BankId = null;
        }
    }
}
