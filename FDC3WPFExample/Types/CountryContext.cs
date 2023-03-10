using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChartIQ.Finsemble.FDC3.Interfaces;

namespace FDC3WPFExample.Types
{
	public class CountryContext : IContext
	{
		public string Type => "fdc3.country";

		public string Name { get; set; }

		public CountryContextId Id { get; set; }
	}

	public class CountryContextId
	{
		public string ISOALPHA3 { get; set; }
	}
}
