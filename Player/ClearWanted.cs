using ScriptUI;
using static ScriptUI.Functions;

namespace Player
{
	internal sealed class ClearWanted : TriggerItem
	{
		public ClearWanted(string caption) : base(caption)
		{
		}

		protected override void OnExecute()
		{
			if (DOES_ENTITY_EXIST(PlayerPed))
			{
				CLEAR_PLAYER_WANTED_LEVEL(PlayerID);
			}
		}
	}
}
