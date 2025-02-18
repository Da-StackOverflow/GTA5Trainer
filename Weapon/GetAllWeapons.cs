using ScriptUI;
using static ScriptUI.Functions;

namespace Weapon
{
	internal sealed class GetAllWeapons : TriggerItem
	{
		public GetAllWeapons(string caption) : base(caption)
		{
		}

		protected override void OnExecute()
		{
			var length = WeaponResources.WeaponsInfos.Length;
			for (int i = 0; i < length; i++)
			{
				GIVE_WEAPON_TO_PED(PlayerPed, GET_HASH_KEY(WeaponResources.WeaponsInfos[i].HashKey), 9999, false, true);
			}

		}
	}
}
