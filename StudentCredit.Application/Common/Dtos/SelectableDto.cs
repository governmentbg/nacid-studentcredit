using System.Linq.Expressions;

namespace StudentCredit.Application.Common.Dtos
{
    public interface IMapping<TFrom, TTo>
    {
        public abstract Expression<Func<TFrom, TTo>> Map();
    }
}
