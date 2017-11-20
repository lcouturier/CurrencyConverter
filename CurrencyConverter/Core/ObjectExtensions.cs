using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace CurrencyConverter
{
	/// <summary>
	/// 
	/// </summary>
	public static class ObjectExtensions
	{
		/// <summary>
		/// Retourne un dictionaire des propriétés et des valeurs
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj">The object.</param>
		/// <returns>Retourne un <see cref="Dictionary{TKey,TValue}"/></returns>
		/// <exception cref="System.ArgumentNullException">obj</exception>
		public static Dictionary<string, string> ToDictionary<T>(this T obj) where T : class
		{
			Contract.Requires<ArgumentNullException>(obj != null);
			Contract.Ensures(Contract.Result<Dictionary<string, string>>() != null);

			return typeof(T).GetProperties().ToDictionary(x => x.Name, x => x.GetValue(obj, null).ToString());
		}

		/// <summary>
		/// Int0 the specified operation.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="value">The value.</param>
		/// <param name="operation">The operation.</param>
		/// <returns></returns>
		/// <example>
		/// <code>
		/// Func<int, int> add = x => x + 1;            
		/// var f = 1.Into(add).Into(add);
		/// </code>
		/// </example>
		public static Func<TResult> Into<T, TResult>(this T value, Func<T, TResult> operation)
		{
			Contract.Requires<ArgumentNullException>(value != null);
			Contract.Requires<ArgumentNullException>(operation != null);

			return () => operation(value);
		}

		/// <summary>
		/// Int0 the specified operation.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="value">The value.</param>
		/// <param name="operation">The operation.</param>
		/// <returns></returns>
		/// <example>
		/// <code>
		/// Func<int, int> add = x => x + 1;            
		/// var f = 1.Into(add).Into(add);
		/// </code>
		/// </example>
		public static Func<TResult> Into<T, TResult>(this Func<T> value, Func<T, TResult> operation)
		{
			Contract.Requires<ArgumentNullException>(value != null);
			Contract.Requires<ArgumentNullException>(operation != null);
			Contract.Ensures(Contract.Result<Func<TResult>>() != null);

			return () => operation(value());
		}

		public static Option<T> Some<T>(this T obj)
		{
			return Option.Of(obj);
		}

		public static Option<T> NoneWhen<T>(this T obj, Predicate<T> predicate)
		{
			Contract.Requires<ArgumentNullException>(predicate != null);
			Contract.Ensures(Contract.Result<Option<T>>() != null);

			return predicate(obj) ? Option.None : Option.Of(obj);
		}

		public static Option<T> SomeWhen<T>(this T obj, Predicate<T> predicate)
		{
			Contract.Requires<ArgumentNullException>(predicate != null);
			Contract.Ensures(Contract.Result<Option<T>>() != null);

			return predicate(obj) ? Option.Of(obj) : Option.None;
		}


		public static TResult IfNotNull<T, TResult>(this T obj, Func<T, TResult> get, Func<TResult> orElse) where T : class
		{
			Contract.Requires<ArgumentNullException>(get != null);
			Contract.Requires<ArgumentNullException>(orElse != null);

			return obj != null ? get(obj) : orElse();
		}

		/// <summary>
		///     Gets the specified or else.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj">The object.</param>
		/// <param name="orElse">The or else.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentNullException">predicate</exception>
		/// <exception cref="ArgumentNullException"><paramref name="orElse" /> is <see langword="null" />.</exception>
		/// <exception cref="Exception">A delegate callback throws an exception.</exception>
		public static string Get<T>(this T obj, Func<string> orElse) where T : class
		{
			Contract.Requires<ArgumentNullException>(orElse != null);
			Contract.Ensures(Contract.Result<string>() != null);

			return obj != null ? obj.ToString() : orElse();
		}

		/// <summary>
		///     Gets the or default.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj">The object.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">predicate</exception>
		public static string GetOrDefault<T>(this T obj) where T : class
		{
			return Get(obj, () => string.Empty);
		}

		/// <summary>
		/// Maps the specified block.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="obj">The object.</param>
		/// <param name="block">The block.</param>
		/// <returns></returns>
		[Obsolete("Work In Progess...")]
		public static TResult Map<T, TResult>(this T obj, Func<T, TResult> block)
		{
			return block(obj);
		}

		/// <summary>
		/// Matches the specified predicate.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj">The object.</param>
		/// <param name="predicate">The predicate.</param>
		/// <param name="orElse">The or else.</param>
		/// <returns></returns>
		public static T Match<T>(this T obj, Predicate<T> predicate, Func<T> orElse)
		{
			Contract.Requires<ArgumentNullException>(predicate != null);
			Contract.Requires<ArgumentNullException>(orElse != null);

			return predicate(obj) ? obj : orElse();
		}


		/// <summary>
		/// Matches the specified predicate. 
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="predicate"></param>
		/// <param name="operation"></param>
		/// <param name="orElse"></param>
		/// <returns></returns>
		public static TResult Match<T, TResult>(this T obj, Predicate<T> predicate, Func<T, TResult> operation, Func<TResult> orElse)
		{
			Contract.Requires<ArgumentNullException>(predicate != null);
			Contract.Requires<ArgumentNullException>(operation != null);
			Contract.Requires<ArgumentNullException>(orElse != null);

			return predicate(obj) ? operation(obj) : orElse();
		}

		/// <summary>
		/// Matches all.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj">The object.</param>
		/// <param name="predicate">The predicate.</param>
		/// <param name="orElse">The or else.</param>
		/// <returns></returns>
		[Obsolete("Work In Progess...")]
		public static T MatchAll<T>(this T obj, List<Predicate<T>> predicate, Func<T> orElse)
		{
			Contract.Requires<ArgumentNullException>(predicate != null);
			Contract.Requires<ArgumentNullException>(orElse != null);

			return predicate.All(f => f(obj)) ? obj : orElse();
		}

		//[Obsolete("Work In Progess...")]
		//public static T MatchAll<T>(this T obj, params Predicate<T>[] predicate, Func<T> orElse)
		//{
		//    Contract.Requires<ArgumentNullException>(predicate != null);
		//    Contract.Requires<ArgumentNullException>(orElse != null);

		//    return predicate.All(f => f(obj)) ? obj : orElse();
		//}

		/// <summary>
		/// Matches any.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj">The object.</param>
		/// <param name="predicate">The predicate.</param>
		/// <param name="orElse">The or else.</param>
		/// <returns></returns>
		[Obsolete("Work In Progess...")]
		public static T MatchAny<T>(this T obj, List<Predicate<T>> predicate, Func<T> orElse)
		{
			Contract.Requires<ArgumentNullException>(predicate != null);
			Contract.Requires<ArgumentNullException>(orElse != null);

			return predicate.Any(f => f(obj)) ? obj : orElse();
		}

		//[Obsolete("Work In Progess...")]
		//public static T MatchAny<T>(this T obj, params Predicate<T>[] predicate, Func<T> orElse)
		//{
		//    Contract.Requires<ArgumentNullException>(predicate != null);
		//    Contract.Requires<ArgumentNullException>(orElse != null);

		//    return predicate.Any(f => f(obj)) ? obj : orElse();
		//}

		/// <summary>
		/// Copie en profondeur d'un objet.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj">Objet original.</param>
		/// <returns>Copie de l'objet</returns>
		/// <exception cref="ArgumentNullException">The <paramref name="serializationStream" /> is null. -or-The <paramref name="graph" /> is null.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Une valeur négative ou une valeur supérieure à <see cref="F:System.Int32.MaxValue" /> est affectée à la position. </exception>
		/// <exception cref="Exception">A delegate callback throws an exception. </exception>
		[SuppressMessage("Microsoft.Reliability", "CA2000:Supprimer les objets avant la mise hors de portée")]
		public static T DeepCopy<T>(this T obj) where T : class
		{
			Contract.Requires<ArgumentNullException>(obj != null);
			Contract.Ensures(Contract.Result<T>() != null);

			return new MemoryStream().Use(ms =>
			{
				var binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(ms, obj);
				ms.Position = 0;
				return (T)binaryFormatter.Deserialize(ms);
			});
		}


		/// <summary>
		/// Joins the specified values.
		/// </summary>
		/// <param name="values">The values.</param>
		/// <returns></returns>
		public static string Join(this object[] values)
		{
			Contract.Requires<ArgumentNullException>(values != null);
			Contract.Requires<ArgumentNullException>(Contract.ForAll(values, x => x != null));
			Contract.Ensures(Contract.Result<string>() != null);

			return Join(values, "|");
		}

		/// <summary>
		/// Joins the specified values.
		/// </summary>
		/// <param name="values">The values.</param>
		/// <param name="separator">The separator.</param>
		/// <returns></returns>
		public static string Join(this object[] values, string separator)
		{
			Contract.Requires<ArgumentNullException>(values != null);
			Contract.Requires<ArgumentNullException>(Contract.ForAll(values, x => x != null));
			Contract.Requires<ArgumentException>(!String.IsNullOrEmpty(separator));
			Contract.Ensures(Contract.Result<string>() != null);

			return string.Join(separator, Array.ConvertAll(values, x => x.ToString()));
		}	}
}
