namespace LibraryNET.Core
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;

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