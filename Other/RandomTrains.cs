using Bridge;
using static Bridge.Functions;

namespace Other
{
	internal sealed class RandomTrains : SwitchItem
	{
		public RandomTrains(string caption) : base(caption)
		{
		}

		protected override
	void OnActive()
		{
			SET_RANDOM_TRAINS(true);
		}

		protected override void OnInactive()
		{
			SET_RANDOM_TRAINS(false);
		}
	}
}
