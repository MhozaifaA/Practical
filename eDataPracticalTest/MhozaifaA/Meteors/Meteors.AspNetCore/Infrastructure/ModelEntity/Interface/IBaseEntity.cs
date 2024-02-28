using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Meteors
{
    /// <summary>
    /// Declaration entity blong to <see langword="Meteors"/> casting
    /// <para>No contain properties ,useful to inherent all entry to cast valid</para>
    /// </summary>
    public interface IMrEntity { }

    /// <summary>
    /// Default primary key type of <see cref="int"/>
    /// </summary>
    public interface IIndex : IIndex<int>
    {
        /// <summary>
        /// primary key type of <see cref="int"/> .
        /// </summary> 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }

    /// <summary>
    /// Generic primary key type of <see cref="{TIndex}"/> .
    /// </summary>
    /// <typeparam name="TIndex"></typeparam>
    public interface IIndex<TIndex> where TIndex : IEquatable<TIndex>
    {
        /// <summary>
        /// primary key type of <see cref="{TIndex}"/>
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TIndex Id { get; set; }
    }



    /// <summary>
    /// Full struct of base contain <see cref="IDeletable"/>, <see cref="ICreatable"/>, <see cref="IUpdatable"/>
    /// <para> Default <see cref="IIndex"/> primary key  <see cref="int"/></para> .
    /// </summary>
    public interface IBaseEntity : IMrEntity, IIndex, IDeletable, ICreatable, IUpdatable { }

    /// <summary>
    /// Full struct of base contain <see cref="IDeletable{TIndex}"/>, <see cref="ICreatable{TIndex}"/>, <see cref="IUpdatable{TIndex}"/>
    /// <para> <see cref="IIndex"/> primary key as <see langword="Generic"/></para> .
    /// </summary>
    public interface IBaseEntity<TIndex> : IMrEntity, IIndex<TIndex>, IDeletable<TIndex>, ICreatable<TIndex>, IUpdatable<TIndex> where TIndex :struct, IEquatable<TIndex> { }




    /// <summary>
    /// Default base contain only primary key <see cref="int"/> .
    /// </summary>
    public interface IBaseDefaultEntity : IMrEntity, IIndex { }

    /// <summary>
    /// Default base contain only primary key as <see langword="Generic"/> .
    /// </summary>
    public interface IBaseDefaultEntity<TIndex> : IMrEntity, IIndex<TIndex> where TIndex : IEquatable<TIndex> { }




    /// <summary>
    /// Delete struct contain <see cref="IDeletable"/>
    /// <para> Default <see cref="IIndex"/> primary key  <see cref="int"/></para> .
    /// </summary>
    public interface IBaseSoftEntity : IMrEntity, IIndex, IDeletable { }

    /// <summary>
    /// Delete struct contain <see cref="IDeletable{TIndex}"/>
    /// <para> <see cref="IIndex"/> primary key as <see langword="Generic"/></para> .
    /// </summary>
    public interface IBaseSoftEntity<TIndex> : IMrEntity, IIndex<TIndex>, IDeletable<TIndex> where TIndex :struct, IEquatable<TIndex> { }





    /// <summary>
    /// Action struct contain <see cref="ICreatable"/>,  <see cref="IUpdatable"/> 
    /// <para> Default <see cref="IIndex"/> primary key  <see cref="int"/></para> .
    /// </summary>
    public interface IBaseActionEntity : IMrEntity, IIndex, ICreatable, IUpdatable { }

    /// <summary>
    /// Action struct contain <see cref="ICreatable{TIndex}"/>,  <see cref="IUpdatabl{TIndex}e"/> 
    /// <para> Default <see cref="IIndex"/> primary key as <see langword="Generic"/></para> .
    /// </summary>
    public interface IBaseActionEntity<TIndex> : IMrEntity, IIndex<TIndex>, ICreatable<TIndex>, IUpdatable<TIndex> where TIndex : struct, IEquatable<TIndex> { }
}
