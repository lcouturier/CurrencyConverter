﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Diagnostics;
using System.CodeDom.Compiler;

namespace CurrencyConverter.Core
{
    /// <summary>
    /// Méthodes d'extensions qui implémente un gestionnaire d'exception et qui retourne un <see cref="Option.None"/> en cas d'erreur
    /// </summary>
	[GeneratedCode("T4CodeGenerator", "1.0.0.0")] 
    public static class FunctionResultNoneOnExceptionTransformExtensions
    {	
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", Justification = "Géré par Code Contract")]
	    public static Func<Option<TResult>> OnExceptionNone<TResult>(this Func<Option<TResult>> func)
        {
            Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<Option<TResult>>>() != null);
            
            return () =>
            {
                try
                {
                    return func();
                }
                catch (Exception e)
                {
                    Trace.TraceError(e.Message, e);
                    return Option.None;
                }
            };
        }

	    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", Justification = "Géré par Code Contract")]
	    public static Func<T1, Option<TResult>> OnExceptionNone<T1, TResult>(this Func<T1, Option<TResult>> func)
        {
            Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, Option<TResult>>>() != null);
            
            return args =>
            {
                try
                {
                    return func(args);
                }
                catch (Exception e)
                {
                    Trace.TraceError(e.Message, e);
                    return Option.None;
                }
            };
        }

	
	    [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
	    [SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", Justification = "Géré par Code Contract")]
        public static Func<T1, T2, Option<TResult>> OnExceptionNone<T1, T2, TResult>(this Func<T1, T2, Option<TResult>> func)
        {
			Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, T2, Option<TResult>>>() != null);

            return (x1, x2) => func.Currying()(x1).OnExceptionNone()(x2);
        }

	
	    [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
	    [SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", Justification = "Géré par Code Contract")]
        public static Func<T1, T2, T3, Option<TResult>> OnExceptionNone<T1, T2, T3, TResult>(this Func<T1, T2, T3, Option<TResult>> func)
        {
			Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, T2, T3, Option<TResult>>>() != null);

            return (x1, x2, x3) => func.Currying()(x1)(x2).OnExceptionNone()(x3);
        }

	
	    [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
	    [SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", Justification = "Géré par Code Contract")]
        public static Func<T1, T2, T3, T4, Option<TResult>> OnExceptionNone<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, Option<TResult>> func)
        {
			Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, T2, T3, T4, Option<TResult>>>() != null);

            return (x1, x2, x3, x4) => func.Currying()(x1)(x2)(x3).OnExceptionNone()(x4);
        }

	
	    [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
	    [SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", Justification = "Géré par Code Contract")]
        public static Func<T1, T2, T3, T4, T5, Option<TResult>> OnExceptionNone<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, Option<TResult>> func)
        {
			Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, T2, T3, T4, T5, Option<TResult>>>() != null);

            return (x1, x2, x3, x4, x5) => func.Currying()(x1)(x2)(x3)(x4).OnExceptionNone()(x5);
        }

	
	    [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
	    [SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", Justification = "Géré par Code Contract")]
        public static Func<T1, T2, T3, T4, T5, T6, Option<TResult>> OnExceptionNone<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, Option<TResult>> func)
        {
			Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, T2, T3, T4, T5, T6, Option<TResult>>>() != null);

            return (x1, x2, x3, x4, x5, x6) => func.Currying()(x1)(x2)(x3)(x4)(x5).OnExceptionNone()(x6);
        }

	
	    [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
	    [SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", Justification = "Géré par Code Contract")]
        public static Func<T1, T2, T3, T4, T5, T6, T7, Option<TResult>> OnExceptionNone<T1, T2, T3, T4, T5, T6, T7, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, Option<TResult>> func)
        {
			Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, T2, T3, T4, T5, T6, T7, Option<TResult>>>() != null);

            return (x1, x2, x3, x4, x5, x6, x7) => func.Currying()(x1)(x2)(x3)(x4)(x5)(x6).OnExceptionNone()(x7);
        }

	
	    [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
	    [SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", Justification = "Géré par Code Contract")]
        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, Option<TResult>> OnExceptionNone<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, Option<TResult>> func)
        {
			Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, T2, T3, T4, T5, T6, T7, T8, Option<TResult>>>() != null);

            return (x1, x2, x3, x4, x5, x6, x7, x8) => func.Currying()(x1)(x2)(x3)(x4)(x5)(x6)(x7).OnExceptionNone()(x8);
        }

	
	    [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
	    [SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", Justification = "Géré par Code Contract")]
        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Option<TResult>> OnExceptionNone<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Option<TResult>> func)
        {
			Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Option<TResult>>>() != null);

            return (x1, x2, x3, x4, x5, x6, x7, x8, x9) => func.Currying()(x1)(x2)(x3)(x4)(x5)(x6)(x7)(x8).OnExceptionNone()(x9);
        }

	}

}