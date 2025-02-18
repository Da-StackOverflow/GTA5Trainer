using ScriptUI;
using static ScriptUI.Functions;

namespace Weapon
{
	internal sealed class RemoveAllWeapon : TriggerItem
	{
		public RemoveAllWeapon(string caption) : base(caption)
		{
		}

		protected override void OnExecute()
		{
			REMOVE_ALL_PED_WEAPONS(PlayerPed, false);
		}
	}
}
