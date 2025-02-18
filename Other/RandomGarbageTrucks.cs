using ScriptUI;
using static ScriptUI.Functions;

namespace Other
{
	internal sealed class RandomGarbageTrucks : SwitchItem
	{
		public RandomGarbageTrucks(string caption) : base(caption)
		{
		}

		protected override
		void OnActive()
		{
			SET_GARBAGE_TRUCKS(true);
		}

		protected override void OnInactive()
		{
			SET_GARBAGE_TRUCKS(false);
		}
	}
}
