using System;

namespace SimpleTimers.Models
{
	public class Timer
	{

		/// <summary>
		/// Creates a timespan with the length of the timer.
		/// </summary>
		public TimeSpan TimerLength { get; set; } = new TimeSpan(0, 10, 0);

		/// <summary>
		/// Name of the timer.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// A unique ID for this specific timer, cannot be changed.
		/// </summary>
		public Guid Guid { get; } = Guid.NewGuid();
	}
}
