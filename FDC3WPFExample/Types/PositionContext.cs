using ChartIQ.Finsemble.FDC3.Interfaces;

namespace FDC3WPFExample.Types
{
	public class PositionContext : IContext
	{
		public string Type => "fdc3.position";

		public InstrumentContext Instrument { get; set; }

		public int Holding { get; set; }
	}
}
