using Meteors.OperationContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Meteors
{
    public class MrRepositoryGeneral<TContext, TKey, TEntity, TDto> : MrRepository<TContext, TKey, TEntity>, IMrRepositoryGeneral<TKey, TEntity, TDto>, IDisposable
    where TEntity : class, IBaseEntity<TKey> where TKey : struct, IEquatable<TKey>
    where TContext : IMrIdentityDbContext<TKey> where TDto : ISelector<TEntity, TDto>
    {

        protected MrRepositoryGeneral(TContext context) : base(context) { }


        public virtual async Task<OperationResult<IEnumerable<TDto>>> FetchAsync() => await RepositoryHandler(_fetch);
        public virtual async Task<OperationResult<TDto>> GetByIdAsync(TKey id) => await RepositoryHandler(_get(id));
        public virtual async Task<OperationResult<TDto>> AddAsync(TDto dto) => await RepositoryHandler(_add(dto));
        public virtual async Task<OperationResult<TDto>> UpdateAsync(TDto dto) => await RepositoryHandler(_update(dto));
        public virtual async Task<OperationResult<TDto>> ModifyAsync(TDto dto) => await RepositoryHandler(_modeify(dto));
        public virtual async Task<OperationResult<TDto>> DeleteAsync(TKey id) => await RepositoryHandler(_delete(id));


        public virtual async Task<OperationResult<IEnumerable<TDto>>> AddMultiAsync(IEnumerable<TDto> dto) => await RepositoryHandler(_addMulti(dto));
        public virtual async Task<OperationResult<IEnumerable<TDto>>> UpdateMultiAsync(IEnumerable<TDto> dtos) => await RepositoryHandler(_updateMulti(dtos));
        public virtual async Task<OperationResult<IEnumerable<TDto>>> ModifyMultiAsync(IEnumerable<TDto> dtos) => await RepositoryHandler(_modifyMulti(dtos));
        public virtual async Task<OperationResult<IEnumerable<TDto>>> DeleteMultiAsync(IEnumerable<TKey> ids) => await RepositoryHandler(_deleteMulti(ids));


     

        private Func<OperationResult<IEnumerable<TDto>>, Task<OperationResult<IEnumerable<TDto>>>> _fetch
            => async operation =>
            {
                Expression<Func<TEntity, TDto>> selector = Selector.GetProfSelector<TEntity, TDto>();
                IEnumerable<TDto> entities = await Query.Select(selector).ToListAsync();
                return operation.SetSuccess(entities);
            };

        private Func<OperationResult<TDto>, Task<OperationResult<TDto>>> _get(TKey id)
          => async operation =>
          {
              TEntity entity = await FindAsync(id);
              if (entity is null)
                  return (Statuses.NotExist, $"{typeof(TEntity).Name}: {id} not exist");
              return operation.SetSuccess(Selector.GetProfSelector<TEntity, TDto>().CompileSelector(entity));
          };

        private Func<OperationResult<TDto>, Task<OperationResult<TDto>>> _add(TDto dto)
            => async operation =>
            {
                TEntity entity = Selector.GetProfInverseSelector<TDto, TEntity>().CompileInverseSelector(dto);
                await Context.AddAsync(entity);
                await Context.SaveChangesAsync();
                return operation.SetSuccess(Selector.GetProfSelector<TEntity, TDto>().CompileSelector(entity));
            };

        //private Func<OperationResult<TDto>, Task<OperationResult<TDto>>> _addwithtranslate(TDto dto)
        //   => async operation =>
        //   {
        //       TEntity entity = Selector.GetProfInverseSelector<TDto, TEntity>().CompileInverseSelector(dto);
        //       if (Options.Translator)
        //           await Context.AddWithTranslateAsync(entity);
        //       else
        //           await Context.AddAsync(entity);
        //       await Context.SaveChangesAsync();
        //       return operation.SetSuccess(Selector.GetProfSelector<TEntity, TDto>().CompileSelector(entity));
        //   };

        private Func<OperationResult<TDto>, Task<OperationResult<TDto>>> _update(TDto dto)
            => async operation =>
            {
                TKey id = dto.GetValueNormalReflection<TDto, TKey>(FixedCommonValue.Id);
                TEntity entity = await Query.FirstOrDefaultAsync(ent => ent.Id.Equals(id));
                if (entity is null)
                    return (Statuses.NotExist, $"{typeof(TEntity).Name}: {id} not exist");
                IBaseEntity<TKey> baseEntity = entity;
                entity = Selector.GetProfInverseSelector<TDto, TEntity>().CompileInverseSelector(dto);
                entity.DateCreated = baseEntity.DateCreated;
                entity.DateUpdated = baseEntity.DateUpdated;
                entity.DateDeleted = baseEntity.DateDeleted;
                entity.CreatedBy = baseEntity.CreatedBy;
                entity.UpdatedBy = baseEntity.UpdatedBy;
                entity.DeletedBy = baseEntity.DeletedBy;
                Context.Update(entity);
                await Context.SaveChangesAsync();
                return operation.SetSuccess(Selector.GetProfSelector<TEntity, TDto>().CompileSelector(entity));
            };

        private Func<OperationResult<TDto>, Task<OperationResult<TDto>>> _modeify(TDto dto)
            => async operation =>
            {
                TKey id = dto.GetValueNormalReflection<TDto, TKey>(FixedCommonValue.Id);
                TEntity entity = await TrackingQuery.FirstOrDefaultAsync(ent => ent.Id.Equals(id));
                if (entity is null)
                    return (Statuses.NotExist, $"{typeof(TEntity).Name}: {id} not exist");
                Selector.GetProfAssignSelector<TDto, TEntity>().CompileAssignSelector(dto, entity);
                await Context.SaveChangesAsync();
                return operation.SetSuccess(Selector.GetProfSelector<TEntity, TDto>().CompileSelector(entity));
            };

        private Func<OperationResult<TDto>, Task<OperationResult<TDto>>> _delete(TKey id)
            => async operation =>
            {
                TEntity entity = await FindAsync(id);
                if (entity is null)
                    return (Statuses.NotExist, $"{typeof(TEntity).Name}: {id} not exist");
                Context.SoftDelete(entity);
                await Context.SaveChangesAsync();
                return operation.SetSuccess(Selector.GetProfSelector<TEntity, TDto>().CompileSelector(entity), $"Soft deleted {typeof(TEntity).Name} success");
            };



        private Func<OperationResult<IEnumerable<TDto>>, Task<OperationResult<IEnumerable<TDto>>>> _addMulti(IEnumerable<TDto> dtos)
          => async operation =>
          {
              if (dtos.IsNullOrEmpty())
                  return operation.SetFailed($"{typeof(IEnumerable<TDto>).FullName}: can't be null or empty");

              Func<TDto, TEntity> compileInverseSelector = Selector.GetProfInverseSelector<TDto, TEntity>().Compile();
              TEntity[] entities = dtos.Select(compileInverseSelector).ToArray();
              await AddRangeThenSaveChangesAsync(entities);
              Func<TEntity, TDto> compileSelector = Selector.GetProfSelector<TEntity, TDto>().Compile();
              return operation.SetSuccess(entities.Select(compileSelector));
          };

        private Func<OperationResult<IEnumerable<TDto>>, Task<OperationResult<IEnumerable<TDto>>>> _deleteMulti(IEnumerable<TKey> ids)
            => async operation =>
            {
                if (ids.IsNullOrEmpty())
                    return operation.SetFailed($"{typeof(IEnumerable<TKey>).FullName}: can't be null or empty");

                IEnumerable<TEntity> entities = await TrackingQuery.Where(e => ids.Contains(e.Id)).ToListAsync();
                if (entities.IsNullOrEmpty())
                    return (Statuses.NotExist, $"{typeof(IEnumerable<TKey>).FullName}: {StringExtension.IntoParentheses(ids.Select(i => i.ToString()).ToArray())} not exist");

                foreach (TEntity entity in entities)
                    Context.SoftDelete(entity);

                await Context.SaveChangesDeletedAsync();

                Func<TEntity, TDto> compileSelector = Selector.GetProfSelector<TEntity, TDto>().Compile();
                return operation.SetSuccess(entities.Select(compileSelector));
            };


        private Func<OperationResult<IEnumerable<TDto>>, Task<OperationResult<IEnumerable<TDto>>>> _updateMulti(IEnumerable<TDto> dtos)
            => async operation =>
            {
                if (dtos.IsNullOrEmpty())
                    return operation.SetFailed($"{typeof(IEnumerable<TDto>).FullName}: can't be null or empty");

                IEnumerable<(TKey key, TDto obj)> full_ids = dtos.Select(dto => (dto.GetValueNormalReflection<TDto, TKey>(FixedCommonValue.Id), dto));
                IEnumerable<TKey> ids = full_ids.Select(i => i.key);

                IEnumerable<TEntity> entities = await Query.Where(entity => ids.Contains(entity.Id)).ToListAsync();
                if (entities.IsNullOrEmpty())
                    return (Statuses.NotExist, $"{typeof(IEnumerable<TDto>).FullName}: {StringExtension.IntoParentheses(ids.Select(i => i.ToString()).ToArray())} not exist");

                Func<TDto, TEntity> compileInverseSelector = Selector.GetProfInverseSelector<TDto, TEntity>().Compile();

                entities = entities.Select(entity =>
                {
                    IBaseEntity<TKey> baseEntity = entity;
                    entity = compileInverseSelector(full_ids.First(i => i.key.Equals(baseEntity.Id)).obj);
                    entity.DateCreated = baseEntity.DateCreated;
                    entity.DateUpdated = baseEntity.DateUpdated;
                    entity.DateDeleted = baseEntity.DateDeleted;
                    entity.CreatedBy = baseEntity.CreatedBy;
                    entity.UpdatedBy = baseEntity.UpdatedBy;
                    entity.DeletedBy = baseEntity.DeletedBy;
                    return entity;
                });

                Context.UpdateRange(entities);
                await Context.SaveChangesAsync();
                Func<TEntity, TDto> compileSelector = Selector.GetProfSelector<TEntity, TDto>().Compile();
                return operation.SetSuccess(entities.Select(compileSelector));
            };

        private Func<OperationResult<IEnumerable<TDto>>, Task<OperationResult<IEnumerable<TDto>>>> _modifyMulti(IEnumerable<TDto> dtos)
           => async operation =>
           {
               if (dtos.IsNullOrEmpty())
                   return operation.SetFailed($"{typeof(IEnumerable<TDto>).FullName}: can't be null or empty");

               IEnumerable<(TKey key, TDto obj)> full_ids = dtos.Select(dto => (dto.GetValueNormalReflection<TDto, TKey>(FixedCommonValue.Id), dto));
               IEnumerable<TKey> ids = full_ids.Select(i => i.key);

               IEnumerable<TEntity> entities = await TrackingQuery.Where(entity => ids.Contains(entity.Id)).ToListAsync();
               if (entities.IsNullOrEmpty())
                   return (Statuses.NotExist, $"{typeof(IEnumerable<TDto>).FullName}: {StringExtension.IntoParentheses(ids.Select(i => i.ToString()).ToArray())} not exist");

               foreach (var entity in entities)
                   Selector.GetProfAssignSelector<TDto, TEntity>().CompileAssignSelector(full_ids.First(i => i.key.Equals(entity.Id)).obj, entity);

               await Context.SaveChangesAsync();
               Func<TEntity, TDto> compileSelector = Selector.GetProfSelector<TEntity, TDto>().Compile();
               return operation.SetSuccess(entities.Select(compileSelector));
           };

       
    }

}
