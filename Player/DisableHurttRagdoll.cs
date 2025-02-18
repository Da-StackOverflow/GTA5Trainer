using ScriptUI;
using static ScriptUI.Functions;

namespace Player
{
	internal sealed class DisableHurttRagdoll : UpdateableItem
	{
		public DisableHurttRagdoll(string caption) : base(caption)
		{
		}

		protected override void OnUpdate()
		{

			if (DOES_ENTITY_EXIST(PlayerPed))
			{
				SET_PED_CONFIG_FLAG(PlayerPed, PedConfigFlags.DontActivateRagdollFromVehicleImpact, true);
				SET_PED_CONFIG_FLAG(PlayerPed, PedConfigFlags.DontActivateRagdollFromBulletImpact, true);
				SET_PED_CONFIG_FLAG(PlayerPed, PedConfigFlags.DontActivateRagdollFromExplosions, true);
				SET_PED_CONFIG_FLAG(PlayerPed, PedConfigFlags.DontActivateRagdollFromFire, true);
				SET_PED_CONFIG_FLAG(PlayerPed, PedConfigFlags.DontActivateRagdollFromElectrocution, true);
			}
		}

		protected override void OnInactive()
		{
			if (DOES_ENTITY_EXIST(PlayerPed))
			{
				SET_PED_CONFIG_FLAG(PlayerPed, PedConfigFlags.DontActivateRagdollFromVehicleImpact, false);
				SET_PED_CONFIG_FLAG(PlayerPed, PedConfigFlags.DontActivateRagdollFromBulletImpact, false);
				SET_PED_CONFIG_FLAG(PlayerPed, PedConfigFlags.DontActivateRagdollFromExplosions, false);
				SET_PED_CONFIG_FLAG(PlayerPed, PedConfigFlags.DontActivateRagdollFromFire, false);
				SET_PED_CONFIG_FLAG(PlayerPed, PedConfigFlags.DontActivateRagdollFromElectrocution, false);
			}
		}
	}
}
