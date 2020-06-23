using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace SimpleTimers.UserControls
{
	public sealed partial class UpdateTimerControl : UserControl
	{
		private List<int> _seconds;
		private List<int> _minutes;
		private List<int> _hours;

		DispatcherTimer Timer = new DispatcherTimer();

		public UpdateTimerControl()
		{
			// This is added because the default time picker does not support seconds.
			_seconds = Enumerable.Range(0, 60).ToList();
			_minutes = Enumerable.Range(0, 60).ToList();
			_hours = Enumerable.Range(0, 24).ToList();

			Timer.Tick += UpdateTimerTick;
			Timer.Interval = new TimeSpan(0, 0, 1);

			this.InitializeComponent();
		}

		public List<int> Seconds => _seconds;

		public List<int> Minutes => _minutes;

		public List<int> Hours => _hours;





		private void UpdateTimerTick(Object sender, object e)
		{
			CountdownTimer.Text = DateTime.Now.ToString("h:mm:ss tt");
		}

		private void UpdateTimerButton_Click(object sender, RoutedEventArgs e)
		{
			CountdownTimer.Text = $"{SelectedHour.SelectedItem:D2}:{SelectedMinute.SelectedItem:D2}:{SelectedSecond.SelectedItem:D2}";
		}
	}
}
