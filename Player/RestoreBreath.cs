using Bridge;
using static Bridge.Functions;

namespace Player
{
	internal sealed class RestoreBreath : TriggerItem
	{
		public RestoreBreath(string caption) : base(caption)
		{
		}

		protected override void OnExecute()
		{
			SET_PLAYER_UNDERWATER_BREATH_PERCENT_REMAINING(PlayerID, 100.0f);
		}
	}
}
