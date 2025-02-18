using ScriptUI;
using System;
using static ScriptUI.Functions;

namespace Player
{
	internal sealed class UnlimitedAbility : UpdateableItem
	{
		public UnlimitedAbility(string caption) : base(caption)
		{
		}

		protected override void OnUpdate()
		{
			if (DOES_ENTITY_EXIST(PlayerPed))
			{
				SPECIAL_ABILITY_CHARGE_LARGE(PlayerID, true, true, 1);
			}
		}
	}
}
