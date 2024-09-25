using System.Windows;
using ChartIQ.Finsemble;

namespace MultiWindowExample
{
	/// <summary>
	/// Interaction logic for Window4.xaml
	/// </summary>
	public partial class Window4 : Window, IIntegratable
	{
		private Finsemble fsbl;

		public Window4()
		{
			InitializeComponent();
		}

		public void SetFinsemble(Finsemble fsbl)
		{
			this.fsbl = fsbl;
		}
	}
}
