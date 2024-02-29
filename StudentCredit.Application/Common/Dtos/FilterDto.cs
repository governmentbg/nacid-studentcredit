namespace StudentCredit.Application.Common.Dtos
{
	public class FilterDto<TEntity>
		where TEntity : class
	{
		public string TextFilter { get; set; }
		public bool? IsActive { get; set; }

		public int? Limit { get; set; } = 10;
		public int? Offset { get; set; } = 0;

		public virtual IQueryable<TEntity> WhereBuilder(IQueryable<TEntity> query)
		{
			return query;
		}

		public virtual IQueryable<TEntity> OrderBuilder(IQueryable<TEntity> query)
		{
			return query;
		}
	}
}
