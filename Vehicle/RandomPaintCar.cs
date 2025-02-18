using ScriptUI;
using static ScriptUI.Functions;

namespace Vehicle
{
	internal sealed class RandomPaintCar : TriggerItem
	{
		public RandomPaintCar(string caption) : base(caption)
		{
		}

		protected override void OnExecute()
		{
			if (IS_PED_IN_ANY_VEHICLE(PlayerPed, false))
			{
				int veh = GET_VEHICLE_PED_IS_USING(PlayerPed);
				SET_VEHICLE_CUSTOM_PRIMARY_COLOUR(veh, Random.Next(255), Random.Next(255), Random.Next(255));
				if (GET_IS_VEHICLE_PRIMARY_COLOUR_CUSTOM(veh))
				{
					SET_VEHICLE_CUSTOM_SECONDARY_COLOUR(veh, Random.Next(255), Random.Next(255), Random.Next(255));
				}
				return;
			}
			SetTips("请先进入车辆");
		}
	}
}
