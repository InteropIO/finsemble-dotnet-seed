using InteropIO.FDC3.Interfaces;

namespace FDC3WPFExample.Types
{
	public class MarketContext : IContext
	{
		public string Type { get; set; }

		public MarketContextId Id { get; set; }

		public string Name { get; set; }

		object IContext<object>.Id => Id;
	}

	public class MarketContextId
	{
		public string Ticker { get; set; }
	}
}
