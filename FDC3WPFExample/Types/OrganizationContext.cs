using System;
using ChartIQ.Finsemble.FDC3.Interfaces;

namespace FDC3WPFExample.Types
{
	public class OrganizationContext : IContext
	{
		public string Type => "fdc3.organization";

		public string Name { get; set; }

		public OrganizationContextId Id { get; set; }
	}

	public class OrganizationContextId
	{
		public string LEI { get; set; }
		public string FDS_ID { get; set; }
	}
}
