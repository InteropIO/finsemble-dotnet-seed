using System;
using ChartIQ.Finsemble.FDC3.Interfaces;

namespace FDC3WPFExample.Types
{
	public class ContactListContext : IContext
	{
		public string Type => "fdc3.contactList";

		public ContactContext[] Contacts { get; set; }
	}
}
