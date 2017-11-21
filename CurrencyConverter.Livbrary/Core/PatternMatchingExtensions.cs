using System;


namespace CurrencyConverter.Library.Core
{
	using System.Diagnostics.Contracts;
	using System.Diagnostics.CodeAnalysis;
	using System.Collections.Generic;
	using System.Linq;
	using System.Runtime.Serialization;

	/// <summary>
	/// Méthode d'extension qui permet à n'importe quel type de faire du Pattern Matching
	/// </summary>
	public static class PatternMatchExtensions
	{
		/// <summary>
		/// Matches the specified value.
		/// </summary>
		/// <typeparam name="T">type encapsulé</typeparam>
		/// <param name="value">Valeur utilisée.</param>
		/// <returns><see cref="PatternMatchContext{T}"/></returns>
		/// <example>
		/// Calcul de factorielle qui utilise le PatternMatching
		/// <code>
		///    <![CDATA[
		///        Func<int, int> factorial = null;
		///        factorial = x => x.Match().With(i => i < 2, i => i).Else((i => i * factorial(i - 1))).Do();        
		///    ]]>
		/// </code>
		/// </example>
		public static PatternMatchContext<T> Match<T>(this T value)
		{
			Contract.Ensures(Contract.Result<PatternMatchContext<T>>() != null);

			return new PatternMatchContext<T>(value);
		}	 }

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
		}	 }

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
			Contract.Invariant(_cases != null);
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
			return new PatternMatch<T, TResult>(_value, _cases.Concat(item));
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
			return new PatternMatch<T, TResult>(_value, _cases.Concat(item));
		}

		/// <summary>
		/// Execute le match
		/// </summary>
		/// <returns>rRetourne un <see cref="TResult"/></returns>
		/// <exception cref="MatchNotFoundException">Incomplete pattern match</exception>
		public TResult Do()
		{
			var result = _cases.FirstOrDefault(x => x.Item1(this._value));
			if (result == null) throw new MatchNotFoundException("Incomplete pattern match");

			return result.Item2(this._value);
		}	 }

	[Serializable]
	public class MatchNotFoundException : Exception
	{
		public MatchNotFoundException()
		{
		}

		public MatchNotFoundException(string message)
			: base(message)
		{
		}

		public MatchNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected MatchNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}	 }
}
