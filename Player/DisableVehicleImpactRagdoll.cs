using Bridge;
using static Bridge.Functions;

namespace Player
{
	internal sealed class DisableVehicleImpactRagdoll : UpdateableItem
	{
		private int _prePed = -1;
		public DisableVehicleImpactRagdoll(string caption) : base(caption)
		{
		}

		protected override
		void OnActive()
		{
			if (DOES_ENTITY_EXIST(PlayerPed))
			{
				if (GET_PED_CONFIG_FLAG(PlayerPed, PedConfigFlags.DontActivateRagdollFromVehicleImpact, true))
				{
					SET_PED_CONFIG_FLAG(PlayerPed, PedConfigFlags.DontActivateRagdollFromVehicleImpact, false);
				}
				_prePed = PlayerPed;
			}
		}

		protected override void OnUpdate()
		{
			if (_prePed == PlayerPed)
			{
				return;
			}

			if (DOES_ENTITY_EXIST(PlayerPed))
			{
				if (GET_PED_CONFIG_FLAG(PlayerPed, PedConfigFlags.DontActivateRagdollFromVehicleImpact, true))
				{
					SET_PED_CONFIG_FLAG(PlayerPed, PedConfigFlags.DontActivateRagdollFromVehicleImpact, false);
				}
				_prePed = PlayerPed;
			}
		}

		protected override void OnInactive()
		{
			if (DOES_ENTITY_EXIST(PlayerPed))
			{
				SET_PED_CONFIG_FLAG(PlayerPed, PedConfigFlags.DontActivateRagdollFromVehicleImpact, true);
			}
			_prePed = -1;
		}
	}
}
