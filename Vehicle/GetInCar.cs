using ScriptUI;
using static ScriptUI.Functions;

namespace Vehicle
{
	internal sealed partial class GetInCar : TriggerItem
	{
		public GetInCar(string title) : base(title)
		{
		}

		protected override unsafe void OnExecute()
		{
			int entity = 0;
			GET_ENTITY_PLAYER_IS_FREE_AIMING_AT(PlayerID, &entity);
			if (IS_ENTITY_A_VEHICLE(entity))
			{
				for(int i = -1; i <= 4; ++i)
				{
					if(IS_VEHICLE_SEAT_FREE(entity, i, true))
					{
						SET_PED_INTO_VEHICLE(PlayerPed, entity, i);
						return;
					}
				}
				
			}
		}
	}
}
