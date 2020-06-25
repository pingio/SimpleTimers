using SimpleTimers.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

			this.DataContextChanged += (s,e) => Bindings.Update();

			this.Loaded += ControlLoaded;



			
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

			if(_timerLength.TotalSeconds <= 0)
			{
				_dispTimer.Stop();

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
		/// This button is clicked whenever the Stop button is clicked in the UI.
		/// It pauses the timer.
		/// </summary>
		/// TODO: Merge this functionaltiy with the start button.
		private void Stop_Click(object sender, RoutedEventArgs e)
		{

			_dispTimer.Stop();
		}
		/// <summary>
		/// this button starts the timer.
		/// </summary>
		private void Start_Click(object sender, RoutedEventArgs e)
		{
			if (!_dispTimer.IsEnabled)
				_dispTimer.Start();

		}
	}
}
