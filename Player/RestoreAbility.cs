using ScriptUI;
using static ScriptUI.Functions;

namespace Player
{
	internal class RestoreAbility : TriggerItem
	{
		public RestoreAbility(string caption) : base(caption)
		{
		}
		protected override void OnExecute()
		{
			if (DOES_ENTITY_EXIST(PlayerPed))
			{
				SPECIAL_ABILITY_CHARGE_LARGE(PlayerID, true, true, 1);
			}
		}
	}
}
