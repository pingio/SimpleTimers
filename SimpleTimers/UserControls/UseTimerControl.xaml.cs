using SimpleTimers.Models;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace SimpleTimers.UserControls
{
	public sealed partial class UseTimerControl : UserControl
	{
		public Timer Timer { get; private set; }

		private TimeSpan _timerLength;
		private DispatcherTimer _dispTimer;

		


		public UseTimerControl()
		{
			_dispTimer = new DispatcherTimer();
			_dispTimer.Tick += TimerTick;
			_dispTimer.Interval = new TimeSpan(0, 0, 1);

			

			

			this.InitializeComponent();

			this.DataContextChanged += (s, e) => Bindings.Update();

			this.Loaded += ControlLoaded;
		}

		/// <summary>
		/// Loads the alert sound that will sound when the timer hits 0.
		/// </summary>
		private async Task LoadSoundAsync()
		{
			var _player = new MediaElement();
			var folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
			var file = await folder.GetFileAsync("timer.mp3");
			var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
			
			_player.SetSource(stream, "audio/mpeg");
			_player.Play();
		}

		/// <summary>
		/// This method is called when the model is loaded. This is allows us to access the underlying datacontext timer.
		/// </summary>
		private void ControlLoaded(object sender, RoutedEventArgs e)
		{
			Timer = this.DataContext as Timer;
			_timerLength = Timer.TimerLength;
			CountdownTimer.Text = _timerLength.ToString(@"hh\:mm\:ss");
		}

		/// <summary>
		/// This method is called every time a tick happens for the <see cref="DispatcherTimer"/>.
		/// Once it hits 0, it'll stop the timer and alert the user.
		/// </summary>
		private void TimerTick(object sender, object e)
		{

			_timerLength = _timerLength.Subtract(TimeSpan.FromSeconds(1));

			if (_timerLength.TotalSeconds <= 0)
			{
				_dispTimer.Stop();
				LoadSoundAsync().GetAwaiter();
				

			}
			CountdownTimer.Text = _timerLength.ToString(@"hh\:mm\:ss");

		}

		/// <summary>
		/// This button is clicked whenever the Reset button is clicked.
		/// It will stop the timer and reset the countdown.
		/// </summary>
		private void Reset_Click(object sender, RoutedEventArgs e)
		{
			_dispTimer.Stop();
			_timerLength = Timer.TimerLength;
			CountdownTimer.Text = _timerLength.ToString(@"hh\:mm\:ss");

		}

		/// <summary>
		/// this button starts the timer.
		/// </summary>
		private void StartStop_Click(object sender, RoutedEventArgs e)
		{

			if (!_dispTimer.IsEnabled)
			{
				if (_timerLength.TotalSeconds == 0)
					return;

				_dispTimer.Start();
				StartPause.Content = new SymbolIcon(Symbol.Pause);
			}
			else
			{
				_dispTimer.Stop();
				StartPause.Content = new SymbolIcon(Symbol.Play);
			}

		}

		/// <summary>
		/// Opens a dialog option to change the timer length and name.
		/// <seealso cref="UpdateTimerControl"/>
		/// </summary>
		private void Edit_Click(object sender, RoutedEventArgs e)
		{
			ContentDialog dialog = new ContentDialog()
			{
				Content = new UpdateTimerControl(),
				CloseButtonText = "Close",

			};
			dialog.DataContext = Timer;
			dialog.Closing += CloseEditCommand;

			dialog.ShowAsync().GetResults();



		}

		/// <summary>
		/// On closing of the dialog, this method gets the datacontext of the dialog <see cref="Timer"/>
		/// and updates this usercontrol with that timer.
		/// </summary>
		private void CloseEditCommand(ContentDialog sender, ContentDialogClosingEventArgs args)
		{
			Timer = sender.DataContext as Timer;
			_timerLength = Timer.TimerLength;
			CountdownTimer.Text = _timerLength.ToString(@"hh\:mm\:ss");
			Name.Text = Timer.Name;
		}
	}
}
