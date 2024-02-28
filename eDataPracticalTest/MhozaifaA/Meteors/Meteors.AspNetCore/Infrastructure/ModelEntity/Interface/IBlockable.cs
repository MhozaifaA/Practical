using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors
{
    public interface IBlockable
    {
        /// <summary>
        /// Assign date when Block 
        /// <para><see langword="Null"/> value not effectable </para>
        /// </summary>
        public Nullable<DateTime> DateBlocked { get; set; }
    }

}
