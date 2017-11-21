using System;
namespace CurrencyConverter.Library
{
	using System;
	using System.Collections.Generic;
	using System.Net.Http;
	using System.Threading.Tasks;
	using Newtonsoft.Json;
	using CurrencyConverter.Library.Core;


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
