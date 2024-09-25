using InteropIO.FDC3.Interfaces;

namespace FDC3WPFExample.Types
{
	public class InstrumentListContext : IContext
	{
		public string Type => "fdc3.instrumentList";

		public InstrumentContext[] Instruments { get; set; }

		public object Id { get; set; }

		public string Name { get; set; }
	}
}
