using Bridge;
using static Bridge.Functions;

namespace Weapon
{
	internal sealed class FireAmmo : UpdateableItem
	{
		public FireAmmo(string caption) : base(caption)
		{
		}

		protected override void OnUpdate()
		{
			if (DOES_ENTITY_EXIST(PlayerPed))
			{
				SET_FIRE_AMMO_THIS_FRAME(PlayerID);
			}
		}
	}
}
