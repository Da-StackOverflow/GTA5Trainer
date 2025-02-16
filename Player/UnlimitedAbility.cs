using Bridge;
using System;
using static Bridge.Functions;

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
				SPECIAL_ABILITY_FILL_METER(PlayerID, true, 0);
			}
		}
	}
}
