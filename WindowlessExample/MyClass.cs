using System;
using System.Collections.Generic;
using ChartIQ.Finsemble;
using Newtonsoft.Json.Linq;

namespace WindowlessExample
{
	internal class MyClass : IDisposable
	{
		private readonly Finsemble FSBL;
		public MyClass(string[] args)
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

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					FSBL.Dispose();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~MyClass() {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}
		#endregion
	}
}