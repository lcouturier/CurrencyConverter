namespace CurrencyConverter.Library.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;

    /// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Option")]
	public class Option<T> : IEquatable<Option<T>>
	{
		#region Constants and Fields

		/// <summary>
		/// Pas de valeur
		/// </summary>
		private static readonly Option<T> none = new Option<T>();

		#endregion Constants and Fields

		private readonly T _value;
		private readonly bool _hasValue;

		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Option{T}"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
		public Option(T value)
		{
			this._value = value;
			this._hasValue = true;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Option{T}"/> class.
		/// </summary>
		public Option()
		{
			this._value = default(T);
			this._hasValue = false;
		}

		#endregion Constructors and Destructors

		#region Properties

		/// <summary>
		///   y a t'il une valeur
		/// </summary>
		public bool HasValue
		{
			get
			{
				return this._hasValue;
			}
		}

		/// <summary>
		///   y a t'il pas de valeur
		/// </summary>
		public bool IsNone
		{
			get { return !this.HasValue; }
		}

		/// <summary>
		///   y a t'il une valeur
		/// </summary>
		public bool IsSome
		{
			get { return this.HasValue; }
		}

		/// <summary>
		///   Retourne la valeur encapsul�e par la classe <see cref="Option{T}" />
		/// </summary>
		/// <exception cref="InvalidOperationException">Le type option n'a pas de valeur</exception>
		public T Value
		{
			get
			{
				if (this.IsNone) throw new InvalidOperationException("Le type option n'a pas de valeur");
				return this._value;
			}
		}


		public IEnumerable<T> ToEnumerable()
		{
			if (this.HasValue)
			{
				yield return this.Value;
			}
		}

		/// <summary>
		/// Retourne la valeur encapsul�e par l'instance en cours ou la valeur par d�faut
		/// </summary>
		/// <param name="elseFunc">Valeur par d�faut.</param>
		/// <returns></returns>
		/// <exception cref="Exception">A delegate callback throws an exception.</exception>
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", Justification = "G�r� par Code Contract")]
		public T GetOrElse(Func<T> elseFunc)
		{
			Contract.Requires<ArgumentNullException>(elseFunc != null);

			return this.HasValue ? this.Value : elseFunc();
		}

		/// <summary>
		/// Gets the or else.
		/// </summary>
		/// <param name="other">The other.</param>
		/// <returns></returns>
		public T GetOrElse(T other)
		{
			return this.HasValue ? this.Value : other;
		}

		/// <summary>
		/// Existses the specified predicate.
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		/// <returns></returns>
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", Justification = "G�r� par Code Contract")]
		public bool Exists(Func<T, bool> predicate)
		{
			Contract.Requires<ArgumentNullException>(predicate != null);

			return this.HasValue && predicate(this.Value);
		}

		/// <summary>
		/// Retourne la valeur encapsul�e par l'instance en cours ou la valeur par d�faut
		/// </summary>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="selector">The selector.</param>
		/// <param name="elseFunc">Valeur par d�faut.</param>
		/// <returns></returns>
		/// <exception cref="Exception">A delegate callback throws an exception.</exception>
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", Justification = "G�r� par Code Contract")]
		public TResult Match<TResult>(Func<T, TResult> selector, Func<TResult> elseFunc)
		{
			Contract.Requires<ArgumentNullException>(selector != null);
			Contract.Requires<ArgumentNullException>(elseFunc != null);

			return this.HasValue ? selector(this.Value) : elseFunc();
		}


		/// <summary>
		/// Ifs some.
		/// </summary>
		/// <param name="action">The action.</param>
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", Justification = "G�r� par Code Contract")]
		public void IfSome(Action<T> action)
		{
			Contract.Requires<ArgumentNullException>(action != null);

			if (this.HasValue)
			{
				action(this.Value);
			}
		}


		/// <summary>
		/// Ifs the none.
		/// </summary>
		/// <param name="elseFunc">The else function.</param>
		/// <returns></returns>
		/// <exception cref="Exception">A delegate callback throws an exception.</exception>
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", Justification = "G�r� par Code Contract")]
		public T IfNone(Func<T> elseFunc)
		{
			Contract.Requires<ArgumentNullException>(elseFunc != null);

			return this.IsNone ? elseFunc() : this.Value;
		}

		/// <summary>
		/// Ifs the none.
		/// </summary>
		/// <param name="defaultValue">The default value.</param>
		/// <returns></returns>
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", Justification = "G�r� par Code Contract")]
		public T IfNone(T defaultValue)
		{
			return this.IsNone ? defaultValue : this.Value;
		}

		#endregion Properties

		#region Operators

		/// <summary>
		/// Options the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Op�rateur Implicite")]
		// ReSharper disable once UnusedParameter.Global
		public static implicit operator Option<T>(Option value)
		{
			return none;
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="T"/> to <see cref="Option{T}"/>.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static implicit operator Option<T>(T value)
		{
			return Option.Of(value);
		}

		#endregion Operators

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
		/// </returns>
		public bool Equals(Option<T> other)
		{
			if (ReferenceEquals(null, other))
			{
				return false;
			}
			return ReferenceEquals(this, other) || EqualityComparer<T>.Default.Equals(this._value, other._value);
		}

		/// <summary>
		/// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
		/// <returns>
		///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
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
			return obj.GetType() == this.GetType() && this.Equals((Option<T>)obj);
		}

		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
		/// </returns>
		public override int GetHashCode()
		{
			return EqualityComparer<T>.Default.GetHashCode(this._value);
		}

		/// <summary>
		/// Implements the operator ==.
		/// </summary>
		/// <param name="left">The left.</param>
		/// <param name="right">The right.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static bool operator ==(Option<T> left, Option<T> right)
		{
			return Equals(left, right);
		}

		/// <summary>
		/// Implements the operator !=.
		/// </summary>
		/// <param name="left">The left.</param>
		/// <param name="right">The right.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static bool operator !=(Option<T> left, Option<T> right)
		{
			return !Equals(left, right);
		}

		#region Public Methods


		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return this.HasValue ? string.Format("Value: {0}, HasValue: {1}", this.Value, this.HasValue) : string.Format("Value: None, HasValue: {0}", this.HasValue);
		}

		/// <summary>
		/// Fonction de binding
		/// </summary>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="mapper">The mapper.</param>
		/// <returns><see cref="Option{TResult}"/></returns>
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", MessageId = "0"), Obsolete("Ne plus utiliser", false)]
		public Option<TResult> Map<TResult>(Func<T, Option<TResult>> mapper)
		{
			Contract.Requires<ArgumentNullException>(mapper != null);
			Contract.Ensures(Contract.Result<Option<TResult>>() != null);

			return this.HasValue ? mapper(this.Value) : Option.None;
		}

		/// <summary>
		/// Maps the specified mapper.
		/// </summary>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="mapper">M�thode de transformation.</param>
		/// <returns></returns>
		/// <example>
		/// <code><![CDATA[
		/// var result = Option.Of(10).Map(x => x.ToString());
		/// ]]></code>
		/// </example>
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", MessageId = "0")]
		public Option<TResult> Map<TResult>(Func<T, TResult> mapper)
		{
			Contract.Requires<ArgumentNullException>(mapper != null);
			Contract.Ensures(Contract.Result<Option<TResult>>() != null);

			return this.HasValue ? Option.Of(mapper(this.Value)) : Option.None;
		}

		#endregion Public Methods
	}


	/// <summary>
	/// Classe qui permet la cr�ation d'un type <see cref="Option{T}"/>
	/// </summary>
	/// <example>
	/// <code>
	/// <![CDATA[
	///     int value = 45;
	///     var o = Option.Some(value);    
	/// ]]>
	/// </code>
	/// </example>
	[SuppressMessage("Microsoft.Design", "CA1053:StaticHolderTypesShouldNotHaveConstructors"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Option"), SuppressMessage("Microsoft.Design", "CA1053:StaticHolderTypesShouldNotHaveConstructors", MessageId = "Utilisation via un op�rateur implicite")]
	public class Option
	{
		#region Constants and Fields

		/// <summary>
		///   Le type <see cref="Option"/> ne contient pas de valeur
		/// </summary>
		[SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible")]
		public static readonly Option None = new Option();

		/// <summary>
		///   Le type <see cref="Option"/> ne contient pas de valeur
		/// </summary>
		[SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible")]
		public static readonly Option Empty = new Option();

		#endregion Constants and Fields

		#region Public Methods

		/// <summary>
		/// Cr�ation d'un type Option
		/// </summary>
		/// <typeparam name="T">Type de la valeur encapsul�e</typeparam>
		/// <param name="value">Valeur encapsul�e.</param>
		/// <returns><see cref="Option{T}"/></returns>
		[Obsolete("Preferez l'utilisation de la m�thode Of")]
		public static Option<T> Some<T>(T value)
		{
			return (typeof(T).IsValueType) ? new Option<T>(value) : value != null ? new Option<T>(value) : None;
		}

		/// <summary>
		/// Cr�ation d'un type Option
		/// </summary>
		/// <typeparam name="T">Type de la valeur encapsul�e</typeparam>
		/// <param name="value">Valeur encapsul�e.</param>
		/// <returns><see cref="Option{T}"/></returns>
		public static Option<T> Of<T>(T value)
		{
			return (typeof(T).IsValueType) ? new Option<T>(value) : value != null ? new Option<T>(value) : None;
		}

		/// <summary>
		/// Cr�ation d'un type Option pour les types par r�f�rence
		/// </summary>
		/// <typeparam name="T">Type de la valeur encapsul�e</typeparam>
		/// <param name="value">Valeur encapsul�e.</param>
		/// <returns><see cref="Option{T}"/></returns>
		public static Option<T> OfNullable<T>(T value) where T : class
		{
			return value != null ? new Option<T>(value) : None;
		}




		#endregion Public Methods	 }
}
