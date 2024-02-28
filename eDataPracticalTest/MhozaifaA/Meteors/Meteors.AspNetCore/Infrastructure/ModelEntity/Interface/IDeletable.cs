using System;
using System.Collections.Generic;
using System.Text;

namespace Meteors
{
    /// <summary>
    /// Enable date deleted 
    /// </summary>
    public interface IDeletable
    {
        /// <summary>
        /// Assign date when delete 
        /// <para><see langword="Null"/> value not effectable </para>
        /// </summary>
        public Nullable<DateTime> DateDeleted { get; set; }
    }

    /// <summary>
    /// Enable By Id <see cref="{TIndex}"/>
    /// </summary>
    /// <typeparam name="TIndex"></typeparam>
    public interface IDateBy<TIndex> where TIndex : struct
    {
        /// <summary>
        /// Normal property BY type of <see cref="TIndex"/>
        /// </summary>
        public Nullable<TIndex> DeletedBy { get; set; }
    }

    /// <summary>
    /// Enable By Id and Date <see cref="IDateBy{TIndex}"/> with <see cref="IDeletable"/>
    /// </summary>
    /// <typeparam name="TIndex"> </typeparam>
    public interface IDeletable<TIndex>: IDeletable, IDateBy<TIndex> where TIndex : struct { }
}
