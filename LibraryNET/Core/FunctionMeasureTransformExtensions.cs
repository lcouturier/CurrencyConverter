//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibraryNET.Core
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;
    using System.Diagnostics;
	using System.CodeDom.Compiler;

	/// <summary>
    ///     Calcul du temps d'execution d'une m�thode
    /// </summary>
    /// <typeparam name="T1">Type en entr�e</typeparam>
    /// <typeparam name="TResult">Type en sortie</typeparam>
    /// <param name="func">Fonction �valu�e</param>
    /// <returns>
    ///     Nouvelle fonction qui retourne un <see cref="Tuple{T1,T2}" />
    ///     qui contient la valeur de retour de la fonction et le temps d'execution
    /// </returns>
    /// <example>
    ///     calcul de factorielle avec memoization de la fonction et ajout d'un calcul de temps d'execution
    ///     <code>
    /// <![CDATA[
    ///    Func<int, int> factorial = null;
    ///    factorial = x => x.Match().With(i => i < 2, i => i).Else((i => i * factorial(i - 1))).Do();
    ///    var f = factorial.Memoize().Measure();
    /// ]]>
    /// </code>
    /// </example>
    /// <exception cref="Exception">A delegate callback throws an exception.</exception>
	[GeneratedCode("T4CodeGenerator", "1.0.0.0")] 
    public static class FunctionMeasureTransformExtensions
    {	
	    /// <summary>
        ///     Calcul du temps d'execution d'une m�thode
        /// </summary>
        /// <typeparam name="T1">Type en entr�e</typeparam>
        /// <typeparam name="TResult">Type en sortie</typeparam>
        /// <param name="func">Fonction �valu�e</param>
        /// <returns>
        ///     Nouvelle fonction qui retourne un <see cref="Tuple{T1,T2}" />
        ///     qui contient la valeur de retour de la fonction et le temps d'execution
        /// </returns>
        /// <example>
        ///     calcul de factorielle avec memoization de la fonction et ajout d'un calcul de temps d'execution
        ///     <code>
        /// <![CDATA[
        ///    Func<int, int> factorial = null;
        ///    factorial = x => x.Match().With(i => i < 2, i => i).Else((i => i * factorial(i - 1))).Do();
        ///    var f = factorial.Memoize().Measure();
        /// ]]>
        /// </code>
        /// </example>
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
		[Obsolete("Ne plus utiliser",false)]
        [SuppressMessage("Microsoft.Reliability", "CA2000:Supprimer les objets avant la mise hors de port�e")] 
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", Justification = "G�r� par Code Contract")]
        public static Func<T1, Tuple<TResult, long>> Measure<T1, TResult>(this Func<T1, TResult> func)
        {
            Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, Tuple<TResult, long>>>() != null);

            return args =>
            {
                TResult result = default(TResult);
                var sw = new Stopwatch();
                new UnitOfWork(sw.Stop).Do(x =>
                    {
                        sw.Start();
                        result = func(args);
                    });

                return Tuple.Create(result, sw.ElapsedTicks);
            };
        }

		/// <summary>
        ///     Calcul du temps d'execution d'une m�thode
        /// </summary>
        /// <typeparam name="T1">Type en entr�e</typeparam>
        /// <typeparam name="TResult">Type en sortie</typeparam>
        /// <param name="func">Fonction �valu�e</param>
        /// <returns>
        ///     Nouvelle fonction qui retourne un <see cref="Tuple{T1,T2}" />
        ///     qui contient la valeur de retour de la fonction et le temps d'execution
        /// </returns>
        /// <example>
        ///     calcul de factorielle avec memoization de la fonction et ajout d'un calcul de temps d'execution
        ///     <code>
        /// <![CDATA[
        ///    Func<int, int> factorial = null;
        ///    factorial = x => x.Match().With(i => i < 2, i => i).Else((i => i * factorial(i - 1))).Do();
        ///    var f = factorial.Memoize().Measure();
        /// ]]>
        /// </code>
        /// </example>
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>		
        [SuppressMessage("Microsoft.Reliability", "CA2000:Supprimer les objets avant la mise hors de port�e")] 
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", Justification = "G�r� par Code Contract")]
        public static Func<Tuple<TResult, TimeSpan>> MeasureElapsed<TResult>(this Func<TResult> func)
        {
            Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<Tuple<TResult, TimeSpan>>>() != null);

            return () =>
            {
                TResult result = default(TResult);
                var sw = new Stopwatch();
                new UnitOfWork(sw.Stop).Do(x =>
                    {
                        sw.Start();
                        result = func();
                    });

                return Tuple.Create(result, sw.Elapsed);
            };
        }

		/// <summary>
        ///     Calcul du temps d'execution d'une m�thode
        /// </summary>
        /// <typeparam name="T1">Type en entr�e</typeparam>
        /// <typeparam name="TResult">Type en sortie</typeparam>
        /// <param name="func">Fonction �valu�e</param>
        /// <returns>
        ///     Nouvelle fonction qui retourne un <see cref="Tuple{T1,T2}" />
        ///     qui contient la valeur de retour de la fonction et le temps d'execution
        /// </returns>
        /// <example>
        ///     calcul de factorielle avec memoization de la fonction et ajout d'un calcul de temps d'execution
        ///     <code>
        /// <![CDATA[
        ///    Func<int, int> factorial = null;
        ///    factorial = x => x.Match().With(i => i < 2, i => i).Else((i => i * factorial(i - 1))).Do();
        ///    var f = factorial.Memoize().Measure();
        /// ]]>
        /// </code>
        /// </example>
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>		
        [SuppressMessage("Microsoft.Reliability", "CA2000:Supprimer les objets avant la mise hors de port�e")] 
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", Justification = "G�r� par Code Contract")]
        public static Func<T1, Tuple<TResult, TimeSpan>> MeasureElapsed<T1, TResult>(this Func<T1, TResult> func)
        {
            Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, Tuple<TResult, TimeSpan>>>() != null);

            return args =>
            {
                TResult result = default(TResult);
                var sw = new Stopwatch();
                new UnitOfWork(sw.Stop).Do(
                    x =>
                    {
                        sw.Start();
                        result = func(args);
                    });

                return Tuple.Create(result, sw.Elapsed);
            };
        }

		    /// <summary>
        ///     Calcul du temps d'execution d'une m�thode
        /// </summary>
		/// <typeparam name="T1">The type of the 1.</typeparam>
/// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="TResult">Type encapsuler par un <see cref="Either"/>.</typeparam>
        /// <param name="func">Fonction � d�corer par un gestionnaire d'exception qui retourn un <see cref="Either{TValue}"/> dans tous les cas.</param>
        /// <returns>
        ///     Nouvelle fonction qui retourne un <see cref="Tuple{T1,T2}" />
        ///     qui contient la valeur de retour de la fonction et le temps d'execution
        /// </returns>
		[Obsolete("Ne plus utiliser",false)]
	    [SuppressMessage("Microsoft.Reliability", "CA2000:Supprimer les objets avant la mise hors de port�e")] 
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", Justification = "G�r� par Code Contract")]
        public static Func<T1, T2, Tuple<TResult,long>> Measure<T1, T2, TResult>(this Func<T1, T2, TResult> func)
        {
			Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, T2, Tuple<TResult, long>>>() != null);

            return (x1, x2) => func.Currying()(x1).Measure()(x2);
        }

		/// <summary>
        ///     Calcul du temps d'execution d'une m�thode
        /// </summary>
		/// <typeparam name="T1">The type of the 1.</typeparam>
/// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="TResult">Type encapsuler par un <see cref="Either"/>.</typeparam>
        /// <param name="func">Fonction � d�corer par un gestionnaire d'exception qui retourn un <see cref="Either{TValue}"/> dans tous les cas.</param>
        /// <returns>
        ///     Nouvelle fonction qui retourne un <see cref="Tuple{T1,T2}" />
        ///     qui contient la valeur de retour de la fonction et le temps d'execution
        /// </returns>		
	    [SuppressMessage("Microsoft.Reliability", "CA2000:Supprimer les objets avant la mise hors de port�e")] 
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", Justification = "G�r� par Code Contract")]
        public static Func<T1, T2, Tuple<TResult,TimeSpan>> MeasureElapsed<T1, T2, TResult>(this Func<T1, T2, TResult> func)
        {
			Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, T2, Tuple<TResult, TimeSpan>>>() != null);

            return (x1, x2) => func.Currying()(x1).MeasureElapsed()(x2);
        }

		    /// <summary>
        ///     Calcul du temps d'execution d'une m�thode
        /// </summary>
		/// <typeparam name="T1">The type of the 1.</typeparam>
/// <typeparam name="T2">The type of the 2.</typeparam>
/// <typeparam name="T3">The type of the 3.</typeparam>
        /// <typeparam name="TResult">Type encapsuler par un <see cref="Either"/>.</typeparam>
        /// <param name="func">Fonction � d�corer par un gestionnaire d'exception qui retourn un <see cref="Either{TValue}"/> dans tous les cas.</param>
        /// <returns>
        ///     Nouvelle fonction qui retourne un <see cref="Tuple{T1,T2}" />
        ///     qui contient la valeur de retour de la fonction et le temps d'execution
        /// </returns>
		[Obsolete("Ne plus utiliser",false)]
	    [SuppressMessage("Microsoft.Reliability", "CA2000:Supprimer les objets avant la mise hors de port�e")] 
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", Justification = "G�r� par Code Contract")]
        public static Func<T1, T2, T3, Tuple<TResult,long>> Measure<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> func)
        {
			Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, T2, T3, Tuple<TResult, long>>>() != null);

            return (x1, x2, x3) => func.Currying()(x1)(x2).Measure()(x3);
        }

		/// <summary>
        ///     Calcul du temps d'execution d'une m�thode
        /// </summary>
		/// <typeparam name="T1">The type of the 1.</typeparam>
/// <typeparam name="T2">The type of the 2.</typeparam>
/// <typeparam name="T3">The type of the 3.</typeparam>
        /// <typeparam name="TResult">Type encapsuler par un <see cref="Either"/>.</typeparam>
        /// <param name="func">Fonction � d�corer par un gestionnaire d'exception qui retourn un <see cref="Either{TValue}"/> dans tous les cas.</param>
        /// <returns>
        ///     Nouvelle fonction qui retourne un <see cref="Tuple{T1,T2}" />
        ///     qui contient la valeur de retour de la fonction et le temps d'execution
        /// </returns>		
	    [SuppressMessage("Microsoft.Reliability", "CA2000:Supprimer les objets avant la mise hors de port�e")] 
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", Justification = "G�r� par Code Contract")]
        public static Func<T1, T2, T3, Tuple<TResult,TimeSpan>> MeasureElapsed<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> func)
        {
			Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, T2, T3, Tuple<TResult, TimeSpan>>>() != null);

            return (x1, x2, x3) => func.Currying()(x1)(x2).MeasureElapsed()(x3);
        }

		    /// <summary>
        ///     Calcul du temps d'execution d'une m�thode
        /// </summary>
		/// <typeparam name="T1">The type of the 1.</typeparam>
/// <typeparam name="T2">The type of the 2.</typeparam>
/// <typeparam name="T3">The type of the 3.</typeparam>
/// <typeparam name="T4">The type of the 4.</typeparam>
        /// <typeparam name="TResult">Type encapsuler par un <see cref="Either"/>.</typeparam>
        /// <param name="func">Fonction � d�corer par un gestionnaire d'exception qui retourn un <see cref="Either{TValue}"/> dans tous les cas.</param>
        /// <returns>
        ///     Nouvelle fonction qui retourne un <see cref="Tuple{T1,T2}" />
        ///     qui contient la valeur de retour de la fonction et le temps d'execution
        /// </returns>
		[Obsolete("Ne plus utiliser",false)]
	    [SuppressMessage("Microsoft.Reliability", "CA2000:Supprimer les objets avant la mise hors de port�e")] 
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", Justification = "G�r� par Code Contract")]
        public static Func<T1, T2, T3, T4, Tuple<TResult,long>> Measure<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> func)
        {
			Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, T2, T3, T4, Tuple<TResult, long>>>() != null);

            return (x1, x2, x3, x4) => func.Currying()(x1)(x2)(x3).Measure()(x4);
        }

		/// <summary>
        ///     Calcul du temps d'execution d'une m�thode
        /// </summary>
		/// <typeparam name="T1">The type of the 1.</typeparam>
/// <typeparam name="T2">The type of the 2.</typeparam>
/// <typeparam name="T3">The type of the 3.</typeparam>
/// <typeparam name="T4">The type of the 4.</typeparam>
        /// <typeparam name="TResult">Type encapsuler par un <see cref="Either"/>.</typeparam>
        /// <param name="func">Fonction � d�corer par un gestionnaire d'exception qui retourn un <see cref="Either{TValue}"/> dans tous les cas.</param>
        /// <returns>
        ///     Nouvelle fonction qui retourne un <see cref="Tuple{T1,T2}" />
        ///     qui contient la valeur de retour de la fonction et le temps d'execution
        /// </returns>		
	    [SuppressMessage("Microsoft.Reliability", "CA2000:Supprimer les objets avant la mise hors de port�e")] 
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", Justification = "G�r� par Code Contract")]
        public static Func<T1, T2, T3, T4, Tuple<TResult,TimeSpan>> MeasureElapsed<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> func)
        {
			Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, T2, T3, T4, Tuple<TResult, TimeSpan>>>() != null);

            return (x1, x2, x3, x4) => func.Currying()(x1)(x2)(x3).MeasureElapsed()(x4);
        }

		    /// <summary>
        ///     Calcul du temps d'execution d'une m�thode
        /// </summary>
		/// <typeparam name="T1">The type of the 1.</typeparam>
/// <typeparam name="T2">The type of the 2.</typeparam>
/// <typeparam name="T3">The type of the 3.</typeparam>
/// <typeparam name="T4">The type of the 4.</typeparam>
/// <typeparam name="T5">The type of the 5.</typeparam>
        /// <typeparam name="TResult">Type encapsuler par un <see cref="Either"/>.</typeparam>
        /// <param name="func">Fonction � d�corer par un gestionnaire d'exception qui retourn un <see cref="Either{TValue}"/> dans tous les cas.</param>
        /// <returns>
        ///     Nouvelle fonction qui retourne un <see cref="Tuple{T1,T2}" />
        ///     qui contient la valeur de retour de la fonction et le temps d'execution
        /// </returns>
		[Obsolete("Ne plus utiliser",false)]
	    [SuppressMessage("Microsoft.Reliability", "CA2000:Supprimer les objets avant la mise hors de port�e")] 
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", Justification = "G�r� par Code Contract")]
        public static Func<T1, T2, T3, T4, T5, Tuple<TResult,long>> Measure<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> func)
        {
			Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, T2, T3, T4, T5, Tuple<TResult, long>>>() != null);

            return (x1, x2, x3, x4, x5) => func.Currying()(x1)(x2)(x3)(x4).Measure()(x5);
        }

		/// <summary>
        ///     Calcul du temps d'execution d'une m�thode
        /// </summary>
		/// <typeparam name="T1">The type of the 1.</typeparam>
/// <typeparam name="T2">The type of the 2.</typeparam>
/// <typeparam name="T3">The type of the 3.</typeparam>
/// <typeparam name="T4">The type of the 4.</typeparam>
/// <typeparam name="T5">The type of the 5.</typeparam>
        /// <typeparam name="TResult">Type encapsuler par un <see cref="Either"/>.</typeparam>
        /// <param name="func">Fonction � d�corer par un gestionnaire d'exception qui retourn un <see cref="Either{TValue}"/> dans tous les cas.</param>
        /// <returns>
        ///     Nouvelle fonction qui retourne un <see cref="Tuple{T1,T2}" />
        ///     qui contient la valeur de retour de la fonction et le temps d'execution
        /// </returns>		
	    [SuppressMessage("Microsoft.Reliability", "CA2000:Supprimer les objets avant la mise hors de port�e")] 
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", Justification = "G�r� par Code Contract")]
        public static Func<T1, T2, T3, T4, T5, Tuple<TResult,TimeSpan>> MeasureElapsed<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> func)
        {
			Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, T2, T3, T4, T5, Tuple<TResult, TimeSpan>>>() != null);

            return (x1, x2, x3, x4, x5) => func.Currying()(x1)(x2)(x3)(x4).MeasureElapsed()(x5);
        }

		    /// <summary>
        ///     Calcul du temps d'execution d'une m�thode
        /// </summary>
		/// <typeparam name="T1">The type of the 1.</typeparam>
/// <typeparam name="T2">The type of the 2.</typeparam>
/// <typeparam name="T3">The type of the 3.</typeparam>
/// <typeparam name="T4">The type of the 4.</typeparam>
/// <typeparam name="T5">The type of the 5.</typeparam>
/// <typeparam name="T6">The type of the 6.</typeparam>
        /// <typeparam name="TResult">Type encapsuler par un <see cref="Either"/>.</typeparam>
        /// <param name="func">Fonction � d�corer par un gestionnaire d'exception qui retourn un <see cref="Either{TValue}"/> dans tous les cas.</param>
        /// <returns>
        ///     Nouvelle fonction qui retourne un <see cref="Tuple{T1,T2}" />
        ///     qui contient la valeur de retour de la fonction et le temps d'execution
        /// </returns>
		[Obsolete("Ne plus utiliser",false)]
	    [SuppressMessage("Microsoft.Reliability", "CA2000:Supprimer les objets avant la mise hors de port�e")] 
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", Justification = "G�r� par Code Contract")]
        public static Func<T1, T2, T3, T4, T5, T6, Tuple<TResult,long>> Measure<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult> func)
        {
			Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, T2, T3, T4, T5, T6, Tuple<TResult, long>>>() != null);

            return (x1, x2, x3, x4, x5, x6) => func.Currying()(x1)(x2)(x3)(x4)(x5).Measure()(x6);
        }

		/// <summary>
        ///     Calcul du temps d'execution d'une m�thode
        /// </summary>
		/// <typeparam name="T1">The type of the 1.</typeparam>
/// <typeparam name="T2">The type of the 2.</typeparam>
/// <typeparam name="T3">The type of the 3.</typeparam>
/// <typeparam name="T4">The type of the 4.</typeparam>
/// <typeparam name="T5">The type of the 5.</typeparam>
/// <typeparam name="T6">The type of the 6.</typeparam>
        /// <typeparam name="TResult">Type encapsuler par un <see cref="Either"/>.</typeparam>
        /// <param name="func">Fonction � d�corer par un gestionnaire d'exception qui retourn un <see cref="Either{TValue}"/> dans tous les cas.</param>
        /// <returns>
        ///     Nouvelle fonction qui retourne un <see cref="Tuple{T1,T2}" />
        ///     qui contient la valeur de retour de la fonction et le temps d'execution
        /// </returns>		
	    [SuppressMessage("Microsoft.Reliability", "CA2000:Supprimer les objets avant la mise hors de port�e")] 
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", Justification = "G�r� par Code Contract")]
        public static Func<T1, T2, T3, T4, T5, T6, Tuple<TResult,TimeSpan>> MeasureElapsed<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult> func)
        {
			Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, T2, T3, T4, T5, T6, Tuple<TResult, TimeSpan>>>() != null);

            return (x1, x2, x3, x4, x5, x6) => func.Currying()(x1)(x2)(x3)(x4)(x5).MeasureElapsed()(x6);
        }

		    /// <summary>
        ///     Calcul du temps d'execution d'une m�thode
        /// </summary>
		/// <typeparam name="T1">The type of the 1.</typeparam>
/// <typeparam name="T2">The type of the 2.</typeparam>
/// <typeparam name="T3">The type of the 3.</typeparam>
/// <typeparam name="T4">The type of the 4.</typeparam>
/// <typeparam name="T5">The type of the 5.</typeparam>
/// <typeparam name="T6">The type of the 6.</typeparam>
/// <typeparam name="T7">The type of the 7.</typeparam>
        /// <typeparam name="TResult">Type encapsuler par un <see cref="Either"/>.</typeparam>
        /// <param name="func">Fonction � d�corer par un gestionnaire d'exception qui retourn un <see cref="Either{TValue}"/> dans tous les cas.</param>
        /// <returns>
        ///     Nouvelle fonction qui retourne un <see cref="Tuple{T1,T2}" />
        ///     qui contient la valeur de retour de la fonction et le temps d'execution
        /// </returns>
		[Obsolete("Ne plus utiliser",false)]
	    [SuppressMessage("Microsoft.Reliability", "CA2000:Supprimer les objets avant la mise hors de port�e")] 
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", Justification = "G�r� par Code Contract")]
        public static Func<T1, T2, T3, T4, T5, T6, T7, Tuple<TResult,long>> Measure<T1, T2, T3, T4, T5, T6, T7, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, TResult> func)
        {
			Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, T2, T3, T4, T5, T6, T7, Tuple<TResult, long>>>() != null);

            return (x1, x2, x3, x4, x5, x6, x7) => func.Currying()(x1)(x2)(x3)(x4)(x5)(x6).Measure()(x7);
        }

		/// <summary>
        ///     Calcul du temps d'execution d'une m�thode
        /// </summary>
		/// <typeparam name="T1">The type of the 1.</typeparam>
/// <typeparam name="T2">The type of the 2.</typeparam>
/// <typeparam name="T3">The type of the 3.</typeparam>
/// <typeparam name="T4">The type of the 4.</typeparam>
/// <typeparam name="T5">The type of the 5.</typeparam>
/// <typeparam name="T6">The type of the 6.</typeparam>
/// <typeparam name="T7">The type of the 7.</typeparam>
        /// <typeparam name="TResult">Type encapsuler par un <see cref="Either"/>.</typeparam>
        /// <param name="func">Fonction � d�corer par un gestionnaire d'exception qui retourn un <see cref="Either{TValue}"/> dans tous les cas.</param>
        /// <returns>
        ///     Nouvelle fonction qui retourne un <see cref="Tuple{T1,T2}" />
        ///     qui contient la valeur de retour de la fonction et le temps d'execution
        /// </returns>		
	    [SuppressMessage("Microsoft.Reliability", "CA2000:Supprimer les objets avant la mise hors de port�e")] 
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", Justification = "G�r� par Code Contract")]
        public static Func<T1, T2, T3, T4, T5, T6, T7, Tuple<TResult,TimeSpan>> MeasureElapsed<T1, T2, T3, T4, T5, T6, T7, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, TResult> func)
        {
			Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, T2, T3, T4, T5, T6, T7, Tuple<TResult, TimeSpan>>>() != null);

            return (x1, x2, x3, x4, x5, x6, x7) => func.Currying()(x1)(x2)(x3)(x4)(x5)(x6).MeasureElapsed()(x7);
        }

		    /// <summary>
        ///     Calcul du temps d'execution d'une m�thode
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
        /// <param name="func">Fonction � d�corer par un gestionnaire d'exception qui retourn un <see cref="Either{TValue}"/> dans tous les cas.</param>
        /// <returns>
        ///     Nouvelle fonction qui retourne un <see cref="Tuple{T1,T2}" />
        ///     qui contient la valeur de retour de la fonction et le temps d'execution
        /// </returns>
		[Obsolete("Ne plus utiliser",false)]
	    [SuppressMessage("Microsoft.Reliability", "CA2000:Supprimer les objets avant la mise hors de port�e")] 
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", Justification = "G�r� par Code Contract")]
        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, Tuple<TResult,long>> Measure<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func)
        {
			Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, T2, T3, T4, T5, T6, T7, T8, Tuple<TResult, long>>>() != null);

            return (x1, x2, x3, x4, x5, x6, x7, x8) => func.Currying()(x1)(x2)(x3)(x4)(x5)(x6)(x7).Measure()(x8);
        }

		/// <summary>
        ///     Calcul du temps d'execution d'une m�thode
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
        /// <param name="func">Fonction � d�corer par un gestionnaire d'exception qui retourn un <see cref="Either{TValue}"/> dans tous les cas.</param>
        /// <returns>
        ///     Nouvelle fonction qui retourne un <see cref="Tuple{T1,T2}" />
        ///     qui contient la valeur de retour de la fonction et le temps d'execution
        /// </returns>		
	    [SuppressMessage("Microsoft.Reliability", "CA2000:Supprimer les objets avant la mise hors de port�e")] 
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", Justification = "G�r� par Code Contract")]
        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, Tuple<TResult,TimeSpan>> MeasureElapsed<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func)
        {
			Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, T2, T3, T4, T5, T6, T7, T8, Tuple<TResult, TimeSpan>>>() != null);

            return (x1, x2, x3, x4, x5, x6, x7, x8) => func.Currying()(x1)(x2)(x3)(x4)(x5)(x6)(x7).MeasureElapsed()(x8);
        }

		    /// <summary>
        ///     Calcul du temps d'execution d'une m�thode
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
        /// <param name="func">Fonction � d�corer par un gestionnaire d'exception qui retourn un <see cref="Either{TValue}"/> dans tous les cas.</param>
        /// <returns>
        ///     Nouvelle fonction qui retourne un <see cref="Tuple{T1,T2}" />
        ///     qui contient la valeur de retour de la fonction et le temps d'execution
        /// </returns>
		[Obsolete("Ne plus utiliser",false)]
	    [SuppressMessage("Microsoft.Reliability", "CA2000:Supprimer les objets avant la mise hors de port�e")] 
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", Justification = "G�r� par Code Contract")]
        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Tuple<TResult,long>> Measure<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func)
        {
			Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Tuple<TResult, long>>>() != null);

            return (x1, x2, x3, x4, x5, x6, x7, x8, x9) => func.Currying()(x1)(x2)(x3)(x4)(x5)(x6)(x7)(x8).Measure()(x9);
        }

		/// <summary>
        ///     Calcul du temps d'execution d'une m�thode
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
        /// <param name="func">Fonction � d�corer par un gestionnaire d'exception qui retourn un <see cref="Either{TValue}"/> dans tous les cas.</param>
        /// <returns>
        ///     Nouvelle fonction qui retourne un <see cref="Tuple{T1,T2}" />
        ///     qui contient la valeur de retour de la fonction et le temps d'execution
        /// </returns>		
	    [SuppressMessage("Microsoft.Reliability", "CA2000:Supprimer les objets avant la mise hors de port�e")] 
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", Justification = "G�r� par Code Contract")]
        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Tuple<TResult,TimeSpan>> MeasureElapsed<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func)
        {
			Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Tuple<TResult, TimeSpan>>>() != null);

            return (x1, x2, x3, x4, x5, x6, x7, x8, x9) => func.Currying()(x1)(x2)(x3)(x4)(x5)(x6)(x7)(x8).MeasureElapsed()(x9);
        }

		    /// <summary>
        ///     Calcul du temps d'execution d'une m�thode
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
/// <typeparam name="T10">The type of the 10.</typeparam>
        /// <typeparam name="TResult">Type encapsuler par un <see cref="Either"/>.</typeparam>
        /// <param name="func">Fonction � d�corer par un gestionnaire d'exception qui retourn un <see cref="Either{TValue}"/> dans tous les cas.</param>
        /// <returns>
        ///     Nouvelle fonction qui retourne un <see cref="Tuple{T1,T2}" />
        ///     qui contient la valeur de retour de la fonction et le temps d'execution
        /// </returns>
		[Obsolete("Ne plus utiliser",false)]
	    [SuppressMessage("Microsoft.Reliability", "CA2000:Supprimer les objets avant la mise hors de port�e")] 
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", Justification = "G�r� par Code Contract")]
        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Tuple<TResult,long>> Measure<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func)
        {
			Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Tuple<TResult, long>>>() != null);

            return (x1, x2, x3, x4, x5, x6, x7, x8, x9, x10) => func.Currying()(x1)(x2)(x3)(x4)(x5)(x6)(x7)(x8)(x9).Measure()(x10);
        }

		/// <summary>
        ///     Calcul du temps d'execution d'une m�thode
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
/// <typeparam name="T10">The type of the 10.</typeparam>
        /// <typeparam name="TResult">Type encapsuler par un <see cref="Either"/>.</typeparam>
        /// <param name="func">Fonction � d�corer par un gestionnaire d'exception qui retourn un <see cref="Either{TValue}"/> dans tous les cas.</param>
        /// <returns>
        ///     Nouvelle fonction qui retourne un <see cref="Tuple{T1,T2}" />
        ///     qui contient la valeur de retour de la fonction et le temps d'execution
        /// </returns>		
	    [SuppressMessage("Microsoft.Reliability", "CA2000:Supprimer les objets avant la mise hors de port�e")] 
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", Justification = "G�r� par Code Contract")]
        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Tuple<TResult,TimeSpan>> MeasureElapsed<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func)
        {
			Contract.Requires<ArgumentNullException>(func != null);
            Contract.Ensures(Contract.Result<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Tuple<TResult, TimeSpan>>>() != null);

            return (x1, x2, x3, x4, x5, x6, x7, x8, x9, x10) => func.Currying()(x1)(x2)(x3)(x4)(x5)(x6)(x7)(x8)(x9).MeasureElapsed()(x10);
        }

	}

}