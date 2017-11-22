
namespace LibraryNET
{
    public class Rate
	{
		public string Name { get; set; }
		public string Value { get; set; }


		public override string ToString()
		{
			return string.Format("[Rate: Name={0}, Value={1}]", Name, Value);
		}
	}
}
