using Bridge;
using static Bridge.Functions;

namespace Time
{
	class TimePause : SwitchItem
	{
		public TimePause(string caption) : base(caption)
		{
		}

		protected override
		void OnActive()
		{
			PAUSE_CLOCK(true);
		}

		protected override void OnInactive()
		{
			PAUSE_CLOCK(false);
		}
	}
}
