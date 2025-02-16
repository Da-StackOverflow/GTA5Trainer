using Bridge;
using static Bridge.Functions;

namespace Player
{
	internal sealed class FixPlayer : TriggerItem
	{
		public FixPlayer(string caption) : base(caption)
		{
		}

		protected override void OnExecute()
		{
			SET_ENTITY_HEALTH(PlayerPed, GET_ENTITY_MAX_HEALTH(PlayerPed), 0, 0);
			ADD_ARMOUR_TO_PED(PlayerPed, GET_PLAYER_MAX_ARMOUR(PlayerID) - GET_PED_ARMOUR(PlayerPed));
			if (IS_PED_IN_ANY_VEHICLE(PlayerPed, true))
			{
				int playerVeh = GET_VEHICLE_PED_IS_USING(PlayerPed);
				if (DOES_ENTITY_EXIST(playerVeh) && !IS_ENTITY_DEAD(playerVeh, false))
				{
					SET_VEHICLE_FIXED(playerVeh);
				}
			}
			SetTips("玩家全部恢复");
		}
	}
}
