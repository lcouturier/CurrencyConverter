namespace CurrencyConverter.Library.Core
{
    using System;
    using System.Collections.Generic;

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
				return this._first;
			}
		}

		public T2 Second
		{
			get
			{
				return this._second;
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
			return EqualityComparer<T1>.Default.Equals(this._first, other._first) && EqualityComparer<T2>.Default.Equals(this._second, other._second);
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
			return this.Equals((Pair<T1, T2>)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (EqualityComparer<T1>.Default.GetHashCode(this._first) * 397) ^ EqualityComparer<T2>.Default.GetHashCode(this._second);
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
			return string.Format("First: {0}, Second: {1}", this._first, this._second);
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
