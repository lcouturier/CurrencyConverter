using System;
namespace CurrencyConverter
{
	using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;
	using System.Diagnostics.Contracts;

	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class LambdaComparer<T> : IEqualityComparer<T>
	{
		private readonly Func<T, T, bool> _lambdaComparer;
		private readonly Func<T, int> _lambdaHash;

		/// <summary>
		/// Initializes a new instance of the <see cref="LambdaComparer{T}"/> class.
		/// </summary>
		/// <param name="lambdaComparer">The lambda comparer.</param>
		public LambdaComparer(Func<T, T, bool> lambdaComparer) :
			this(lambdaComparer, o => 0)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LambdaComparer{T}"/> class.
		/// </summary>
		/// <param name="lambdaComparer">The lambda comparer.</param>
		/// <param name="lambdaHash">The lambda hash.</param>
		public LambdaComparer(Func<T, T, bool> lambdaComparer, Func<T, int> lambdaHash)
		{
			Contract.Requires<ArgumentNullException>(lambdaComparer != null);
			Contract.Requires<ArgumentNullException>(lambdaHash != null);

			this._lambdaComparer = lambdaComparer;
			this._lambdaHash = lambdaHash;
		}

		[ContractInvariantMethod]
		private void ObjectInvariant()
		{
			Contract.Invariant(_lambdaComparer != null);
			Contract.Invariant(_lambdaHash != null);
		}

		/// <summary>
		/// Determines whether the specified objects are equal.
		/// </summary>
		/// <param name="x">The first object of type T to compare.</param>
		/// <param name="y">The second object of type T to compare.</param>
		/// <returns>
		/// true if the specified objects are equal; otherwise, false.
		/// </returns>
		/// <exception cref="Exception">A delegate callback throws an exception. </exception>
		public bool Equals(T x, T y)
		{
			return this._lambdaComparer(x, y);
		}

		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <param name="obj">The object.</param>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
		/// </returns>
		public int GetHashCode(T obj)
		{
			return this._lambdaHash(obj);
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public static class LambdaComparer
	{
		/// <summary>
		/// Donne la possibilité de créer un Comparer à partir d'une expression Lambda
		/// </summary>
		/// <typeparam name="T"></typeparam>
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
		/// <remarks>
		/// la méthode Intersect se trouve dans le namespace Edenred.France.Framework.Extensions.EnumerableExtensions
		/// </remarks>
		public static LambdaComparer<T> Create<T>(Func<T, T, bool> comparer)
		{
			Contract.Requires<ArgumentNullException>(comparer != null);
			Contract.Ensures(Contract.Result<LambdaComparer<T>>() != null);

			return new LambdaComparer<T>(comparer);
		}

		/// <summary>
		/// Creates the specified comparer.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="comparer">The comparer.</param>
		/// <param name="lambdaHash">The lambda hash.</param>
		/// <returns></returns>
		public static LambdaComparer<T> Create<T>(Func<T, T, bool> comparer, Func<T, int> lambdaHash)
		{
			Contract.Requires<ArgumentNullException>(comparer != null);
			Contract.Requires<ArgumentNullException>(lambdaHash != null);
			Contract.Ensures(Contract.Result<LambdaComparer<T>>() != null);

			return new LambdaComparer<T>(comparer, lambdaHash);
		}	 }
}
