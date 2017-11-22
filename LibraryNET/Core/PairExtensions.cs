namespace CurrencyConverter.Library.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// 
    /// </summary>
    public static class PairExtensions
    {
        /// <summary>
        /// To the list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static List<T> ToList<T>(this Pair<T, T> obj)
        {
            return new List<T>()
            {
                obj.First,
                obj.Second
            };
        }


        public static Pair<T2, T1> Swap<T2, T1>(this Pair<T1, T2> obj)
        {
            Contract.Requires<ArgumentNullException>(obj != null);
            Contract.Ensures(Contract.Result<Pair<T2, T1>>() != null);

            return Pair.Of(obj.Second, obj.First);
        }

        /// <summary>
        /// Applique une fontion sur les élémentes de la paire
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="operation">The operation.</param>
        /// <returns></returns>
        public static TResult Apply<T1, T2, TResult>(this Pair<T1, T2> obj, Func<T1, T2, TResult> operation)
        {
            Contract.Requires<ArgumentNullException>(obj != null);
            Contract.Requires<ArgumentNullException>(operation != null);

            return operation(obj.First, obj.Second);
        }

        /// <summary>
        /// Applies the specified operation.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="operation">The operation.</param>
        /// <returns></returns>
        public static Pair<T1, T2> Apply<T1, T2>(this Pair<T1, T2> obj, Action<T1, T2> operation)
        {
            Contract.Requires<ArgumentNullException>(obj != null);
            Contract.Requires<ArgumentNullException>(operation != null);

            operation(obj.First, obj.Second);
            return new Pair<T1, T2>(obj.First, obj.Second);
        }

        /// <summary>
        /// Applies the specified operation.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <typeparam name="T4">The type of the 4.</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="operation">The operation.</param>
        /// <returns></returns>
        public static Pair<T3, T4> Apply<T1, T2, T3, T4>(this Pair<T1, T2> obj, Func<T1, T2, Pair<T3, T4>> operation)
        {
            Contract.Requires<ArgumentNullException>(obj != null);
            Contract.Requires<ArgumentNullException>(operation != null);

            return operation(obj.First, obj.Second);
        }

        /// <summary>
        /// To the pair.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static Pair<T1, T2> ToPair<T1, T2>(this T1 obj, T2 value)
        {
            return Pair.Of(obj, value);
        }
    }
}