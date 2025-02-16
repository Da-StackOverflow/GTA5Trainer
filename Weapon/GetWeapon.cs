using Bridge;
using static Bridge.Functions;

namespace Weapon
{
	internal sealed class GetWeapon : TriggerItem
	{
		private readonly ItemInfo _weaponInfo;
		public GetWeapon(ItemInfo weaponInfo) : base(weaponInfo.Name)
		{
			_weaponInfo = weaponInfo;
		}

		protected override void OnExecute()
		{
			GIVE_WEAPON_TO_PED(PlayerPed, GET_HASH_KEY(_weaponInfo.HashKey), 9999, false, true);
		}
	}
}
