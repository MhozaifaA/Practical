
namespace Meteors
{
    public static class EntityEntryExtension
    {
        public static TEntity ToEntity<TEntity>(this Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry)
            => entry.Entity.CastTo<TEntity>();

    }
}
