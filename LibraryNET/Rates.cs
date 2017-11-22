
namespace LibraryNET
{
    using System.IO;
    using System.Net.Http;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Newtonsoft.Json;    

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
                Trace.WriteLine(result);

                return result.DeserializeJson<Rates>();
            }
        }
    }



    public class Rates
	{	
		[JsonProperty("base")]
		public string Base { get; set; }
		[JsonProperty("date")]
		public string Date { get; set; }
		[JsonProperty("rates")]
		public Dictionary<string,double> Items { get; set; }

      
		public override string ToString()
		{
			return string.Format("[Rates: Base={0}, Date={1}]", Base, Date);
		}
	}
}
