namespace LibraryNET.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
	/// Méthodes d'extensions de la classe <see cref="Exception"/>
	/// </summary>
	public static class ExceptionExtensions
	{
		/// <summary>
		/// Retourne un message d'erreur qui est la concaténation des messages de toutes les exceptions
		/// </summary>
		/// <param name="ex"><see cref="Exception"/>.</param>
		/// <returns><see cref="string"/></returns>
		public static string GetMessage(this Exception ex)
		{
			return ex.GetMessages().Join("\n");
		}

		/// <summary>
		/// Retourne la liste des messages de l'exception courante et de l'innerException
		/// </summary>
		/// <param name="ex">Exception courante.</param>
		/// <returns>Liste des messages d'erreurs</returns>
		public static IEnumerable<string> GetMessages(this Exception ex)
		{
			return ex == null ? new List<string>() : GetMessages(ex.InnerException).Concat(new List<string>() { ex.Message });
		}	 }
}
