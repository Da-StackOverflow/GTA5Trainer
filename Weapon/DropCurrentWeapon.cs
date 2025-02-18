using ScriptUI;
using static ScriptUI.Functions;

namespace Weapon
{

	internal sealed class DropCurrentWeapon : TriggerItem
	{
		public DropCurrentWeapon(string caption) : base(caption)
		{
		}

		protected unsafe override void OnExecute()
		{
			uint weaponHash = 0;
			if (GET_CURRENT_PED_WEAPON(PlayerPed, &weaponHash, true))
			{
				REMOVE_WEAPON_FROM_PED(PlayerPed, weaponHash);
			}
		}
	}
}
