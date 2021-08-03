using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf
{
	public class StaticHtmlPage
	{
		public static string Html { get
			{
				return @"
					<html>
						<body>
							<h1>FDC3 RaiseIntent Example</h1>
							<div>Add the symbol for a stock you'd like to see. This demo will raise and intent for that symbol using the FDC3 standard.</div>
							<div style=""padding-top:10px;padding-bottom:10px;"">
								<span>
									<label>
										Symbol
										<input value=""AAPL"" id=""SymbolInput""> 
									</label>
								</span>
								<span>
									<button id = ""ShowChartButton""> Show chart </button>
								 </span>
							</div>
							<div id=""noDesktopAgentError"" style=""display:none;"">Error: There is no FDC3 desktop agent providing the FDC3 API. Make sure to start Finsemble v6.1+.</div>
							<script>
								document.getElementById(""ShowChartButton"").addEventListener(""click"", () => {
									if(!window.fdc3){
										document.getElementById(""noDesktopAgentError"").style.display = ""block"";
										return;
									}
									fdc3.raiseIntent(""ViewChart"", {type:""fdc3.instrument"", name:""AAPL"", id: {""ticker"":document.getElementById(""SymbolInput"").value}});
								});
							</script>
						</body>
					</html>
				 ";
			} 
		}
	}
}
