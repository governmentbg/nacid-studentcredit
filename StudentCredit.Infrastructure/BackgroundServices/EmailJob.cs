using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using StudentCredit.Data.Emails.Enums;
using StudentCredit.Data.Emails;
using StudentCredit.Infrastructure.ConfigurationExtensions.Models;
using StudentCredit.Infrastructure.Interfaces.Contexts;
using StudentCredit.Infrastructure.Emails;

namespace StudentCredit.Infrastructure.BackgroundServices
{
    public class EmailJob : IHostedService, IDisposable
    {
        private readonly IServiceProvider serviceProvider;
        private readonly EmailsConfiguration emailConfiguration;
        private Timer timer;

        public EmailJob(IServiceProvider serviceProvider, IOptions<EmailsConfiguration> options)
        {
            this.serviceProvider = serviceProvider;
            emailConfiguration = options.Value;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(emailConfiguration.JobPeriod));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }

        private async void DoWork(object state)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<IAppDbContext>();
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                var pendingEmails = emailService.GetPendingEmails(emailConfiguration.JobLimit);

                foreach (var email in pendingEmails)
                {
                    bool isSent = emailService.SendEmail(email, emailConfiguration);
                    foreach (EmailAddressee addressee in email.Addressees)
                    {
                        addressee.Status = isSent ? EmailStatus.Sent : EmailStatus.Failed;
                    }
                }

                await dbContext.SaveChangesAsync();
            }
        }
    }
}