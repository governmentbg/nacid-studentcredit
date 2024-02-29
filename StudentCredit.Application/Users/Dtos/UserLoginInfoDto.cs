using StudentCredit.Application.Nomenclatures.Dtos;

namespace StudentCredit.Application.Users.Dtos
{
    public class UserLoginInfoDto
    {
        public string Fullname { get; set; }
        public string Token { get; set; }
        public string RoleAlias { get; set; }
        public BankNomenclatureDto Bank { get; set; }
    }
}
