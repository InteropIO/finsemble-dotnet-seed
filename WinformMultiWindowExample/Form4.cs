using ChartIQ.Finsemble;
using MultiWiformExample;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformMultiWindowExample
{
	public partial class Form4 : Form, IIntegratable
	{
		public Form4()
		{
			InitializeComponent();
		}

		public void SetFinsemble(Finsemble fsbl)
		{
			FSBL = fsbl;
			FSBL.RPC("Logger.log", new List<JToken> { "Winform example connected to Finsemble. Form4" });
		}
	}
}
