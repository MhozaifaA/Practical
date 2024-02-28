using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Meteors
{
    /// <summary>
    /// Basics extensions
    /// </summary>
    public static class StringExtension
    {
        

        /// <summary>
        /// Converts the string representation of <see cref="{T}"/>
        /// enumerated constants to an equivalent enumerated object. A parameter specifies
        /// whether the operation is case-insensitive.
        /// <para>ignoreCase is auto active <see langword="true"/> .</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value)
            => (T)System.Enum.Parse(typeof(T), value, true);


        public static string IntoParentheses(params string[] vector)
          => "(" + System.String.Join(FixedCommonValue.Comma, vector) + ")";

        public static string IntoParentheses(IEnumerable<string> vector)
       => "(" + System.String.Join(FixedCommonValue.Comma, vector) + ")";


        /// <summary>
        /// Generic convert type string to value, some of customize struct not work with normal ChangeType as GUID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ChangeType<T>(this string value)
        {
            if (typeof(T) == typeof(Guid))
                return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(value);
            return (T)Convert.ChangeType(value, typeof(T));
        }

    }
}
