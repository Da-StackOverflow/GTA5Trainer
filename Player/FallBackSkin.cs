using Bridge;
using static Bridge.Functions;

namespace Player
{
	internal sealed class FallBackSkin : TriggerItem
	{
		public FallBackSkin(string caption) : base(caption)
		{
		}

		protected override void OnExecute()
		{
			SET_PED_DEFAULT_COMPONENT_VARIATION(PlayerPed);
			GlobalValue.SetBoolValue("Player.ChangeSkin.ChangedSkin", false);
		}
	}
}
