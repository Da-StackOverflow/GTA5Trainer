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
			RESTORE_PLAYER_STAMINA(PlayerID, 100.0f);
		}
	}
}
