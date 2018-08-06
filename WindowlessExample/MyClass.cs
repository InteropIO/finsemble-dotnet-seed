using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChartIQ.Finsemble;
using Newtonsoft.Json.Linq;

namespace WindowlessExample
{
    internal class MyClass
    {
        private Finsemble FSBL;
        public MyClass (string[] args)
        {
            FSBL = new Finsemble(args, null);
            FSBL.Connect();
            FSBL.Connected += FSBL_Connected;
        }

        private void FSBL_Connected(object sender, EventArgs e)
        {
            FSBL.RPC("Logger.log", new List<JToken> {
                "Log Test"
            });

            
        }
    }
}
