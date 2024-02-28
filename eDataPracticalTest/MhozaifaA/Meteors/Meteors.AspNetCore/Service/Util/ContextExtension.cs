using Meteors.AspNetCore.Infrastructure.ModelEntity.Securty;
using Microsoft.EntityFrameworkCore;


namespace Meteors
{
    public static class ContextExtension
    {

        public static IQueryable<TEntity> SortOptionsOrder<TEntity>(this IQueryable<TEntity> query, SortOptions sortOptions)
        {

            return sortOptions.Sort switch
            {
                Sort.Non => query,
                Sort.Ascending when CheckOrderAndThenByColumn<TEntity>(sortOptions) =>
                query.OrderBy(o => EF.Property<TEntity>(o, sortOptions.OrderByColumn)).ThenBy(o => EF.Property<TEntity>(o, sortOptions.ThenByColumn)),

                Sort.Ascending when CheckOrderByColumn<TEntity>(sortOptions) =>
                    query.OrderBy(o => EF.Property<TEntity>(o, sortOptions.OrderByColumn)),

                Sort.Descending when CheckOrderAndThenByColumn<TEntity>(sortOptions) =>
                    query.OrderByDescending(o => EF.Property<TEntity>(o, sortOptions.OrderByColumn)).ThenByDescending(o => EF.Property<TEntity>(o, sortOptions.ThenByColumn)),

                Sort.Descending when CheckOrderByColumn<TEntity>(sortOptions) =>
                    query.OrderByDescending(o => EF.Property<TEntity>(o, sortOptions.OrderByColumn)),

                _ => query,
            };
        }

        public static IQueryable<TEntity> Valid<TEntity>(this IQueryable<TEntity> query) where TEntity : IMrIdentity, IDeletable
        {
            return query.Where(q => !q.DateDeleted.HasValue);
        }

        public static IQueryable<TEntity> Invalid<TEntity>(this IQueryable<TEntity> query) where TEntity : IMrIdentity, IDeletable
        {
            return query.Where(q => q.DateDeleted.HasValue);
        }

       



        private static bool CheckOrderByColumn<TEntity>(SortOptions sortOptions)
        {
            return string.IsNullOrEmpty(sortOptions.OrderByColumn) && ObjectExtension.HasProperty<TEntity>(sortOptions.OrderByColumn);
        }

        private static bool CheckThenByColumn<TEntity>(SortOptions sortOptions)
        {
            return string.IsNullOrEmpty(sortOptions.ThenByColumn) && ObjectExtension.HasProperty<TEntity>(sortOptions.ThenByColumn);
        }

        private static bool CheckOrderAndThenByColumn<TEntity>(SortOptions sortOptions)
        {
            return CheckOrderByColumn<TEntity>(sortOptions) && CheckThenByColumn<TEntity>(sortOptions);
        }


    }
}
