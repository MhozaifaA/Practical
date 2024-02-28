//Include mini - Meteors

using Meteors.AspNetCore.Infrastructure.EntityFramework.Util;
using Meteors.AspNetCore.Infrastructure.ModelEntity.Securty;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;


namespace Meteors
{


    public class MrIdentityDbContext<TKey> : MrIdentityDbContext<IdentityUser<TKey>, IdentityRole<TKey>, TKey>
    where TKey : struct, IEquatable<TKey>
    {
        public MrIdentityDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }

    public class MrIdentityDbContext<TUser, TRole, TKey> : MrIdentityDbContext<TUser, TRole, TKey, IdentityUserClaim<TKey>, IdentityUserRole<TKey>, IdentityUserLogin<TKey>, IdentityRoleClaim<TKey>, IdentityUserToken<TKey>>
    where TUser : IdentityUser<TKey>
    where TRole : IdentityRole<TKey>
    where TKey : struct, IEquatable<TKey>
    {
        public MrIdentityDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }


    public class MrIdentityDbContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken> : IdentityDbContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken>,
        IMrIdentityDbContext<TKey> where TUser : IdentityUser<TKey>
    where TRole : IdentityRole<TKey>
    where TKey : struct, IEquatable<TKey>
    where TUserClaim : IdentityUserClaim<TKey>
    where TUserRole : IdentityUserRole<TKey>
    where TUserLogin : IdentityUserLogin<TKey>
    where TRoleClaim : IdentityRoleClaim<TKey>
    where TUserToken : IdentityUserToken<TKey>
    {
        protected readonly IHttpResolverService HttpResolverService;
        //protected readonly IMrTranslate MrTranslate;

        public MrIdentityDbContext(DbContextOptions options) : base(options)
        {
            this.HttpResolverService = this.GetService<IHttpResolverService>(isInternal: true);           
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var entityType in builder.Model.GetEntityTypes().Where(x => x.ClrType.BaseType == typeof(BaseEntity<TKey>) ||
                    x.ClrType.GetInterfaces().Any(i => i == typeof(IMrIdentity))))
            {
                AddIndex(builder, entityType, nameof(IBaseEntity<TKey>.DateCreated));

                AddQueryFilter(builder, f => !f.DateDeleted.HasValue, entityType);

                //if (entityType.ClrType.GetInterfaces().Contains(typeof(ILanguages)))
                //    AddLanguages(builder, entityType);
            }
        }

        public DbContext @Base => this;


        public virtual TKey? CurrentUserId
        {
            get { return HttpResolverService?.GetCurrentUserId<TKey>(); }
        }

        public virtual string CurrentUserName
        {
            get { return HttpResolverService?.GetCurrentUserName(); }
        }


        //public virtual string? Lang
        //{
        //    get { return HttpResolverService?.Lang(); }
        //}

        public virtual IHttpResolverService CurrentHttpResolverService
        {
            get { return HttpResolverService; }
        }

        public virtual T? GetService<T>() where T : class => AccessorExtensions.GetService<T?>(this);

        public virtual T GetService<T>(bool isInternal = false)
        {

            if (!isInternal)
                return GetService<T>();

            // Please visit https://github.com/dotnet/efcore/blob/bccfcae0f90f9966fb4affe4777427cc8ef286a6/src/EFCore/Infrastructure/Internal/InfrastructureExtensions.cs
            // And https://github.com/dotnet/efcore/blob/main/src/EFCore/Infrastructure/AccessorExtensions.cs
            // What the problem with AccessorExtensions.GetService<T>(this) !
            // sometimes not all services used wich used by internal MrDbContext such as IMrTranslate

            var internalServiceProvider = (this as IInfrastructure<IServiceProvider>).Instance;

            var service = internalServiceProvider.GetService(typeof(T))
                ?? internalServiceProvider.GetService<IDbContextOptions>()
                    ?.Extensions.OfType<CoreOptionsExtension>().FirstOrDefault()
                    ?.ApplicationServiceProvider
                    ?.GetService(typeof(T));

            return (T)service;
        }



        public virtual event BeforeSaveChangesSignature BeforeSaveChangesSignature
        {
            add => _beforeSaveChangesSignature += value;
            remove => _beforeSaveChangesSignature -= value;
        }

        public virtual new DbSet<TEntity> Set<TEntity>() where TEntity : class, IBaseEntity<TKey>
               => base.Set<TEntity>();


        public virtual new TEntity Find<TEntity>([NotNull] params TKey[] keyValues) where TEntity : class, IBaseEntity<TKey>
            => base.Find<TEntity>(keyValues.Cast<object>().ToArray());


        public virtual new ValueTask<TEntity> FindAsync<TEntity>([NotNull] params TKey[] keyValues) where TEntity : class, IBaseEntity<TKey>
            => base.FindAsync<TEntity>(keyValues.Cast<object>().ToArray());




        public virtual new EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class, IBaseEntity<TKey>
            => base.Add(entity);

        public virtual new ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class, IBaseEntity<TKey>
            => base.AddAsync(entity, cancellationToken);

        public virtual new void AddRange<TEntity>(params TEntity[] entities) where TEntity : class, IBaseEntity<TKey>
            => base.AddRange(entities);

        public virtual new void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IBaseEntity<TKey>
          => base.AddRange(entities);

        public virtual new Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class, IBaseEntity<TKey>
            => base.AddRangeAsync(entities, cancellationToken);




        public virtual new EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class, IBaseEntity<TKey>
            => base.Update(entity);

        public virtual new void UpdateRange<TEntity>(params TEntity[] entities) where TEntity : class, IBaseEntity<TKey>
            => base.UpdateRange(entities);

        public virtual new void UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IBaseEntity<TKey>
            => base.UpdateRange(entities);



        public virtual new EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class, IBaseEntity<TKey>
            => base.Remove(entity);

        public virtual new void RemoveRange<TEntity>(params TEntity[] entities) where TEntity : class, IBaseEntity<TKey>
            => base.RemoveRange(entities);

        public virtual new void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IBaseEntity<TKey>
         => base.RemoveRange(entities);




        public virtual EntityEntry<TEntity> SoftDelete<TEntity>(TEntity entity) where TEntity : class, IBaseEntity<TKey>
            => !ModifyDeletable(ref entity) ? default : Entry(entity);

        public virtual EntityEntry<TEntity> SoftDelete<TEntity>(TKey key) where TEntity : class, IBaseEntity<TKey>
        {
            TEntity entity = Find<TEntity>(key);
            return !ModifyDeletable(ref entity) ? default : Entry(entity);
        }

        public virtual async Task<EntityEntry<TEntity>> SoftDeleteAsync<TEntity>(TKey key) where TEntity : class, IBaseEntity<TKey>
        {
            TEntity entity = await FindAsync<TEntity>(key);
            return !ModifyDeletable(ref entity) ? default : Entry(entity);
        }

        public virtual EntityEntry<TEntity> SoftDelete<TEntity>(Expression<Func<TEntity, bool>> findEntity) where TEntity : class, IBaseEntity<TKey>
        {
            TEntity entity = this.Set<TEntity>().FirstOrDefault(findEntity);
            return !ModifyDeletable(ref entity) ? default : Entry(entity);
        }

        public virtual async Task<EntityEntry<TEntity>> SoftDeleteAsync<TEntity>(Expression<Func<TEntity, bool>> findEntity) where TEntity : class, IBaseEntity<TKey>
        {
            TEntity entity = await this.Set<TEntity>().FirstOrDefaultAsync(findEntity);
            return !ModifyDeletable(ref entity) ? default : Entry(entity);
        }


        public virtual EntityEntry<TEntity> SoftDeleteTraversal<TEntity>(TEntity entity) where TEntity : class, IBaseEntity<TKey>
            => BuildSoftDeleteTree(ref entity);


        //public virtual EntityEntry<TEntity> SoftDeleteTraversal<TEntity, TProperty>(TEntity entity, params Func<TEntity, TProperty>[] without) where TEntity : class, IBaseEntity<TKey>
        //    => !ModifyDeletable(ref entity) ? default : SoftDeleteTraversal(base.Entry<TEntity>(entity), without.Select(x => x.Method.ReturnType));


        public virtual async Task<EntityEntry<TEntity>> SoftDeleteTraversalAsync<TEntity>(Expression<Func<TEntity, bool>> findEntity, params string[] navigationPropertyPath) where TEntity : class, IBaseEntity<TKey>
        {
            TEntity entity = await navigationPropertyPath.Aggregate(this.Set<TEntity>().AsQueryable(), (query, next) => query = query.Include(next)).FirstOrDefaultAsync(findEntity);
            return BuildSoftDeleteTree(ref entity, navigationPropertyPath);
        }

        public virtual async Task<EntityEntry<TEntity>> SoftDeleteTraversalAsync<TEntity, TProperty>(Expression<Func<TEntity, bool>> findEntity, Expression<Func<TEntity, ICollection<TProperty>>> include, params string[] navigationPropertyPath) where TEntity : class, IBaseEntity<TKey>
        {
            TEntity entity = await navigationPropertyPath.Aggregate(this.Set<TEntity>().Include(include).AsQueryable(), (query, next) => query = query.Include(next)).FirstOrDefaultAsync(findEntity);
            return BuildSoftDeleteTree(ref entity, navigationPropertyPath, include.Compile().Method.ReturnType);
        }

        public virtual async Task<EntityEntry<TEntity>> SoftDeleteTraversalAsync<TEntity, TProperty, TPreviousProperty>(Expression<Func<TEntity, bool>> findEntity, Expression<Func<TEntity, ICollection<TProperty>>> include, Expression<Func<TProperty, ICollection<TPreviousProperty>>> thenInclude, params string[] navigationPropertyPath) where TEntity : class, IBaseEntity<TKey>
        {
            TEntity entity = await navigationPropertyPath.Aggregate(this.Set<TEntity>().Include(include).ThenInclude(thenInclude).AsQueryable(), (query, next) => query = query.Include(next)).FirstOrDefaultAsync(findEntity);
            return BuildSoftDeleteTree(ref entity, navigationPropertyPath, include.Compile().Method.ReturnType, thenInclude.Compile().Method.ReturnType);
        }

        public virtual async Task<EntityEntry<TEntity>> SoftDeleteTraversalAsync<TEntity, TProperty, TPreviousProperty, TPreviousPreviousProperty>(Expression<Func<TEntity, bool>> findEntity, Expression<Func<TEntity, ICollection<TProperty>>> include, Expression<Func<TProperty, ICollection<TPreviousProperty>>> thenInclude, Expression<Func<TPreviousProperty, ICollection<TPreviousPreviousProperty>>> thenNextInclude, params string[] navigationPropertyPath) where TEntity : class, IBaseEntity<TKey>
        {
            TEntity entity = await navigationPropertyPath.Aggregate(this.Set<TEntity>().Include(include).ThenInclude(thenInclude).ThenInclude(thenNextInclude).AsQueryable(), (query, next) => query = query.Include(next)).FirstOrDefaultAsync(findEntity);
            return BuildSoftDeleteTree(ref entity, navigationPropertyPath, include.Compile().Method.ReturnType, thenInclude.Compile().Method.ReturnType, thenNextInclude.Compile().Method.ReturnType);
        }

        public virtual async Task<EntityEntry<TEntity>> SoftDeleteTraversalAsync<TEntity, TProperty, TPreviousProperty, TPreviousPreviousProperty, TPreviousPreviousPreviousProperty>(Expression<Func<TEntity, bool>> findEntity, Expression<Func<TEntity, ICollection<TProperty>>> include, Expression<Func<TProperty, ICollection<TPreviousProperty>>> thenInclude, Expression<Func<TPreviousProperty, ICollection<TPreviousPreviousProperty>>> thenNextInclude, Expression<Func<TPreviousPreviousProperty, ICollection<TPreviousPreviousPreviousProperty>>> thenNextNextInclude, params string[] navigationPropertyPath) where TEntity : class, IBaseEntity<TKey>
        {
            TEntity entity = await navigationPropertyPath.Aggregate(this.Set<TEntity>().Include(include).ThenInclude(thenInclude).ThenInclude(thenNextInclude).ThenInclude(thenNextNextInclude).AsQueryable(), (query, next) => query = query.Include(next)).FirstOrDefaultAsync(findEntity);
            return BuildSoftDeleteTree(ref entity, navigationPropertyPath, include.Compile().Method.ReturnType, thenInclude.Compile().Method.ReturnType, thenNextInclude.Compile().Method.ReturnType, thenNextNextInclude.Compile().Method.ReturnType);
        }

        public virtual async Task<EntityEntry<TEntity>> SoftDeleteTraversalAsync<TEntity, TProperty, TPreviousProperty, TPreviousPreviousProperty, TPreviousPreviousPreviousProperty, TPreviousPreviousPreviousPreviousProperty>(Expression<Func<TEntity, bool>> findEntity, Expression<Func<TEntity, ICollection<TProperty>>> include, Expression<Func<TProperty, ICollection<TPreviousProperty>>> thenInclude, Expression<Func<TPreviousProperty, ICollection<TPreviousPreviousProperty>>> thenNextInclude, Expression<Func<TPreviousPreviousProperty, ICollection<TPreviousPreviousPreviousProperty>>> thenNextNextInclude, Expression<Func<TPreviousPreviousPreviousProperty, ICollection<TPreviousPreviousPreviousPreviousProperty>>> thenNextNextNextInclude, params string[] navigationPropertyPath) where TEntity : class, IBaseEntity<TKey>
        {
            TEntity entity = await navigationPropertyPath.Aggregate(this.Set<TEntity>().Include(include).ThenInclude(thenInclude).ThenInclude(thenNextInclude).ThenInclude(thenNextNextInclude).ThenInclude(thenNextNextNextInclude).AsQueryable(), (query, next) => query = query.Include(next)).FirstOrDefaultAsync(findEntity);
            return BuildSoftDeleteTree(ref entity, navigationPropertyPath, include.Compile().Method.ReturnType, thenInclude.Compile().Method.ReturnType, thenNextInclude.Compile().Method.ReturnType, thenNextNextInclude.Compile().Method.ReturnType, thenNextNextNextInclude.Compile().Method.ReturnType);
        }




        public virtual async Task<EntityEntry<TEntity>> SoftDeleteTraversalAsync<TEntity>(TKey key, params string[] navigationPropertyPath) where TEntity : class, IBaseEntity<TKey>
        {
            TEntity entity = await navigationPropertyPath.Aggregate(this.Set<TEntity>().AsQueryable(), (query, next) => query = query.Include(next)).FirstOrDefaultAsync(source => source.Id.Equals(key));
            return BuildSoftDeleteTree(ref entity, navigationPropertyPath);
        }

        public virtual async Task<EntityEntry<TEntity>> SoftDeleteTraversalAsync<TEntity, TProperty>(TKey key, Expression<Func<TEntity, ICollection<TProperty>>> include, params string[] navigationPropertyPath) where TEntity : class, IBaseEntity<TKey>
        {
            TEntity entity = await navigationPropertyPath.Aggregate(this.Set<TEntity>().Include(include).AsQueryable(), (query, next) => query = query.Include(next)).FirstOrDefaultAsync(source => source.Id.Equals(key));
            return BuildSoftDeleteTree(ref entity, navigationPropertyPath, include.Compile().Method.ReturnType);
        }

        public virtual async Task<EntityEntry<TEntity>> SoftDeleteTraversalAsync<TEntity, TProperty, TPreviousProperty>(TKey key, Expression<Func<TEntity, ICollection<TProperty>>> include, Expression<Func<TProperty, ICollection<TPreviousProperty>>> thenInclude, params string[] navigationPropertyPath) where TEntity : class, IBaseEntity<TKey>
        {
            TEntity entity = await navigationPropertyPath.Aggregate(this.Set<TEntity>().Include(include).ThenInclude(thenInclude).AsQueryable(), (query, next) => query = query.Include(next)).FirstOrDefaultAsync(source => source.Id.Equals(key));
            return BuildSoftDeleteTree(ref entity, navigationPropertyPath, include.Compile().Method.ReturnType, thenInclude.Compile().Method.ReturnType);
        }

        public virtual async Task<EntityEntry<TEntity>> SoftDeleteTraversalAsync<TEntity, TProperty, TPreviousProperty, TPreviousPreviousProperty>(TKey key, Expression<Func<TEntity, ICollection<TProperty>>> include, Expression<Func<TProperty, ICollection<TPreviousProperty>>> thenInclude, Expression<Func<TPreviousProperty, ICollection<TPreviousPreviousProperty>>> thenNextInclude, params string[] navigationPropertyPath) where TEntity : class, IBaseEntity<TKey>
        {
            TEntity entity = await navigationPropertyPath.Aggregate(this.Set<TEntity>().Include(include).ThenInclude(thenInclude).ThenInclude(thenNextInclude).AsQueryable(), (query, next) => query = query.Include(next)).FirstOrDefaultAsync(source => source.Id.Equals(key));
            return BuildSoftDeleteTree(ref entity, navigationPropertyPath, include.Compile().Method.ReturnType, thenInclude.Compile().Method.ReturnType, thenNextInclude.Compile().Method.ReturnType);
        }

        public virtual async Task<EntityEntry<TEntity>> SoftDeleteTraversalAsync<TEntity, TProperty, TPreviousProperty, TPreviousPreviousProperty, TPreviousPreviousPreviousProperty>(TKey key, Expression<Func<TEntity, ICollection<TProperty>>> include, Expression<Func<TProperty, ICollection<TPreviousProperty>>> thenInclude, Expression<Func<TPreviousProperty, ICollection<TPreviousPreviousProperty>>> thenNextInclude, Expression<Func<TPreviousPreviousProperty, ICollection<TPreviousPreviousPreviousProperty>>> thenNextNextInclude, params string[] navigationPropertyPath) where TEntity : class, IBaseEntity<TKey>
        {
            TEntity entity = await navigationPropertyPath.Aggregate(this.Set<TEntity>().Include(include).ThenInclude(thenInclude).ThenInclude(thenNextInclude).ThenInclude(thenNextNextInclude).AsQueryable(), (query, next) => query = query.Include(next)).FirstOrDefaultAsync(source => source.Id.Equals(key));
            return BuildSoftDeleteTree(ref entity, navigationPropertyPath, include.Compile().Method.ReturnType, thenInclude.Compile().Method.ReturnType, thenNextInclude.Compile().Method.ReturnType, thenNextNextInclude.Compile().Method.ReturnType);
        }

        public virtual async Task<EntityEntry<TEntity>> SoftDeleteTraversalAsync<TEntity, TProperty, TPreviousProperty, TPreviousPreviousProperty, TPreviousPreviousPreviousProperty, TPreviousPreviousPreviousPreviousProperty>(TKey key, Expression<Func<TEntity, ICollection<TProperty>>> include, Expression<Func<TProperty, ICollection<TPreviousProperty>>> thenInclude, Expression<Func<TPreviousProperty, ICollection<TPreviousPreviousProperty>>> thenNextInclude, Expression<Func<TPreviousPreviousProperty, ICollection<TPreviousPreviousPreviousProperty>>> thenNextNextInclude, Expression<Func<TPreviousPreviousPreviousProperty, ICollection<TPreviousPreviousPreviousPreviousProperty>>> thenNextNextNextInclude, params string[] navigationPropertyPath) where TEntity : class, IBaseEntity<TKey>
        {
            TEntity entity = await navigationPropertyPath.Aggregate(this.Set<TEntity>().Include(include).ThenInclude(thenInclude).ThenInclude(thenNextInclude).ThenInclude(thenNextNextInclude).ThenInclude(thenNextNextNextInclude).AsQueryable(), (query, next) => query = query.Include(next)).FirstOrDefaultAsync(source => source.Id.Equals(key));
            return BuildSoftDeleteTree(ref entity, navigationPropertyPath, include.Compile().Method.ReturnType, thenInclude.Compile().Method.ReturnType, thenNextInclude.Compile().Method.ReturnType, thenNextNextInclude.Compile().Method.ReturnType, thenNextNextNextInclude.Compile().Method.ReturnType);
        }



        //public virtual async Task<EntityEntry<TEntity>> TranslateAsync<TEntity>(TEntity entity) where TEntity : class, IBaseEntity<TKey>
        //{
        //    if (MrTranslate is null || !MrTranslate.IsEnabled())
        //        return Entry(entity);
        //    System.Reflection.PropertyInfo entityProp = entity.GetType().GetProperty(nameof(ILanguages.Languages));
        //    if (entityProp is null) return Entry(entity);
        //    //if (entityProp.GetMethod.Invoke(entity,null) is not null) return Entry(entity);

        //    IOrderedEnumerable<KeyValuePair<string, (string value, ITranslateAttribute info)>> propsNameList =
        //        entity.GetTranslatePropertiesInfo().OrderBy(x => x.Value.info.GetIsHtml);

        //    CultureDictionary languages = new CultureDictionary();
        //    // key: code ,value: translates
        //    Dictionary<string, List<string>> codesTranslateProps = await MrTranslate.TranslateAsync(propsNameList.Select(x => x.Value));

        //    IEnumerable<string> listKeys = propsNameList.Select(x => x.Key);
        //    foreach (KeyValuePair<string, List<string>> item in codesTranslateProps)
        //        languages.AddOrUpdate(item.Key, new PropertyTranslate(listKeys.Zip(item.Value).ToDictionary(x => x.First, x => x.Second)));

        //    entityProp.SetMethod.Invoke(entity, new object[] { languages });
        //    return Entry(entity);
        //}

       
        public override int SaveChanges()
            => SaveChanges(acceptAllChangesOnSuccess: true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            _beforeSaveChangesSignature?.Invoke();
            BeforeSaveChanges();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => SaveChangesAsync(acceptAllChangesOnSuccess: true, cancellationToken: cancellationToken);


        public override Task<int> SaveChangesAsync(
          bool acceptAllChangesOnSuccess,
          CancellationToken cancellationToken = default)
        {
            _beforeSaveChangesSignature?.Invoke();
            BeforeSaveChanges();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }


        public virtual int SaveChangesDeleted()
        {
            int task = SaveChanges(acceptAllChangesOnSuccess: true);
            DetachDeleted();
            return task;
        }

        public virtual async Task<int> SaveChangesDeletedAsync(CancellationToken cancellationToken = default)
        {
            int task = await SaveChangesAsync(acceptAllChangesOnSuccess: true, cancellationToken: cancellationToken);
            DetachDeleted();
            return task;
        }



        protected virtual void DetachDeleted()
        {
            var deletedTracker = ChangeTracker.Entries().Where(entry => entry.State == EntityState.Unchanged && entry.ToEntity<IDeletable>().DateDeleted.HasValue);
            foreach (var entity in deletedTracker)
                entity.State = EntityState.Detached;
        }


        protected virtual void BeforeSaveChanges()
        {
            TKey? actionBy = HttpResolverService?.GetCurrentUserId<TKey>();// ?? default(TKey);

            foreach (EntityEntry entry in ChangeTracker.Entries())
            {
                IBaseEntity<TKey> entity = entry.Entity.AsTo<IBaseEntity<TKey>>();
                if (entity is null) continue;

                switch (entry.State)
                {
                    case EntityState.Detached:

                        break;

                    case EntityState.Unchanged:

                        break;

                    case EntityState.Deleted:

                        break;

                    case EntityState.Modified:
                        if (entity.DateDeleted is null)
                        {
                            entity.UpdatedBy = actionBy;
                            if (!entity.DateUpdated.HasValue)
                                entity.DateUpdated = DateTime.Now.ToLocalTime();
                        }
                        else
                            entity.DeletedBy = actionBy;

                        break;

                    case EntityState.Added:
                        entity.CreatedBy = actionBy;
                        if(entity.DateCreated == default(DateTime)) //not init
                            entity.DateCreated = DateTime.Now.ToLocalTime();
                        break;
                }


            }
        }





        #region -   Private   -

        private BeforeSaveChangesSignature _beforeSaveChangesSignature;


        private static void AddQueryFilter(ModelBuilder builder, Expression<Func<IBaseEntity<TKey>, bool>> filterSoftDelete, IMutableEntityType entityType)
        {
            var newParam = Expression.Parameter(entityType.ClrType);
            var newbody = ReplacingExpressionVisitor.Replace(filterSoftDelete.Parameters.Single(), newParam, filterSoftDelete.Body);
            builder.Entity(entityType.ClrType).HasQueryFilter(Expression.Lambda(newbody, newParam));
        }

        private static void AddIndex(ModelBuilder builder, IMutableEntityType entityType, params string[] propertyNames)
        {
            builder.Entity(entityType.ClrType).HasIndex(propertyNames);
        }

        //private static void AddLanguages(ModelBuilder builder, IMutableEntityType entityType)
        //{
        //    builder.Entity(entityType.ClrType).Property(nameof(ILanguages.Languages)).HasJsonConversion<CultureDictionary>();
        //}

        private static bool ModifyDeletable<TEntity>(ref TEntity entity) where TEntity : class, IBaseEntity<TKey>
        {
            if (entity is null) return false;
            if (entity.DateDeleted.HasValue) return false;
            entity.DateDeleted = DateTime.Now.ToLocalTime();
            return true;
        }

        private EntityEntry<TEntity> BuildSoftDeleteTree<TEntity>(ref TEntity entity, params Type[] includeTypes) where TEntity : class, IBaseEntity<TKey>
             => !ModifyDeletable(ref entity) ? default : SoftDeleteTraversal(base.Entry<TEntity>(entity), includeTypes);

        private EntityEntry<TEntity> BuildSoftDeleteTree<TEntity>(ref TEntity entity, string[] navigationPropertyPath, params Type[] includeTypes) where TEntity : class, IBaseEntity<TKey>
            => !ModifyDeletable(ref entity) ? default : SoftDeleteTraversal(base.Entry<TEntity>(entity), navigationPropertyPath, includeTypes);


        private EntityEntry<TEntity> SoftDeleteTraversal<TEntity>(EntityEntry<TEntity> entry) where TEntity : class, IBaseEntity<TKey>
            => _softDeleteTraversal(entry);

        private EntityEntry<TEntity> SoftDeleteTraversal<TEntity>(EntityEntry<TEntity> entry, IEnumerable<Type> includeType) where TEntity : class, IBaseEntity<TKey>
            => _softDeleteTraversal(entry, includeType);

        private EntityEntry<TEntity> SoftDeleteTraversal<TEntity>(EntityEntry<TEntity> entry, params Type[] includeTypes) where TEntity : class, IBaseEntity<TKey>
           => _softDeleteTraversal(entry, includeTypes);

        private EntityEntry<TEntity> SoftDeleteTraversal<TEntity>(EntityEntry<TEntity> entry, string[] navigationPropertyPath, params Type[] includeTypes) where TEntity : class, IBaseEntity<TKey>
         => _softDeleteTraversal(entry, includeTypes, navigationPropertyPath);



        private EntityEntry<TEntity> _softDeleteTraversal<TEntity>(EntityEntry<TEntity> entry, IEnumerable<Type> includeTypes = default, string[] navigationPropertyPath = default) where TEntity : class, IBaseEntity<TKey>
        {
            includeTypes = includeTypes ?? new List<Type>();
            navigationPropertyPath = navigationPropertyPath ?? new string[0];
            bool ifOnLastTracking = !includeTypes.Any() && navigationPropertyPath.Length == 0;
            IEnumerable<string> _navigationPropertyPath = navigationPropertyPath.SelectMany(x => x.Split('.')).Distinct();
            foreach (var navigationEntry in entry.Navigations.Where(n => !((INavigation)n.Metadata).IsOnDependent && (ifOnLastTracking || (includeTypes.Contains(n.Metadata.ClrType) || _navigationPropertyPath.Contains(n.Metadata.Name)))))
            {
                if (navigationEntry.CurrentValue is null) continue;
                if (navigationEntry is CollectionEntry collectionEntry)
                {
                    foreach (var dependentEntry in collectionEntry.CurrentValue)
                    {
                        var _base = dependentEntry.CastTo<IDeletable>();
                        if (_base.DateDeleted.HasValue) continue;
                        _base.DateDeleted = DateTime.Now.ToLocalTime();
                        _softDeleteTraversal(Entry(dependentEntry), includeTypes, _navigationPropertyPath);
                    }
                }
                else
                {
                    var dependentEntry = navigationEntry.CurrentValue.AsTo<IDeletable>();
                    if (dependentEntry != null)
                    {
                        dependentEntry.DateDeleted = DateTime.Now.ToLocalTime();
                    }
                }
            }

            return entry;
        }



        private void _softDeleteTraversal(EntityEntry entry, IEnumerable<Type> outType = default, IEnumerable<string> navigationPropertyPath = default)
        {
            bool ifOnLastTracking = !outType.Any() && !navigationPropertyPath.Any();
            foreach (var navigationEntry in entry.Navigations.Where(n => !((INavigation)n.Metadata).IsOnDependent && (ifOnLastTracking || (outType.Contains(n.Metadata.ClrType) || navigationPropertyPath.Contains(n.Metadata.Name)))))
            {
                if (navigationEntry.CurrentValue is null) continue;
                if (navigationEntry is CollectionEntry collectionEntry)
                {
                    foreach (var dependentEntry in collectionEntry.CurrentValue)
                    {
                        var _base = dependentEntry.CastTo<IDeletable>();
                        if (_base.DateDeleted.HasValue) continue;
                        _base.DateDeleted = DateTime.Now.ToLocalTime();
                        _softDeleteTraversal(Entry(dependentEntry), outType, navigationPropertyPath);
                    }
                }
            }
        }


       //Transaction removed mini-Meteors
        #endregion

    }
}
