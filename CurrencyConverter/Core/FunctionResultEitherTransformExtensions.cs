﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CurrencyConverter.Core
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;
    using System.Diagnostics;
	using System.CodeDom.Compiler;

	[GeneratedCode("T4CodeGenerator", "1.0.0.0")] 
    public static class FunctionResultEitherTransformExtensions
    {	
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", Justification = "Géré par Code Contract")]
		public static Func<Either<TResult>> OnExceptionEither<TResult>(this Func<TResult> func)
        {
            Contract.Requires<ArgumentNullException>(func != null);

            return () =>
            {
                try
                {
                    return Either.Success(func());
                }
                catch (Exception e)
                {
                    Trace.TraceError(e.Message, e);
                    return Either.Error<TResult>(e);
                }
            };
        }
		
	    /// <summary>
        /// Décoration d'une fonction par un gestionnaire d'exception qui retourn un <see cref="Either{TValue}"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult">Type encapsuler par un <see cref="Either"/>.</typeparam>
        /// <param name="func">Fonction à décorer par un gestionnaire d'exception qui retourn un <see cref="Either{TValue}"/> dans tous les cas.</param>
        /// <returns>Retourne une fonction qui renvoie un <see cref="Either"/></returns>
	    [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", Justification = "Géré par Code Contract")]
        public static Func<T, Either<TResult>> OnExceptionEither<T, TResult>(this Func<T, TResult> func)
        {
            Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T, Either<TResult>>>() != null);

            return args =>
            {
                try
                {
                    return Either.Success(func(args));
                }
                catch (Exception e)
                {
                    Trace.TraceError(e.Message, e);
                    return Either.Error<TResult>(e);
                }
            };
        }

		/// <summary>
        /// Décoration d'une fonction par un gestionnaire d'exception qui retourn un <see cref="Either{TValue}"/>.
        /// </summary>                
		/// <typeparam name="T1">The type of the 1.</typeparam>
/// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="TResult">Type encapsuler par un <see cref="Either"/>.</typeparam>
        /// <param name="func">Fonction à décorer par un gestionnaire d'exception qui retourn un <see cref="Either{TValue}"/> dans tous les cas.</param>
        /// <returns>Retourne une fonction qui renvoie un <see cref="Either"/></returns>
	    [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", Justification = "Géré par Code Contract")]
        public static Func<T1, T2, Either<TResult>> OnExceptionEither<T1, T2, TResult>(this Func<T1, T2, TResult> func)
        {
			Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, T2, Either<TResult>>>() != null);

            return (x1, x2) => func.Currying()(x1).OnExceptionEither()(x2);
        }

		/// <summary>
        /// Décoration d'une fonction par un gestionnaire d'exception qui retourn un <see cref="Either{TValue}"/>.
        /// </summary>                
		/// <typeparam name="T1">The type of the 1.</typeparam>
/// <typeparam name="T2">The type of the 2.</typeparam>
/// <typeparam name="T3">The type of the 3.</typeparam>
        /// <typeparam name="TResult">Type encapsuler par un <see cref="Either"/>.</typeparam>
        /// <param name="func">Fonction à décorer par un gestionnaire d'exception qui retourn un <see cref="Either{TValue}"/> dans tous les cas.</param>
        /// <returns>Retourne une fonction qui renvoie un <see cref="Either"/></returns>
	    [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", Justification = "Géré par Code Contract")]
        public static Func<T1, T2, T3, Either<TResult>> OnExceptionEither<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> func)
        {
			Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, T2, T3, Either<TResult>>>() != null);

            return (x1, x2, x3) => func.Currying()(x1)(x2).OnExceptionEither()(x3);
        }

		/// <summary>
        /// Décoration d'une fonction par un gestionnaire d'exception qui retourn un <see cref="Either{TValue}"/>.
        /// </summary>                
		/// <typeparam name="T1">The type of the 1.</typeparam>
/// <typeparam name="T2">The type of the 2.</typeparam>
/// <typeparam name="T3">The type of the 3.</typeparam>
/// <typeparam name="T4">The type of the 4.</typeparam>
        /// <typeparam name="TResult">Type encapsuler par un <see cref="Either"/>.</typeparam>
        /// <param name="func">Fonction à décorer par un gestionnaire d'exception qui retourn un <see cref="Either{TValue}"/> dans tous les cas.</param>
        /// <returns>Retourne une fonction qui renvoie un <see cref="Either"/></returns>
	    [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", Justification = "Géré par Code Contract")]
        public static Func<T1, T2, T3, T4, Either<TResult>> OnExceptionEither<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> func)
        {
			Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, T2, T3, T4, Either<TResult>>>() != null);

            return (x1, x2, x3, x4) => func.Currying()(x1)(x2)(x3).OnExceptionEither()(x4);
        }

		/// <summary>
        /// Décoration d'une fonction par un gestionnaire d'exception qui retourn un <see cref="Either{TValue}"/>.
        /// </summary>                
		/// <typeparam name="T1">The type of the 1.</typeparam>
/// <typeparam name="T2">The type of the 2.</typeparam>
/// <typeparam name="T3">The type of the 3.</typeparam>
/// <typeparam name="T4">The type of the 4.</typeparam>
/// <typeparam name="T5">The type of the 5.</typeparam>
        /// <typeparam name="TResult">Type encapsuler par un <see cref="Either"/>.</typeparam>
        /// <param name="func">Fonction à décorer par un gestionnaire d'exception qui retourn un <see cref="Either{TValue}"/> dans tous les cas.</param>
        /// <returns>Retourne une fonction qui renvoie un <see cref="Either"/></returns>
	    [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", Justification = "Géré par Code Contract")]
        public static Func<T1, T2, T3, T4, T5, Either<TResult>> OnExceptionEither<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> func)
        {
			Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, T2, T3, T4, T5, Either<TResult>>>() != null);

            return (x1, x2, x3, x4, x5) => func.Currying()(x1)(x2)(x3)(x4).OnExceptionEither()(x5);
        }

		/// <summary>
        /// Décoration d'une fonction par un gestionnaire d'exception qui retourn un <see cref="Either{TValue}"/>.
        /// </summary>                
		/// <typeparam name="T1">The type of the 1.</typeparam>
/// <typeparam name="T2">The type of the 2.</typeparam>
/// <typeparam name="T3">The type of the 3.</typeparam>
/// <typeparam name="T4">The type of the 4.</typeparam>
/// <typeparam name="T5">The type of the 5.</typeparam>
/// <typeparam name="T6">The type of the 6.</typeparam>
        /// <typeparam name="TResult">Type encapsuler par un <see cref="Either"/>.</typeparam>
        /// <param name="func">Fonction à décorer par un gestionnaire d'exception qui retourn un <see cref="Either{TValue}"/> dans tous les cas.</param>
        /// <returns>Retourne une fonction qui renvoie un <see cref="Either"/></returns>
	    [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", Justification = "Géré par Code Contract")]
        public static Func<T1, T2, T3, T4, T5, T6, Either<TResult>> OnExceptionEither<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult> func)
        {
			Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, T2, T3, T4, T5, T6, Either<TResult>>>() != null);

            return (x1, x2, x3, x4, x5, x6) => func.Currying()(x1)(x2)(x3)(x4)(x5).OnExceptionEither()(x6);
        }

		/// <summary>
        /// Décoration d'une fonction par un gestionnaire d'exception qui retourn un <see cref="Either{TValue}"/>.
        /// </summary>                
		/// <typeparam name="T1">The type of the 1.</typeparam>
/// <typeparam name="T2">The type of the 2.</typeparam>
/// <typeparam name="T3">The type of the 3.</typeparam>
/// <typeparam name="T4">The type of the 4.</typeparam>
/// <typeparam name="T5">The type of the 5.</typeparam>
/// <typeparam name="T6">The type of the 6.</typeparam>
/// <typeparam name="T7">The type of the 7.</typeparam>
        /// <typeparam name="TResult">Type encapsuler par un <see cref="Either"/>.</typeparam>
        /// <param name="func">Fonction à décorer par un gestionnaire d'exception qui retourn un <see cref="Either{TValue}"/> dans tous les cas.</param>
        /// <returns>Retourne une fonction qui renvoie un <see cref="Either"/></returns>
	    [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", Justification = "Géré par Code Contract")]
        public static Func<T1, T2, T3, T4, T5, T6, T7, Either<TResult>> OnExceptionEither<T1, T2, T3, T4, T5, T6, T7, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, TResult> func)
        {
			Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, T2, T3, T4, T5, T6, T7, Either<TResult>>>() != null);

            return (x1, x2, x3, x4, x5, x6, x7) => func.Currying()(x1)(x2)(x3)(x4)(x5)(x6).OnExceptionEither()(x7);
        }

		/// <summary>
        /// Décoration d'une fonction par un gestionnaire d'exception qui retourn un <see cref="Either{TValue}"/>.
        /// </summary>                
		/// <typeparam name="T1">The type of the 1.</typeparam>
/// <typeparam name="T2">The type of the 2.</typeparam>
/// <typeparam name="T3">The type of the 3.</typeparam>
/// <typeparam name="T4">The type of the 4.</typeparam>
/// <typeparam name="T5">The type of the 5.</typeparam>
/// <typeparam name="T6">The type of the 6.</typeparam>
/// <typeparam name="T7">The type of the 7.</typeparam>
/// <typeparam name="T8">The type of the 8.</typeparam>
        /// <typeparam name="TResult">Type encapsuler par un <see cref="Either"/>.</typeparam>
        /// <param name="func">Fonction à décorer par un gestionnaire d'exception qui retourn un <see cref="Either{TValue}"/> dans tous les cas.</param>
        /// <returns>Retourne une fonction qui renvoie un <see cref="Either"/></returns>
	    [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", Justification = "Géré par Code Contract")]
        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, Either<TResult>> OnExceptionEither<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func)
        {
			Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, T2, T3, T4, T5, T6, T7, T8, Either<TResult>>>() != null);

            return (x1, x2, x3, x4, x5, x6, x7, x8) => func.Currying()(x1)(x2)(x3)(x4)(x5)(x6)(x7).OnExceptionEither()(x8);
        }

		/// <summary>
        /// Décoration d'une fonction par un gestionnaire d'exception qui retourn un <see cref="Either{TValue}"/>.
        /// </summary>                
		/// <typeparam name="T1">The type of the 1.</typeparam>
/// <typeparam name="T2">The type of the 2.</typeparam>
/// <typeparam name="T3">The type of the 3.</typeparam>
/// <typeparam name="T4">The type of the 4.</typeparam>
/// <typeparam name="T5">The type of the 5.</typeparam>
/// <typeparam name="T6">The type of the 6.</typeparam>
/// <typeparam name="T7">The type of the 7.</typeparam>
/// <typeparam name="T8">The type of the 8.</typeparam>
/// <typeparam name="T9">The type of the 9.</typeparam>
        /// <typeparam name="TResult">Type encapsuler par un <see cref="Either"/>.</typeparam>
        /// <param name="func">Fonction à décorer par un gestionnaire d'exception qui retourn un <see cref="Either{TValue}"/> dans tous les cas.</param>
        /// <returns>Retourne une fonction qui renvoie un <see cref="Either"/></returns>
	    [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", Justification = "Géré par Code Contract")]
        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Either<TResult>> OnExceptionEither<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func)
        {
			Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Either<TResult>>>() != null);

            return (x1, x2, x3, x4, x5, x6, x7, x8, x9) => func.Currying()(x1)(x2)(x3)(x4)(x5)(x6)(x7)(x8).OnExceptionEither()(x9);
        }

	}

}