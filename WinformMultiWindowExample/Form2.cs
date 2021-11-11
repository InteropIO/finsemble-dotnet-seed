using ChartIQ.Finsemble;
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
			FSBL.Logger.Log(new JToken[] { "Winform example connected to Finsemble. Form2" });
		}
	}
}
