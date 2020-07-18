using SimpleTimers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;

namespace SimpleTimers.UserControls
{
	public sealed partial class UpdateTimerControl : UserControl
	{
		private List<int> _seconds;
		private List<int> _minutes;
		private List<int> _hours;

		public Timer Timer => this.DataContext as Timer;

		public UpdateTimerControl()
		{

			// This is added because the default time picker does not support seconds.
			_seconds = Enumerable.Range(0, 60).ToList();
			_minutes = Enumerable.Range(0, 60).ToList();
			_hours = Enumerable.Range(0, 24).ToList();


			this.InitializeComponent();
		}

		public List<int> Seconds => _seconds;

		public List<int> Minutes => _minutes;

		public List<int> Hours => _hours;

		/// <summary>
		/// Whenever an hour, minute, or second is updated, update the timerlength and time.
		/// </summary>
		private void SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// This is called as a ContentDialog and is called on load, there will be no selected item, ensures that it does not crash.
			if (Timer == null || SelectedHour.SelectedItem == null || SelectedMinute.SelectedItem == null || SelectedSecond.SelectedItem == null)
				return;

			Timer.TimerLength = new TimeSpan((int)SelectedHour.SelectedItem, (int)SelectedMinute.SelectedItem, (int)SelectedSecond.SelectedItem);
			CountdownTimer.Text = $"{SelectedHour.SelectedItem:D2}:{SelectedMinute.SelectedItem:D2}:{SelectedSecond.SelectedItem:D2}";
		}

		/// <summary>
		/// Called whenever the textbox updates, saves the new timer name.
		/// </summary>
		private void NameChanging(TextBox sender, TextBoxTextChangingEventArgs args)
		{
			Timer.Name = sender.Text;
		}
	}
}
