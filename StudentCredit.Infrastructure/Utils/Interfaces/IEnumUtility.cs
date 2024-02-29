using StudentCredit.Infrastructure.Interfaces.Registration;

namespace StudentCredit.Infrastructure.Utils.Interfaces
{
    public interface IEnumUtility : IScopedService
    {
        string GetDescription(object value);
    }
}
