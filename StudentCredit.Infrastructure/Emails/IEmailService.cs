using StudentCredit.Data.Emails;
using StudentCredit.Infrastructure.Interfaces;
using StudentCredit.Infrastructure.Interfaces.Registration;

namespace StudentCredit.Infrastructure.Emails
{
    public interface IEmailService : IScopedService
	{
        IEnumerable<Email> GetPendingEmails(int limit);

        Task<Email> ComposeEmailAsync(string alias, object templateData, params string[] recipients);

        bool SendEmail(Email email, IEmailConfiguration emailConfiguration);
    }
}
