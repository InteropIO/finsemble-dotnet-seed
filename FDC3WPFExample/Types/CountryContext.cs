using InteropIO.FDC3.Interfaces;

namespace FDC3WPFExample.Types
{
	public class CountryContext : IContext
	{
		public string Type => "fdc3.country";

		public string Name { get; set; }

		public CountryContextId Id { get; set; }

		object IContext<object>.Id => Id;
	}

	public class CountryContextId
	{
		public string ISOALPHA3 { get; set; }
	}
}
