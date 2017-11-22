namespace LibraryNET.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;
    using System.Linq;

    /// <summary>
	/// Implémentation du pattern spécification à la mode fonctionnelle
	/// </summary>
	public static class SpecificationExtensions
	{
		/// <summary>
		/// Ands the specified value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <param name="specifications">The specifications.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"><paramref name="source" /> a la valeur null.</exception>
		/// <example>
		/// <code>
		/// <![CDATA[
		///    var customer = new { Id = 42, Name = "Edenred", IsActive = true};
		///    var result = customer.And(x => x.IsActive, x => x.Id == 42);
		/// ]]>
		/// </code>
		/// </example>
		public static bool And<T>(this T value, params Predicate<T>[] specifications) where T : class
		{
			Contract.Requires<ArgumentNullException>(value != null);
			Contract.Requires<ArgumentNullException>(specifications != null);

			return And(value, specifications.ToList());
		}

		/// <summary>
		/// Ands the specified value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <param name="specifications">The specifications.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"><paramref name="collection" /> ou <paramref name="predicate" /> est null.</exception>
		/// <example>
		/// <code>
		/// <![CDATA[
		///    var customer = new { Id = 42, Name = "Edenred", IsActive = true};
		///    var result = customer.And(x => x.IsActive, x => x.Id == 42);
		/// ]]>
		/// </code>
		/// </example>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static bool And<T>(this T value, IEnumerable<Predicate<T>> specifications) where T : class
		{
			Contract.Requires<ArgumentNullException>(value != null);
			Contract.Requires<ArgumentNullException>(specifications != null);

			return specifications.All(f => f(value));
		}

		/// <summary>
		/// Ors the specified value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <param name="specifications">The specifications.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"><paramref name="source" /> ou <paramref name="predicate" /> est null.</exception>
		public static bool Or<T>(this T value, params Predicate<T>[] specifications) where T : class
		{
			Contract.Requires<ArgumentNullException>(value != null);
			Contract.Requires<ArgumentNullException>(specifications != null);

			return Or(value, specifications.ToList());
		}

		/// <summary>
		/// Ors the specified value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <param name="specifications">The specifications.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"><paramref name="source" /> ou <paramref name="predicate" /> est null.</exception>
		/// <exception cref="Exception">A delegate callback throws an exception.</exception>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static bool Or<T>(this T value, IEnumerable<Predicate<T>> specifications) where T : class
		{
			Contract.Requires<ArgumentNullException>(value != null);
			Contract.Requires<ArgumentNullException>(specifications != null);

			return specifications.Any(f => f(value));
		}

		/// <summary>
		/// Xors the specified value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <param name="specifications">The specifications.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"><paramref name="source" /> ou <paramref name="predicate" /> est null.</exception>
		/// <exception cref="OverflowException">Le nombre d'éléments dans <paramref name="source" /> est supérieur à <see cref="F:System.Int32.MaxValue" />.</exception>
		public static bool Xor<T>(this T value, params Predicate<T>[] specifications) where T : class
		{
			Contract.Requires<ArgumentNullException>(value != null);
			Contract.Requires<ArgumentNullException>(specifications != null);

			return Xor(value, specifications.ToList());
		}

		/// <summary>
		/// Xors the specified value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <param name="specifications">The specifications.</param>
		/// <returns></returns>
		/// <exception cref="OverflowException">Le nombre d'éléments dans <paramref name="source" /> est supérieur à <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="source" /> ou <paramref name="predicate" /> est null.</exception>
		/// <exception cref="Exception">A delegate callback throws an exception.</exception>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static bool Xor<T>(this T value, IEnumerable<Predicate<T>> specifications) where T : class
		{
			Contract.Requires<ArgumentNullException>(value != null);
			Contract.Requires<ArgumentNullException>(specifications != null);

			return specifications.Count(f => f(value)) == 1;
		}

		/// <summary>
		/// Nots the specified value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <param name="specifications">The specifications.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"><paramref name="source" /> ou <paramref name="predicate" /> est null.</exception>
		public static bool Not<T>(this T value, params Predicate<T>[] specifications) where T : class
		{
			Contract.Requires<ArgumentNullException>(value != null);
			Contract.Requires<ArgumentNullException>(specifications != null);

			return Not(value, specifications.ToList());
		}

		/// <summary>
		/// Nots the specified value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <param name="specifications">The specifications.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"><paramref name="source" /> ou <paramref name="predicate" /> est null.</exception>
		/// <example>
		///   <code><![CDATA[
		/// var user = new User() { Married = true, Name = "Albert" };
		/// var result = user.Not(UserRules.Validity);
		/// Console.WriteLine(result);
		/// ]]></code>
		/// </example>
		/// <exception cref="Exception">A delegate callback throws an exception.</exception>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static bool Not<T>(this T value, IEnumerable<Predicate<T>> specifications) where T : class
		{
			Contract.Requires<ArgumentNullException>(value != null);
			Contract.Requires<ArgumentNullException>(specifications != null);

			return !specifications.Any(f => f(value));
		}	}
}
