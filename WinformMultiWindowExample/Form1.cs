using ChartIQ.Finsemble;
using MultiWiformExample;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Threading;

namespace WinformMultiWindowExample
{
    public partial class Form1 : Form, IIntegratable
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void SetFinsemble(Finsemble fsbl)
        {
            FSBL = fsbl;
            FSBL.RPC("Logger.log", new List<JToken> { "Winform example connected to Finsemble. Form1" });
        }
    }
}

