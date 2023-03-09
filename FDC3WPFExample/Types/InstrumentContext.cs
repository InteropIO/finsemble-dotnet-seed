using ChartIQ.Finsemble.FDC3.Interfaces;

namespace FDC3WPFExample.Types
{
	public class InstrumentContext : IContext
	{
		public string Type => "fdc3.instrument";

		public string Name { get; set; }

		public InstrumentContextId Id { get; set; }
	}

	public class InstrumentContextId
	{
		public string Ticker { get; set; }
		public string RIC { get; set; }
		public string ISIN { get; set; }
	}
}
