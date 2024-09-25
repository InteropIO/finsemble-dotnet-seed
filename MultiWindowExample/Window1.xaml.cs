using ChartIQ.Finsemble;
using System.Windows;
using System.Windows.Media;
using ChartIQ.Finsemble.Models;

namespace MultiWindowExample
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window, IIntegratable
	{
		private Finsemble fsbl;

		public Window1()
		{
			InitializeComponent();
		}

		public void SetFinsemble(Finsemble fsbl)
		{
			this.fsbl = fsbl;
		}
	}
}
