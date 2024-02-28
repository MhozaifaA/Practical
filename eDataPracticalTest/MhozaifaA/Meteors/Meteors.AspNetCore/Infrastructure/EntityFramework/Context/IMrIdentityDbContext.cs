using Meteors.AspNetCore.Infrastructure.EntityFramework.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Meteors
{
    public interface IMrIdentityDbContext<TKey>: IDisposable  where TKey :struct, IEquatable<TKey> 
    {
        DbContext Base { get; }
        IHttpResolverService CurrentHttpResolverService { get; }

        TKey? CurrentUserId { get; }
        //string Lang { get; }

        event BeforeSaveChangesSignature BeforeSaveChangesSignature;

        EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class, IBaseEntity<TKey>;
        ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class, IBaseEntity<TKey>;
        void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IBaseEntity<TKey>;
        void AddRange<TEntity>(params TEntity[] entities) where TEntity : class, IBaseEntity<TKey>;
        Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class, IBaseEntity<TKey>;
      
        TEntity Find<TEntity>([NotNull] params TKey[] keyValues) where TEntity : class, IBaseEntity<TKey>;
        ValueTask<TEntity> FindAsync<TEntity>([NotNull] params TKey[] keyValues) where TEntity : class, IBaseEntity<TKey>;
        T? GetService<T>() where T : class;
        T GetService<T>(bool isInternal = false);
        EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class, IBaseEntity<TKey>;
        void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IBaseEntity<TKey>;
        void RemoveRange<TEntity>(params TEntity[] entities) where TEntity : class, IBaseEntity<TKey>;
        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
        int SaveChangesDeleted();
        Task<int> SaveChangesDeletedAsync(CancellationToken cancellationToken = default);
        DbSet<TEntity> Set<TEntity>() where TEntity : class, IBaseEntity<TKey>;
        EntityEntry<TEntity> SoftDelete<TEntity>(Expression<Func<TEntity, bool>> findEntity) where TEntity : class, IBaseEntity<TKey>;
        EntityEntry<TEntity> SoftDelete<TEntity>(TEntity entity) where TEntity : class, IBaseEntity<TKey>;
        EntityEntry<TEntity> SoftDelete<TEntity>(TKey key) where TEntity : class, IBaseEntity<TKey>;
        Task<EntityEntry<TEntity>> SoftDeleteAsync<TEntity>(Expression<Func<TEntity, bool>> findEntity) where TEntity : class, IBaseEntity<TKey>;
        Task<EntityEntry<TEntity>> SoftDeleteAsync<TEntity>(TKey key) where TEntity : class, IBaseEntity<TKey>;
        EntityEntry<TEntity> SoftDeleteTraversal<TEntity>(TEntity entity) where TEntity : class, IBaseEntity<TKey>;
        Task<EntityEntry<TEntity>> SoftDeleteTraversalAsync<TEntity, TProperty, TPreviousProperty, TPreviousPreviousProperty, TPreviousPreviousPreviousProperty, TPreviousPreviousPreviousPreviousProperty>(Expression<Func<TEntity, bool>> findEntity, Expression<Func<TEntity, ICollection<TProperty>>> include, Expression<Func<TProperty, ICollection<TPreviousProperty>>> thenInclude, Expression<Func<TPreviousProperty, ICollection<TPreviousPreviousProperty>>> thenNextInclude, Expression<Func<TPreviousPreviousProperty, ICollection<TPreviousPreviousPreviousProperty>>> thenNextNextInclude, Expression<Func<TPreviousPreviousPreviousProperty, ICollection<TPreviousPreviousPreviousPreviousProperty>>> thenNextNextNextInclude, params string[] navigationPropertyPath) where TEntity : class, IBaseEntity<TKey>;
        Task<EntityEntry<TEntity>> SoftDeleteTraversalAsync<TEntity, TProperty, TPreviousProperty, TPreviousPreviousProperty, TPreviousPreviousPreviousProperty, TPreviousPreviousPreviousPreviousProperty>(TKey key, Expression<Func<TEntity, ICollection<TProperty>>> include, Expression<Func<TProperty, ICollection<TPreviousProperty>>> thenInclude, Expression<Func<TPreviousProperty, ICollection<TPreviousPreviousProperty>>> thenNextInclude, Expression<Func<TPreviousPreviousProperty, ICollection<TPreviousPreviousPreviousProperty>>> thenNextNextInclude, Expression<Func<TPreviousPreviousPreviousProperty, ICollection<TPreviousPreviousPreviousPreviousProperty>>> thenNextNextNextInclude, params string[] navigationPropertyPath) where TEntity : class, IBaseEntity<TKey>;
        Task<EntityEntry<TEntity>> SoftDeleteTraversalAsync<TEntity, TProperty, TPreviousProperty, TPreviousPreviousProperty, TPreviousPreviousPreviousProperty>(Expression<Func<TEntity, bool>> findEntity, Expression<Func<TEntity, ICollection<TProperty>>> include, Expression<Func<TProperty, ICollection<TPreviousProperty>>> thenInclude, Expression<Func<TPreviousProperty, ICollection<TPreviousPreviousProperty>>> thenNextInclude, Expression<Func<TPreviousPreviousProperty, ICollection<TPreviousPreviousPreviousProperty>>> thenNextNextInclude, params string[] navigationPropertyPath) where TEntity : class, IBaseEntity<TKey>;
        Task<EntityEntry<TEntity>> SoftDeleteTraversalAsync<TEntity, TProperty, TPreviousProperty, TPreviousPreviousProperty, TPreviousPreviousPreviousProperty>(TKey key, Expression<Func<TEntity, ICollection<TProperty>>> include, Expression<Func<TProperty, ICollection<TPreviousProperty>>> thenInclude, Expression<Func<TPreviousProperty, ICollection<TPreviousPreviousProperty>>> thenNextInclude, Expression<Func<TPreviousPreviousProperty, ICollection<TPreviousPreviousPreviousProperty>>> thenNextNextInclude, params string[] navigationPropertyPath) where TEntity : class, IBaseEntity<TKey>;
        Task<EntityEntry<TEntity>> SoftDeleteTraversalAsync<TEntity, TProperty, TPreviousProperty, TPreviousPreviousProperty>(Expression<Func<TEntity, bool>> findEntity, Expression<Func<TEntity, ICollection<TProperty>>> include, Expression<Func<TProperty, ICollection<TPreviousProperty>>> thenInclude, Expression<Func<TPreviousProperty, ICollection<TPreviousPreviousProperty>>> thenNextInclude, params string[] navigationPropertyPath) where TEntity : class, IBaseEntity<TKey>;
        Task<EntityEntry<TEntity>> SoftDeleteTraversalAsync<TEntity, TProperty, TPreviousProperty, TPreviousPreviousProperty>(TKey key, Expression<Func<TEntity, ICollection<TProperty>>> include, Expression<Func<TProperty, ICollection<TPreviousProperty>>> thenInclude, Expression<Func<TPreviousProperty, ICollection<TPreviousPreviousProperty>>> thenNextInclude, params string[] navigationPropertyPath) where TEntity : class, IBaseEntity<TKey>;
        Task<EntityEntry<TEntity>> SoftDeleteTraversalAsync<TEntity, TProperty, TPreviousProperty>(Expression<Func<TEntity, bool>> findEntity, Expression<Func<TEntity, ICollection<TProperty>>> include, Expression<Func<TProperty, ICollection<TPreviousProperty>>> thenInclude, params string[] navigationPropertyPath) where TEntity : class, IBaseEntity<TKey>;
        Task<EntityEntry<TEntity>> SoftDeleteTraversalAsync<TEntity, TProperty, TPreviousProperty>(TKey key, Expression<Func<TEntity, ICollection<TProperty>>> include, Expression<Func<TProperty, ICollection<TPreviousProperty>>> thenInclude, params string[] navigationPropertyPath) where TEntity : class, IBaseEntity<TKey>;
        Task<EntityEntry<TEntity>> SoftDeleteTraversalAsync<TEntity, TProperty>(Expression<Func<TEntity, bool>> findEntity, Expression<Func<TEntity, ICollection<TProperty>>> include, params string[] navigationPropertyPath) where TEntity : class, IBaseEntity<TKey>;
        Task<EntityEntry<TEntity>> SoftDeleteTraversalAsync<TEntity, TProperty>(TKey key, Expression<Func<TEntity, ICollection<TProperty>>> include, params string[] navigationPropertyPath) where TEntity : class, IBaseEntity<TKey>;
        Task<EntityEntry<TEntity>> SoftDeleteTraversalAsync<TEntity>(Expression<Func<TEntity, bool>> findEntity, params string[] navigationPropertyPath) where TEntity : class, IBaseEntity<TKey>;
        Task<EntityEntry<TEntity>> SoftDeleteTraversalAsync<TEntity>(TKey key, params string[] navigationPropertyPath) where TEntity : class, IBaseEntity<TKey>;
        
        EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class, IBaseEntity<TKey>;
        void UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IBaseEntity<TKey>;
        void UpdateRange<TEntity>(params TEntity[] entities) where TEntity : class, IBaseEntity<TKey>;

        //Task TransactionAsync(Action action);
        //Task<IEntity> TransactionAsync<IEntity>(Func<IEntity> action) where IEntity : class;
        //void Transaction(Action action);
        //IEntity Transaction<IEntity>(Func<IEntity> action) where IEntity : class;

    }
}