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
				var seats = GET_VEHICLE_MODEL_NUMBER_OF_SEATS(GET_ENTITY_MODEL(entity));
				for (int i = 0; i < seats; ++i)
				{
					if(IS_VEHICLE_SEAT_FREE(entity, i - 1, true))
					{
						SET_PED_INTO_VEHICLE(PlayerPed, entity, i - 1);
						return;
					}
				}
				SetTips("没有空座位");
				return;
			}
			SetTips("没有瞄准到汽车");
		}
	}
}
