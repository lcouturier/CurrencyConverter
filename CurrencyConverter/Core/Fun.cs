﻿using System;
namespace CurrencyConverter
{
	using System.Diagnostics.CodeAnalysis;
	using System.Diagnostics.Contracts;
	/// <summary>
	/// Facilite la création de fonction lambda
	/// </summary>
	/// <example>
	/// <code>
	/// <![CDATA[
	/// var parseInt = Fun.Create((string s) => Int32.Parse(s)).ReturnOption().OnExceptionNone();
	/// var i = parseInt("sdfs");
	/// ]]>
	/// </code>
	/// </example>
	public static class Fun
	{
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", Justification = "Géré par Code Contract")]
		public static Func<TResult> Create<TResult>(Func<TResult> func)
		{
			Contract.Requires<ArgumentNullException>(func != null);
			Contract.Ensures(Contract.Result<Func<TResult>>() != null);

			return func;
		}

		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", Justification = "Géré par Code Contract")]
		public static Func<T1, TResult> Create<T1, TResult>(Func<T1, TResult> func)
		{
			Contract.Requires<ArgumentNullException>(func != null);
			Contract.Ensures(Contract.Result<Func<T1, TResult>>() != null);

			return func;
		}


		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", Justification = "Géré par Code Contract")]
		public static Func<T1, T2, TResult> Create<T1, T2, TResult>(Func<T1, T2, TResult> func)
		{
			Contract.Requires<ArgumentNullException>(func != null);
			Contract.Ensures(Contract.Result<Func<T1, T2, TResult>>() != null);

			return func;
		}


		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", Justification = "Géré par Code Contract")]
		public static Func<T1, T2, T3, TResult> Create<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func)
		{
			Contract.Requires<ArgumentNullException>(func != null);
			Contract.Ensures(Contract.Result<Func<T1, T2, T3, TResult>>() != null);

			return func;
		}


		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", Justification = "Géré par Code Contract")]
		public static Func<T1, T2, T3, T4, TResult> Create<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> func)
		{
			Contract.Requires<ArgumentNullException>(func != null);
			Contract.Ensures(Contract.Result<Func<T1, T2, T3, T4, TResult>>() != null);

			return func;
		}


		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", Justification = "Géré par Code Contract")]
		public static Func<T1, T2, T3, T4, T5, TResult> Create<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> func)
		{
			Contract.Requires<ArgumentNullException>(func != null);
			Contract.Ensures(Contract.Result<Func<T1, T2, T3, T4, T5, TResult>>() != null);

			return func;
		}


		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", Justification = "Géré par Code Contract")]
		public static Func<T1, T2, T3, T4, T5, T6, TResult> Create<T1, T2, T3, T4, T5, T6, TResult>(Func<T1, T2, T3, T4, T5, T6, TResult> func)
		{
			Contract.Requires<ArgumentNullException>(func != null);
			Contract.Ensures(Contract.Result<Func<T1, T2, T3, T4, T5, T6, TResult>>() != null);

			return func;
		}


		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", Justification = "Géré par Code Contract")]
		public static Func<T1, T2, T3, T4, T5, T6, T7, TResult> Create<T1, T2, T3, T4, T5, T6, T7, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> func)
		{
			Contract.Requires<ArgumentNullException>(func != null);
			Contract.Ensures(Contract.Result<Func<T1, T2, T3, T4, T5, T6, T7, TResult>>() != null);

			return func;
		}


		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", Justification = "Géré par Code Contract")]
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func)
		{
			Contract.Requires<ArgumentNullException>(func != null);
			Contract.Ensures(Contract.Result<Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>>() != null);

			return func;
		}


		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", Justification = "Géré par Code Contract")]
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func)
		{
			Contract.Requires<ArgumentNullException>(func != null);
			Contract.Ensures(Contract.Result<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>>() != null);

			return func;
		}


		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", Justification = "Géré par Code Contract")]
		public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func)
		{
			Contract.Requires<ArgumentNullException>(func != null);
			Contract.Ensures(Contract.Result<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>>() != null);

			return func;
		}	}
}
