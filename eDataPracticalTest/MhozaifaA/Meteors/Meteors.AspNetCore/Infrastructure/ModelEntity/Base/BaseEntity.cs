using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Meteors
{

    /// <summary>
    /// Full struct of base contain <see cref="IDeletable"/>, <see cref="ICreatable"/>, <see cref="IUpdatable"/>
    /// <para> Default <see cref="IIndex"/> primary key  <see cref="int"/></para> 
    /// Inherent <see cref="IBaseEntity"/> wich allow default full struct .
    /// </summary>
    public abstract class BaseEntity : IBaseEntity
    {
        [Key]
        public int Id { get; set; }

        public Nullable<DateTime> DateDeleted { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DateCreated { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Nullable<DateTime> DateUpdated { get; set; }
    }


    /// <summary>
    /// Full struct of base contain <see cref="IDeletable{TIndex}"/>, <see cref="ICreatable{TIndex}"/>, <see cref="IUpdatable{TIndex}"/>
    /// <para> <see cref="IIndex"/> primary key as <see langword="Generic"/></para> 
    /// Inherent <see cref="IBaseEntity{TIndex}"/> wich allow full struct .
    /// </summary>
    public abstract class BaseEntity<TIndex> : IBaseEntity<TIndex> where TIndex :struct, IEquatable<TIndex>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TIndex Id { get; set; }

        public Nullable<DateTime> DateDeleted { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DateCreated { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Nullable<DateTime> DateUpdated { get; set; }


        public Nullable<TIndex> DeletedBy { get; set; }
        public Nullable<TIndex> CreatedBy { get; set; }
        public Nullable<TIndex> UpdatedBy { get; set; }
    }



    /// <summary>
    /// Default base contain only primary key <see cref="int"/>
    /// <para>Inherent <see cref="IBaseDefaultEntity"/> wich allow default of default struct .</para>
    /// </summary>
    public abstract class BaseDefaultEntity : IBaseDefaultEntity
    {
        [Key]
        public int Id { get; set; }
    }

    /// <summary>
    /// Default base contain only primary key as <see langword="Generic"/> .
    /// <para>Inherent <see cref="IBaseDefaultEntity{TIndex}"/> wich allow default of default struct .</para>
    /// </summary>
    public abstract class BaseDefaultEntity<TIndex> : IBaseDefaultEntity<TIndex> where TIndex : IEquatable<TIndex>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TIndex Id { get; set; }
    }




    /// <summary>
    /// Delete struct contain <see cref="IDeletable"/>
    /// <para> Default <see cref="IIndex"/> primary key  <see cref="int"/></para>
    /// <para>Inherent <see cref="IBaseSoftEntity"/> wich allow default soft struct .</para>
    /// </summary>
    public abstract class BaseSoftEntity : IBaseSoftEntity
    {
        [Key]
        public int Id { get; set; }

        public Nullable<DateTime> DateDeleted { get; set; }
    }

    /// <summary>
    /// Delete struct contain <see cref="IDeletable{TIndex}"/>
    /// <para> <see cref="IIndex"/> primary key as <see langword="Generic"/></para>
    /// <para>Inherent <see cref="IBaseSoftEntity{T}"/> wich allow default soft struct .</para>
    /// </summary>
    public abstract class BaseSoftEntity<TIndex> : IBaseSoftEntity<TIndex> where TIndex :struct, IEquatable<TIndex>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TIndex Id { get; set; }

        public Nullable<DateTime> DateDeleted { get; set; }
        public Nullable<TIndex> DeletedBy { get; set; }
    }




    /// <summary>
    /// Action struct contain <see cref="ICreatable"/>,  <see cref="IUpdatable"/> 
    /// <para> Default <see cref="IIndex"/> primary key  <see cref="int"/></para>
    /// <para>Inherent <see cref="IBaseActionEntity"/> wich allow default action struct .</para>
    /// </summary>
    public abstract class BaseActionEntity : IBaseActionEntity
    {
        [Key]
        public int Id { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DateCreated { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Nullable<DateTime> DateUpdated { get; set; }
    }

    /// <summary>
    /// Action struct contain <see cref="ICreatable"/>,  <see cref="IUpdatable"/> 
    /// <para> Default <see cref="IIndex"/> primary key  <see cref="int"/></para>
    /// <para>Inherent <see cref="IBaseActionEntity{T}"/> wich allow default action struct .</para>
    /// </summary>
    public abstract class BaseActionEntity<TIndex> : IBaseActionEntity<TIndex> where TIndex :struct, IEquatable<TIndex>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TIndex Id { get; set; }


        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DateCreated { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Nullable<DateTime> DateUpdated { get; set; }


        public Nullable<TIndex> CreatedBy { get; set; }
        public Nullable<TIndex> UpdatedBy { get; set; }
    }


}
