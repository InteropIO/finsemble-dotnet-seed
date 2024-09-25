using InteropIO.FDC3.Interfaces;

namespace FDC3WPFExample.Types
{
	public class ContactListContext : IContext
	{
		public string Type => "fdc3.contactList";

		public ContactContext[] Contacts { get; set; }

		public object Id { get; set; }

		public string Name { get; set; }
	}
}
