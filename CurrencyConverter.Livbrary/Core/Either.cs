using System;

namespace CurrencyConverter.Library.Core
{
	using System.Diagnostics.Contracts;
	using System.Diagnostics.CodeAnalysis;

	/// <summary>
	/// 
	/// </summary>
	public static class Either
	{
		/// <summary>
		/// Successes the specified value.
		/// </summary>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static Either<TValue> Success<TValue>(TValue value)
		{
			Contract.Ensures(Contract.Result<Either<TValue>>() != null);
			return new Either<TValue>(value);
		}

		/// <summary>
		/// Errors the specified exception.
		/// </summary>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="exception">The exception.</param>
		/// <returns></returns>
		public static Either<TValue> Error<TValue>(Exception exception)
		{
			Contract.Requires<ArgumentNullException>(exception != null);
			Contract.Ensures(Contract.Result<Either<TValue>>() != null);

			return new Either<TValue>(exception, Option.None);
		}

		/// <summary>
		/// Retourne une instance de <see cref="Either{TValue}"/> en mode erreur
		/// </summary>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="message">Message d'erreur.</param>
		/// <returns>Une instance de <see cref="Either{TValue}"/> en mode erreur</returns>
		/// <example>
		/// <code>
		/// <![CDATA[
		///    var result = Either.Error<int>("Erreur");
		/// ]]>
		/// </code>
		/// </example>
		public static Either<TValue> Error<TValue>(string message)
		{
			Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(message));
			Contract.Ensures(Contract.Result<Either<TValue>>() != null);

			return new Either<TValue>(message);
		}
	}



	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	public class Either<TValue>
	{
		private readonly Option<string> _message;
		private readonly Option<Exception> _exception;
		private readonly Option<TValue> _value;

		public Either(string message)
		{
			Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(message));

			this._exception = Option.None;
			this._value = Option.None;
			this._message = Option.Of(message);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Either{TValue}"/> class.
		/// </summary>
		/// <param name="exception">The exception.</param>
		/// <param name="none">The none.</param>
		public Either(Exception exception, Option none)
		{
			Contract.Requires<ArgumentNullException>(exception != null);
			Contract.Requires<ArgumentNullException>(none != null);

			this._exception = Option.Of(exception);
			this._message = Option.None;
			this._value = none;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Either{TValue}"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
		public Either(TValue value)
		{
			this._value = Option.Of(value);
			this._message = Option.None;
			this._exception = Option.None;
		}

		[ContractInvariantMethod]
		private void ObjectInvariant()
		{
			Contract.Invariant(_message != null);
			Contract.Invariant(_exception != null);
			Contract.Invariant(_value != null);
		}

		/// <summary>
		/// Gets a value indicating whether this instance has value.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has value; otherwise, <c>false</c>.
		/// </value>
		public bool HasValue
		{
			get
			{
				return this._value.HasValue;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance has error.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has error; otherwise, <c>false</c>.
		/// </value>
		public bool HasError
		{
			get
			{
				return this._exception.HasValue || this._message.HasValue;
			}
		}

		/// <summary>
		/// Gets the exception.
		/// </summary>
		/// <value>
		/// The exception.
		/// </value>
		public Exception Exception
		{
			get
			{
				Contract.Assert(this._exception.HasValue);
				return this._exception.Value;
			}
		}

		/// <summary>
		/// Gets the message.
		/// </summary>
		/// <value>
		/// The message.
		/// </value>
		public string Message
		{
			get
			{
				Contract.Assert(this._message.HasValue);
				return this._message.Value;
			}
		}

		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		public TValue Value
		{
			get
			{
				Contract.Assert(this._value.HasValue);
				return this._value.Value;
			}
		}

		/// <summary>
		/// Retourne la valeur en cours sinon une valeur par défault
		/// </summary>
		/// <param name="failure">Fonction qui renvoi une valeur par défault.</param>
		/// <returns>Retourne le valeur de la classe</returns>
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", MessageId = "0")]
		public TValue GetOrElse(Func<TValue> failure)
		{
			Contract.Requires<ArgumentNullException>(failure != null);

			return this.HasValue ? Value : failure();
		}

		/// <summary>
		/// Gets the or else.
		/// </summary>
		/// <returns></returns>
		public TValue GetOrElse()
		{
			Func<TValue> failure = () => default(TValue);
			return this.GetOrElse(failure);
		}


		/// <summary>
		/// Retourne le message de l'exception encapsulée dans le type <see cref="Either{TValue}"/>
		/// </summary>
		/// <returns>Message de l'exception</returns>
		public string GetMessage()
		{
			return this.Match().With(x => x.HasError, x => x.Exception.GetMessage()).Else(x => x.Value.ToString()).Do();
		}

		/// <summary>
		/// Matches the specified selector.
		/// </summary>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="selector">The selector.</param>
		/// <param name="elseFunc">The else function.</param>
		/// <returns></returns>
		/// <example>
		/// <code>
		/// <![CDATA[
		/// var either = Either.Error<int>("Erreur");
		/// var result = either.Match(x => x.ToString(), () => "99");
		/// ]]>
		/// </code>
		/// </example>        
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", Justification = "Géré par Code Contract")]
		public TResult Match<TResult>(Func<TValue, TResult> selector, Func<TResult> elseFunc)
		{
			Contract.Requires<ArgumentNullException>(selector != null);
			Contract.Requires<ArgumentNullException>(elseFunc != null);

			return HasValue ? selector(this.Value) : elseFunc();
		}	 }


	/// <summary>
	/// </summary>
	public static class EitherExtensions
	{
		#region Public Methods and Operators

		/// <summary>
		/// Transformation de la méthode par l'ajout d'un gestionnaire d'exception
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="selector">The f.</param>
		/// <returns></returns>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures"), SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public static Func<Either<T>, Either<T>> Bind<T>(this Func<T, bool> selector)
		{
			Contract.Requires<ArgumentNullException>(selector != null);
			Contract.Ensures(Contract.Result<Func<Either<T>, Either<T>>>() != null);

			return args =>
			{
				try
				{
					if (args.HasError)
					{
						return args;
					}

					var result = selector(args.Value);
					return result
						? Either.Success(args.Value)
						: Either.Error<T>(string.Format("Le traitement de la méthode a échoué !!!"));
				}
				catch (Exception e)
				{
					return Either.Error<T>(e);
				}
			};
		}

		/// <summary>
		///     Protects the specified selector.
		/// </summary>
		/// <typeparam name="T1">The type of the 1.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="selector">The selector.</param>
		/// <returns></returns>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures"), SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public static Func<T1, Either<TResult>> Protect<T1, TResult>(this Func<T1, TResult> selector)
		{
			Contract.Requires<ArgumentNullException>(selector != null);
			Contract.Ensures(Contract.Result<Func<T1, Either<TResult>>>() != null);
			return x =>
			{
				try
				{
					var result = selector(x);
					return Either.Success(result);
				}
				catch (Exception e)
				{
					return Either.Error<TResult>(e);
				}
			};
		}

		/// <summary>
		///     Retourne une fonction qui protège l'appel de la fonction initiale et retourne une classe de tye
		///     <see cref="Either{TValue}" />
		/// </summary>
		/// <typeparam name="T1">The type of the 1.</typeparam>
		/// <typeparam name="T2">The type of the 2.</typeparam>
		/// <typeparam name="TResult">The type of the value.</typeparam>
		/// <param name="selector">The selector.</param>
		/// <returns></returns>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures"), SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public static Func<T1, T2, Either<TResult>> Protect<T1, T2, TResult>(this Func<T1, T2, TResult> selector)
		{
			Contract.Requires<ArgumentNullException>(selector != null);
			Contract.Ensures(Contract.Result<Func<T1, T2, Either<TResult>>>() != null);
			return (x, y) =>
			{
				try
				{
					var result = selector(x, y);
					return Either.Success(result);
				}
				catch (Exception e)
				{
					return Either.Error<TResult>(e);
				}
			};
		}

		/// <summary>
		///     Protects the specified selector.
		/// </summary>
		/// <typeparam name="T1">The type of the 1.</typeparam>
		/// <typeparam name="T2">The type of the 2.</typeparam>
		/// <typeparam name="T3">The type of the 3.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="selector">The selector.</param>
		/// <returns></returns>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures"), SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public static Func<T1, T2, T3, Either<TResult>> Protect<T1, T2, T3, TResult>(
			this Func<T1, T2, T3, TResult> selector)
		{
			Contract.Requires<ArgumentNullException>(selector != null);
			Contract.Ensures(Contract.Result<Func<T1, T2, T3, Either<TResult>>>() != null);
			return (x, y, z) =>
			{
				try
				{
					var result = selector(x, y, z);
					return Either.Success(result);
				}
				catch (Exception e)
				{
					return Either.Error<TResult>(e);
				}
			};
		}

		/// <summary>
		///     Selects the many.
		/// </summary>
		/// <typeparam name="T1">The type of the 1.</typeparam>
		/// <typeparam name="T2">The type of the 2.</typeparam>
		/// <typeparam name="T3">The type of the 3.</typeparam>
		/// <param name="obj">The object.</param>
		/// <param name="selector">The selector.</param>
		/// <param name="resultSelector">The result selector.</param>
		/// <returns></returns>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static Either<T3> SelectMany<T1, T2, T3>(
			this Either<T1> obj,
			Func<T1, Either<T2>> selector,
			Func<T1, T2, T3> resultSelector)
		{
			if (obj.HasError)
			{
				return Either.Error<T3>(obj.Exception);
			}
			var result = selector(obj.Value);
			return result.HasError
				? Either.Error<T3>(result.Exception)
				: resultSelector.Protect()(obj.Value, result.Value);
		}


		#endregion
	 }
}
