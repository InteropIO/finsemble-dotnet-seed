using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Interop;
using Finsemble.WPF.Core;
using Microsoft.IdentityModel.Tokens;

namespace WPFMultiWindowExampleCore
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml with Finsemble instance
	/// </summary>
	public partial class MainWindow : Window
	{
		private FinsembleWPF FSBL { get; set; }
		private JsonWebKey JWK = new JsonWebKey()
		{
			D = "S7msrBKYM_VhXmAWTLhoRobLTevToYbX3xkbkN-EiaZ6Hg-xfozn5uAQGnBnoP1ldKOgoj5Z3dx6kTgR-3xfonEfdkk6wn0OVNbuFYyGkeeV4ts5JmyVpihFqE3RbkWuQ5D5xpIhXWl1fOWEuFfGCYIib2pmBUyc4Lz4OYmMOIGEC9nJg6ZuoKOh0nDZBjjO6vbYbXCEi0ys-FD7NAWsM8jTNDxLyXmpCNSVJOnGTX9CcxnFGdLVO8fqbooaydSHtFJE9YVqUKWp54hOBFMHdsTY5iT88urrvdBLxtGf6NGUVetpw-nFiOihDRPb9wMuLY9CT4DDzLecxadrLKh0PQ",
			DP = "dw8tnJQEpXazaYFcOhCvlU0Y4kGiul1W-_MTCXml8njCEx0Gp4s8jWf_QK7PcdzZRl-t_NTu12i2UGn8lCvOrSc4g66OkwZxPCVuvGXqQQ2DHTgFR2vk-Q53cFrMtjo8FvplkQuf92vS58ulq-iogDp7xxxXTAhmWaPA_d2i3C0",
			DQ = "EffO_SIA7qpBsSDHDKN3TdgybckYjN738roGKcU23ZXDDRy8h9X_lYtMSvjBQz7CRdia7F7aXGJLS08LSAfajRH-W8ssYfRge3twkrqxDXspWwIb77eMSINUUzRA1QQD7kSs_-LsU-rujsqZa-dnBxWhareFtVM-957lkzp4lTU",
			E = "AQAB",
			N = "zPOxYfLiAd3rM7KOLDBIeLl0kjQ7fk43mTTc1Nm9BDaSNVWqvOshSMCHmqOrKZX_WwA67Y6CQxWI5rZ6WNzoLHQU3PyqOvAdB7RgXCHlSeVYZ2haTcbWRjAXQ89H1WfnW96VwAbZn5nccxQGYlZIl3AMcwNRqV4hmiKtJVq4-2OzA-zs9Yg4Pfs6TbKR1XbNKz6EAPHDev0-7Xdnb6x1-qr4uL2Bq0ZwgfnyKQIRLc3zhD5kwqiJ5N7uZAZomLRkMFMXwy1UftD6fQUlFT2yoISn403RwN7YHEL8KoA9X7Jgs-dtlBh8c38QzQ1vbdMBzPuyRb07GMMKb6bdjdBV1w",
			P = "6RJ2697p-SS6CIJYdDU8AxgyKHx3kMkHnFzLQakoi1h6UUNxb-r8v1PUU5xZA5ijmvJv9Ag5uIOhT3u3vSOkADqGsqctNGtFf60hbjnkMDQnDNr2b-1_ayCn2pQM6mmisASfcnOJKUag4tLw3weK-t5uN9KYqfDBU7fVjqnq8HM",
			Q = "4R0RUlCnmZRzEw9sqjwxMuvNM1BTSubvMvG0VIlBkYbCn9MOdwurBPxrYqnUcbw-q-qzQy6st6a4L-EAZSnfD3FEEFKeINOJK06l0EwjcLeP8B4YQ-bxd9UroXpl9ACiMqHzyvJCNOpw8A22nbjKVnVhW1E17F-LFAJoWBetYA0",
			QI = "PODgpJrXxPAp72v_O0fNfAhWjHLeTk9TfLARl9lzPpYIoYR5tgP1Y_A-3feH_xtCfkzcCskfXIerQlY9lVmqs-eGEYjfuuPVYIruN4OsskMY1nz-h_14clyUmUwfCQJDV4qjcAzf80IMu53jYEW1BydRf90snRjk1dYgSq_qtTQ",
		};

		public MainWindow(string[] args)
		{
			InitializeComponent();

			ConnectToFinsemble(args);

			this.Closed += MainWindow_Closed;
		}

		/// <summary>
		/// Dispose of Finsemble object when window is closed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainWindow_Closed(object sender, EventArgs e)
		{
			FSBL.Dispose();
		}

		/// <summary>
		/// The ConnectToFinsemble.
		/// </summary>
		/// <param name="args">Command line parameters <see cref="string"/>.</param>
		private async void ConnectToFinsemble(string[] args)
		{
			FSBL = new FinsembleWPF(this, new WindowInteropHelper(this).Handle.ToString("X"), args);
			FSBL.Connected += Finsemble_Connected;
			await FSBL.Connect("WPFMultiWindowExampleCore", JWK);
		}

		private void Finsemble_Connected(object sender, EventArgs e)
		{
			Trace.TraceInformation("FSBL connected");

			Application.Current.Dispatcher.Invoke(() =>
			{
				Show();
			});

			Trace.TraceInformation($"The window \"{this.LabelWindowName.Content}\" is connected to Finsemble.");
		}
	}
}
