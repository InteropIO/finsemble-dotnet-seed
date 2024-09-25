using System.Windows;
using System.Windows.Media;
using ChartIQ.Finsemble;
using ChartIQ.Finsemble.Models;

namespace MultiWindowExample
{
	/// <summary>
	/// Interaction logic for Window3.xaml
	/// </summary>
	public partial class Window3 : Window, IIntegratable
	{
		private Finsemble fsbl;

		public Window3()
		{
			InitializeComponent();
		}

		public void SetFinsemble(Finsemble fsbl)
		{
			this.fsbl = fsbl;
		}
	}
}
