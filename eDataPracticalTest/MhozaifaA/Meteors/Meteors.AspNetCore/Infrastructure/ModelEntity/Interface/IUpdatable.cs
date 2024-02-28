using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Meteors
{
    /// <summary>
    /// Enable date updated 
    /// </summary>
    public interface IUpdatable
    {
        /// <summary>
        /// Assign date when update 
        /// <para><see langword="Null"/> value not effectable </para>
        /// <para> Note: <see cref="DatabaseGeneratedOption.Computed"/> is active </para>
        /// </summary>
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Nullable<DateTime> DateUpdated { get; set; }
    }

    /// <summary>
    /// Enable By Id <see cref="{TIndex}"/>
    /// </summary>
    /// <typeparam name="TIndex"></typeparam>
    public interface IUpdateBy<TIndex> where TIndex : struct
    {
        /// <summary>
        /// Normal property BY type of <see cref="TIndex"/>
        /// </summary>
        public Nullable<TIndex> UpdatedBy { get; set; }
    }

    /// <summary>
    /// Enable By Id and Date <see cref="IUpdateBy{TIndex}"/> with <see cref="IUpdatable"/>
    /// </summary>
    /// <typeparam name="TIndex"> </typeparam>
    public interface IUpdatable<TIndex>: IUpdatable, IUpdateBy<TIndex> where TIndex : struct { }
}
