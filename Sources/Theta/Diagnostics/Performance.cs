// Theta
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.md" in th root project directory.
// SUPPORT: See "SUPPORT.md" in the root project directory.

namespace Theta.Diagnostics
{
	public static class Performance
	{
		public static System.TimeSpan Time_DateTimNow(System.Action action)
		{
			System.DateTime a = System.DateTime.Now;
			action();
			System.DateTime b = System.DateTime.Now;
			return b - a;
		}

		public static System.TimeSpan Time_StopWatch(System.Action action)
		{
			System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
			watch.Restart();
			action();
			watch.Stop();
			return watch.Elapsed;
		}
	}
}
