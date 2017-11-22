
namespace LibraryNET
{    
    using System.Collections.Generic;
    using Newtonsoft.Json;    
   

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
