using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json;
using Windows.ApplicationModel.Resources;
using Windows.Storage;

namespace SimpleTimers.Models
{
	class TimerTracker
	{
		private static TimerTracker _instance = null;

		public static TimerTracker Instance
		{
			get
			{
				if (_instance == null)
					_instance = new TimerTracker { Timers = new ObservableCollection<Timer>() };
				return _instance;
			}
		}

		public ObservableCollection<Timer> Timers { get; set; }
	}
}
