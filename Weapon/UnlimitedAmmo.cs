using ScriptUI;
using static ScriptUI.Functions;

namespace Weapon
{
	internal sealed class UnlimitedAmmo : UpdateableItem
	{
		public UnlimitedAmmo(string caption) : base(caption)
		{
		}

		protected unsafe override void OnUpdate()
		{
			uint cur;
			if (GET_CURRENT_PED_WEAPON(PlayerPed, &cur, true))
			{
				if (IS_WEAPON_VALID(cur))
				{
					int maxAmmo;
					if (GET_MAX_AMMO(PlayerPed, cur, &maxAmmo))
					{
						SET_PED_AMMO(PlayerPed, cur, maxAmmo, true);

						maxAmmo = GET_MAX_AMMO_IN_CLIP(PlayerPed, cur, true);
						if (maxAmmo > 0)
						{
							SET_AMMO_IN_CLIP(PlayerPed, cur, maxAmmo);
						}
					}
				}
			}
		}
	}
}
