using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace StudentCredit.Infrastructure.Interfaces.Contexts
{
	public interface IBaseContext
	{
		DbSet<T> Set<T>() where T : class;
		EntityEntry<TEntity> Entry<TEntity>([NotNull] TEntity entity) where TEntity : class;

		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
