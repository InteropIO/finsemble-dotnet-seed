using ChartIQ.Finsemble.FDC3.Interfaces;

namespace FDC3WPFExample.Types
{
	public class ContactContext : IContext
	{
		public string Type => "fdc3.contact";

		public string Name { get; set; }

		public ContactContextId Id { get; set; }
	}

	public class ContactContextId
	{
		public string Email { get; set; }
	}
}
