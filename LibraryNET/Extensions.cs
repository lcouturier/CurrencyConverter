
namespace CurrencyConverter.Library
{
    using System.IO;
    using Newtonsoft.Json;
    using System.Diagnostics.CodeAnalysis;


    public static class Extensions
	{
        /// <summary>
        /// Deserializes the json.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException">Le fichier est introuvable.</exception>
        /// <exception cref="DirectoryNotFoundException">Le chemin d'accès spécifié n'est pas valide, il se trouve par exemple sur un lecteur non mappé.</exception>
        /// <exception cref="IOException"><paramref name="path" /> comprend une syntaxe incorrecte ou non valide pour les noms de fichiers, les noms de répertoires ou les noms de volumes.</exception>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Supprimer les objets avant la mise hors de port�e", Justification = "Impl�menter dans la m�thode Use")]
		public static T DeserializeJson<T>(this string value) where T : class
		{
			using (var sr = new StreamReader(value))
			{
				return new JsonSerializer().Deserialize(sr, typeof(T)) as T;
			}
		}
	}
}
