namespace CurrencyConverter.UnitTesting
{
	using System.Diagnostics;
	using Library.Core;
	using Library;
	using NUnit.Framework;

	[TestFixture()]
	public class Test
	{
		[Test()]
		public void TestLoadRates()
		{
			var items = RatesFactory.LoadAsync().Result;
			Assert.IsNotNull(items);

			Trace.WriteLine(items.ToString());
		}


		[Test()]
		public void TestJson()
		{
			const string Value = "{ \"base\":\"EUR\",\"date\":\"2017-11-20\",\"rates\":{ \"AUD\":1.5592,\"BGN\":1.9558,\"BRL\":3.8388} }";
			var result = StringExtensions.DeserializeJson<Rates>(Value);

			Trace.WriteLine(result.ToString());
		}
	}
}