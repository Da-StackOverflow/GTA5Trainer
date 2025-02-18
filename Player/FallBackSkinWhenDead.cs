using ScriptUI;
using static ScriptUI.Functions;

namespace Player
{
	internal sealed class FallBackSkinWhenDead : UpdateableItem
	{
		public FallBackSkinWhenDead(string caption) : base(caption)
		{

		}

		protected override void OnUpdate()
		{
			if (GlobalValue.GetBoolValue("Player.ChangeSkin.ChangedSkin"))
			{
				if (!DOES_ENTITY_EXIST(PlayerPed))
				{
					return;
				}

				if (IS_ENTITY_DEAD(PlayerPed, false) || IS_PLAYER_BEING_ARRESTED(PlayerID, true))
				{
					SET_PED_DEFAULT_COMPONENT_VARIATION(PlayerPed);
					GlobalValue.SetBoolValue("Player.ChangeSkin.ChangedSkin", false);
				}
			}
		}
	}
}
