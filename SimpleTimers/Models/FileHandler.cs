using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace SimpleTimers.Models
{
	class FileHandler
	{


		public async Task<StorageFile> GetRoamingFileAsync(string name)
		{
			var roamingFolder = Windows.Storage.ApplicationData.Current.RoamingFolder;
			StorageFile openFile;
			try
			{
				openFile = await roamingFolder.CreateFileAsync(name, CreationCollisionOption.OpenIfExists);
			}
			catch (Exception)
			{
				throw;
			}


			return openFile;

		}


		public StorageFile GetRoamingFile(string name)
		{
			var roamingFolder = Windows.Storage.ApplicationData.Current.RoamingFolder;
			StorageFile openFile;
			try
			{
				openFile = roamingFolder.CreateFileAsync(name, CreationCollisionOption.OpenIfExists).GetResults();
			}
			catch (Exception)
			{
				throw;
			}


			return openFile;

		}


	}
}
