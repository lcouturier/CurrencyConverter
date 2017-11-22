namespace LibraryNET.Core
{
    using System;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <example>
    /// <code>
    /// <![CDATA[
    ///     var result =
    ///            "Value".Match()
    ///                .With(x => x.Equals("test"), x => "OK")
    ///                .With(x => x.Equals("value"), x => "Not OK")
    ///                .Else(x => "Test")
    ///                .Do();]]>
    /// </code>
    /// </example>
    public class PatternMatchContext<T>
    {
        /// <summary>
        /// Valeur encapsulée par <see cref="PatternMatch{T,TResult}"/>
        /// </summary>
        private readonly T _value;
        /// <summary>
        /// Initializes a new instance of the <see cref="PatternMatchContext{T}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        internal PatternMatchContext(T value)
        {
            this._value = value;
        }

        /// <summary>
        /// Withes the specified condition.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="condition">The condition.</param>
        /// <param name="result">The result.</param>
        /// <returns><see cref="PatternMatch{T,TResult}"/></returns>        
        /// <exception cref="ArgumentNullException"><paramref name="first" /> ou <paramref name="second" /> est null.</exception>
        public PatternMatch<T, TResult> With<TResult>(Predicate<T> condition, Func<T, TResult> result)
        {
            Contract.Requires<ArgumentNullException>(condition != null);
            Contract.Requires<ArgumentNullException>(result != null);
            Contract.Ensures(Contract.Result<PatternMatch<T, TResult>>() != null);

            var match = new PatternMatch<T, TResult>(this._value);
            return match.With(condition, result);
        }    }
}