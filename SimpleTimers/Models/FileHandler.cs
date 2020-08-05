using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.Data.Json;
using Windows.Storage;

namespace SimpleTimers.Models
{
	class FileHandler
	{
		


		/// <summary>
		/// Gets a roaming file from the appdata/roaming/simpletimers directory asynchronously.
		/// </summary>
		/// <param name="name">The name of the file to get.</param>
		/// <param name="collisionOption">What to do if the file already exists. Defaulted to open the file.</param>
		/// <returns>returns a <see cref="StorageFile"/> of the current file.</returns>
		public async Task<StorageFile> GetRoamingFileAsync(string name, CreationCollisionOption collisionOption = CreationCollisionOption.OpenIfExists)
		{
			var roamingFolder = Windows.Storage.ApplicationData.Current.RoamingFolder;
			StorageFile openFile;
			try
			{
				openFile = await roamingFolder.CreateFileAsync(name, collisionOption);
			}
			catch (Exception)
			{
				throw;
			}


			return openFile;

		}

		/// <summary>
		/// Loads the timers asynchronously. 
		/// </summary>
		/// <returns>The <see cref="TimerTracker"/> instance with the current timers loaded.</returns>
		public async Task LoadTimersAsync()
		{
			var roamingTimers = ResourceLoader.GetForCurrentView().GetString("RoamingTimersFileName");
			var jsonTimers = await GetRoamingFileAsync(roamingTimers);


			string jsonText = await FileIO.ReadTextAsync(jsonTimers);

			if (jsonText.Length > 0)
			{
				var timers = JsonSerializer.Deserialize<ObservableCollection<Timer>>(jsonText);
				foreach(var timer in timers)
				{
					TimerTracker.Instance.Timers.Add(timer);
				}
			}
		}
		/// <summary>
		/// Saves the timers to the file.
		/// </summary>
		/// <param name="tracker">The <see cref="TimerTracker"/> instance to save the timers from.</param>
		public async Task SaveTimersAsync(TimerTracker tracker)
		{
			var roamingTimers = ResourceLoader.GetForCurrentView().GetString("RoamingTimersFileName");
			var jsonTimers = await GetRoamingFileAsync(roamingTimers);

			string jsonConvert = JsonSerializer.Serialize<ObservableCollection<Timer>>(tracker.Timers);


			 await FileIO.WriteTextAsync(jsonTimers, jsonConvert);
		}

	}
}
