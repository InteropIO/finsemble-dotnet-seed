using ChartIQ.Finsemble.FDC3.Interfaces;
using ChartIQ.Finsemble.FDC3.Types;
using FDC3WPFExample.Types;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace FDC3WPFExample
{
	public static class DefaultData
	{
		public static Dictionary<string, IContext> DefaultContexts = new Dictionary<string, IContext>()
		{
			{
				"fdc3.contact", new ContactContext()
				{
					Name = "Jane Doe",
					Id = new ContactContextId() { Email = "jane.doe@mail.com" }
				}
			},
			{
				"fdc3.contactList", new ContactListContext()
				{
					Contacts = new ContactContext[]
					{
						new ContactContext()
						{
							Name = "Jane Doe",
							Id = new ContactContextId() { Email = "jane.doe@mail.com" }
						},
						new ContactContext()
						{
							Name = "John Doe",
							Id = new ContactContextId() { Email = "john.doe@mail.com" }
						},
					}
				}
			},
			{
				"fdc3.instrument", new InstrumentContext()
				{
					Name = "Microsoft",
					Id = new InstrumentContextId()
					{
						Ticker = "MSFT",
						RIC = "MSFT.OQ",
						ISIN = "US5949181045"
					}
				}
			},
			{
				"fdc3.instrumentList", new InstrumentListContext()
				{
					Instruments = new InstrumentContext[]
					{
						new InstrumentContext()
						{
							Id = new InstrumentContextId() { Ticker = "AAPL" }
						},
						new InstrumentContext()
						{
							Id = new InstrumentContextId() { Ticker = "MSFT" }
						},
					}
				}
			},
			{
				"fdc3.organization", new OrganizationContext()
				{
					Name = "Cargill, Incorporated",
					Id = new OrganizationContextId()
					{
						LEI = "QXZYQNMR4JZ5RIRN4T31",
						FDS_ID = "00161G-E"
					}
				}
			},
			{
				"fdc3.country", new CountryContext()
				{
					Name = "Sweden",
					Id = new CountryContextId()
					{
						ISOALPHA3 = "SWE"
					}
				}
			},
			{
				"fdc3.position", new PositionContext()
				{
					Instrument = new InstrumentContext()
					{
						Id = new InstrumentContextId() { Ticker = "AAPL" }
					},
					Holding = 2000000
				}
			},
			{
				"fdc3.portfolio", new PortfolioContext()
				{
					Positions = new[]
					{
						new PositionContext()
						{
							Holding = 2000000,
							Instrument = new InstrumentContext()
							{
								Id = new InstrumentContextId() { Ticker = "AAPL" }
							}
						},
						new PositionContext()
						{
							Holding = 1500000,
							Instrument = new InstrumentContext()
							{
								Id = new InstrumentContextId() { Ticker = "MSFT" }
							}
						},
						new PositionContext()
						{
							Holding = 3000000,
							Instrument = new InstrumentContext()
							{
								Id = new InstrumentContextId() { Ticker = "IBM" }
							}
						}
					}
				}
			}
		};
	}
}
