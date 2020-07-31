using System.Windows;
using ChartIQ.Finsemble;

namespace MultiWindowExample
{
	/// <summary>
	/// Interface for windows that can be integrated with Finsemble.
	/// </summary>
	interface IIntegratable
    {
		/// <summary>
		/// Sets the instance of Finsemble to be used by this object.
		/// </summary>
		/// <param name="fsbl">The instance of Finsemble</param>
		void SetFinsemble(Finsemble fsbl);
    }
}
