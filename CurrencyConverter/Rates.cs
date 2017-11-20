
namespace CurrencyConverter
{
	using System;
	using System.Collections.Generic;
	using System.Net.Http;
	using System.Threading.Tasks;
	using CurrencyConverter.Core;
	using Newtonsoft.Json;

	public class Rates
	{
		public Rates()
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
			return await new HttpClient().Use(async client =>
			 {
				 var response = await client.GetAsync("https://api.fixer.io/latest");
				 return response.Content.ReadAsStringAsync().Result.DeserializeJson<Rates>();
			 });

		}
	}

	public class Rate
	{
		public string Name { get; set; }
		public string Value { get; set; }
	}
}
