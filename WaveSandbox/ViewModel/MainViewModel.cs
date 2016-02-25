using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using WaveSandbox.Extensions;
using WaveSandbox.Model;

namespace WaveSandbox.ViewModel {
	/// <summary>
	/// This class contains properties that the main View can data bind to.
	/// <para>
	/// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
	/// </para>
	/// <para>
	/// You can also use Blend to data bind with the tool's support.
	/// </para>
	/// <para>
	/// See http://www.galasoft.ch/mvvm
	/// </para>
	/// </summary>
	public class MainViewModel : ViewModelBase {
		/// <summary>
		/// Initializes a new instance of the MainViewModel class.
		/// </summary>

		WaveBox _sandbox = null;

		public MainViewModel() {
			////if (IsInDesignMode)
			////{
			////    // Code runs in Blend --> create design time data.
			////}
			////else
			////{
			////    // Code runs "for real"
			////}

			_sandbox = new WaveBox();

			this.SelectFile1Command = new RelayCommand<object>(p => SelectFile1(p));
			this.SelectFile2Command = new RelayCommand<object>(p => SelectFile2(p));
			this.CombineFileCommand = new RelayCommand<object>(p => CombineFiles(p));
		}

		public ICommand SelectFile1Command {
			get;
			private set;
		}

		public ICommand SelectFile2Command {
			get;
			private set;
		}

		public ICommand CombineFileCommand {
			get;
			private set;
		}

		public bool IsCombineAvailable {
			get { return !string.IsNullOrWhiteSpace(_sandbox.File1Path) && !string.IsNullOrWhiteSpace(_sandbox.File2Path); }
		}

		private const string WAVE_FILTER = "Wave files (*.wav)|*.wav";
		private void PostViewModelUpdates() {
			this.RaisePropertyChanged("IsCombineAvailable");
		}

		public void SelectFile1(object o) {
			_sandbox.File1Path = UIServices.SelectFileWithWin32OpenDialog(WAVE_FILTER);
			this.PostViewModelUpdates();
		}

		public void SelectFile2(object o) {
			_sandbox.File2Path = UIServices.SelectFileWithWin32OpenDialog(WAVE_FILTER);
			this.PostViewModelUpdates();
		}

		public void CombineFiles(object o) {
			var result = _sandbox.CombineFiles();
			if (result) {
				UIServices.ShowMessageBox("success!");
			} else {
				UIServices.ShowMessageBox("fail!");
			}
		}

	}
}