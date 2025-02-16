using Bridge;
using static Bridge.Functions;

namespace Other
{
	internal sealed class RandomBoats : SwitchItem
	{
		public RandomBoats(string caption) : base(caption)
		{
		}

		protected override void OnActive()
		{
			SET_RANDOM_BOATS(true);
		}

		protected override void OnInactive()
		{
			SET_RANDOM_BOATS(false);
		}
	}
}
