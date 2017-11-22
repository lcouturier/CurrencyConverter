namespace LibraryNET.Core
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;

    public static class DisposableExtensions
	{
		#region Public Methods

		/// <summary>
		/// Encapsule l'utilsation du using
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj">The object.</param>
		/// <param name="action">The action.</param>
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", MessageId = "1")]
		public static void Use<T>(this T obj, Action<T> action) where T : class, IDisposable
		{
			Contract.Requires<ArgumentNullException>(obj != null);
			Contract.Requires<ArgumentNullException>(action != null);

			using (obj)
			{
				action(obj);
			}
		}

		/// <summary>
		/// Encapsule l'utilisation du using
		/// </summary>
		/// <typeparam name="T">type qui implémente <see cref="IDisposable" /></typeparam>
		/// <typeparam name="TResult">Type de la valeur de retour.</typeparam>
		/// <param name="obj">Instance en cours.</param>
		/// <param name="func">Fonction executée dans le using.</param>
		/// <returns>
		/// Retourne une valeur de type <typeparam name="TResult" />
		/// </returns>
		/// <example>
		///   <code>
		///       <![CDATA[
		///       var result = new StreamReader("201304171344115665.xml").Use(sr => sr.ReadToEnd());
		///       ]]>
		///   </code>
		/// </example>
		/// <exception cref="Exception">A delegate callback throws an exception. </exception>
		[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de méthodes publiques", MessageId = "1")]
		public static TResult Use<T, TResult>(this T obj, Func<T, TResult> func)
			where T : class, IDisposable
		{
			Contract.Requires<ArgumentNullException>(obj != null);
			Contract.Requires<ArgumentNullException>(func != null);

			using (obj)
			{
				return func(obj);
			}
		}





		#endregion Public Methods
	 }
}
