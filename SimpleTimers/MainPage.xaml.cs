using SimpleTimers.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SimpleTimers
{


	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{

		private bool switched = false;

		public MainPage()
		{
			
			this.InitializeComponent();

			this.Loaded += LoadTimersAsync;

			if (ApplicationView.GetForCurrentView().IsViewModeSupported(ApplicationViewMode.CompactOverlay))
			{
				AlwaysOnTop.Visibility = Visibility.Visible;
			}

		}


		private async void LoadTimersAsync(object sender, RoutedEventArgs e)
		{
			//Timers = TimerTracker.Instance.Timers;
			var fh = new FileHandler();
			await fh.LoadTimersAsync();

		
		}

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{
			TimerTracker.Instance.Timers.Add(new Timer());
		}

		private async void AlwaysOnTop_Click(object sender, RoutedEventArgs e)
		{
			bool modeSwitched = false;
			if (!switched)
			{
				var preferences = ViewModePreferences.CreateDefault(ApplicationViewMode.CompactOverlay);
				preferences.CustomSize = new Windows.Foundation.Size(750, 400);
				modeSwitched = await ApplicationView.GetForCurrentView().TryEnterViewModeAsync(ApplicationViewMode.CompactOverlay, preferences);
			}
			else
				modeSwitched = await ApplicationView.GetForCurrentView().TryEnterViewModeAsync(ApplicationViewMode.Default);

			if (modeSwitched)
				switched = !switched;

		}
	}
}
