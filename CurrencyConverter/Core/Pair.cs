using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;


namespace CurrencyConverter
{
	

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


	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T1">The type of the 1.</typeparam>
	/// <typeparam name="T2">The type of the 2.</typeparam>
	public class Pair<T1, T2> : IEquatable<Pair<T1, T2>>
	{
		private readonly T1 _first;
		private readonly T2 _second;

		public Pair(T1 first, T2 second)
		{
			this._first = first;
			this._second = second;
		}

		public T1 First
		{
			get
			{
				return _first;
			}
		}

		public T2 Second
		{
			get
			{
				return _second;
			}
		}

		public bool Equals(Pair<T1, T2> other)
		{
			if (ReferenceEquals(null, other))
			{
				return false;
			}
			if (ReferenceEquals(this, other))
			{
				return true;
			}
			return EqualityComparer<T1>.Default.Equals(_first, other._first) && EqualityComparer<T2>.Default.Equals(_second, other._second);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}
			if (ReferenceEquals(this, obj))
			{
				return true;
			}
			if (obj.GetType() != this.GetType())
			{
				return false;
			}
			return Equals((Pair<T1, T2>)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (EqualityComparer<T1>.Default.GetHashCode(_first) * 397) ^ EqualityComparer<T2>.Default.GetHashCode(_second);
			}
		}

		public static bool operator ==(Pair<T1, T2> left, Pair<T1, T2> right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(Pair<T1, T2> left, Pair<T1, T2> right)
		{
			return !Equals(left, right);
		}

		public override string ToString()
		{
			return string.Format("First: {0}, Second: {1}", _first, _second);
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public static class Pair
	{
		/// <summary>
		/// Creates the specified first.
		/// </summary>
		/// <typeparam name="T1">The type of the 1.</typeparam>
		/// <typeparam name="T2">The type of the 2.</typeparam>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		/// <returns></returns>
		public static Pair<T1, T2> Of<T1, T2>(T1 first, T2 second)
		{
			return new Pair<T1, T2>(first, second);
		}
	}
}
