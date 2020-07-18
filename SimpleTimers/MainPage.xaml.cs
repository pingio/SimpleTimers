using SimpleTimers.Models;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SimpleTimers
{


	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		private TimerTracker _tracker;
		public MainPage()
		{
			_tracker = new TimerTracker(true);
			Timers = _tracker.Timers;
			this.InitializeComponent();

		}

		public ObservableCollection<Timer> Timers { get; private set; }
	}
}
