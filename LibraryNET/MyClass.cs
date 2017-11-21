
namespace LibraryNET
{	
	using System.IO;
	using System.Net.Http;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Newtonsoft.Json;
	using System.Diagnostics.CodeAnalysis;


	public static class Extensions
	{
		[SuppressMessage("Microsoft.Reliability", "CA2000:Supprimer les objets avant la mise hors de port�e", Justification = "Impl�menter dans la m�thode Use")]
		public static T DeserializeJson<T>(this string value) where T : class
		{
			using (var sr = new StreamReader(value))
			{
				return new JsonSerializer().Deserialize(sr, typeof(T)) as T;
			}
		}
	}

	public class Rates
	{
		private Rates()
		{
		}

		[JsonProperty("base")]
		public string Base { get; set; }
		[JsonProperty("dase")]
		public string Date { get; set; }

		[JsonProperty("rates")]
		public List<Rate> Items { get; set; }


		public static async Task<Rates> LoadAsync()
		{
			using (var client = new HttpClient())
			{ 
				 var response = await client.GetAsync("https://api.fixer.io/latest");
				 return response.Content.ReadAsStringAsync().Result.DeserializeJson<Rates>();
			}				
		}

		public override string ToString()
		{
			return string.Format("[Rates: Base={0}, Date={1}]", Base, Date);
		}
	}

	public class Rate
	{
		public string Name { get; set; }
		public string Value { get; set; }

		public override string ToString()
		{
			return string.Format("[Rate: Name={0}, Value={1}]", Name, Value);
		}	}
}
