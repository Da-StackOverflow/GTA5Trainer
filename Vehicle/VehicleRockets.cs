using Bridge;
using static Bridge.Functions;

namespace Vehicle
{
	internal sealed class VehicleRockets : UpdateableItem
	{
		private
			long _vehBoostLastTime = 0;
		public VehicleRockets(string caption) : base(caption)
		{
		}

		protected override void OnActive()
		{
			SetTips("E键发射火箭", 10000);
		}

		protected unsafe override void OnUpdate()
		{
			if (!DOES_ENTITY_EXIST(PlayerPed))
			{
				return;
			}

			if (Input.IsKeyDown(KeyCode.E) && _vehBoostLastTime + 150 < Time.Now &&
				IS_PLAYER_CONTROL_ON(PlayerID) && IS_PED_IN_ANY_VEHICLE(PlayerPed, true))
			{
				int veh = GET_VEHICLE_PED_IS_USING(PlayerPed);

				Vector3 v0, v1;
				GET_MODEL_DIMENSIONS(GET_ENTITY_MODEL(veh), &v0, &v1);

				uint weaponAssetRocket = GET_HASH_KEY("WEAPON_VEHICLE_ROCKET");
				if (!HAS_WEAPON_ASSET_LOADED(weaponAssetRocket))
				{
					REQUEST_WEAPON_ASSET(weaponAssetRocket, 31, 0);
					while (!HAS_WEAPON_ASSET_LOADED(weaponAssetRocket))
					{
						Wait(0);
					}
				}

				Vector3 coords0from = GET_OFFSET_FROM_ENTITY_IN_WORLD_COORDS(veh, -(v1.X + 0.25f), v1.Y + 1.25f, 0.1f);
				Vector3 coords1from = GET_OFFSET_FROM_ENTITY_IN_WORLD_COORDS(veh, (v1.X + 0.25f), v1.Y + 1.25f, 0.1f);
				Vector3 coords0to = GET_OFFSET_FROM_ENTITY_IN_WORLD_COORDS(veh, -v1.X, v1.Y + 100.0f, 0.1f);
				Vector3 coords1to = GET_OFFSET_FROM_ENTITY_IN_WORLD_COORDS(veh, v1.X, v1.Y + 100.0f, 0.1f);

				SHOOT_SINGLE_BULLET_BETWEEN_COORDS(coords0from.X, coords0from.Y, coords0from.Z,
					coords0to.X, coords0to.Y, coords0to.Z,
					250, true, weaponAssetRocket, PlayerPed, true, false, -1.0f
				);
				SHOOT_SINGLE_BULLET_BETWEEN_COORDS(
					coords1from.X, coords1from.Y, coords1from.Z,
					coords1to.X, coords1to.Y, coords1to.Z,
					250, true, weaponAssetRocket, PlayerPed, true, false, -1.0f
				);

				_vehBoostLastTime = Time.Now;
			}
		}
	}
}
