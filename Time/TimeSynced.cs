using Bridge;
using static Bridge.Functions;

namespace Time
{
	internal sealed class TimeSynced : UpdateableItem
	{
		public TimeSynced(string caption) : base(caption)
		{
		}

		protected override void OnUpdate()
		{
			var now = System.DateTime.Now;
			SET_CLOCK_TIME(now.Hour, now.Minute, now.Second);
		}
	}
}
