using ScriptUI;
using static ScriptUI.Functions;

namespace Weapon
{
	internal sealed class UpdateWeapon : TriggerItem
	{
		public UpdateWeapon() : base("")
		{
		}

		public ItemInfo ComponentInfo { get; set; }

		protected unsafe override void OnExecute()
		{
			uint weaponHash = 0;
			if (GET_CURRENT_PED_WEAPON(PlayerPed, &weaponHash, true))
			{
				GIVE_WEAPON_COMPONENT_TO_PED(PlayerPed, weaponHash, GET_HASH_KEY(ComponentInfo.HashKey));
			}
		}
	}
}
