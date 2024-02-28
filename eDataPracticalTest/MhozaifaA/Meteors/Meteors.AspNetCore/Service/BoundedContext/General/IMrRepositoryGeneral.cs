

namespace Meteors
{

    public interface IMrRepositoryGeneral<TKey, TDto> : IMrRepository<TKey> where TKey : struct, IEquatable<TKey> where TDto : ISelector<TDto>
    {
        Task<OperationResult<IEnumerable<TDto>>> FetchAsync();
        Task<OperationResult<TDto>> GetByIdAsync(TKey id);
        Task<OperationResult<TDto>> UpdateAsync(TDto dto);
        Task<OperationResult<TDto>> DeleteAsync(TKey id);
        Task<OperationResult<TDto>> AddAsync(TDto dto);
        Task<OperationResult<TDto>> ModifyAsync(TDto dto);
        Task<OperationResult<IEnumerable<TDto>>> AddMultiAsync(IEnumerable<TDto> dto);
        Task<OperationResult<IEnumerable<TDto>>> DeleteMultiAsync(IEnumerable<TKey> ids);
        Task<OperationResult<IEnumerable<TDto>>> UpdateMultiAsync(IEnumerable<TDto> dtos);
        Task<OperationResult<IEnumerable<TDto>>> ModifyMultiAsync(IEnumerable<TDto> dtos);
    }

    public interface IMrRepositoryGeneral<TKey, TEntity, TDto> : IMrRepositoryGeneral<TKey, TDto>
         where TEntity : class, IBaseEntity<TKey> where TKey : struct, IEquatable<TKey> where TDto : ISelector<TEntity, TDto> {}

}
