using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors
{
    public enum DataBaseTypes
    {
        /// <summary>
        /// As <see cref="char"/> in c# .
        /// <para>Take size as char(n) </para>
        /// </summary>
        CHAR,
        /// <summary>
        /// As <see cref="char"/> in c# .
        /// <para>Take size as nchar(n) </para>
        /// <para>Unicode</para>
        /// </summary>
        NCHAR,
        /// <summary>
        /// As <see cref="string"/> in c# .
        /// <para>Take size as varchar(n) </para>
        /// </summary>
        VARCHAR,
        /// <summary>
        /// As <see cref="string"/> in c# .
        /// <para>Take size as nvarchar(n) </para>
        /// <para>Unicode</para>
        /// </summary>
        NVARCHAR,
        /// <summary>
        /// As <see cref="string"/> in c# 2GB of text data .
        /// </summary>
        TEXT,
        /// <summary>
        /// As <see cref="bool"/> in c#.
        /// <para>take <see langword="0"/>,<see langword="1"/>,<see langword="null"/></para>
        /// </summary>
        BIT,
        /// <summary>
        /// As <see cref="uint"/> in c#.
        /// <para> but numbers from <see langword="0"/> to <see langword="255"/></para>
        /// </summary>
        TINYINT,
        /// <summary>
        /// As <see cref="short"/> in c#.
        /// </summary>
        SMALLINT,
        /// <summary>
        /// As <see cref="int"/> in c#.
        /// </summary>
        INT,
        /// <summary>
        /// As <see cref="long"/> in c#.
        /// </summary>
        BIGINT,
        /// <summary>
        /// As <see cref="double"/> in c#
        /// </summary>
        FLOAT,
        /// <summary>
        /// As <see cref="decimal"/> in c#
        /// <para>take n , default of n <see langword="53"/> , n=<see langword="24"/>  As <see cref="float"/> in c#</para>
        /// </summary>
        DECIMAL,
        /// <summary>
        /// As <see cref="DateTime"/> in c#
        /// <para>from <see langword="1753"/> to <see langword="9999"/></para>
        /// <para>take p  as point , s as scale</para>
        /// </summary>
        DATETIME,
        /// <summary>
        /// As <see cref="DateTime"/> in c#
        /// <para>from <see langword="0001"/> to <see langword="9999"/></para>
        /// </summary>
        DATETIME2,
        /// <summary>
        /// As <see cref="DateTime"/> in c#
        /// <para>from <see langword="0001"/> to <see langword="9999"/> without time</para>
        /// </summary>
        DATE,
        /// <summary>
        /// As <see cref="DateTimeOffset"/> in c#
        /// <para>from <see langword="0001"/> to <see langword="9999"/> with time zone offset</para>
        /// </summary>
        DATETIMEOFFSET,

    }
}
