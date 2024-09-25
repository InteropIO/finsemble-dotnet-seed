using System.Windows;
using System.Windows.Media;
using ChartIQ.Finsemble;
using ChartIQ.Finsemble.Models;

namespace MultiWindowExample
{
	/// <summary>
	/// Interaction logic for Window2.xaml
	/// </summary>
	public partial class Window2 : Window, IIntegratable
	{
		private Finsemble fsbl;

		public Window2()
		{
			InitializeComponent();
		}

		public void SetFinsemble(Finsemble fsbl)
		{
			this.fsbl = fsbl;

		}
	}
}
