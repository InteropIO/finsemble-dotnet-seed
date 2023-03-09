using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChartIQ.Finsemble.FDC3.Interfaces;

namespace FDC3WPFExample.Types
{
	public class PortfolioContext : IContext
	{
		public string Type => "fdc3.portfolio";

		public PositionContext[] Positions { get; set; }
	}
}
