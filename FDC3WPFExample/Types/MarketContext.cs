using ChartIQ.Finsemble.FDC3.Interfaces;

namespace FDC3WPFExample.Types
{
	public class MarketContext : IContext
	{
		public string Type { get; set; }

		public MarketContextId Id { get; set; }
	}

	public class MarketContextId
	{
		public string Ticker { get; set; }
	}
}
