using Bridge;
using static Bridge.Functions;

namespace Other
{
	internal sealed class RandomCops : SwitchItem
	{
		public RandomCops(string caption) : base(caption)
		{
		}

		protected override
		void OnActive()
		{
			if (CAN_CREATE_RANDOM_COPS())
			{
				SET_CREATE_RANDOM_COPS(true);
			}
		}

		protected override void OnInactive()
		{
			SET_CREATE_RANDOM_COPS(false);
		}
	}
}
