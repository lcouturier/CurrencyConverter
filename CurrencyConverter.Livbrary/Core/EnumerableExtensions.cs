using System;
using System.Collections.Generic;

namespace CurrencyConverter.Library.Core
{
	using System.Diagnostics.Contracts;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;

	/// <summary>
	/// Méthode d'extensions du type IEnumerable
	/// </summary>
	public static class EnumerableExtensions
	{
		/// <summary>
		/// Selects the many.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TDisposable">The type of the disposable.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="items">The items.</param>
		/// <param name="selector">The selector.</param>
		/// <param name="resultSelector">The result selector.</param>
		/// <returns></returns>
		/// <exception cref="Exception">A delegate callback throws an exception.</exception>
		/// <example>
		/// <code>
		///    <![CDATA[
		///         var r = from file in Directory.GetFiles(@"D:\Temp\", "*.log")
		///         from x in File.Open(file, FileMode.Open)
		///         from y in new StreamReader(x)
		///         select y.ReadToEnd();
		///    ]]>
		/// </code>
		/// </example>
		//public static IEnumerable<TResult> SelectMany<T, TDisposable, TResult>(this IEnumerable<T> items, Func<T, TDisposable> selector, Func<T, TDisposable, TResult> resultSelector)
		//    where TDisposable : IDisposable
		//{
		//    Contract.Requires<ArgumentNullException>(items != null);
		//    Contract.Requires<ArgumentNullException>(selector != null);
		//    Contract.Requires<ArgumentNullException>(resultSelector != null);
		//    Contract.Ensures(Contract.Result<IEnumerable<TResult>>() != null);

		//    foreach (var item in items)
		//    {
		//        using (var disposable = selector(item))
		//        {
		//            yield return resultSelector(item, disposable);
		//        }
		//    }
		//}

		/// <summary>
		/// Execution d'une action pour chaque élément
		/// </summary>
		/// <typeparam name="T">Type de l'élément</typeparam>
		/// <param name="items">Liste des éléments.</param>
		/// <param name="action">Action à executer.</param>
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", MessageId = "0"), Obsolete("Ne plus utiliser", false)]
		public static void DoForEach<T>(this IEnumerable<T> items, Action<T> action)
		{
			foreach (var item in items)
			{
				action(item);
			}
		}


		/// <summary>
		/// Y a ti'il des doublons dans la liste
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <returns></returns>
		public static bool AnyDuplicates<T>(this IEnumerable<T> list)
		{
			var hashset = new HashSet<T>();
			return list.Any(e => !hashset.Add(e));
		}

		public static IEnumerable<TSource> Duplicates<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, bool> compare)
		{
			Contract.Requires<ArgumentNullException>(source != null);
			Contract.Requires<ArgumentNullException>(compare != null);
			Contract.Ensures(Contract.Result<IEnumerable<TSource>>() != null);

			return source
				.GroupBy(x => x, x => x, LambdaComparer.Create(compare))
				.Where(i => i.IsMultiple())
				.SelectMany(i => i);
		}

		internal static bool IsMultiple<T>(this IEnumerable<T> source)
		{
			Contract.Requires<ArgumentNullException>(source != null);

			var enumerator = source.GetEnumerator();
			return enumerator.MoveNext() && enumerator.MoveNext();
		}

		/// <summary>
		/// Intersects the specified first.
		/// </summary>
		/// <typeparam name="TSource">The type of the source.</typeparam>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		/// <param name="comparer">The comparer.</param>
		/// <returns></returns>        
		/// <example>
		/// <code>
		///  <![CDATA[ 
		///    var items1 = new [] { new Customer() { Id = 42, Name = "Edenred" }, new Customer() { Id = 18, Name = "Manpower" } };
		///    var items2 = new [] { new Customer() { Id = 18, Name = "Manpower" } };
		///    
		///    Func<Customer, Customer, bool> compare = (x, y) => x.Id == y.Id;                
		///    
		///   var result = items1.Intersect(items2, LambdaComparer.Create(compare));
		///    Assert.IsTrue(result.Count() == 1);
		/// 
		///    foreach (var customer in result)
		///    {
		///        Console.WriteLine(customer);
		///    }
		///  ]]>
		/// </code>
		/// </example>
		public static IEnumerable<TSource> Intersect<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TSource, bool> comparer)
		{
			Contract.Requires<ArgumentNullException>(first != null);
			Contract.Requires<ArgumentNullException>(second != null);
			Contract.Requires<ArgumentNullException>(comparer != null);
			Contract.Ensures(Contract.Result<IEnumerable<TSource>>() != null);

			return first.Intersect(second, LambdaComparer.Create(comparer));
		}

		public static IEnumerable<TSource> Intersect<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TSource, bool> comparer, Func<TSource, int> hash)
		{
			Contract.Requires<ArgumentNullException>(first != null);
			Contract.Requires<ArgumentNullException>(second != null);
			Contract.Requires<ArgumentNullException>(comparer != null);
			Contract.Requires<ArgumentNullException>(hash != null);
			Contract.Ensures(Contract.Result<IEnumerable<TSource>>() != null);

			return first.Intersect(second, LambdaComparer.Create(comparer, hash));
		}

		public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TSource, bool> comparer)
		{
			Contract.Requires<ArgumentNullException>(first != null);
			Contract.Requires<ArgumentNullException>(second != null);
			Contract.Requires<ArgumentNullException>(comparer != null);
			Contract.Ensures(Contract.Result<IEnumerable<TSource>>() != null);

			return first.Except(second, LambdaComparer.Create(comparer));
		}

		/// <summary>
		/// Aggregates the specified items.
		/// </summary>        
		/// <typeparam name="T"></typeparam>
		/// <param name="items">The items.</param>
		/// <param name="selector">The selector.</param>
		/// <param name="empty">The empty.</param>
		/// <returns></returns>
		/// <exception cref="OverflowException">Le nombre d'éléments dans <paramref name="source" /> est supérieur à <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="Exception">A delegate callback throws an exception. </exception>
		/// <exception cref="ArgumentNullException"><paramref name="source" /> a la valeur null.</exception>
		/// <exception cref="InvalidOperationException"><paramref name="source" /> ne contient aucun élément.</exception>
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", MessageId = "2")]
		public static T Aggregate<T>(this IEnumerable<T> items, Func<T, T, T> selector, Func<T> empty)
		{
			Contract.Requires<ArgumentNullException>(items != null);
			Contract.Requires<ArgumentNullException>(selector != null);
			Contract.Requires<ArgumentNullException>(empty != null);

			return items.Count() != 0 ? items.Aggregate(selector) : empty();
		}

		/// <summary>
		/// Vierifie pour chaque élément de la liste la condition, si la liste est vide la valeur retournée est celle ce la fonction empty().
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="items">Liste des élémentes.</param>
		/// <param name="operation">Prédicat appliquié sur chaque élément.</param>
		/// <param name="empty">Retourne la valeur par défault.</param>
		/// <returns></returns>
		public static bool All<T>(this IEnumerable<T> items, Func<T, bool> operation, Func<bool> empty)
		{
			Contract.Requires<ArgumentNullException>(items != null);
			Contract.Requires<ArgumentNullException>(operation != null);
			Contract.Requires<ArgumentNullException>(empty != null);

			return items.Count() != 0 ? items.All(operation) : empty();
		}

		/// <summary>
		/// Firsts the or none.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="items">The items.</param>
		/// <param name="predicate">The predicate.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"><paramref name="source" /> ou <paramref name="predicate" /> est null.</exception>
		/// <exception cref="OverflowException">Le nombre d'éléments dans <paramref name="source" /> est supérieur à <see cref="F:System.Int32.MaxValue" />.</exception>
		public static Option<T> FirstOrNone<T>(this IEnumerable<T> items, Func<T, bool> predicate)
		{
			Contract.Requires<ArgumentNullException>(items != null);
			Contract.Ensures(Contract.Result<Option<T>>() != null);

			var result = items.Count(predicate);
			Func<T> get = () => items.FirstOrDefault(predicate);
			return result != 0 ? Option.Of(get()) : Option.None;
		}

		/// <summary>
		/// Firsts the or none.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="items">The items.</param>
		/// <returns></returns>
		public static Option<T> FirstOrNone<T>(this IEnumerable<T> items)
		{
			Contract.Requires<ArgumentNullException>(items != null);
			Contract.Ensures(Contract.Result<Option<T>>() != null);

			var result = items.Count();
			Func<T> get = () => items.FirstOrDefault();
			return result != 0 ? Option.Of(get()) : Option.None;
		}


		/// <summary>
		/// Génération d'une chaine en ajoutant le séparateur spécifié entre chaque élément
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="items">Liste des éléments.</param>
		/// <param name="separator">Le séparateur.</param>
		/// <returns>Retourne une chaine</returns>
		public static string Join<T>(this IEnumerable<T> items)
		{
			Contract.Requires<ArgumentNullException>(items != null);
			Contract.Ensures(Contract.Result<string>() != null);

			return items.Select(x => x.ToString()).Aggregate((string.Concat));
		}


		public static string Join<T>(this IEnumerable<T> items, string separator)
		{
			Contract.Requires<ArgumentNullException>(items != null);
			Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(separator));
			Contract.Ensures(Contract.Result<string>() != null);

			return string.Join(separator, items.Select(x => x.ToString()));
		}

		/// <summary>
		/// Heads the specified items.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="items">The items.</param>
		/// <returns></returns>
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", MessageId = "0"), Pure]
		public static T Head<T>(this IEnumerable<T> items)
		{
			Contract.Requires<ArgumentNullException>(items != null);

			return items.First();
		}

		/// <summary>
		/// Tails the specified items.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="items">The items.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"><paramref name="source" /> a la valeur null.</exception>
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", MessageId = "0"), Pure]
		public static IEnumerable<T> Tail<T>(this IEnumerable<T> items)
		{
			Contract.Requires<ArgumentNullException>(items != null);
			Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
			Contract.Ensures(Contract.OldValue(items.Count()) == Contract.Result<IEnumerable<T>>().Count() + 1);

			return items.Skip(1);
		}

		/// <summary>
		/// Cycles the specified items.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="items">The items.</param>
		/// <returns></returns>
		/// <example>
		/// <code>
		/// <![CDATA[
		/// var name = Letters.ToCharArray().Cycle().Take(25).ToArray();
		/// var result = name.Shuffle().Join()
		/// ]]>
		/// </code>
		/// </example>
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", MessageId = "0"), Pure]
		public static IEnumerable<T> Cycle<T>(this IEnumerable<T> items)
		{
			Contract.Requires<ArgumentNullException>(items != null);
			Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);

			while (true)
			{
				foreach (var item in items)
				{
					yield return item;
				}
			}
		}

		/// <summary>
		/// Slices the specified size.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="items">The items.</param>
		/// <param name="size">The size.</param>
		/// <returns></returns>
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", MessageId = "0"), Pure]
		public static IEnumerable<IEnumerable<T>> Windowed<T>(this IEnumerable<T> items, int size)
		{
			Contract.Requires<ArgumentNullException>(items != null);
			Contract.Ensures(Contract.Result<IEnumerable<IEnumerable<T>>>() != null);

			int count = 0;
			Func<int, int, IEnumerable<T>> skip = (x, y) => items.Skip(y).Take(x);

			var ids = skip(size, count);
			while (ids.Any() && ids.Count() == size)
			{
				yield return ids;
				count++;
				ids = skip(size, count);
			}
		}

		/// <summary>
		/// Indexes the of maximum.
		/// </summary>
		/// <param name="items">The items.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"><paramref name="source" /> ou <paramref name="func" /> est null.</exception>
		/// <exception cref="InvalidOperationException"><paramref name="source" /> ne contient aucun élément.</exception>
		public static int IndexOfMax(this IEnumerable<int> items)
		{
			Contract.Requires<ArgumentNullException>(items != null);

			return items.Select((x, i) => new { Value = x, Index = i }).Aggregate((x, y) => x.Value > y.Value ? x : y).Index;
		}


		public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> items, int size)
		{
			Contract.Requires<ArgumentNullException>(items != null);
			Contract.Ensures(Contract.Result<IEnumerable<IEnumerable<T>>>() != null);

			Func<int, int, IEnumerable<T>> skip = (x, y) => items.Skip(x).Take(y);

			var next = 0;
			var ids = skip(next, size);
			while (ids.Any())
			{
				yield return ids;
				next = next + size;
				ids = skip(next, size);
			}
		}

		public static IEnumerable<T> IntersectSorted<T>(this IEnumerable<T> sequence1, IEnumerable<T> sequence2, IComparer<T> comparer)
		{
			using (var cursor1 = sequence1.GetEnumerator())
			using (var cursor2 = sequence2.GetEnumerator())
			{
				if (!cursor1.MoveNext() || !cursor2.MoveNext())
				{
					yield break;
				}
				var value1 = cursor1.Current;
				var value2 = cursor2.Current;

				while (true)
				{
					int comparison = comparer.Compare(value1, value2);
					if (comparison < 0)
					{
						if (!cursor1.MoveNext())
						{
							yield break;
						}
						value1 = cursor1.Current;
					}
					else if (comparison > 0)
					{
						if (!cursor2.MoveNext())
						{
							yield break;
						}
						value2 = cursor2.Current;
					}
					else
					{
						yield return value1;
						if (!cursor1.MoveNext() || !cursor2.MoveNext())
						{
							yield break;
						}
						value1 = cursor1.Current;
						value2 = cursor2.Current;
					}
				}
			}
		}	 }
}
