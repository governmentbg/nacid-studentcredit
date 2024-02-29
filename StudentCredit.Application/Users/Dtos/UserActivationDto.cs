namespace StudentCredit.Application.Users.Dtos
{
    public class UserActivationDto
    {
        public string Token { get; set; }
        public string Password { get; set; }
        public string PasswordAgain { get; set; }
    }
}
