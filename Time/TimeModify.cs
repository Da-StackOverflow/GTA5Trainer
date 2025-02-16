using Bridge;
using static Bridge.Functions;

namespace Time
{
	internal sealed class TimeModify : TriggerItem
	{
		private readonly int _hour;
		public TimeModify(string caption, int hour) : base(caption)
		{
			_hour = hour;
		}

		protected override
		void OnExecute()
		{
			int h = GET_CLOCK_HOURS();
			h += _hour;
			h += 24;
			h %= 24;
			int m = GET_CLOCK_MINUTES();
			SET_CLOCK_TIME(h, m, 0);
			SetTips($"{h:D2}:{m:D2}");
		}
	}
}
