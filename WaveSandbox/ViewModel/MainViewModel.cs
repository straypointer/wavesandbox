using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using WaveSandbox.Extensions;

namespace WaveSandbox.ViewModel
{
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
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

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

		public void SelectFile1(object o) {
			UIServices.ShowMessageBox("Coming Soon!", "Foo");
		}

		public void SelectFile2(object o) {

		}

		public void CombineFiles(object o) {

		}

    }
}