namespace LibraryNET.Core
{
    using System.Diagnostics.Contracts;

    /// <summary>
	/// Méthode d'extension qui permet à n'importe quel type de faire du Pattern Matching
	/// </summary>
	public static class PatternMatchExtensions
	{
		/// <summary>
		/// Matches the specified value.
		/// </summary>
		/// <typeparam name="T">type encapsulé</typeparam>
		/// <param name="value">Valeur utilisée.</param>
		/// <returns><see cref="PatternMatchContext{T}"/></returns>
		/// <example>
		/// Calcul de factorielle qui utilise le PatternMatching
		/// <code>
		///    <![CDATA[
		///        Func<int, int> factorial = null;
		///        factorial = x => x.Match().With(i => i < 2, i => i).Else((i => i * factorial(i - 1))).Do();        
		///    ]]>
		/// </code>
		/// </example>
		public static PatternMatchContext<T> Match<T>(this T value)
		{
			Contract.Ensures(Contract.Result<PatternMatchContext<T>>() != null);

			return new PatternMatchContext<T>(value);
		}	 }
}
