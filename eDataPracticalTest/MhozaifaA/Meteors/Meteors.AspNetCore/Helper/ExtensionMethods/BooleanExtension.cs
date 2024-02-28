using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors
{
    public static class BooleanExtension
    {

        /// <summary>
        /// Nested iF statement return side by <see langword="Boolean"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="trueSide"></param>
        /// <param name="falseSide"></param>
        /// <returns></returns>
        public static T NestedIF<T>(this bool value, T trueSide, T falseSide)
        => value ? trueSide : falseSide;

        /// <summary>
        /// Nested iF statement return side by <see langword="Boolean"/> <see cref="value"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="trueSide"></param>
        /// <param name="falseSide"></param>
        /// <returns></returns>
        public static string NestedIF(this bool value, string trueSide, string falseSide)
         => value.NestedIF<string>(trueSide, falseSide);


        /// <summary>
        /// Nested iF statement return side by <see langword="Boolean"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="trueSide"></param>
        /// <param name="falseSide"></param>
        /// <returns></returns>
        public static T NestedIF<T>(this bool value, Func<T> trueSide, Func<T> falseSide)
        => value ? trueSide() : falseSide(); //value.NestedIF<T>(trueSide(), falseSide())



        /// <summary>
        /// Nested iF statement return side by <see langword="Boolean"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="trueSide"></param>
        /// <param name="falseSide"></param>
        /// <returns></returns>
        public static T NestedIF<T>(this bool value, T trueSide, Func<T> falseSide)
        => value ? trueSide : falseSide();//value.NestedIF<T>(trueSide, falseSide())



        /// <summary>
        /// Nested iF statement return side by <see langword="Boolean"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="trueSide"></param>
        /// <param name="falseSide"></param>
        /// <returns></returns>
        public static T NestedIF<T>(this bool value, Func<T> trueSide, T falseSide)
        => value ? trueSide() : falseSide;//value.NestedIF<T>(trueSide(), falseSide)

    }
}
