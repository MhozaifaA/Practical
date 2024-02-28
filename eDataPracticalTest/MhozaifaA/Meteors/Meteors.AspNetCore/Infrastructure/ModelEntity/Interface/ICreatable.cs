using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Meteors
{
    /// <summary>
    /// Enable date created
    /// </summary>
    public interface ICreatable
    {
        /// <summary>
        /// Assign date when create 
        /// <para><see langword="Null"/> value not effectable </para>
        /// <para> Note: <see cref="DatabaseGeneratedOption.Computed"/> is active </para>
        /// </summary>
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DateCreated { get; set; }
    }

    /// <summary>
    /// Enable By Id <see cref="{TIndex}"/>
    /// </summary>
    /// <typeparam name="TIndex"></typeparam>
    public interface ICreateBy<TIndex> where TIndex : struct
    {
        /// <summary>
        /// Normal property BY type of <see cref="TIndex"/>
        /// </summary>
        public Nullable<TIndex> CreatedBy { get; set; }
    }

    /// <summary>
    /// Enable By Id and Date <see cref="ICreateBy{TIndex}"/> with <see cref="ICreatable"/>
    /// </summary>
    /// <typeparam name="TIndex"> </typeparam>
    public interface ICreatable<TIndex> : ICreatable, ICreateBy<TIndex> where TIndex : struct { }
}
