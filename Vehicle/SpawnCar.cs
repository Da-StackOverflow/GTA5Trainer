using ScriptUI;
using static ScriptUI.Functions;

namespace Vehicle
{
	internal sealed class SpawnCar : TriggerItem
	{
		public SpawnCar(ItemInfo carInfo) : base(carInfo.Name)
		{
			_carInfo = carInfo;
		}
		private readonly ItemInfo _carInfo;

		protected unsafe override void OnExecute()
		{
			uint model = GET_HASH_KEY(_carInfo.HashKey);
			if (IS_MODEL_IN_CDIMAGE(model) && IS_MODEL_A_VEHICLE(model))
			{
				REQUEST_MODEL(model);
				while (!HAS_MODEL_LOADED(model))
				{
					Wait(0);
				}
				Vector3 coords = GET_OFFSET_FROM_ENTITY_IN_WORLD_COORDS(PlayerPed, 0.0f, 5.0f, 0.0f);
				int veh = CREATE_VEHICLE(model, coords.X, coords.Y, coords.Z, 0.0f, true, true, true);
				SET_VEHICLE_ON_GROUND_PROPERLY(veh, 5.0f);

				SET_ENTITY_HEADING(veh, GET_ENTITY_HEADING(PlayerPed));

				if (GlobalValue.GetBoolValue("Vehicle.SetSpawnCarAndWarpInFlag.WrapInWhenCarSpawned"))
				{
					SET_PED_INTO_VEHICLE(PlayerPed, veh, -1);
				}

				Wait(0);
				SET_MODEL_AS_NO_LONGER_NEEDED(model);
				SET_VEHICLE_AS_NO_LONGER_NEEDED(&veh);
			}
		}
	}
}
