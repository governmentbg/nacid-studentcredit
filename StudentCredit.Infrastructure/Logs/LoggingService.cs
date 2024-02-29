using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using StudentCredit.Data.Logs;
using StudentCredit.Data.Logs.Enums;
using StudentCredit.Infrastructure.DomainValidation.Models;
using StudentCredit.Infrastructure.Interfaces.Contexts;
using StudentCredit.Infrastructure.Logs.Extensions;

namespace StudentCredit.Infrastructure.Logs
{
    public class LoggingService : ILoggingService
    {
        private readonly IAppLogContext context;

        public LoggingService(IAppLogContext context)
        {
            this.context = context;
        }

        public async Task LogExceptionAsync(Exception exception, LogType type, HttpRequest request = null)
        {
            string exceptionMessage;
            if (exception is DomainErrorException customException)
            {
                exceptionMessage = "Message:" + " " + string.Join(", ", customException.ErrorMessages.Select(x => x.DomainErrorCode)) + " " + $"\nStackTrace: {exception.StackTrace}";
            }
            else
            {
                exceptionMessage = $"Message: {exception.Message} \nStackTrace: {exception.StackTrace}";
            }

            await this.LogAsync(type, exceptionMessage, request);
        }

        public async Task LogRequestAsync(HttpRequest request)
        {
            await this.LogAsync(LogType.TraceLog, null, request);
        }

		public async Task LogMessageAsync(string message, HttpRequest request = null)
		{
			await this.LogAsync(LogType.InformationMessageLog, message, request);
		}

		public async Task LogConsumerException(string message)
		{
			await this.LogAsync(LogType.RabbitMQ, message);
		}

		private async Task LogAsync(LogType type, string message, HttpRequest request = null)
        {
            var log = new Log
            {
                Type = type,
                LogDate = DateTime.UtcNow,
                IP = request?.HttpContext.Connection.RemoteIpAddress.ToString(),
                Verb = request?.Method,
                Url = request?.GetDisplayUrl(),
                UserAgent = request?.Headers["User-Agent"].ToString(),
                Message = message,
                UserId = request?.GetUserId()
            };

            this.context.Set<Log>().Add(log);
            await this.context.SaveChangesAsync();
        }
    }
}
