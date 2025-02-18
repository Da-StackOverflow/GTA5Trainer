using ScriptUI;
using static ScriptUI.Functions;

namespace Weapon
{
	internal sealed class ExplosiveMelee : UpdateableItem
	{
		public ExplosiveMelee(string caption) : base(caption)
		{
		}

		protected override void OnUpdate()
		{
			if (DOES_ENTITY_EXIST(PlayerPed))
			{
				SET_EXPLOSIVE_MELEE_THIS_FRAME(PlayerID);
			}
		}
	}
}
