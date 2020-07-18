using System.Collections.ObjectModel;
using System.Text.Json;
using Windows.ApplicationModel.Resources;
using Windows.Storage;

namespace SimpleTimers.Models
{
	class TimerTracker
	{
		public TimerTracker(bool test = false)
		{

			if (!test)
			{
				var roamingTimers = ResourceLoader.GetForCurrentView().GetString("RoamingTimersFileName");

				try
				{
					var fh = new FileHandler();

					var timersFile = fh.GetRoamingFile(roamingTimers);
					var jsonContents = FileIO.ReadTextAsync(timersFile).GetResults();
					var timersList = JsonSerializer.Deserialize<ObservableCollection<Timer>>(jsonContents);


					Timers = timersList;

				}
				catch
				{
					Timers = new ObservableCollection<Timer>();
				}
			}
			else
			{
				Timers = new ObservableCollection<Timer> { new Timer { Name = "1" }, new Timer { Name = "2" } };
			}

		}

		public ObservableCollection<Timer> Timers { get; private set; }
	}
}
