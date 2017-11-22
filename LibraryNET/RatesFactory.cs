using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace LibraryNET
{
	public static class RatesFactory
	{
		/// <summary>
		/// Loads the asynchronous.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="FileNotFoundException">Le fichier est introuvable.</exception>
		/// <exception cref="DirectoryNotFoundException">Le chemin d'accès spécifié n'est pas valide, il se trouve par exemple sur un lecteur non mappé.</exception>
		/// <exception cref="IOException"><paramref name="path" /> comprend une syntaxe incorrecte ou non valide pour les noms de fichiers, les noms de répertoires ou les noms de volumes.</exception>
		public static async Task<Rates> LoadAsync()
		{
			using (var client = new HttpClient())
			{
				var response = await client.GetAsync("https://api.fixer.io/latest");
				var result = response.Content.ReadAsStringAsync().Result;
				Trace.TraceInformation(response.StatusCode.ToString());
				     
				return result.DeserializeJson<Rates>();
			}
		}
	 }
}
