using Bridge;
using static Bridge.Functions;

namespace Player
{
	internal sealed class RestoreStamina : TriggerItem
	{
		public RestoreStamina(string caption) : base(caption)
		{
		}

		protected override void OnExecute()
		{
			RESET_PLAYER_STAMINA(PlayerID);
		}
	}
}
