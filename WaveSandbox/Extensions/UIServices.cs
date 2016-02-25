using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Win32;

namespace WaveSandbox.Extensions {
	
	public class UIServices {

		/// <summary>
		///   A value indicating whether the UI is currently busy
		/// </summary>
		private static bool IsBusy;

		/// <summary>
		/// Sets the busystate as busy.
		/// </summary>
		public static void SetBusyState() {
			SetBusyState(true);
		}

		/// <summary>
		/// Sets the busystate to busy or not busy.
		/// </summary>
		/// <param name="busy">if set to <c>true</c> the application is now busy.</param>
		private static void SetBusyState(bool busy) {
			if (busy != IsBusy) {
				IsBusy = busy;
				Mouse.OverrideCursor = busy ? Cursors.Wait : null;

				if (IsBusy) {
					new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.ApplicationIdle, dispatcherTimer_Tick, Application.Current.Dispatcher);
				}
			}
		}

		/// <summary>
		/// Handles the Tick event of the dispatcherTimer control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private static void dispatcherTimer_Tick(object sender, EventArgs e) {
			var dispatcherTimer = sender as DispatcherTimer;
			if (dispatcherTimer != null) {
				SetBusyState(false);
				dispatcherTimer.Stop();
			}
		}

		private const string STOCK_TITLE = "Message";

		public static void ShowMessageBox(string msg, string title = STOCK_TITLE) {
			MessageBox.Show(msg, title);
		}

		public static bool ShowMessageBoxYesNo(string msg, string title = STOCK_TITLE) {
			var result = MessageBox.Show(msg, title, MessageBoxButton.YesNo);
			return (result == MessageBoxResult.Yes);
		}

		public static string SelectFileWithWin32OpenDialog(string filter) {
			var openFileDialog = new OpenFileDialog();
			if (!string.IsNullOrWhiteSpace(filter)) {
				openFileDialog.Filter = filter;
			}
			if (openFileDialog.ShowDialog() == true) {
				return openFileDialog.FileName;
			}
			return string.Empty;
		}

		
	}
}
