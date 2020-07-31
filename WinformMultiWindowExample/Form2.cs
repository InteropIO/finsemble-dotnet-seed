using ChartIQ.Finsemble;
using MultiWiformExample;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Threading;

namespace WinformMultiWindowExample
{
	public partial class Form2 : Form, IIntegratable
	{
		public Form2()
		{
			InitializeComponent();
		}

		public void SetFinsemble(Finsemble fsbl)
		{
			FSBL = fsbl;
			FSBL.RPC("Logger.log", new List<JToken> { "Winform example connected to Finsemble. Form2" });
		}
	}
}
