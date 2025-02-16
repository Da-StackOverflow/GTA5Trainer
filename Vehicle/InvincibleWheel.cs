using Bridge;
using static Bridge.Functions;

namespace Vehicle
{
	internal sealed class InvincibleWheel : UpdateableItem
	{
		private int _preVeh = -1;
		public InvincibleWheel(string caption) : base(caption)
		{
		}

		protected override
		void OnActive()
		{
			if (DOES_ENTITY_EXIST(PlayerPed) && IS_PED_IN_ANY_VEHICLE(PlayerPed, true))
			{
				int veh = GET_VEHICLE_PED_IS_USING(PlayerPed);
				SET_VEHICLE_TYRES_CAN_BURST(veh, false);
				SET_VEHICLE_WHEELS_CAN_BREAK(veh, false);
				SET_VEHICLE_HAS_STRONG_AXLES(veh, true);
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
				SET_VEHICLE_TYRES_CAN_BURST(veh, false);
				SET_VEHICLE_WHEELS_CAN_BREAK(veh, false);
				SET_VEHICLE_HAS_STRONG_AXLES(veh, true);
				_preVeh = veh;
			}
		}

		protected override void OnInactive()
		{
			if (DOES_ENTITY_EXIST(PlayerPed) && IS_PED_IN_ANY_VEHICLE(PlayerPed, true))
			{
				int veh = GET_VEHICLE_PED_IS_USING(PlayerPed);
				SET_VEHICLE_TYRES_CAN_BURST(veh, true);
				SET_VEHICLE_WHEELS_CAN_BREAK(veh, true);
				SET_VEHICLE_HAS_STRONG_AXLES(veh, false);
			}
			_preVeh = -1;
		}
	}
}
