using Bridge;
using static Bridge.Functions;

namespace Vehicle
{
	internal sealed class InvincibleCar : UpdateableItem
	{
		private int _preVeh = -1;
		public InvincibleCar(string caption) : base(caption)
		{
		}

		protected override
		void OnActive()
		{
			if (DOES_ENTITY_EXIST(PlayerPed) && IS_PED_IN_ANY_VEHICLE(PlayerPed, true))
			{
				int veh = GET_VEHICLE_PED_IS_USING(PlayerPed);
				SET_ENTITY_INVINCIBLE(veh, true, false);
				SET_ENTITY_PROOFS(veh, true, true, true, true, true, true, true, true);
				SET_VEHICLE_TYRES_CAN_BURST(veh, false);
				SET_VEHICLE_WHEELS_CAN_BREAK(veh, false);
				SET_VEHICLE_CAN_BE_VISIBLY_DAMAGED(veh, false);
				_preVeh = veh;
			}
		}

		protected override void OnUpdate()
		{
			if (DOES_ENTITY_EXIST(PlayerPed) && IS_PED_IN_ANY_VEHICLE(PlayerPed, true))
			{
				int veh = GET_VEHICLE_PED_IS_USING(PlayerPed);
				if (veh == _preVeh)
				{
					return;
				}
				SET_ENTITY_INVINCIBLE(veh, true, false);
				SET_ENTITY_PROOFS(veh, true, true, true, true, true, true, true, true);
				SET_VEHICLE_TYRES_CAN_BURST(veh, false);
				SET_VEHICLE_WHEELS_CAN_BREAK(veh, false);
				SET_VEHICLE_CAN_BE_VISIBLY_DAMAGED(veh, false);
				_preVeh = veh;
			}
		}

		protected override void OnInactive()
		{
			if (DOES_ENTITY_EXIST(PlayerPed) && IS_PED_IN_ANY_VEHICLE(PlayerPed, true))
			{
				int veh = GET_VEHICLE_PED_IS_USING(PlayerPed);
				SET_ENTITY_INVINCIBLE(veh, false, false);
				SET_ENTITY_PROOFS(veh, false, false, false, false, false, false, false, false);
				SET_VEHICLE_TYRES_CAN_BURST(veh, true);
				SET_VEHICLE_WHEELS_CAN_BREAK(veh, true);
				SET_VEHICLE_CAN_BE_VISIBLY_DAMAGED(veh, true);
			}
			_preVeh = -1;
		}
	}
}
