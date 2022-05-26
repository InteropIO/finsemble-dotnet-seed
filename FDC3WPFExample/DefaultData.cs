using ChartIQ.Finsemble.FDC3.Types;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace FDC3WPFExample
{
	public static class DefaultData
	{
		public static Dictionary<string, Context> DefaultContexts = new Dictionary<string, Context>()
		{
			{"fdc3.contact", new Context(new JObject
				{
					["type"] = "fdc3.contact",
					["name"] = "Jane Doe",
					["id"] = new JObject
					{
						["email"] = "jane.doe@mail.com"
					}
				}
			)},
			{"fdc3.contactList", new Context(new JObject
				{
					["type"] = "fdc3.contactList",
					["contacts"] = new JArray
					{
						new JObject
						{
							["type"] = "fdc3.contact",
							["name"] = "Jane Doe",
							["id"] = new JObject
							{
								["email"] = "jane.doe@mail.com"
							}
						},
						new JObject
						{
							["type"] = "fdc3.contact",
							["name"] = "John Doe",
							["id"] = new JObject
							{
								["email"] = "john.doe@mail.com"
							}
						}
					}
				}
			)},
			{"fdc3.instrument", new Context(new JObject
				{
					["type"] = "fdc3.instrument",
					["name"] = "Microsoft",
					["id"] = new JObject
					{
						["ticker"] = "MSFT",
						["RIC"] = "MSFT.OQ",
						["ISIN"] = "US5949181045"
					}
				}
			)},
			{"fdc3.instrumentList", new Context(new JObject
				{
					["type"] = "fdc3.instrumentList",
					["instruments"] = new JArray
					{
						new JObject
						{
							["type"] = "fdc3.instrument",
							["id"] = new JObject
							{
								["ticker"] = "AAPL"
							}
						},
						new JObject
						{
							["type"] = "fdc3.instrument",
							["id"] = new JObject
							{
								["ticker"] = "MSFT"
							}
						}
					}
				}
			)},
			{"fdc3.organization", new Context(new JObject
				{
					["type"] = "fdc3.organization",
					["name"] = "Cargill, Incorporated",
					["id"] = new JObject
					{
						["LEI"] = "QXZYQNMR4JZ5RIRN4T31",
						["FDS_ID"] = "00161G-E"
					}
				}
			)},
			{"fdc3.country", new Context(new JObject
				{
					["type"] = "fdc3.country",
					["name"] = "Sweden",
					["id"] = new JObject
					{
						["ISOALPHA3"] = "SWE"
					}
				}
			)},
			{"fdc3.position", new Context(new JObject
				{
					["type"] = "fdc3.position",
					["instrument"] = new JObject
					{
						["type"] = "fdc3.instrument",
						["id"] = new JObject
						{
							["ticker"] = "AAPL"
						}
					},
					["holding"] = 2000000
				}
			)},
			{"fdc3.portfolio", new Context(new JObject
				{
					["type"] = "fdc3.portfolio",
					["positions"] = new JArray
					{
						new JObject
						{
							["type"] = "fdc3.position",
							["instrument"] = new JObject
							{
								["type"] = "fdc3.instrument",
								["id"] = new JObject
								{
									["ticker"] = "AAPL"
								}
							},
							["holding"] = 2000000
						},
						new JObject
						{
							["type"] = "fdc3.position",
							["instrument"] = new JObject
							{
								["type"] = "fdc3.instrument",
								["id"] = new JObject
								{
									["ticker"] = "MSFT"
								}
							},
							["holding"] = 1500000
						},
						new JObject
						{
							["type"] = "fdc3.position",
							["instrument"] = new JObject
							{
								["type"] = "fdc3.instrument",
								["id"] = new JObject
								{
									["ticker"] = "IBM"
								}
							},
							["holding"] = 3000000
						}
					}
				}
			)},
		};
	}
}
