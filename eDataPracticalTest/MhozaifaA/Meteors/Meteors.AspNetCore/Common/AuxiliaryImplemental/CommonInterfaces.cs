using System.Linq.Expressions;

namespace Meteors
{

    public interface ISelector<TDto> {}

    /// <summary>
    /// <para>_</para>
    /// add
    /// <code>
    /// public static  <see cref="Expression{Func{TEntity, TDto}}"/>  Selector { get; set; }  = entity => newDto() {... };
    /// <para></para>
    /// public static <see cref="Expression{Func{TDto, TEntity}}"/>  InverseSelector { get; set; }  = dto => newEntity() {... };
    /// <para></para>
    /// public static<see cref="Action{TDto, TEntity}"/> AssignSelector { get; set; }  = (dto,entity) => { ... };
    /// </code>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDto"></typeparam>
    public interface ISelector<TEntity, TDto> : ISelector<TDto>        
    {
        static Expression<Func<TEntity, TDto>> Selector { get; set; }
        static Expression<Func<TDto, TEntity>> InverseSelector { get; set; }
        static Action<TDto,TEntity> AssignSelector { get; set; }
    }

    public static class Selector
    {
        public static Expression<Func<TEntity, TDto>> GetSelector<TEntity, TDto>()
            => ObjectExtension.GetValueReflection<TDto, Expression<Func<TEntity, TDto>>>(nameof(ISelector<TEntity, TDto>.Selector));

        public static Expression<Func<TEntity, TDto>> GetProfSelector<TEntity, TDto>()
           => ObjectExtension.GetNValueNormalReflection<TDto, Expression<Func<TEntity, TDto>>>(nameof(ISelector<TEntity, TDto>.Selector));

        public static Expression<Func<TDto, TEntity>> GetInverseSelector<TDto, TEntity>()
            => ObjectExtension.GetValueReflection<TDto, Expression<Func<TDto, TEntity>>>(nameof(ISelector<TDto, TEntity>.InverseSelector));

        public static Expression<Func<TDto, TEntity>> GetProfInverseSelector<TDto, TEntity>()
             => ObjectExtension.GetNValueNormalReflection<TDto, Expression<Func<TDto, TEntity>>>(nameof(ISelector<TDto, TEntity>.InverseSelector));

        public static Action<TDto, TEntity> GetAssignSelector<TDto, TEntity>()
            => ObjectExtension.GetValueReflection<TDto, Action<TDto, TEntity>>(nameof(ISelector<TDto, TEntity>.AssignSelector));

        public static Action<TDto, TEntity> GetProfAssignSelector<TDto, TEntity>()
            => ObjectExtension.GetNValueNormalReflection<TDto, Action<TDto, TEntity>>(nameof(ISelector<TDto, TEntity>.AssignSelector));


        public static TDto CompileSelector<TEntity, TDto>(this Expression<Func<TEntity, TDto>> selector, TEntity entity)
         =>  selector.Compile().Invoke(entity);

        public static TEntity CompileInverseSelector<TDto, TEntity>(this Expression<Func<TDto, TEntity>> selector, TDto dto)
         => selector.Compile().Invoke(dto);

        public static void CompileAssignSelector<TDto, TEntity>(this Action<TDto, TEntity> selector, TDto dto , TEntity entity)
         => selector(dto, entity);

    }

}
