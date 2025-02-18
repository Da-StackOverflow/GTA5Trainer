using ScriptUI;
using static ScriptUI.Functions;

namespace Weapon
{
	internal sealed class ExplosiveAmmo : UpdateableItem
	{
		public ExplosiveAmmo(string caption) : base(caption)
		{
		}

		protected override void OnUpdate()
		{
			if (DOES_ENTITY_EXIST(PlayerPed))
			{
				SET_EXPLOSIVE_AMMO_THIS_FRAME(PlayerID);
			}
		}
	}
}
