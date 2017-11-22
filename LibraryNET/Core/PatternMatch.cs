namespace LibraryNET.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    /// <summary>
    /// implémentation du PatternMathing (switch...case au stéroîdes)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <example>
    ///   <code>
    ///   <![CDATA[
    ///       int? n = 3;
    ///       var evenOdd = n.Match()
    ///                      .With(IsEven, x => "Even number")
    ///                      .With(IsOdd, x => "Odd number")
    ///                      .Else(x => "Null")
    ///                      .Do();
    ///   ]]>
    ///   </code>
    /// </example>
    public class PatternMatch<T, TResult>
    {
        private readonly T _value;
        private readonly IEnumerable<Tuple<Predicate<T>, Func<T, TResult>>> _cases = new List<Tuple<Predicate<T>, Func<T, TResult>>>();

        internal PatternMatch(T value)
        {
            this._value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatternMatch{T, TResult}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="cases">The cases.</param>
        internal PatternMatch(T value, IEnumerable<Tuple<Predicate<T>, Func<T, TResult>>> cases)
        {
            Contract.Requires<ArgumentNullException>(cases != null);

            this._value = value;
            this._cases = cases;
        }

        /// <summary>
        /// Objects the invariant.
        /// </summary>
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this._cases != null);
        }

        /// <summary>
        /// Withes the specified condition.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="first" /> ou <paramref name="second" /> est null.</exception>
        public PatternMatch<T, TResult> With(Predicate<T> condition, Func<T, TResult> result)
        {
            Contract.Requires<ArgumentNullException>(condition != null);
            Contract.Requires<ArgumentNullException>(result != null);
            Contract.Ensures(Contract.Result<PatternMatch<T, TResult>>() != null);

            var item = new List<Tuple<Predicate<T>, Func<T, TResult>>>() { new Tuple<Predicate<T>, Func<T, TResult>>(condition, result) };
            return new PatternMatch<T, TResult>(this._value, this._cases.Concat(item));
        }

        /// <summary>
        /// Spécifit la fonction qui sera utilisée dans tous les cas
        /// </summary>
        /// <param name="elseFunc">The else function.</param>
        /// <returns>
        ///   <see cref="PatternMatch{T,TResult}" />
        /// </returns>
        public PatternMatch<T, TResult> Else(Func<T, TResult> elseFunc)
        {
            Contract.Requires<ArgumentNullException>(elseFunc != null);
            Contract.Ensures(Contract.Result<PatternMatch<T, TResult>>() != null);

            var item = new List<Tuple<Predicate<T>, Func<T, TResult>>>() { new Tuple<Predicate<T>, Func<T, TResult>>(x => true, elseFunc) };
            return new PatternMatch<T, TResult>(this._value, this._cases.Concat(item));
        }

        /// <summary>
        /// Execute le match
        /// </summary>
        /// <returns>rRetourne un <see cref="TResult"/></returns>
        /// <exception cref="MatchNotFoundException">Incomplete pattern match</exception>
        public TResult Do()
        {
            var result = this._cases.FirstOrDefault(x => x.Item1(this._value));
            if (result == null) throw new MatchNotFoundException("Incomplete pattern match");

            return result.Item2(this._value);
        }    }
}